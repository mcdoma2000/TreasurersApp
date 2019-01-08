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

        [HttpGet("get", Name = "AddressGet")]
        public IActionResult Get()
        {
            IActionResult ret = null;
            List<Address> list = new List<Address>();

            try
            {
                using (var db = new BTAContext())
                {
                    if (db.Address.Count() > 0)
                    {
                        list = db.Address
                            .OrderBy(r => r.State)
                            .ThenBy(r => r.City)
                            .ThenBy(r => r.PostalCode)
                            .ToList();
                    }
                    ret = StatusCode(StatusCodes.Status200OK, list);
                }
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
                using (var db = new BTAContext())
                {
                    entity = db.Address.Find(id);
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

        [HttpPost("post", Name = "AddressPost")]
        public IActionResult Post([FromBody]AddressRequest request)
        {
            string json = JsonConvert.SerializeObject(request);
            Guid userGuid = GetUserGuidFromUserName(request.UserName);
            var returnResult = new AddressActionResult(false, new List<string>(), null);
            if (request != null)
            {
                if (request.Data.AddressId > 0)
                {
                    returnResult.StatusMessages.Add("Attempting to create a new address, but an Id is present.");
                }
                else
                {
                    try
                    {
                        using (var db = new BTAContext())
                        {
                            var now = DateTime.Now;
                            request.Data.CreatedBy = userGuid;
                            request.Data.CreatedDate = now;
                            request.Data.LastModifiedBy = userGuid;
                            request.Data.LastModifiedDate = now;
                            var resultAddress = db.Address.Add(request.Data);
                            db.SaveChanges();
                            var entity = resultAddress.Entity;
                            if (entity != null)
                            {
                                returnResult.Success = true;
                                returnResult.StatusMessages.Add("Successfully added address.");
                                returnResult.Data = entity;
                            }
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
        public IActionResult Put([FromBody]AddressRequest request)
        {
            string json = JsonConvert.SerializeObject(request);
            Guid userGuid = GetUserGuidFromUserName(request.UserName);
            var returnResult = new AddressActionResult(false, new List<string>(), null);
            if (request != null)
            {
                if (request.Data.AddressId <= 0)
                {
                    returnResult.StatusMessages.Add("Attempting to update an existing address, but an Id is not present.");
                }
                else
                {
                    try
                    {
                        using (var db = new BTAContext())
                        {
                            var resultAddress = db.Address.SingleOrDefault(x => x.AddressId == request.Data.AddressId);
                            if (resultAddress != null)
                            {
                                resultAddress.AddressLine1 = request.Data.AddressLine1;
                                resultAddress.AddressLine2 = request.Data.AddressLine2;
                                resultAddress.AddressLine3 = request.Data.AddressLine3;
                                resultAddress.City = request.Data.City;
                                resultAddress.State = request.Data.State;
                                resultAddress.PostalCode = request.Data.PostalCode;
                                resultAddress.LastModifiedBy = userGuid;
                                resultAddress.LastModifiedDate = DateTime.Now;
                                db.SaveChanges();
                                returnResult.Success = true;
                                returnResult.Data = resultAddress;
                                returnResult.StatusMessages.Add("Successfully updated address.");
                            }
                            else
                            {
                                returnResult.Success = false;
                                returnResult.StatusMessages.Add(string.Format("Unable to locate address for index: {0}", request.Data.AddressId));
                                returnResult.Data = null;
                            }
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
                using (var db = new BTAContext())
                {
                    if (db.Address.Any(x => x.AddressId == id) == false)
                    {
                        returnResult.StatusMessages.Add("Attempted to delete a nonexisting address.");
                    }
                    else
                    {
                        var resultAddress = db.Address.Single(x => x.AddressId == id);
                        db.Address.Remove(resultAddress);
                        db.SaveChanges();
                        returnResult.Success = true;
                        returnResult.Data = resultAddress;
                        returnResult.StatusMessages.Add("Successfully deleted address.");
                    }
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