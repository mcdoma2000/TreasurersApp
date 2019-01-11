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
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _env;
        private readonly IMemoryCache _memoryCache;
        private readonly BTAContext _context;
        private readonly string _region;

        protected ILogger Logger { get { return _logger; } private set { } }
        protected IConfiguration Config { get { return _config; } private set { } }
        protected IHostingEnvironment Env { get { return _env; } private set { } }
        protected IMemoryCache MemoryCache { get { return _memoryCache;  } private set { } }
        protected BTAContext Context { get { return _context; } set { } }
        protected string Region { get { return _region; } private set { } }

        public BaseController(IConfiguration config, ILogger logger, IHostingEnvironment env, IMemoryCache memoryCache, BTAContext context)
        {
            _env = env;
            _config = config;
            _logger = logger;
            _memoryCache = memoryCache;
            _region = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            _context = context;
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
