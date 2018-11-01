using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreasurersApp.Database;
using TreasurersApp.Models;

namespace TreasurersApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
#if RELEASE
    [Authorize]
#endif
    public class ContributorController : BaseController
    {
        public ContributorController(IHostingEnvironment env) : base(env)
        {

        }

        [HttpGet]
#if RELEASE
        [Authorize(Policy = "CanAccessContributors")]
#endif
        public IActionResult Get()
        {
            IActionResult ret = null;
            List<AppContributor> list = new List<AppContributor>();

            try
            {
                using (var db = new TreasurersAppDbContext(this.GetDatabasePath()))
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
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to get all Contributors");
            }

            return ret;
        }

        [HttpGet("{id}")]
#if RELEASE
        [Authorize(Policy = "CanAccessContributors")]
#endif
        public IActionResult Get(int id)
        {
            IActionResult ret = null;
            AppContributor entity = null;

            try
            {
                using (var db = new TreasurersAppDbContext(GetDatabasePath()))
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
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to retrieve a single Contributor.");
            }

            return ret;
        }

        [HttpPost(Name = "ContributorPost")]
#if RELEASE
        [Authorize(Policy = "CanAccessContributors")]
#endif
        public IActionResult Post([FromBody]AppContributor contributor)
        {
            return new EmptyResult();
        }

        [HttpPut(Name = "ContributorPut")]
#if RELEASE
        [Authorize(Policy = "CanAccessContributors")]
#endif
        public IActionResult Put([FromBody]AppContributor contributor)
        {
            return new EmptyResult();
        }

        [HttpDelete(Name = "ContributorDelete")]
#if RELEASE
        [Authorize(Policy = "CanAccessContributors")]
#endif
        public IActionResult Delete([FromBody]AppContributor contributor)
        {
            return new EmptyResult();
        }
    }
}