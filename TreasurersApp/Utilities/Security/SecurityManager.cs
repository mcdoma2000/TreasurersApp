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
        private string _dbPath;
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

        public SecurityUserAuth ValidateUser(SecurityUser user)
        {
            SecurityUserAuth ret = new SecurityUserAuth();
            SecurityUser authUser = null;

            using (var db = new TreasurersAppDbContext(DbPath))
            {
                // Attempt to validate user
                authUser = db.Users.Where(
                  u => u.UserName.ToLower() == user.UserName.ToLower()
                  && u.Password == user.Password).FirstOrDefault();
            }

            if (authUser != null)
            {
                // Build User Security Object
                ret = BuildUserAuthObject(authUser);
            }

            return ret;
        }

        protected List<SecurityClaim> GetUserClaims(SecurityUser authUser)
        {
            List<SecurityClaim> list = new List<SecurityClaim>();

            try
            {
                using (var db = new TreasurersAppDbContext(DbPath))
                {
                    var userClaims = db.UserClaims
                        .Where(x => x.UserID == authUser.UserID)
                        .Select(x => x.ClaimID)
                        .ToList();
                    list = db.Claims.Where(x => userClaims.Contains(x.ClaimID)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Exception trying to retrieve user claims.", ex);
            }

            return list;
        }

        protected SecurityUserAuth BuildUserAuthObject(SecurityUser authUser)
        {
            SecurityUserAuth ret = new SecurityUserAuth();
            List<SecurityUserClaim> claims = new List<SecurityUserClaim>();

            // Set User Properties
            ret.UserName = authUser.UserName;
            ret.IsAuthenticated = true;
            ret.BearerToken = new Guid().ToString();

            // Get all claims for this user
            ret.Claims = GetUserClaims(authUser);

            // Set JWT bearer token
            ret.BearerToken = BuildJwtToken(ret);

            return ret;
        }

        protected string BuildJwtToken(SecurityUserAuth authUser)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(_settings.Key));

            // Create standard JWT claims
            List<Claim> jwtClaims = new List<Claim>();
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Sub,
                authUser.UserName));
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString()));

            // Add custom claims
            foreach (var claim in authUser.Claims)
            {
                jwtClaims.Add(new Claim(claim.ClaimType, claim.ClaimValue));
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
