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
using Newtonsoft.Json;
using TreasurersApp.Database;
using TreasurersApp.Models;

namespace TreasurersApp.Controllers
{
    [Route("api/[controller]")]
    public class AddressController : BaseController
    {
        public AddressController(IConfiguration config, ILogger<AddressController> logger, IHostingEnvironment env, IMemoryCache memoryCache) 
            : base(config, logger, env, memoryCache)
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
                using (var db = new TreasurersAppDbContext(DatabasePath))
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
                using (var db = new TreasurersAppDbContext(DatabasePath))
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
            var returnResult = new AppAddressActionResult(false, new List<string>(), null);
            if (address != null)
            {
                try
                {
                    using (var db = new TreasurersAppDbContext(DatabasePath))
                    {
                        var resultAddress = db.Addresses.Add(address);
                        db.SaveChanges();
                        var entity = resultAddress.Entity;
                        if (entity != null)
                        {
                            returnResult.Success = true;
                            returnResult.StatusMessages.Add("Successfully added address.");
                            returnResult.Address = entity;
                        }
                    }
                }
                catch (Exception e)
                {
                    returnResult.Success = false;
                    returnResult.StatusMessages.Add(e.Message);
                    returnResult.Address = null;
                }
            }
            else
            {
                returnResult.Success = false;
                returnResult.StatusMessages.Add("Empty address posted for add.");
                returnResult.Address = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }

        [HttpPut(Name = "AddressPut")]
#if RELEASE
        [Authorize(Policy = "CanAccessAddresses")]
#endif
        public IActionResult Put([FromBody]AppAddress address)
        {
            string json = JsonConvert.SerializeObject(address);
            var returnResult = new AppAddressActionResult(false, new List<string>(), null);
            if (address != null)
            {
                try
                {
                    using (var db = new TreasurersAppDbContext(DatabasePath))
                    {
                        var resultAddress = db.Addresses.SingleOrDefault(x => x.Id == address.Id);
                        if (resultAddress != null)
                        {
                            resultAddress.AddressLine1 = address.AddressLine1;
                            resultAddress.AddressLine2 = address.AddressLine2;
                            resultAddress.AddressLine3 = address.AddressLine3;
                            resultAddress.City = address.City;
                            resultAddress.State = address.State;
                            resultAddress.PostalCode = address.PostalCode;
                            db.SaveChanges();
                            returnResult.Success = true;
                            returnResult.Address = resultAddress;
                            returnResult.StatusMessages.Add("Successfully updated address.");
                        }
                    }
                }
                catch (Exception e)
                {
                    returnResult.Success = false;
                    returnResult.StatusMessages.Add(e.Message);
                    returnResult.Address = null;
                }
            }
            else
            {
                returnResult.Success = false;
                returnResult.StatusMessages.Add("Empty address posted for update.");
                returnResult.Address = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }

        [HttpDelete(Name = "AddressDelete")]
        [Authorize(Policy = "CanPerformAdmin")]
        public IActionResult Delete(int id)
        {
            var returnResult = new AppAddressActionResult(false, new List<string>(), null);
            try
            {
                using (var db = new TreasurersAppDbContext(DatabasePath))
                {
                    var resultAddress = db.Addresses.SingleOrDefault(x => x.Id == id);
                    if (resultAddress != null)
                    {
                        db.Addresses.Remove(resultAddress);
                        db.SaveChanges();
                        returnResult.Success = true;
                        returnResult.Address = resultAddress;
                        returnResult.StatusMessages.Add("Successfully deleted address.");
                    }
                }
            }
            catch (Exception e)
            {
                returnResult.Success = false;
                returnResult.StatusMessages.Add(e.Message);
                returnResult.Address = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }
    }
}