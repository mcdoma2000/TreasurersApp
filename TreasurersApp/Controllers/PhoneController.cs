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
    public class PhoneController : BaseController
    {
        public PhoneController(IConfiguration config, ILogger<PhoneController> logger, IHostingEnvironment env, IMemoryCache memoryCache, BTAContext context) 
            : base(config, logger, env, memoryCache, context)
        {
        }

        [HttpGet("get", Name = "PhoneGet")]
        public IActionResult Get()
        {
            IActionResult ret = null;
            List<PhoneNumber> list = new List<PhoneNumber>();

            try
            {
                if (Context.PhoneNumber.Count() > 0)
                {
                    list = Context.PhoneNumber.ToList();
                }
                ret = StatusCode(StatusCodes.Status200OK, list);
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to get all phone numbers");
            }

            return ret;
        }

        [HttpGet("getbyid/{id}", Name = "PhoneGetByID")]
        public IActionResult Get(int id)
        {
            IActionResult ret = null;
            PhoneNumber entity = null;

            try
            {
                entity = Context.PhoneNumber.Find(id);
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

        [HttpPost("post", Name = "PhonePost")]
        public IActionResult Post([FromBody]PhoneNumberRequest request)
        {
            string json = JsonConvert.SerializeObject(request);
            var returnResult = new PhoneNumberActionResult(false, new List<string>(), null);
            if (request != null)
            {
                if (request.Data.PhoneNumberId > 0)
                {
                    returnResult.StatusMessages.Add("Attempting to create a new phone, but an Id is present.");
                }
                else
                {
                    try
                    {
                        var resultAddress = Context.PhoneNumber.Add(request.Data);
                        Context.SaveChanges();
                        var entity = resultAddress.Entity;
                        if (entity != null)
                        {
                            returnResult.Success = true;
                            returnResult.StatusMessages.Add("Successfully added phone.");
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
                returnResult.StatusMessages.Add("Empty phone number posted for add.");
                returnResult.Data = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }

        [HttpPut("put", Name = "PhonePut")]
        public IActionResult Put([FromBody]PhoneNumberRequest request)
        {
            string json = JsonConvert.SerializeObject(request);
            var returnResult = new PhoneNumberActionResult(false, new List<string>(), null);
            if (request != null)
            {
                if (request.Data.PhoneNumberId <= 0)
                {
                    returnResult.StatusMessages.Add("Attempting to update an existing phone number, but an Id is not present.");
                }
                else
                {
                    try
                    {
                        var resultPhone = Context.PhoneNumber.SingleOrDefault(x => x.PhoneNumberId == request.Data.PhoneNumberId);
                        if (resultPhone != null)
                        {
                            resultPhone.PhoneNumber_ = request.Data.PhoneNumber_;
                            Context.SaveChanges();
                            returnResult.Success = true;
                            returnResult.Data = resultPhone;
                            returnResult.StatusMessages.Add("Successfully updated address.");
                        }
                        else
                        {
                            returnResult.Success = false;
                            returnResult.StatusMessages.Add(string.Format("Unable to locate phone number for index: {0}", request.Data.PhoneNumberId));
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
                returnResult.StatusMessages.Add("Empty phone number posted for update.");
                returnResult.Data = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }

        [HttpDelete("delete", Name = "PhoneDelete")]
        [Authorize(Policy = "CanPerformAdmin")]
        public IActionResult Delete(int id)
        {
            var returnResult = new PhoneNumberActionResult(false, new List<string>(), null);
            try
            {
                if (Context.PhoneNumber.Any(x => x.PhoneNumberId == id) == false)
                {
                    returnResult.StatusMessages.Add("Attempted to delete a nonexisting phone number.");
                }
                else
                {
                    var resultPhoneNumber = Context.PhoneNumber.Single(x => x.PhoneNumberId == id);
                    Context.PhoneNumber.Remove(resultPhoneNumber);
                    Context.SaveChanges();
                    returnResult.Success = true;
                    returnResult.Data = resultPhoneNumber;
                    returnResult.StatusMessages.Add("Successfully deleted phone number.");
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