﻿using System;
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
        public MaintenanceController(IConfiguration config, ILogger<AddressController> logger, IHostingEnvironment env, IMemoryCache memoryCache)
            : base(config, logger, env, memoryCache)
        {
        }

        [HttpGet("get", Name = "UsersGet")]
        [Authorize(Policy = "CanPerformAdmin")]
        public IActionResult GetUsers()
        {
            IActionResult results = null;
            List<SecurityUserEdit> users = new List<SecurityUserEdit>();
            try
            {
                using (var db = new BTAContext())
                {
                    users = db.User.Select(x => new SecurityUserEdit()
                    {
                        UserID = x.UserId,
                        UserName = x.UserName,
                        DisplayName = x.DisplayName,
                        Password = x.Password
                    }).ToList();
                    foreach (var u in users)
                    {
                        var userClaims = db.UserClaim.Where(x => x.UserId == u.UserID).ToList();
                        u.Claims = userClaims.Select(x => x.Claim).ToList();
                    }
                    results = StatusCode(StatusCodes.Status200OK, users);
                }
            }
            catch (Exception e)
            {
                results = HandleException(e, "Exception trying to get all users");
            }
            return results;
        }
    }
}