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
        private readonly TreasurersAppDbContext _db = null;

        public TreasurersAppDbContext DataContext
        {
            get { return _db; }
            private set { }
        }

        public SecurityManager(JwtSettings settings, TreasurersAppDbContext db)
        {
            _settings = settings;
            this._db = db;
        }

        public UserAuth ValidateUser(User user)
        {
            UserAuth ret = new UserAuth();
            User authUser = null;

            // Attempt to validate user
            authUser = DataContext.Users.Where(
                u => u.UserName.ToLower() == user.UserName.ToLower()
                && u.Password == user.Password).FirstOrDefault();

            if (authUser != null)
            {
                // Build User Security Object
                ret = BuildUserAuthObject(authUser);
            }

            return ret;
        }

        protected List<Models.Claim> GetClaimsForUser(User authUser)
        {
            List<Models.Claim> claims = new List<Models.Claim>();
            try
            {
                var list = DataContext.UserClaims.Where(x => x.UserID == authUser.UserID).Select(x => x.ClaimID).ToList();
                claims.AddRange(DataContext.Claims.Where(x => list.Contains(x.ClaimID)).ToList());
            }
            catch (Exception ex)
            {
                throw new Exception("Exception trying to retrieve user claims.", ex);
            }

            return claims;
        }

        protected UserAuth BuildUserAuthObject(User authUser)
        {
            UserAuth ret = new UserAuth();
            List<UserClaim> claims = new List<UserClaim>();

            // Set User Properties
            ret.UserName = authUser.UserName;
            ret.IsAuthenticated = true;
            ret.BearerToken = new Guid().ToString();

            // Get all claims for this user
            ret.Claims = GetClaimsForUser(authUser);

            // Set JWT bearer token
            ret.BearerToken = BuildJwtToken(ret);

            return ret;
        }

        protected string BuildJwtToken(UserAuth authUser)
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
                jwtClaims.Add(new System.Security.Claims.Claim(claim.ClaimName, claim.ClaimValue));
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
