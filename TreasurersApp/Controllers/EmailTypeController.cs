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
    public class EmailTypeController : BaseController
    {
        public EmailTypeController(IConfiguration config, ILogger<EmailTypeController> logger, IHostingEnvironment env, IMemoryCache memoryCache, BTAContext context) 
            : base(config, logger, env, memoryCache, context)
        {
        }

        [HttpGet("get", Name = "EmailTypeGet")]
        public IActionResult Get()
        {
            IActionResult ret = null;
            List<EmailType> list = new List<EmailType>();

            try
            {
                if (Context.EmailType.Count() > 0)
                {
                    list = Context.EmailType.ToList();
                }
                ret = StatusCode(StatusCodes.Status200OK, list);
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to get all email types");
            }

            return ret;
        }

        [HttpGet("getbyid/{id}", Name = "EmailTypeGetByID")]
        public IActionResult Get(int id)
        {
            IActionResult ret = null;
            EmailType entity = null;

            try
            {
                entity = Context.EmailType.Find(id);
                if (entity != null)
                {
                    ret = StatusCode(StatusCodes.Status200OK, entity);
                }
                else
                {
                    ret = StatusCode(StatusCodes.Status404NotFound,
                        "Can't Find Address: " + id.ToString());
                }
                entity = Context.EmailType.Find(id);
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
                ret = HandleException(ex, "Exception trying to retrieve a single email type.");
            }

            return ret;
        }

        [HttpPost("post", Name = "EmailTypePost")]
        public IActionResult Post([FromBody]EmailTypeRequest request)
        {
            string json = JsonConvert.SerializeObject(request);
            var returnResult = new EmailTypeActionResult(false, new List<string>(), null);
            if (request != null)
            {
                if (request.Data.EmailTypeId > 0)
                {
                    returnResult.StatusMessages.Add("Attempting to create a new email type, but an Id is present.");
                }
                else
                {
                    try
                    {
                        var resultAddress = Context.EmailType.Add(request.Data);
                        Context.SaveChanges();
                        var entity = resultAddress.Entity;
                        if (entity != null)
                        {
                            returnResult.Success = true;
                            returnResult.StatusMessages.Add("Successfully added email type.");
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
                returnResult.StatusMessages.Add("Empty email type posted for add.");
                returnResult.Data = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }

        [HttpPut("put", Name = "EmailTypePut")]
        public IActionResult Put([FromBody]EmailTypeRequest request)
        {
            string json = JsonConvert.SerializeObject(request);
            var returnResult = new EmailTypeActionResult(false, new List<string>(), null);
            if (request != null)
            {
                if (request.Data.EmailTypeId <= 0)
                {
                    returnResult.StatusMessages.Add("Attempting to update an existing email type, but an Id is not present.");
                }
                else
                {
                    try
                    {
                        var resultEmailType = Context.EmailType.SingleOrDefault(x => x.EmailTypeId == request.Data.EmailTypeId);
                        if (resultEmailType != null)
                        {
                            resultEmailType.Name = request.Data.Name;
                            resultEmailType.Description = request.Data.Description;
                            resultEmailType.Active = request.Data.Active;
                            Context.SaveChanges();
                            returnResult.Success = true;
                            returnResult.Data = resultEmailType;
                            returnResult.StatusMessages.Add("Successfully updated email type.");
                        }
                        else
                        {
                            returnResult.Success = false;
                            returnResult.StatusMessages.Add(string.Format("Unable to locate email type for index: {0}", request.Data.EmailTypeId));
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
                returnResult.StatusMessages.Add("Empty email type posted for update.");
                returnResult.Data = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }

        [HttpDelete("delete", Name = "EmailTypeDelete")]
        [Authorize(Policy = "CanPerformAdmin")]
        public IActionResult Delete(int id)
        {
            var returnResult = new EmailTypeActionResult(false, new List<string>(), null);
            try
            {
                if (Context.EmailType.Any(x => x.EmailTypeId == id) == false)
                {
                    returnResult.StatusMessages.Add("Attempted to delete a nonexisting email type.");
                }
                else
                {
                    var resultEmailType = Context.EmailType.Single(x => x.EmailTypeId == id);
                    Context.EmailType.Remove(resultEmailType);
                    Context.SaveChanges();
                    returnResult.Success = true;
                    returnResult.Data = resultEmailType;
                    returnResult.StatusMessages.Add("Successfully deleted email type.");
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