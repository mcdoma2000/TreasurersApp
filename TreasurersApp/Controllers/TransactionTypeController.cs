using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TreasurersApp.Database;
using TreasurersApp.Models;

namespace TreasurersApp.Controllers
{
    [Route("api/[controller]")]
    public class TransactionTypeController : BaseController
    {
        public TransactionTypeController(IConfiguration config, ILogger<AddressController> logger, IHostingEnvironment env, IMemoryCache memoryCache, BTAContext context)
            : base(config, logger, env, memoryCache, context)
        {
        }

        [HttpGet("get", Name = "TransactionTypeGet")]
        public IActionResult Get(bool includeInactive)
        {
            IActionResult ret = null;
            List<TransactionType> list = new List<TransactionType>();

            try
            {
                if (Context.TransactionType.Count() > 0)
                {
                    // If includeInactive == true, return all, otherwise return only active records.
                    list = Context.TransactionType
                        .Where(x => (x.Active.HasValue && x.Active.Value) || includeInactive)
                        .OrderBy(x => x.DisplayOrder)
                        .ToList();
                }
                ret = StatusCode(StatusCodes.Status200OK, list);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An exception occurred while trying to retrieve transaction types.");
                ret = StatusCode(StatusCodes.Status500InternalServerError);
            }

            return ret;
        }

        [HttpGet("getviewmodels", Name = "TransactionTypeGetViewModels")]
        public IActionResult GetViewModels(bool includeInactive)
        {
            IActionResult ret = null;
            List<TransactionTypeViewModel> list = new List<TransactionTypeViewModel>();

            try
            {
                if (Context.TransactionType.Count() > 0)
                {
                    // If includeInactive == true, return all, otherwise return only active records.
                    list = Context.TransactionType
                        .Where(x => x.Active ?? false || includeInactive)
                        .Select(x => new TransactionTypeViewModel()
                        {
                            TransactionTypeID = x.TransactionTypeId,
                            TransactionCategoryID = x.TransactionCategoryId,
                            TransactionCategoryDescription = x.TransactionCategory.Description,
                            Name = x.Name,
                            DisplayOrder = x.DisplayOrder,
                            Active = x.Active ?? false
                        }).ToList();
                }
                ret = StatusCode(StatusCodes.Status200OK, list);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An exception occurred while trying to retrieve transaction types.");
                ret = StatusCode(StatusCodes.Status500InternalServerError);
            }

            return ret;
        }

        [HttpGet("getbyid", Name = "TransactionTypeGetByID")]
        public IActionResult Get(int id)
        {
            IActionResult ret = null;
            TransactionType entity = null;

            try
            {
                entity = Context.TransactionType.Find(id);
                if (entity != null)
                {
                    ret = StatusCode(StatusCodes.Status200OK, entity);
                }
                else
                {
                    ret = StatusCode(StatusCodes.Status404NotFound,
                                    "Can't Find transaction type for id: " + id.ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An exception occurred while trying to retrieve a single transaction type.");
                ret = StatusCode(StatusCodes.Status500InternalServerError);
            }

            return ret;
        }

        [HttpPost("post", Name = "TransactionTypePost")]
        public IActionResult Post([FromBody]TransactionTypeRequest request)
        {
            var returnResult = new TransactionTypeActionResult(false, new List<string>(), null);
            if (request != null)
            {
                try
                {
                    var resultTransactionType = Context.TransactionType.Add(request.Data);
                    Context.SaveChanges();
                    var entity = resultTransactionType.Entity;
                    if (entity != null)
                    {
                        returnResult.Success = true;
                        returnResult.StatusMessages.Add("Successfully added transaction type.");
                        returnResult.Data = entity;
                    }
                }
                catch (Exception e)
                {
                    Logger.LogError(e, "An exception occurred while attempting to add a transaction type.");
                    returnResult.Success = false;
                    returnResult.StatusMessages.Add("An exception occurred while attempting to add a transaction type.");
                    returnResult.Data = null;
                }
            }
            else
            {
                returnResult.Success = false;
                returnResult.StatusMessages.Add("Empty transaction type posted for add.");
                returnResult.Data = null;
            }
            return StatusCode(StatusCodes.Status200OK, returnResult);
        }

        [HttpPut("put", Name = "TransactionTypePut")]
        public IActionResult Put([FromBody]TransactionTypeRequest request)
        {
            var returnResult = new TransactionTypeActionResult(false, new List<string>(), null);
            if (request != null)
            {
                try
                {
                    var resultTransactionType = Context.TransactionType.SingleOrDefault(x => x.TransactionTypeId == request.Data.TransactionTypeId);
                    if (resultTransactionType != null)
                    {
                        resultTransactionType.TransactionCategoryId = request.Data.TransactionCategoryId;
                        resultTransactionType.Name = request.Data.Name;
                        resultTransactionType.Description = request.Data.Description;
                        resultTransactionType.DisplayOrder = request.Data.DisplayOrder;
                        resultTransactionType.Active = request.Data.Active;
                        Context.SaveChanges();

                        returnResult.Success = true;
                        returnResult.Data = resultTransactionType;
                        returnResult.StatusMessages.Add("Successfully updated transaction type.");
                    }
                    else
                    {
                        returnResult.Success = false;
                        returnResult.StatusMessages.Add(string.Format("Unable to locate transaction type for id: {0}", request.Data.TransactionTypeId));
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
            else
            {
                returnResult.Success = false;
                returnResult.StatusMessages.Add("Empty transaction type posted for update.");
                returnResult.Data = null;
            }
            return StatusCode(StatusCodes.Status200OK, returnResult);
        }

        [HttpDelete("delete", Name = "TransactionTypeDelete")]
        public IActionResult Delete(int id)
        {
            var returnResult = new TransactionTypeActionResult(false, new List<string>(), null);
            try
            {
                if (Context.TransactionType.Any(x => x.TransactionTypeId == id) == false)
                {
                    returnResult.Success = false;
                    returnResult.StatusMessages.Add(string.Format("Unable to locate transaction type for id: {0}", id));
                    returnResult.Data = null;
                }
                else
                {
                    var resultTransactionType = Context.TransactionType.Single(x => x.TransactionTypeId == id);
                    Context.Remove(resultTransactionType);
                    Context.SaveChanges();
                    returnResult.Success = true;
                    returnResult.Data = resultTransactionType;
                    returnResult.StatusMessages.Add("Successfully deleted transaction type.");
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
                returnResult.Success = false;
                returnResult.StatusMessages.Add("An exception occurred while attempting to delete the transaction type.");
                returnResult.Data = null;
            }
            return StatusCode(StatusCodes.Status200OK, returnResult);
        }
    }
}