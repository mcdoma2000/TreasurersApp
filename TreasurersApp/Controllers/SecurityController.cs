using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreasurersApp.Utilities.Security;
using TreasurersApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using TreasurersApp.Database;

namespace TreasurersApp.Controllers
{
    [Route("api/[controller]")]
    public class SecurityController : BaseController
    {
        private readonly JwtSettings _settings;
        private readonly TreasurersAppDbContext db;

        public SecurityController(JwtSettings settings, IConfiguration config, ILogger<AddressController> logger, IHostingEnvironment env, IMemoryCache memoryCache, TreasurersAppDbContext db)
            : base(config, logger, env, memoryCache)
        {
            this.db = db;
            _settings = settings;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]User user)
        {
            IActionResult ret = null;
            UserAuth auth = new UserAuth();
            SecurityManager mgr = new SecurityManager(_settings, db);

            auth = mgr.ValidateUser(user);
            if (auth.IsAuthenticated)
            {
                ret = StatusCode(StatusCodes.Status200OK, auth);
            }
            else
            {
                ret = StatusCode(StatusCodes.Status404NotFound,
                                 "Invalid User Name/Password.");
            }

            return ret;
        }
    }
}
