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
using TreasurersApp.Database;
using TreasurersApp.Models;

namespace TreasurersApp.Controllers
{
    [Route("api/[controller]")]
    public class TransactionCategoryController : BaseController
    {
        public TransactionCategoryController(IConfiguration config, ILogger<TransactionCategoryController> logger, IHostingEnvironment env, IMemoryCache memoryCache)
            : base(config, logger, env, memoryCache)
        {
        }

        [HttpGet("get", Name = "TransactionCategoryGet")]
        public IActionResult Get(bool includeInactive)
        {
            IActionResult ret = null;
            List<TransactionCategory> list = new List<TransactionCategory>();

            try
            {
                using (var db = new BTAContext())
                {
                    if (db.TransactionCategory.Count() > 0)
                    {
                        // If includeInactive == true, return all, otherwise return only active records.
                        list = db.TransactionCategory
                            .Where(x => (x.Active.HasValue && x.Active.Value) || includeInactive)
                            .OrderBy(p => p.DisplayOrder)
                            .ToList();
                    }
                    ret = StatusCode(StatusCodes.Status200OK, list);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Exception trying to get all contribution categories.");
                ret = StatusCode(StatusCodes.Status200OK, list);
            }

            return ret;
        }

        [HttpGet("getbyid", Name = "TransactionCategoryGetById")]
        public IActionResult Get(int id)
        {
            IActionResult ret = null;
            TransactionCategory entity = null;

            try
            {
                using (var db = new BTAContext())
                {
                    entity = db.TransactionCategory.Find(id);
                    if (entity != null)
                    {
                        ret = StatusCode(StatusCodes.Status200OK, entity);
                    }
                    else
                    {
                        Logger.LogError(string.Format("Can't find contribution category entry: {0}", id));
                        ret = StatusCode(StatusCodes.Status404NotFound);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An exception occurred while attempting to retrieve a single contribution category entry.");
                ret = StatusCode(StatusCodes.Status500InternalServerError);
            }

            return ret;
        }

        [HttpPost("post", Name = "TransactionCategoryPost")]
        public IActionResult Post([FromBody]TransactionCategoryRequest request)
        {
            var result = new TransactionCategoryActionResult(false, new List<string>(), null);
            try
            {
                if (request != null)
                {
                    using (var db = new BTAContext())
                    {
                        db.TransactionCategory.Add(request.Data);
                        db.SaveChanges();
                        result.Success = true;
                        result.StatusMessages.Add("Successfully added contribution category.");
                        result.Data = request.Data;
                    }
                }
                else
                {
                    result.Success = false;
                    result.StatusMessages.Add("Invalid data passed to create new contribution category.");
                    result.Data = null;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Exception trying to insert a new contribution category entry.");
                Logger.LogError(ex.ToString());
                result.Success = false;
                result.Data = null;
                result.StatusMessages.Add("Exception trying to insert a new contribution category entry.");
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPut("put", Name = "TransactionCategoryPut")]
        public IActionResult Put([FromBody]TransactionCategoryRequest request)
        {
            var result = new TransactionCategoryActionResult(false, new List<string>(), null);
            try
            {
                if (request != null)
                {
                    using (var db = new BTAContext())
                    {
                        db.Update(request.Data);
                        db.SaveChanges();
                        result.Success = true;
                        result.Data = request.Data;
                        result.StatusMessages.Add("Successfully updated contribution category.");
                    }
                }
                else
                {
                    result.Success = false;
                    result.Data = null;
                    result.StatusMessages.Add("Invalid data passed to contribution category update.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Exception trying to update a contribution category entry.");
                Logger.LogError(ex.ToString());
                result.Success = false;
                result.Data = null;
                result.StatusMessages.Add("Exception trying to update a contribution category entry.");
            }

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpDelete("delete", Name = "TransactionCategoryDelete")]
        [Authorize(Policy = "CanPerformAdmin")]
        public IActionResult Delete(int id)
        {
            var returnResult = new TransactionCategoryActionResult(false, new List<string>(), null);
            try
            {
                using (var db = new BTAContext())
                {
                    if (db.TransactionCategory.Any(x => x.TransactionCategoryId == id) == false)
                    {
                        returnResult.StatusMessages.Add("Attempted to delete a nonexisting contribution category.");
                    }
                    else
                    {
                        var resultCategory = db.TransactionCategory.Single(x => x.TransactionCategoryId == id);
                        db.TransactionCategory.Remove(resultCategory);
                        db.SaveChanges();
                        returnResult.Success = true;
                        returnResult.Data = resultCategory;
                        returnResult.StatusMessages.Add("Successfully deleted contribution category.");
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError("An exception occurred while attempting to delete a contribution category.");
                Logger.LogError(e.ToString());
                returnResult.Success = false;
                returnResult.StatusMessages.Add("An exception occurred while attempting to delete a contribution category.");
                returnResult.Data = null;
            }
            return StatusCode(StatusCodes.Status200OK, returnResult);
        }
    }
}
