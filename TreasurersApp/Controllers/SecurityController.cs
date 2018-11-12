using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreasurersApp.Utilities.Security;
using TreasurersApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;

namespace TreasurersApp.Controllers
{
  [Route("api/[controller]")]
  public class SecurityController : BaseController
  {
    private JwtSettings _settings;
    public SecurityController(JwtSettings settings, IConfiguration config, ILogger<SecurityController> logger, IHostingEnvironment env, IMemoryCache memoryCache) 
            : base(config, logger, env, memoryCache)
    {
      _settings = settings;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody]AppUser user)
    {
      IActionResult ret = null;
      AppUserAuth auth = new AppUserAuth();
      SecurityManager mgr = new SecurityManager(_settings, DatabasePath);

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
