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
        public AddressTypeController(IConfiguration config, ILogger<AddressTypeController> logger, IHostingEnvironment env, IMemoryCache memoryCache, BTAContext context) 
            : base(config, logger, env, memoryCache, context)
        {
        }

        [HttpGet("get", Name = "AddressTypeGet")]
        public IActionResult Get()
        {
            IActionResult ret = null;
            List<AddressType> list = new List<AddressType>();

            try
            {
                if (Context.AddressType.Count() > 0)
                {
                    list = Context.AddressType.ToList();
                }
                ret = StatusCode(StatusCodes.Status200OK, list);
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
                entity = Context.AddressType.Find(id);
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
                ret = HandleException(ex, "Exception trying to retrieve a single address type.");
            }

            return ret;
        }

        [HttpPost("post", Name = "AddressTypePost")]
        public IActionResult Post([FromBody]AddressTypeRequest request)
        {
            string json = JsonConvert.SerializeObject(request);
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
                        var now = DateTime.Now;
                        var resultAddress = Context.AddressType.Add(request.Data);
                        Context.SaveChanges();
                        var entity = resultAddress.Entity;
                        if (entity != null)
                        {
                            returnResult.Success = true;
                            returnResult.StatusMessages.Add("Successfully added address type.");
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
                        var resultPhone = Context.AddressType.SingleOrDefault(x => x.AddressTypeId == request.Data.AddressTypeId);
                        if (resultPhone != null)
                        {
                            resultPhone.Name = request.Data.Name;
                            resultPhone.Description = request.Data.Description;
                            resultPhone.Active = request.Data.Active;
                            Context.SaveChanges();
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
                if (Context.AddressType.Any(x => x.AddressTypeId == id) == false)
                {
                    returnResult.StatusMessages.Add("Attempted to delete a nonexisting address type.");
                }
                else
                {
                    var resultAddressType = Context.AddressType.Single(x => x.AddressTypeId == id);
                    Context.AddressType.Remove(resultAddressType);
                    Context.SaveChanges();
                    returnResult.Success = true;
                    returnResult.Data = resultAddressType;
                    returnResult.StatusMessages.Add("Successfully deleted address type.");
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