using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace TreasurersApp.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
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

        public BaseApiController(IConfiguration config, ILogger logger, IHostingEnvironment env, IMemoryCache memoryCache)
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

        protected IActionResult HandleException(Exception ex, string msg)
        {
            IActionResult ret;

            // Log it out
            _logger.LogError(ex, msg);

            // Create new exception with generic message        
            ret = StatusCode(
                StatusCodes.Status500InternalServerError, new Exception(msg, ex));

            return ret;
        }
    }
}
