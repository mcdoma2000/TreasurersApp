﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreasurersApp.Database;
using TreasurersApp.Models;

namespace TreasurersApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class MaintenanceController : BaseController
    {
        public MaintenanceController(IHostingEnvironment env) : base(env)
        {
        }

        [HttpGet]
        [Authorize(Policy = "CanPerformAdmin")]
        [Route("users")]
        public IActionResult GetUsers()
        {
            IActionResult results = null;
            List<AppUserEdit> users = new List<AppUserEdit>();
            try
            {
                using (var db = new TreasurersAppDbContext(GetDatabasePath()))
                {
                    users = db.Users.Select(x => new AppUserEdit()
                    {
                        Id = x.UserId,
                        UserName = x.UserName,
                        DisplayName = x.DisplayName,
                        Password = x.Password,
                        UserClaims = db.UserClaims.Where(y => y.UserId == x.UserId).ToList()
                    }).ToList();
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