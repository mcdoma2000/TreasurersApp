using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ContributionTypeController : BaseController
    {
        public ContributionTypeController(IConfiguration config, ILogger<AddressController> logger, IHostingEnvironment env, IMemoryCache memoryCache)
            : base(config, logger, env, memoryCache)
        {
        }

        [HttpGet("get", Name = "ContributionTypeGet")]
        public IActionResult Get(bool includeInactive)
        {
            IActionResult ret = null;
            List<ContributionType> list = new List<ContributionType>();

            try
            {
                using (var db = new BTAContext())
                {
                    if (db.ContributionType.Count() > 0)
                    {
                        // If includeInactive == true, return all, otherwise return only active records.
                        list = db.ContributionType
                            .Where(x => (x.Active.HasValue && x.Active.Value) || includeInactive)
                            .OrderBy(x => x.DisplayOrder)
                            .ToList();
                    }
                    ret = StatusCode(StatusCodes.Status200OK, list);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An exception occurred while trying to retrieve contribution types.");
                ret = StatusCode(StatusCodes.Status500InternalServerError);
            }

            return ret;
        }

        [HttpGet("getviewmodels", Name = "ContributionTypeGetViewModels")]
        public IActionResult GetViewModels(bool includeInactive)
        {
            IActionResult ret = null;
            List<ContributionTypeViewModel> list = new List<ContributionTypeViewModel>();

            try
            {
                using (var db = new BTAContext())
                {
                    if (db.ContributionType.Count() > 0)
                    {
                        // If includeInactive == true, return all, otherwise return only active records.
                        list = db.ContributionType
                            .Where(x => x.Active ?? false || includeInactive)
                            .Select(x => new ContributionTypeViewModel()
                            {
                                ContributionTypeID = x.ContributionTypeId,
                                CategoryID = x.CategoryId,
                                CategoryDescription = x.Category.Description,
                                ContributionTypeName = x.ContributionTypeName,
                                DisplayOrder = x.DisplayOrder,
                                Active = x.Active ?? false
                            }).ToList();
                    }
                    ret = StatusCode(StatusCodes.Status200OK, list);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An exception occurred while trying to retrieve contribution types.");
                ret = StatusCode(StatusCodes.Status500InternalServerError);
            }

            return ret;
        }

        [HttpGet("getbyid", Name = "ContributionTypeGetByID")]
        public IActionResult Get(int id)
        {
            IActionResult ret = null;
            ContributionType entity = null;

            try
            {
                using (var db = new BTAContext())
                {
                    entity = db.ContributionType.Find(id);
                    if (entity != null)
                    {
                        ret = StatusCode(StatusCodes.Status200OK, entity);
                    }
                    else
                    {
                        ret = StatusCode(StatusCodes.Status404NotFound,
                                        "Can't Find contribution type for id: " + id.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An exception occurred while trying to retrieve a single contribution type.");
                ret = StatusCode(StatusCodes.Status500InternalServerError);
            }

            return ret;
        }

        [HttpPost("post", Name = "ContributionTypePost")]
        public IActionResult Post([FromBody]ContributionType contributionType)
        {
            var returnResult = new ContributionTypeActionResult(false, new List<string>(), null);
            if (contributionType != null)
            {
                try
                {
                    using (var db = new BTAContext())
                    {
                        var resultContributionType = db.ContributionType.Add(contributionType);
                        db.SaveChanges();
                        var entity = resultContributionType.Entity;
                        if (entity != null)
                        {
                            returnResult.Success = true;
                            returnResult.StatusMessages.Add("Successfully added contribution type.");
                            returnResult.Data = entity;
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.LogError(e, "An exception occurred while attempting to add a contribution type.");
                    returnResult.Success = false;
                    returnResult.StatusMessages.Add("An exception occurred while attempting to add a contribution type.");
                    returnResult.Data = null;
                }
            }
            else
            {
                returnResult.Success = false;
                returnResult.StatusMessages.Add("Empty contribution type posted for add.");
                returnResult.Data = null;
            }
            return StatusCode(StatusCodes.Status200OK, returnResult);
        }

        [HttpPut("put", Name = "ContributionTypePut")]
        public IActionResult Put([FromBody]ContributionType contributionType)
        {
            var returnResult = new ContributionTypeActionResult(false, new List<string>(), null);
            if (contributionType != null)
            {
                try
                {
                    using (var db = new BTAContext())
                    {
                        var resultContributionType = db.ContributionType.SingleOrDefault(x => x.ContributionTypeId == contributionType.ContributionTypeId);
                        if (resultContributionType != null)
                        {
                            resultContributionType.CategoryId = contributionType.CategoryId;
                            resultContributionType.ContributionTypeName = contributionType.ContributionTypeName;
                            resultContributionType.Description = contributionType.Description;
                            db.SaveChanges();
                            returnResult.Success = true;
                            returnResult.Data = resultContributionType;
                            returnResult.StatusMessages.Add("Successfully updated contribution type.");
                        }
                        else
                        {
                            returnResult.Success = false;
                            returnResult.StatusMessages.Add(string.Format("Unable to locate contribution type for id: {0}", contributionType.ContributionTypeId));
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
            else
            {
                returnResult.Success = false;
                returnResult.StatusMessages.Add("Empty contribution type posted for update.");
                returnResult.Data = null;
            }
            return StatusCode(StatusCodes.Status200OK, returnResult);
        }

        [HttpDelete("delete", Name = "ContributionTypeDelete")]
        public IActionResult Delete(int id)
        {
            var returnResult = new ContributionTypeActionResult(false, new List<string>(), null);
            try
            {
                using (var db = new BTAContext())
                {
                    if (db.ContributionType.Any(x => x.ContributionTypeId == id) == false)
                    {
                        returnResult.Success = false;
                        returnResult.StatusMessages.Add(string.Format("Unable to locate contribution type for id: {0}", id));
                        returnResult.Data = null;
                    }
                    else
                    {
                        var resultContributionType = db.ContributionType.Single(x => x.ContributionTypeId == id);
                        db.Remove(resultContributionType);
                        db.SaveChanges();
                        returnResult.Success = true;
                        returnResult.Data = resultContributionType;
                        returnResult.StatusMessages.Add("Successfully deleted contribution type.");
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
                returnResult.Success = false;
                returnResult.StatusMessages.Add("An exception occurred while attempting to delete the contribution type.");
                returnResult.Data = null;
            }
            return StatusCode(StatusCodes.Status200OK, returnResult);
        }
    }
}