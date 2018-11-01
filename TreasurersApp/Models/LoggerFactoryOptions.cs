using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreasurersApp.Models
{
    // https://stackoverflow.com/questions/28897372/access-to-configuration-object-from-startup-class
    public class LoggerFactoryOptions
    {
        public ILoggerFactory LoggerFactory;
    }
}
