using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreasurersApp.Utilities.Security;
using TreasurersApp.Model;
using Microsoft.AspNetCore.Hosting;

namespace TreasurersApp.Controllers
{
  [Route("api/[controller]")]
  public class SecurityController : BaseApiController
  {
    private JwtSettings _settings;
    public SecurityController(JwtSettings settings, IHostingEnvironment env) : base(env)
    {
      _settings = settings;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody]AppUser user)
    {
      IActionResult ret = null;
      AppUserAuth auth = new AppUserAuth();
      SecurityManager mgr = new SecurityManager(_settings, GetDatabasePath());

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
