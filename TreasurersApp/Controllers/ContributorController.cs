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
                    }
                    ret = StatusCode(StatusCodes.Status200OK, list);
                }
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to get all Contributor");
            }

            return ret;
        }

        [HttpGet("getbyid/{id}", Name = "ContributorGetById")]
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
                        ret = StatusCode(StatusCodes.Status404NotFound, "Can't Find Contributor: " + id.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to retrieve a single Contributor.");
            }

            return ret;
        }

        [HttpGet("getvm", Name = "ContributorGetVM")]
        public IActionResult GetVM()
        {
            IActionResult ret = null;
            List<ContributorViewModel> list = new List<ContributorViewModel>();

            try
            {
                using (var db = new BTAContext())
                {
                    if (db.Contributor.Count() > 0)
                    {
                        list = (from cnt in db.Contributor
                                orderby cnt.LastName, cnt.FirstName, cnt.MiddleName
                                select new ContributorViewModel()
                                {
                                    ContributorId = cnt.ContributorId,
                                    FirstName = cnt.FirstName,
                                    MiddleName = cnt.MiddleName,
                                    LastName = cnt.LastName,
                                    AddressId = cnt.ContributorAddresses.Count == 0 ?
                                        (int?)null :
                                        cnt.ContributorAddresses.OrderBy(x => x.Preferred).First().Address.AddressId,
                                    AddressText = cnt.ContributorAddresses.Count > 0 ?
                                      string.Format("{0}, {1}, {2} {3}",
                                        cnt.ContributorAddresses.OrderByDescending(x => x.Preferred).First().Address.AddressLine1,
                                        cnt.ContributorAddresses.OrderByDescending(x => x.Preferred).First().Address.City,
                                        cnt.ContributorAddresses.OrderByDescending(x => x.Preferred).First().Address.State,
                                        cnt.ContributorAddresses.OrderByDescending(x => x.Preferred).First().Address.PostalCode) :
                                      null,
                                    EmailAddressId = cnt.ContributorEmailAddresses.Count == 0 ? (int?)null :
                                        cnt.ContributorEmailAddresses.OrderByDescending(x => x.Preferred).First().EmailAddress.EmailAddressId,
                                    EmailAddress = cnt.ContributorEmailAddresses.Count == 0 ? null :
                                        cnt.ContributorEmailAddresses.OrderByDescending(x => x.Preferred).First().EmailAddress.Email,
                                    PhoneNumberId = cnt.ContributorPhoneNumbers.Count == 0 ? (int?)null :
                                        cnt.ContributorPhoneNumbers.OrderByDescending(x => x.Preferred).First().PhoneNumber.PhoneNumberId,
                                    PhoneNumber = cnt.ContributorPhoneNumbers.Count == 0 ? null :
                                        cnt.ContributorPhoneNumbers.OrderByDescending(x => x.Preferred).First().PhoneNumber.PhoneNumber_,
                                }).ToList();
                    }
                    ret = StatusCode(StatusCodes.Status200OK, list);
                }
            }
            catch (Exception ex)
            {
                ret = HandleException(ex, "Exception trying to get all Contributor");
            }

            return ret;
        }

        [HttpGet("getvmbyid/{id}", Name = "ContributorGetVMById")]
        public IActionResult GetVM(int id)
        {
            IActionResult ret = null;
            Contributor entity = null;
            ContributorViewModel viewModel = null;

            try
            {
                using (var db = new BTAContext())
                {
                    entity = db.Contributor.SingleOrDefault(x => x.ContributorId == id);
                    if (entity != null)
                    {
                        viewModel = new ContributorViewModel(entity);
                        ret = StatusCode(StatusCodes.Status200OK, viewModel);
                    }
                    else
                    {
                        ret = StatusCode(StatusCodes.Status404NotFound, "Can't Find Contributor: " + id.ToString());
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
        public IActionResult Post([FromBody]ContributorRequest request)
        {
            var returnResult = new ContributorActionResult(false, new List<string>(), null);
            Guid userGuid = GetUserGuidFromUserName(request.UserName);
            if (request != null)
            {
                try
                {
                    using (var db = new BTAContext())
                    {
                        DateTime now = DateTime.Now;
                        var contrib = new Contributor()
                        {
                            FirstName = request.Data.FirstName,
                            MiddleName = request.Data.MiddleName,
                            LastName = request.Data.LastName,
                            BahaiId = request.Data.BahaiId,
                            CreatedBy = userGuid,
                            CreatedDate = now,
                            LastModifiedBy = userGuid,
                            LastModifiedDate = now
                        };
                        var resultContributor = db.Contributor.Add(contrib);
                        db.SaveChanges();
                        var entity = resultContributor.Entity;
                        if (entity != null)
                        {
                            returnResult.Success = true;
                            returnResult.StatusMessages.Add("Successfully added contributor.");
                            returnResult.Data = entity;
                        }
                        else
                        {
                            returnResult.Success = false;
                            returnResult.StatusMessages.Add("Failed to add contributor.");
                            returnResult.Data = entity;
                        }
                    }
                }
                catch (Exception e)
                {
                    string errMsg = "An exception occurred while attempting to add a contributor.";
                    Logger.LogError(e, errMsg);
                    returnResult.Success = false;
                    returnResult.StatusMessages.Add(errMsg);
                    returnResult.Data = null;
                }
            }
            else
            {
                returnResult.Success = false;
                returnResult.StatusMessages.Add("Empty contributor posted for add.");
                returnResult.Data = null;
            }
            return StatusCode(StatusCodes.Status200OK, returnResult);
        }

        [HttpPut("put", Name = "ContributorPut")]
        public IActionResult Put([FromBody]ContributorRequest request)
        {
            var returnResult = new ContributorActionResult(false, new List<string>(), null);
            if (request != null)
            {
                try
                {
                    using (var db = new BTAContext())
                    {
                        var resultContributor = db.Contributor.SingleOrDefault(x => x.ContributorId == request.Data.ContributorId);
                        if (resultContributor != null)
                        {
                            resultContributor.FirstName = request.Data.FirstName;
                            resultContributor.MiddleName = request.Data.MiddleName;
                            resultContributor.LastName = request.Data.LastName;
                            db.SaveChanges();
                            returnResult.Success = true;
                            returnResult.Data = resultContributor;
                            returnResult.StatusMessages.Add("Successfully updated contributor.");
                        }
                        else
                        {
                            string errMsg = string.Format("Unable to locate contributor for id: {0}", request.Data.ContributorId);
                            Logger.LogError(errMsg, null);
                            returnResult.Success = false;
                            returnResult.StatusMessages.Add(errMsg);
                            returnResult.Data = null;
                        }
                    }
                }
                catch (Exception e)
                {
                    string excMsg = "An exception occurred while attempting to update a contributor.";
                    Logger.LogError(e, excMsg);
                    returnResult.Success = false;
                    returnResult.StatusMessages.Add(excMsg);
                    returnResult.Data = null;
                }
            }
            else
            {
                returnResult.Success = false;
                returnResult.StatusMessages.Add("Empty contributor posted for update.");
                returnResult.Data = null;
            }
            return StatusCode(StatusCodes.Status200OK, returnResult);
        }

        [HttpDelete("delete", Name = "ContributorDelete")]
        public IActionResult Delete(int id)
        {
            var returnResult = new ContributorActionResult(false, new List<string>(), null);
            try
            {
                using (var db = new BTAContext())
                {
                    if (db.Contributor.Any(x => x.ContributorId == id) == false)
                    {
                        returnResult.Success = false;
                        returnResult.StatusMessages.Add(string.Format("Unable to locate contributor for id: {0}", id));
                        returnResult.Data = null;
                    }
                    else
                    {
                        var resultContributor = db.Contributor.Single(x => x.ContributorId == id);
                        db.Remove(resultContributor);
                        db.SaveChanges();
                        returnResult.Success = true;
                        returnResult.Data = resultContributor;
                        returnResult.StatusMessages.Add("Successfully deleted contributor.");
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
                returnResult.Success = false;
                returnResult.StatusMessages.Add("An exception occurred while attempting to delete the contributor.");
                returnResult.Data = null;
            }
            return StatusCode(StatusCodes.Status200OK, returnResult);
        }
    }
}