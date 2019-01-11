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
    public class CashJournalController : BaseController
    {
        public CashJournalController(IConfiguration config, ILogger<AddressController> logger, IHostingEnvironment env, IMemoryCache memoryCache, BTAContext context)
            : base(config, logger, env, memoryCache, context)
        {
        }

        [HttpGet("get", Name = "CashJournalGet")]
        public IActionResult Get()
        {
            IActionResult ret = null;
            List<CashJournal> list = new List<CashJournal>();

            try
            {
                if (Context.CashJournal.Count() > 0)
                {
                    list = Context.CashJournal.OrderBy(p => p.CreatedDate).ToList();
                    ret = StatusCode(StatusCodes.Status200OK, list);
                }
                else
                {
                    ret = StatusCode(StatusCodes.Status404NotFound, "Can't Find Cash Journal Entries");
                }
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to get all cash journal entries");
            }

            return ret;
        }

        [HttpGet("getbyid", Name = "CashJournalGetByID")]
        public IActionResult Get(int id)
        {
            IActionResult ret = null;
            CashJournal entity = null;

            try
            {
                entity = Context.CashJournal.Find(id);
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
            catch (Exception ex)
            {
                ret = HandleException(ex,
                  "Exception trying to retrieve a single cash journal entry.");
            }

            return ret;
        }

        [HttpPost("/post", Name = "CashJournalPost")]
        public IActionResult Post([FromBody]CashJournalRequest request)
        {
            IActionResult ret = null;

            try
            {
                if (request != null && request.Data != null)
                {
                    Context.CashJournal.Add(request.Data);
                    Context.SaveChanges();
                    ret = StatusCode(StatusCodes.Status201Created, request.Data);
                }
                else
                {
                    ret = StatusCode(StatusCodes.Status400BadRequest, "Invalid data passed to create new cash journal entry");
                }
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to insert a new cash journal entry");
            }

            return ret;
        }

        [HttpPut("/put", Name = "CashJournalPut")]
        public IActionResult Put([FromBody]CashJournalRequest request)
        {
            IActionResult ret = null;

            try
            {
                if (request != null && request.Data != null)
                {
                    Context.Update(request.Data);
                    Context.SaveChanges();
                    ret = StatusCode(StatusCodes.Status200OK, request);
                }
                else
                {
                    ret = StatusCode(StatusCodes.Status400BadRequest, "Invalid data passed for cash journal update");
                }
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to update cash journal entry: " + request.Data.CashJournalId.ToString());
            }

            return ret;
        }
    }
}