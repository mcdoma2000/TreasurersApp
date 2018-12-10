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
        public ContributorController(IConfiguration config, ILogger<AddressController> logger, IHostingEnvironment env, IMemoryCache memoryCache)
            : base(config, logger, env, memoryCache)
        {
        }

        [HttpGet("get", Name = "ContributorGet")]
        public IActionResult Get()
        {
            IActionResult ret = null;
            List<Contributor> list = new List<Contributor>();

            try
            {
                using (var db = new BTAContext())
                {
                    if (db.Contributor.Count() > 0)
                    {
                        list = db.Contributor
                            .OrderBy(r => r.LastName)
                            .ThenBy(r => r.FirstName)
                            .ThenBy(r => r.MiddleName)
                            .ToList();
                        ret = StatusCode(StatusCodes.Status200OK, list);
                    }
                    else
                    {
                        ret = StatusCode(StatusCodes.Status404NotFound, "Can't Find Contributor");
                    }
                }
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to get all Contributor");
            }

            return ret;
        }

        [HttpGet("getbyid", Name = "ContributorGetById")]
        public IActionResult Get(int id)
        {
            IActionResult ret = null;
            Contributor entity = null;

            try
            {
                using (var db = new BTAContext())
                {
                    entity = db.Contributor.Find(id);
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
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to retrieve a single Contributor.");
            }

            return ret;
        }

        [HttpPost("post", Name = "ContributorPost")]
        public IActionResult Post([FromBody]Contributor contributor)
        {
            var returnResult = new ContributorActionResult(false, new List<string>(), null);
            if (contributor != null)
            {
                try
                {
                    using (var db = new BTAContext())
                    {
                        var resultContributionType = db.Contributor.Add(contributor);
                        db.SaveChanges();
                        var entity = resultContributionType.Entity;
                        if (entity != null)
                        {
                            returnResult.Success = true;
                            returnResult.StatusMessages.Add("Successfully added contributor.");
                            returnResult.Data = entity;
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
                returnResult.StatusMessages.Add("Empty contributyor posted for add.");
                returnResult.Data = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }

        [HttpPut("put", Name = "ContributorPut")]
        public IActionResult Put([FromBody]Contributor contributor)
        {
            var returnResult = new ContributorActionResult(false, new List<string>(), null);
            if (contributor != null)
            {
                try
                {
                    using (var db = new BTAContext())
                    {
                        var resultContributor = db.Contributor.SingleOrDefault(x => x.ContributorId == contributor.ContributorId);
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
                            returnResult.StatusMessages.Add(string.Format("Unable to locate contributor for id: {0}", contributor.ContributorId));
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
                returnResult.StatusMessages.Add("Empty contributor posted for update.");
                returnResult.Data = null;
            }
            return returnResult.Success ?
                StatusCode(StatusCodes.Status200OK, returnResult) :
                StatusCode(StatusCodes.Status500InternalServerError, returnResult);
        }
    }
}