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
    public class ContributionCategoryController : BaseController
    {
        public ContributionCategoryController(IConfiguration config, ILogger<ContributionCategoryController> logger, IHostingEnvironment env, IMemoryCache memoryCache)
            : base(config, logger, env, memoryCache)
        {
        }

        [HttpGet("get", Name = "ContributionCategoryGet")]
        public IActionResult Get()
        {
            IActionResult ret = null;
            List<ContributionCategory> list = new List<ContributionCategory>();

            try
            {
                using (var db = new TreasurersAppDbContext(DatabasePath))
                {
                    if (db.ContributionCategories.Count() > 0)
                    {
                        list = db.ContributionCategories.OrderBy(p => p.Description).ToList();
                        ret = StatusCode(StatusCodes.Status200OK, list);
                    }
                    else
                    {
                        ret = StatusCode(StatusCodes.Status404NotFound, "Can't find contribution categories.");
                    }
                }
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to get all contribution categories.");
            }

            return ret;
        }

        [HttpGet("getbyid", Name = "ContributionCategoryGetById")]
        public IActionResult Get(int id)
        {
            IActionResult ret = null;
            ContributionCategory entity = null;

            try
            {
                using (var db = new TreasurersAppDbContext(DatabasePath))
                {
                    entity = db.ContributionCategories.Find(id);
                    if (entity != null)
                    {
                        ret = StatusCode(StatusCodes.Status200OK, entity);
                    }
                    else
                    {
                        ret = StatusCode(StatusCodes.Status404NotFound,
                                    "Can't find contribution category entry: " + id.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ret = HandleException(ex,
                  "Exception trying to retrieve a single contribution category entry.");
            }

            return ret;
        }

        [HttpPost("post", Name = "ContributionCategoryPost")]
        public IActionResult Post([FromBody]ContributionCategory entity)
        {
            IActionResult ret = null;
            var result = new ContributionCategoryActionResult(false, new List<string>(), null);
            try
            {
                if (entity != null)
                {
                    using (var db = new TreasurersAppDbContext(DatabasePath))
                    {
                        db.ContributionCategories.Add(entity);
                        db.SaveChanges();
                        result.Success = true;
                        result.StatusMessages.Add("Successfully added contribution category.");
                        result.Data = entity;
                        ret = StatusCode(StatusCodes.Status201Created, result);
                    }
                }
                else
                {
                    result.Success = false;
                    result.StatusMessages.Add("Invalid data passed to create new contribution category.");
                    result.Data = null;
                    ret = StatusCode(StatusCodes.Status205ResetContent, result);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Exception trying to insert a new contribution category entry.");
                Logger.LogError(ex.ToString());
                result.Success = false;
                result.Data = null;
                result.StatusMessages.Add("Exception trying to insert a new contribution category entry.");
                ret = StatusCode(StatusCodes.Status500InternalServerError, result);
            }

            return ret;
        }

        [HttpPut("put", Name = "ContributionCategoryPut")]
        public IActionResult Put([FromBody]ContributionCategory entity)
        {
            IActionResult ret = null;
            var result = new ContributionCategoryActionResult(false, new List<string>(), null);
            try
            {
                if (entity != null)
                {
                    using (var db = new TreasurersAppDbContext(DatabasePath))
                    {
                        db.Update(entity);
                        db.SaveChanges();
                        result.Success = true;
                        result.Data = entity;
                        result.StatusMessages.Add("Successfully updated contribution category.");
                        ret = StatusCode(StatusCodes.Status200OK, result);
                    }
                }
                else
                {
                    result.Success = false;
                    result.Data = null;
                    result.StatusMessages.Add("Invalid data passed for contribution category update.");
                    ret = StatusCode(StatusCodes.Status205ResetContent, result);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Exception trying to update a contribution category entry.");
                Logger.LogError(ex.ToString());
                result.Success = false;
                result.Data = null;
                result.StatusMessages.Add("Exception trying to update a contribution category entry.");
                ret = StatusCode(StatusCodes.Status500InternalServerError, result);
            }

            return ret;
        }

        [HttpDelete("delete", Name = "ContributionCategoryDelete")]
        [Authorize(Policy = "CanPerformAdmin")]
        public IActionResult Delete(int id)
        {
            var returnResult = new ContributionCategoryActionResult(false, new List<string>(), null);
            try
            {
                using (var db = new TreasurersAppDbContext(DatabasePath))
                {
                    if (db.ContributionCategories.Any(x => x.ContributionCategoryID == id) == false)
                    {
                        returnResult.StatusMessages.Add("Attempted to delete a nonexisting contribution category.");
                    }
                    else
                    {
                        var resultCategory = db.ContributionCategories.Single(x => x.ContributionCategoryID == id);
                        db.ContributionCategories.Remove(resultCategory);
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
                return StatusCode(StatusCodes.Status205ResetContent, returnResult);
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }
    }
}
