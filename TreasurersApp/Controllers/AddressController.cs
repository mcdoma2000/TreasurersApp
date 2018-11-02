using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TreasurersApp.Database;
using TreasurersApp.Models;

namespace TreasurersApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AddressController : BaseController
    {
        public AddressController(IHostingEnvironment env) : base(env)
        {

        }

        [HttpGet()]
#if RELEASE
        [Authorize(Policy = "CanAccessAddresses")]
#endif
        public IActionResult Get()
        {
            IActionResult ret = null;
            List<AppAddress> list = new List<AppAddress>();

            try
            {
                using (var db = new TreasurersAppDbContext(GetDatabasePath()))
                {
                    if (db.Addresses.Count() > 0)
                    {
                        list = db.Addresses
                            .OrderBy(r => r.State)
                            .ThenBy(r => r.City)
                            .ThenBy(r => r.PostalCode)
                            .ToList();
                        ret = StatusCode(StatusCodes.Status200OK, list);
                    }
                    else
                    {
                        ret = StatusCode(StatusCodes.Status404NotFound, "Can't Find Addresses");
                    }
                }
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to get all Addresses");
            }

            return ret;
        }

        [HttpGet("{id}")]
#if RELEASE
        [Authorize(Policy = "CanAccessAddresses")]
#endif
        public IActionResult Get(int id)
        {
            IActionResult ret = null;
            AppAddress entity = null;

            try
            {
                using (var db = new TreasurersAppDbContext(GetDatabasePath()))
                {
                    entity = db.Addresses.Find(id);
                    if (entity != null)
                    {
                        ret = StatusCode(StatusCodes.Status200OK, entity);
                    }
                    else
                    {
                        ret = StatusCode(StatusCodes.Status404NotFound, 
                                        "Can't Find Address: " + id.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to retrieve a single Address.");
            }

            return ret;
        }

        [HttpPost(Name = "AddressPost")]
#if RELEASE
        [Authorize(Policy = "CanAccessAddresses")]
#endif
        public IActionResult Post([FromBody]AppAddress address)
        {
            string json = JsonConvert.SerializeObject(address);
            return new EmptyResult();
        }

        [HttpPut(Name = "AddressPut")]
#if RELEASE
        [Authorize(Policy = "CanAccessAddresses")]
#endif
        public IActionResult Put([FromBody]AppAddress address)
        {
            string json = JsonConvert.SerializeObject(address);
            return new EmptyResult();
        }

        [HttpDelete(Name = "AddressDelete")]
        [Authorize(Policy = "CanPerformAdmin")]
        public IActionResult Delete([FromBody]AppAddress address)
        {
            string json = JsonConvert.SerializeObject(address);
            return new EmptyResult();
        }
    }
}