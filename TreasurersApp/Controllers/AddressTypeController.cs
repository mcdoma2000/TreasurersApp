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
    public class AddressTypeController : BaseController
    {
        public AddressTypeController(IConfiguration config, ILogger<AddressTypeController> logger, IHostingEnvironment env, IMemoryCache memoryCache) 
            : base(config, logger, env, memoryCache)
        {
        }

        [HttpGet("get", Name = "AddressTypeGet")]
        public IActionResult Get()
        {
            IActionResult ret = null;
            List<AddressType> list = new List<AddressType>();

            try
            {
                using (var db = new BTAContext())
                {
                    if (db.AddressType.Count() > 0)
                    {
                        list = db.AddressType.ToList();
                    }
                    ret = StatusCode(StatusCodes.Status200OK, list);
                }
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to get all address types");
            }

            return ret;
        }

        [HttpGet("getbyid/{id}", Name = "AddressTypeGetByID")]
        public IActionResult Get(int id)
        {
            IActionResult ret = null;
            AddressType entity = null;

            try
            {
                using (var db = new BTAContext())
                {
                    entity = db.AddressType.Find(id);
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
                ret = HandleException(ex, "Exception trying to retrieve a single address type.");
            }

            return ret;
        }

        [HttpPost("post", Name = "AddressTypePost")]
        public IActionResult Post([FromBody]AddressTypeRequest request)
        {
            string json = JsonConvert.SerializeObject(request);
            Guid userGuid = GetUserGuidFromUserName(request.UserName);
            var returnResult = new AddressTypeActionResult(false, new List<string>(), null);
            if (request != null)
            {
                if (request.Data.AddressTypeId > 0)
                {
                    returnResult.StatusMessages.Add("Attempting to create a new address type, but an Id is present.");
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
                            request.Data.LastModifiedDate = now;
                            request.Data.LastModifiedBy = userGuid;
                            var resultAddress = db.AddressType.Add(request.Data);
                            db.SaveChanges();
                            var entity = resultAddress.Entity;
                            if (entity != null)
                            {
                                returnResult.Success = true;
                                returnResult.StatusMessages.Add("Successfully added address type.");
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
                returnResult.StatusMessages.Add("Empty address type posted for add.");
                returnResult.Data = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }

        [HttpPut("put", Name = "AddressTypePut")]
        public IActionResult Put([FromBody]AddressTypeRequest request)
        {
            string json = JsonConvert.SerializeObject(request);
            Guid userGuid = GetUserGuidFromUserName(request.UserName);
            var returnResult = new AddressTypeActionResult(false, new List<string>(), null);
            if (request != null)
            {
                if (request.Data.AddressTypeId <= 0)
                {
                    returnResult.StatusMessages.Add("Attempting to update an existing address type, but an Id is not present.");
                }
                else
                {
                    try
                    {
                        using (var db = new BTAContext())
                        {
                            var resultPhone = db.AddressType.SingleOrDefault(x => x.AddressTypeId == request.Data.AddressTypeId);
                            if (resultPhone != null)
                            {
                                resultPhone.Name = request.Data.Name;
                                resultPhone.Description = request.Data.Description;
                                resultPhone.Active = request.Data.Active;
                                resultPhone.LastModifiedDate = DateTime.Now;
                                resultPhone.LastModifiedBy = userGuid;
                                db.SaveChanges();
                                returnResult.Success = true;
                                returnResult.Data = resultPhone;
                                returnResult.StatusMessages.Add("Successfully updated address type.");
                            }
                            else
                            {
                                returnResult.Success = false;
                                returnResult.StatusMessages.Add(string.Format("Unable to locate address type for index: {0}", request.Data.AddressTypeId));
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
                returnResult.StatusMessages.Add("Empty address type posted for update.");
                returnResult.Data = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }

        [HttpDelete("delete", Name = "AddressTypeDelete")]
        [Authorize(Policy = "CanPerformAdmin")]
        public IActionResult Delete(int id)
        {
            var returnResult = new AddressTypeActionResult(false, new List<string>(), null);
            try
            {
                using (var db = new BTAContext())
                {
                    if (db.AddressType.Any(x => x.AddressTypeId == id) == false)
                    {
                        returnResult.StatusMessages.Add("Attempted to delete a nonexisting address type.");
                    }
                    else
                    {
                        var resultAddressType = db.AddressType.Single(x => x.AddressTypeId == id);
                        db.AddressType.Remove(resultAddressType);
                        db.SaveChanges();
                        returnResult.Success = true;
                        returnResult.Data = resultAddressType;
                        returnResult.StatusMessages.Add("Successfully deleted address type.");
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