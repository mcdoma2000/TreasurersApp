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
        private readonly TreasurersAppDbContext db;

        public AddressController(IConfiguration config, ILogger<AddressController> logger, IHostingEnvironment env, IMemoryCache memoryCache, TreasurersAppDbContext db) 
            : base(config, logger, env, memoryCache)
        {
            this.db = db;
        }

        [HttpGet("get", Name = "AddressGet")]
        public IActionResult Get()
        {
            IActionResult ret = null;
            List<Address> list = new List<Address>();

            try
            {
                if (db.Addresses.Count() > 0)
                {
                    list = db.Addresses
                        .OrderBy(r => r.State)
                        .ThenBy(r => r.City)
                        .ThenBy(r => r.PostalCode)
                        .ToList();
                }
                ret = StatusCode(StatusCodes.Status200OK, list);
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to get all Addresses");
            }

            return ret;
        }

        [HttpGet("getbyid/{id}", Name = "AddressGetByID")]
        public IActionResult Get(int id)
        {
            IActionResult ret = null;
            Address entity = null;

            try
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
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to retrieve a single Address.");
            }

            return ret;
        }

        [HttpPost("post", Name = "AddressPost")]
        public IActionResult Post([FromBody]Address address)
        {
            string json = JsonConvert.SerializeObject(address);
            var returnResult = new AddressActionResult(false, new List<string>(), null);
            if (address != null)
            {
                if (address.AddressID > 0)
                {
                    returnResult.StatusMessages.Add("Attempting to create a new address, but an Id is present.");
                }
                else
                {
                    try
                    {
                        var resultAddress = db.Addresses.Add(address);
                        db.SaveChanges();
                        var entity = resultAddress.Entity;
                        if (entity != null)
                        {
                            returnResult.Success = true;
                            returnResult.StatusMessages.Add("Successfully added address.");
                            returnResult.Data = entity;
                        }
                    }
                    catch (Exception e)
                    {
                        returnResult.Success = false;
                        returnResult.StatusMessages.Add(e.Message);
                        returnResult.Data = null;
                    }
                }
            }
            else
            {
                returnResult.Success = false;
                returnResult.StatusMessages.Add("Empty address posted for add.");
                returnResult.Data = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }

        [HttpPut("put", Name = "AddressPut")]
        public IActionResult Put([FromBody]Address address)
        {
            string json = JsonConvert.SerializeObject(address);
            var returnResult = new AddressActionResult(false, new List<string>(), null);
            if (address != null)
            {
                if (address.AddressID <= 0)
                {
                    returnResult.StatusMessages.Add("Attempting to update an existing address, but an Id is not present.");
                }
                else
                {
                    try
                    {
                        var resultAddress = db.Addresses.SingleOrDefault(x => x.AddressID == address.AddressID);
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
                            returnResult.Data = resultAddress;
                            returnResult.StatusMessages.Add("Successfully updated address.");
                        }
                        else
                        {
                            returnResult.Success = false;
                            returnResult.StatusMessages.Add(string.Format("Unable to locate address for index: {0}", address.AddressID));
                            returnResult.Data = null;
                        }
                    }
                    catch (Exception e)
                    {
                        returnResult.Success = false;
                        returnResult.StatusMessages.Add(e.Message);
                        returnResult.Data = null;
                    }
                }
            }
            else
            {
                returnResult.Success = false;
                returnResult.StatusMessages.Add("Empty address posted for update.");
                returnResult.Data = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }

        [HttpDelete("delete", Name = "AddressDelete")]
        [Authorize(Policy = "CanPerformAdmin")]
        public IActionResult Delete(int id)
        {
            var returnResult = new AddressActionResult(false, new List<string>(), null);
            try
            {
                if (db.Addresses.Any(x => x.AddressID == id) == false)
                {
                    returnResult.StatusMessages.Add("Attempted to delete a nonexisting address.");
                }
                else
                {
                    var resultAddress = db.Addresses.Single(x => x.AddressID == id);
                    db.Addresses.Remove(resultAddress);
                    db.SaveChanges();
                    returnResult.Success = true;
                    returnResult.Data = resultAddress;
                    returnResult.StatusMessages.Add("Successfully deleted address.");
                }
            }
            catch (Exception e)
            {
                returnResult.Success = false;
                returnResult.StatusMessages.Add(e.Message);
                returnResult.Data = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }
    }
}