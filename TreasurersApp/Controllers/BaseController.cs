using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace TreasurersApp.Controllers
{
    public class BaseController : Controller
    {
        private IHostingEnvironment _env;
        public IHostingEnvironment Env
        {
            get { return _env; }
            set { }
        }

        public BaseController(IHostingEnvironment env)
        {
            _env = env;
        }

        protected string GetDatabasePath()
        {
            string dbPath = Path.Combine(Env.WebRootPath, @"Database\BTA.mdf");
            return dbPath;
        }

        protected IActionResult HandleException(Exception ex, string msg)
        {
            IActionResult ret;

            // Create new exception with generic message        
            ret = StatusCode(
                StatusCodes.Status500InternalServerError, new Exception(msg, ex));

            return ret;
        }
    }
}
