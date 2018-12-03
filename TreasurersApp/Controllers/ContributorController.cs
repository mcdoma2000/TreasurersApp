using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ContributorController : BaseController
    {
        private readonly TreasurersAppDbContext db;

        public ContributorController(IConfiguration config, ILogger<AddressController> logger, IHostingEnvironment env, IMemoryCache memoryCache, TreasurersAppDbContext db)
            : base(config, logger, env, memoryCache)
        {
            this.db = db;
        }

        [HttpGet("/get", Name = "ContributorGet")]
        public IActionResult Get()
        {
            IActionResult ret = null;
            List<Contributor> list = new List<Contributor>();

            try
            {
                if (db.Contributors.Count() > 0)
                {
                    list = db.Contributors
                        .OrderBy(r => r.LastName)
                        .ThenBy(r => r.FirstName)
                        .ThenBy(r => r.MiddleName)
                        .ToList();
                    ret = StatusCode(StatusCodes.Status200OK, list);
                }
                else
                {
                    ret = StatusCode(StatusCodes.Status404NotFound, "Can't Find Contributors");
                }
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to get all Contributors");
            }

            return ret;
        }

        [HttpGet("/getbyid", Name = "ContributorGetById")]
        public IActionResult Get(int id)
        {
            IActionResult ret = null;
            Contributor entity = null;

            try
            {
                entity = db.Contributors.Find(id);
                if (entity != null)
                {
                    ret = StatusCode(StatusCodes.Status200OK, entity);
                }
                else
                {
                    ret = StatusCode(StatusCodes.Status404NotFound, 
                                    "Can't Find Contributor: " + id.ToString());
                }
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to retrieve a single Contributor.");
            }

            return ret;
        }

        [HttpPost("/post", Name = "ContributorPost")]
        public IActionResult Post([FromBody]Contributor contributor)
        {
            var returnResult = new ContributorActionResult(false, new List<string>(), null);
            if (contributor != null)
            {
                try
                {
                    var resultContributionType = db.Contributors.Add(contributor);
                    db.SaveChanges();
                    var entity = resultContributionType.Entity;
                    if (entity != null)
                    {
                        returnResult.Success = true;
                        returnResult.StatusMessages.Add("Successfully added contributor.");
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
                returnResult.StatusMessages.Add("Empty contributyor posted for add.");
                returnResult.Data = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }

        [HttpPut("/put", Name = "ContributorPut")]
        public IActionResult Put([FromBody]Contributor contributor)
        {
            var returnResult = new ContributorActionResult(false, new List<string>(), null);
            if (contributor != null)
            {
                try
                {
                    var resultContributor = db.Contributors.SingleOrDefault(x => x.ContributorID == contributor.ContributorID);
                    if (resultContributor != null)
                    {
                        resultContributor.FirstName = contributor.FirstName;
                        resultContributor.MiddleName = contributor.MiddleName;
                        resultContributor.LastName = contributor.LastName;
                        resultContributor.AddressId = contributor.AddressId;
                        db.SaveChanges();
                        returnResult.Success = true;
                        returnResult.Data = resultContributor;
                        returnResult.StatusMessages.Add("Successfully updated contributor type.");
                    }
                    else
                    {
                        returnResult.Success = false;
                        returnResult.StatusMessages.Add(string.Format("Unable to locate contributor for id: {0}", contributor.ContributorID));
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
                returnResult.StatusMessages.Add("Empty contributor posted for update.");
                returnResult.Data = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }
    }
}