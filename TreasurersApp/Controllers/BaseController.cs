using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;
using TreasurersApp.Database;

namespace TreasurersApp.Controllers
{
    public class BaseController : Controller
    {
        public static readonly Guid UnknownUserGuid = Guid.Parse("00000000-0000-0000-0000-000000000000");
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _env;
        private readonly IMemoryCache _memoryCache;
        private readonly string _databasePath;
        private readonly string _region;

        protected ILogger Logger { get { return _logger; } private set { } }
        protected IConfiguration Config { get { return _config; } private set { } }
        protected IHostingEnvironment Env { get { return _env; } private set { } }
        protected IMemoryCache MemoryCache { get { return _memoryCache;  } private set { } }
        protected string DatabasePath { get { return _databasePath; } private set { } }
        protected string Region { get { return _region; } private set { } }

        public BaseController(IConfiguration config, ILogger logger, IHostingEnvironment env, IMemoryCache memoryCache)
        {
            _env = env;
            _config = config;
            _logger = logger;
            _memoryCache = memoryCache;
            _region = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            string dbLoc = string.Format("Database:{0}:DatabaseDirectory", Region);
            _databasePath = Config[dbLoc] ?? "";
            if (string.IsNullOrEmpty(_databasePath))
            {
                throw new InvalidDataException("Database path is missing.");
            }
        }

        public Guid GetUserGuidFromUserName(string userName)
        {
            Guid userGuid = UnknownUserGuid;
            using (var db = new BTAContext())
            {
                foreach (var usr in db.User)
                {
                    if (usr.UserName == userName)
                    {
                        userGuid = usr.UserId;
                        break;
                    }
                }
            }
            return userGuid;
        }

        protected IActionResult HandleException(Exception ex, string msg)
        {
            IActionResult ret;

            // Create new exception with generic message        
            ret = StatusCode(
                StatusCodes.Status500InternalServerError, new Exception(msg, ex));
            _logger.LogError(msg);
            _logger.LogError(ex.ToString());

            return ret;
        }
    }
}
