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
    public class ContributionTypeController : BaseController
    {
        public ContributionTypeController(IConfiguration config, ILogger<ContributionTypeController> logger, IHostingEnvironment env, IMemoryCache memoryCache)
            : base(config, logger, env, memoryCache)
        {

        }

        [HttpGet(Name = "ContributionTypeGetAll")]
        public IActionResult Get()
        {
            IActionResult ret = null;
            List<AppContributionType> list = new List<AppContributionType>();

            try
            {
                using (var db = new TreasurersAppDbContext(DatabasePath))
                {
                    if (db.ContributionTypes.Count() > 0)
                    {
                        list = db.ContributionTypes
                            .OrderBy(r => r.ContributionTypeCategory)
                            .ThenBy(r => r.Description)
                            .ToList();
                    }
                    ret = StatusCode(StatusCodes.Status200OK, list);
                }
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to get all contribution types");
            }

            return ret;
        }

        [HttpGet("{id}", Name = "ContributionTypeGetByID")]
        public IActionResult Get(int id)
        {
            IActionResult ret = null;
            AppContributionType entity = null;

            try
            {
                using (var db = new TreasurersAppDbContext(DatabasePath))
                {
                    entity = db.ContributionTypes.Find(id);
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
                ret = HandleException(ex, "Exception trying to retrieve a single contribution type.");
            }

            return ret;
        }

        [HttpPost(Name = "ContributionTypePost")]
        [ValidateAntiForgeryToken]
        public ActionResult Post([FromBody]AppContributionType contributionType)
        {
            var returnResult = new AppContributionTypeActionResult(false, new List<string>(), null);
            if (contributionType != null)
            {
                try
                {
                    using (var db = new TreasurersAppDbContext(DatabasePath))
                    {
                        var resultContributionType = db.ContributionTypes.Add(contributionType);
                        db.SaveChanges();
                        var entity = resultContributionType.Entity;
                        if (entity != null)
                        {
                            returnResult.Success = true;
                            returnResult.StatusMessages.Add("Successfully added contribution type.");
                            returnResult.ContributionType = entity;
                        }
                    }
                }
                catch (Exception e)
                {
                    returnResult.Success = false;
                    returnResult.StatusMessages.Add(e.Message);
                    returnResult.ContributionType = null;
                }
            }
            else
            {
                returnResult.Success = false;
                returnResult.StatusMessages.Add("Empty contribution type posted for add.");
                returnResult.ContributionType = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }

        [HttpPut(Name = "ContributionTypePut")]
        [ValidateAntiForgeryToken]
        public ActionResult Put([FromBody]AppContributionType contributionType)
        {
            var returnResult = new AppContributionTypeActionResult(false, new List<string>(), null);
            if (contributionType != null)
            {
                try
                {
                    using (var db = new TreasurersAppDbContext(DatabasePath))
                    {
                        var resultContributionType = db.ContributionTypes.SingleOrDefault(x => x.Id == contributionType.Id);
                        if (resultContributionType != null)
                        {
                            resultContributionType.ContributionTypeCategory = contributionType.ContributionTypeCategory;
                            resultContributionType.ContributionTypeName = contributionType.ContributionTypeName;
                            resultContributionType.Description = contributionType.Description;
                            db.SaveChanges();
                            returnResult.Success = true;
                            returnResult.ContributionType = resultContributionType;
                            returnResult.StatusMessages.Add("Successfully updated contribution type.");
                        }
                        else
                        {
                            returnResult.Success = false;
                            returnResult.StatusMessages.Add(string.Format("Unable to locate contribution type for id: {0}", contributionType.Id));
                            returnResult.ContributionType = null;
                        }
                    }
                }
                catch (Exception e)
                {
                    returnResult.Success = false;
                    returnResult.StatusMessages.Add(e.Message);
                    returnResult.ContributionType = null;
                }
            }
            else
            {
                returnResult.Success = false;
                returnResult.StatusMessages.Add("Empty contribution type posted for update.");
                returnResult.ContributionType = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }

        [HttpDelete(Name = "ContributionTypeDelete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var returnResult = new AppContributionTypeActionResult(false, new List<string>(), null);
            try
            {
                using (var db = new TreasurersAppDbContext(DatabasePath))
                {
                    var resultContributionType = db.ContributionTypes.SingleOrDefault(x => x.Id == id);
                    if (resultContributionType != null)
                    {
                        db.ContributionTypes.Remove(resultContributionType);
                        db.SaveChanges();
                        returnResult.Success = true;
                        returnResult.ContributionType = resultContributionType;
                        returnResult.StatusMessages.Add("Successfully deleted contribution type.");
                    }
                }
            }
            catch (Exception e)
            {
                returnResult.Success = false;
                returnResult.StatusMessages.Add(e.Message);
                returnResult.ContributionType = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }
    }
}