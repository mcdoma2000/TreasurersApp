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
    public class PhoneTypeController : BaseController
    {
        public PhoneTypeController(IConfiguration config, ILogger<PhoneTypeController> logger, IHostingEnvironment env, IMemoryCache memoryCache) 
            : base(config, logger, env, memoryCache)
        {
        }

        [HttpGet("get", Name = "PhoneTypeGet")]
        public IActionResult Get()
        {
            IActionResult ret = null;
            List<PhoneType> list = new List<PhoneType>();

            try
            {
                using (var db = new BTAContext())
                {
                    if (db.PhoneType.Count() > 0)
                    {
                        list = db.PhoneType.ToList();
                    }
                    ret = StatusCode(StatusCodes.Status200OK, list);
                }
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to get all phone types");
            }

            return ret;
        }

        [HttpGet("getbyid/{id}", Name = "PhoneTypeGetByID")]
        public IActionResult Get(int id)
        {
            IActionResult ret = null;
            PhoneType entity = null;

            try
            {
                using (var db = new BTAContext())
                {
                    entity = db.PhoneType.Find(id);
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
                ret = HandleException(ex, "Exception trying to retrieve a single phone type.");
            }

            return ret;
        }

        [HttpPost("post", Name = "PhoneTypePost")]
        public IActionResult Post([FromBody]PhoneTypeRequest request)
        {
            string json = JsonConvert.SerializeObject(request);
            var returnResult = new PhoneTypeActionResult(false, new List<string>(), null);
            if (request != null)
            {
                if (request.Data.PhoneTypeId > 0)
                {
                    returnResult.StatusMessages.Add("Attempting to create a new phone type, but an Id is present.");
                }
                else
                {
                    try
                    {
                        using (var db = new BTAContext())
                        {
                            var resultAddress = db.PhoneType.Add(request.Data);
                            db.SaveChanges();
                            var entity = resultAddress.Entity;
                            if (entity != null)
                            {
                                returnResult.Success = true;
                                returnResult.StatusMessages.Add("Successfully added phone type.");
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
                returnResult.StatusMessages.Add("Empty phone type posted for add.");
                returnResult.Data = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }

        [HttpPut("put", Name = "PhoneTypePut")]
        public IActionResult Put([FromBody]PhoneTypeRequest request)
        {
            string json = JsonConvert.SerializeObject(request);
            var returnResult = new PhoneTypeActionResult(false, new List<string>(), null);
            if (request != null)
            {
                if (request.Data.PhoneTypeId <= 0)
                {
                    returnResult.StatusMessages.Add("Attempting to update an existing phone type, but an Id is not present.");
                }
                else
                {
                    try
                    {
                        using (var db = new BTAContext())
                        {
                            var resultPhoneType = db.PhoneType.SingleOrDefault(x => x.PhoneTypeId == request.Data.PhoneTypeId);
                            if (resultPhoneType != null)
                            {
                                resultPhoneType.Name = request.Data.Name;
                                resultPhoneType.Description = request.Data.Description;
                                resultPhoneType.Active = request.Data.Active;
                                db.SaveChanges();
                                returnResult.Success = true;
                                returnResult.Data = resultPhoneType;
                                returnResult.StatusMessages.Add("Successfully updated phone type.");
                            }
                            else
                            {
                                returnResult.Success = false;
                                returnResult.StatusMessages.Add(string.Format("Unable to locate phone type for index: {0}", request.Data.PhoneTypeId));
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
                returnResult.StatusMessages.Add("Empty phone type posted for update.");
                returnResult.Data = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }

        [HttpDelete("delete", Name = "PhoneTypeDelete")]
        [Authorize(Policy = "CanPerformAdmin")]
        public IActionResult Delete(int id)
        {
            var returnResult = new PhoneTypeActionResult(false, new List<string>(), null);
            try
            {
                using (var db = new BTAContext())
                {
                    if (db.PhoneType.Any(x => x.PhoneTypeId == id) == false)
                    {
                        returnResult.StatusMessages.Add("Attempted to delete a nonexisting phone type.");
                    }
                    else
                    {
                        var resultPhoneType = db.PhoneType.Single(x => x.PhoneTypeId == id);
                        db.PhoneType.Remove(resultPhoneType);
                        db.SaveChanges();
                        returnResult.Success = true;
                        returnResult.Data = resultPhoneType;
                        returnResult.StatusMessages.Add("Successfully deleted phone type.");
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