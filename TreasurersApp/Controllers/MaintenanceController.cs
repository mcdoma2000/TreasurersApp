using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TreasurersApp.Database;
using TreasurersApp.Models;

namespace TreasurersApp.Controllers
{
    [Route("api/[controller]")]
    public class MaintenanceController : BaseController
    {
        private readonly TreasurersAppDbContext db;

        public MaintenanceController(IConfiguration config, ILogger<AddressController> logger, IHostingEnvironment env, IMemoryCache memoryCache, TreasurersAppDbContext db)
            : base(config, logger, env, memoryCache)
        {
            this.db = db;
        }

        [HttpGet("/get", Name = "UsersGet")]
        [Authorize(Policy = "CanPerformAdmin")]
        public IActionResult GetUsers()
        {
            IActionResult results = null;
            List<UserEdit> users = new List<UserEdit>();
            try
            {
                users = db.Users.Select(x => new UserEdit()
                {
                    UserID = x.UserID,
                    UserName = x.UserName,
                    DisplayName = x.DisplayName,
                    Password = x.Password
                }).ToList();
                foreach (var u in users)
                {
                    var userClaims = db.UserClaims.Where(x => x.UserID == u.UserID).Select(x => x.ClaimID).ToList();
                    u.Claims = db.Claims.Where(x => userClaims.Contains(x.ClaimID)).ToList();
                }
                results = StatusCode(StatusCodes.Status200OK, users);
            }
            catch (Exception e)
            {
                results = HandleException(e, "Exception trying to get all users");
            }
            return results;
        }
    }
}