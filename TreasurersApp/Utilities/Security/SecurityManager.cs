using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TreasurersApp.Database;
using TreasurersApp.Models;

namespace TreasurersApp.Utilities.Security
{
    public class SecurityManager
    {
        private JwtSettings _settings = null;
        private readonly string _dbPath;
        public string DbPath
        {
            get { return _dbPath; }
            set { }
        }

        public SecurityManager(JwtSettings settings, string dbPath)
        {
            _settings = settings;
            _dbPath = dbPath;
        }

        public SecurityUserAuth ValidateUser(User user)
        {
            SecurityUserAuth ret = new SecurityUserAuth();
            User authUser = null;

            using (var db = new BTAContext())
            {
                // Attempt to validate user
                authUser = db.User.FirstOrDefault(u => u.UserName.ToLower() == user.UserName.ToLower() && u.Password == user.Password);
                if (authUser != null)
                {
                    db.Entry(authUser).Collection(x => x.UserClaims).Load();
                    foreach (var uc in authUser.UserClaims)
                    {
                        db.Entry(uc).Reference(x => x.Claim).Load();
                    }
                }
            }

            if (authUser != null)
            {
                // Build User Security Object
                ret = BuildUserAuthObject(authUser);
            }

            return ret;
        }

        protected SecurityUserAuth BuildUserAuthObject(User authUser)
        {
            SecurityUserAuth ret = new SecurityUserAuth();
            List<UserClaim> claims = new List<UserClaim>();

            // Set User Properties
            ret.UserName = authUser.UserName;
            ret.IsAuthenticated = true;
            ret.BearerToken = new Guid().ToString();

            // Get all claims for this user
            ret.Claims = authUser.UserClaims.Select(x => new ClaimViewModel() { ClaimId = x.ClaimId, ClaimType = x.Claim.ClaimType, ClaimValue = x.Claim.ClaimValue }).ToList();

            // Set JWT bearer token
            ret.BearerToken = BuildJwtToken(ret);

            return ret;
        }

        protected string BuildJwtToken(SecurityUserAuth authUser)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(_settings.Key));

            // Create standard JWT claims
            List<System.Security.Claims.Claim> jwtClaims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Sub, authUser.UserName),
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Add custom claims
            foreach (var claim in authUser.Claims)
            {
                jwtClaims.Add(new System.Security.Claims.Claim(claim.ClaimType, claim.ClaimValue));
            }

            // Create the JwtSecurityToken object
            var token = new JwtSecurityToken(
              issuer: _settings.Issuer,
              audience: _settings.Audience,
              claims: jwtClaims,
              notBefore: DateTime.UtcNow,
              expires: DateTime.UtcNow.AddMinutes(
                  _settings.MinutesToExpiration),
              signingCredentials: new SigningCredentials(key,
                          SecurityAlgorithms.HmacSha256)
            );

            // Create a string representation of the Jwt token
            return new JwtSecurityTokenHandler().WriteToken(token); ;
        }
    }
}
