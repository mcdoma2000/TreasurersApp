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
        private readonly TreasurersAppDbContext db;

        public ContributionTypeController(IConfiguration config, ILogger<AddressController> logger, IHostingEnvironment env, IMemoryCache memoryCache, TreasurersAppDbContext db)
            : base(config, logger, env, memoryCache)
        {
            this.db = db;
        }

        [HttpGet("/get", Name = "ContributionTypeGet")]
        public IActionResult Get()
        {
            IActionResult ret = null;
            List<ContributionType> list = new List<ContributionType>();

            try
            {
                if (db.ContributionTypes.Count() > 0)
                {
                    list = db.ContributionTypes
                        .OrderBy(r => r.Description)
                        .ToList();
                }
                ret = StatusCode(StatusCodes.Status200OK, list);
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to get all contribution types");
            }

            return ret;
        }

        [HttpGet("/getbyid", Name = "ContributionTypeGetByID")]
        public IActionResult Get(int id)
        {
            IActionResult ret = null;
            ContributionType entity = null;

            try
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
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to retrieve a single contribution type.");
            }

            return ret;
        }

        [HttpPost("/post", Name = "ContributionTypePost")]
        [ValidateAntiForgeryToken]
        public ActionResult Post([FromBody]ContributionType contributionType)
        {
            var returnResult = new ContributionTypeActionResult(false, new List<string>(), null);
            if (contributionType != null)
            {
                try
                {
                    var resultContributionType = db.ContributionTypes.Add(contributionType);
                    db.SaveChanges();
                    var entity = resultContributionType.Entity;
                    if (entity != null)
                    {
                        returnResult.Success = true;
                        returnResult.StatusMessages.Add("Successfully added contribution type.");
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
            else
            {
                returnResult.Success = false;
                returnResult.StatusMessages.Add("Empty contribution type posted for add.");
                returnResult.Data = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }

        [HttpPut("/put", Name = "ContributionTypePut")]
        [ValidateAntiForgeryToken]
        public ActionResult Put([FromBody]ContributionType contributionType)
        {
            var returnResult = new ContributionTypeActionResult(false, new List<string>(), null);
            if (contributionType != null)
            {
                try
                {
                    var resultContributionType = db.ContributionTypes.SingleOrDefault(x => x.ContributionTypeID == contributionType.ContributionTypeID);
                    if (resultContributionType != null)
                    {
                        resultContributionType.CategoryID = contributionType.CategoryID;
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
                        returnResult.StatusMessages.Add(string.Format("Unable to locate contribution type for id: {0}", contributionType.ContributionTypeID));
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
                returnResult.StatusMessages.Add("Empty contribution type posted for update.");
                returnResult.Data = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }
    }
}