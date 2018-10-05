using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreasurersApp.Database;
using TreasurersApp.Model;

namespace TreasurersApp.Controllers
{
    [Produces("application/json")]
    [Route("api/CashJournal")]
    [Authorize]
    public class CashJournalController : BaseApiController
    {
        public CashJournalController(IHostingEnvironment env) : base(env)
        {

        }

        [HttpGet]
        [Authorize(Policy = "CanAccessCashJournal")]
        public IActionResult Get()
        {
            IActionResult ret = null;
            List<AppCashJournal> list = new List<AppCashJournal>();

            try
            {
                using (var db = new BtaDbContext(GetDatabasePath()))
                {
                    if (db.CashJournals.Count() > 0)
                    {
                        list = db.CashJournals.OrderBy(p => p.CreatedDate).ToList();
                        ret = StatusCode(StatusCodes.Status200OK, list);
                    }
                    else
                    {
                        ret = StatusCode(StatusCodes.Status404NotFound, "Can't Find Cash Journal Entries");
                    }
                }
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to get all cash journal entries");
            }

            return ret;
        }

        [HttpGet("{id}", Name = "GetCashJournal")]
        [Authorize(Policy = "CanAccessCashJournal")]
        public IActionResult Get(int id)
        {
            IActionResult ret = null;
            AppCashJournal entity = null;

            try
            {
                using (var db = new BtaDbContext(GetDatabasePath()))
                {
                    entity = db.CashJournals.Find(id);
                    if (entity != null)
                    {
                        ret = StatusCode(StatusCodes.Status200OK, entity);
                    }
                    else
                    {
                        ret = StatusCode(StatusCodes.Status404NotFound,
                                 "Can't Find Cash Journal Entry: " + id.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ret = HandleException(ex,
                  "Exception trying to retrieve a single cash journal entry.");
            }

            return ret;
        }

        [HttpPost()]
        [Authorize(Policy = "CanEditCashJournal")]
        public IActionResult Post([FromBody]AppCashJournal entity)
        {
            IActionResult ret = null;

            try
            {
                using (var db = new BtaDbContext(GetDatabasePath()))
                {
                    if (entity != null)
                    {
                        db.CashJournals.Add(entity);
                        db.SaveChanges();
                        ret = StatusCode(StatusCodes.Status201Created,
                            entity);
                    }
                    else
                    {
                        ret = StatusCode(StatusCodes.Status400BadRequest, "Invalid data passed to create new cash journal entry");
                    }
                }
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to insert a new cash journal entry");
            }

            return ret;
        }

        [HttpPut()]
        [Authorize(Policy = "CanEditCashJournal")]
        public IActionResult Put([FromBody]AppCashJournal entity)
        {
            IActionResult ret = null;

            try
            {
                using (var db = new BtaDbContext(GetDatabasePath()))
                {
                    if (entity != null)
                    {
                        db.Update(entity);
                        db.SaveChanges();
                        ret = StatusCode(StatusCodes.Status200OK, entity);
                    }
                    else
                    {
                        ret = StatusCode(StatusCodes.Status400BadRequest, "Invalid data passed for cash journal update");
                    }
                }
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to update cash journal entry: " + entity.Id.ToString());
            }

            return ret;
        }
    }
}