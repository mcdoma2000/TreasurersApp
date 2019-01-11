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
    public class EmailController : BaseController
    {
        public EmailController(IConfiguration config, ILogger<EmailController> logger, IHostingEnvironment env, IMemoryCache memoryCache, BTAContext context) 
            : base(config, logger, env, memoryCache, context)
        {
        }

        [HttpGet("get", Name = "EmailGet")]
        public IActionResult Get()
        {
            IActionResult ret = null;
            List<EmailAddress> list = new List<EmailAddress>();

            try
            {
                if (Context.EmailAddress.Count() > 0)
                {
                    list = Context.EmailAddress
                        .OrderBy(r => r.Email)
                        .ToList();
                }
                ret = StatusCode(StatusCodes.Status200OK, list);
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to get all email addresses");
            }

            return ret;
        }

        [HttpGet("getbyid/{id}", Name = "EmailGetByID")]
        public IActionResult Get(int id)
        {
            IActionResult ret = null;
            EmailAddress entity = null;

            try
            {
                entity = Context.EmailAddress.Find(id);
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
                ret = HandleException(ex, "Exception trying to retrieve a single email address.");
            }

            return ret;
        }

        [HttpPost("post", Name = "EmailPost")]
        public IActionResult Post([FromBody]EmailRequest request)
        {
            string json = JsonConvert.SerializeObject(request);
            var returnResult = new EmailAddressActionResult(false, new List<string>(), null);
            if (request != null)
            {
                if (request.Data.EmailAddressId > 0)
                {
                    returnResult.StatusMessages.Add("Attempting to create a new email address, but an Id is present.");
                }
                else
                {
                    try
                    {
                        var now = DateTime.Now;
                        var resultEmailAddress = Context.EmailAddress.Add(request.Data);
                        Context.SaveChanges();
                        var entity = resultEmailAddress.Entity;
                        if (entity != null)
                        {
                            returnResult.Success = true;
                            returnResult.StatusMessages.Add("Successfully added email address.");
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
                returnResult.StatusMessages.Add("Empty email address posted for add.");
                returnResult.Data = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }

        [HttpPut("put", Name = "EmailPut")]
        public IActionResult Put([FromBody]EmailRequest request)
        {
            string json = JsonConvert.SerializeObject(request);
            var returnResult = new EmailAddressActionResult(false, new List<string>(), null);
            if (request != null)
            {
                if (request.Data.EmailAddressId <= 0)
                {
                    returnResult.StatusMessages.Add("Attempting to update an existing email address, but an Id is not present.");
                }
                else
                {
                    try
                    {
                        var resultEmail = Context.EmailAddress.SingleOrDefault(x => x.EmailAddressId == request.Data.EmailAddressId);
                        if (resultEmail != null)
                        {
                            resultEmail.Email = request.Data.Email;
                            Context.SaveChanges();
                            returnResult.Success = true;
                            returnResult.Data = resultEmail;
                            returnResult.StatusMessages.Add("Successfully updated address.");
                        }
                        else
                        {
                            returnResult.Success = false;
                            returnResult.StatusMessages.Add(string.Format("Unable to locate email address for index: {0}", request.Data.EmailAddressId));
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
                returnResult.StatusMessages.Add("Empty email address posted for update.");
                returnResult.Data = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }

        [HttpDelete("delete", Name = "EmailDelete")]
        [Authorize(Policy = "CanPerformAdmin")]
        public IActionResult Delete(int id)
        {
            var returnResult = new EmailAddressActionResult(false, new List<string>(), null);
            try
            {
                if (Context.EmailAddress.Any(x => x.EmailAddressId == id) == false)
                {
                    returnResult.StatusMessages.Add("Attempted to delete a nonexisting email address.");
                }
                else
                {
                    var resultAddress = Context.EmailAddress.Single(x => x.EmailAddressId == id);
                    Context.EmailAddress.Remove(resultAddress);
                    Context.SaveChanges();
                    returnResult.Success = true;
                    returnResult.Data = resultAddress;
                    returnResult.StatusMessages.Add("Successfully deleted email address.");
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