using System;
using System.Threading.Tasks;
using EduxSoft.Base.Data.Abstractions;
using EduxSoft.Base.Data.Entities;
using EduxSoft.Base.ViewModels.Configuration;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoftinuxBase.Security.Common.Attributes;
using SoftinuxBase.Security.Common.Enums;

namespace EduxSoft.Base.Controllers
{
    [PermissionRequirement(Permission.Admin)]
    public class ConfigurationController : SoftinuxBase.Infrastructure.ControllerBase
    {
        private readonly ILogger logger;
        //private readonly IStorage storage;
        public ConfigurationController(IStorage storage_, ILoggerFactory loggerFactory_ = null) : base(storage_, loggerFactory_)
        {
            //storage = storage_;
            logger = loggerFactory_.CreateLogger(GetType().FullName);
            logger.LogInformation("oups");
        }

        [HttpGet]
        public IActionResult Index()
        {
             var model = new IndexViewModel();
             var mydata = _storage.GetRepository<ISchoolRepository>().GetOne();
            if (mydata == null)
            {
                ViewBag.IsEmpty = true;
                return View();
            }
            else
            {
                ViewBag.IsEmpty = false;
                model.SchoolInfo = mydata;
                return View(model);
            }
                        
        }
        [Route("configuration/add-school")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return await Task.Run(() => View());
        }
        [Route("configuration/new-school")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSchool(CreateSchoolModel info)
        {
            if (ModelState.IsValid)
            {
                var schoolInfo = new SchoolInfo();
                schoolInfo.Name = info.Name;
                schoolInfo.Address = info.Address;
                schoolInfo.IsPrimary = info.IsPrimary;
                schoolInfo.IsSecondary = info.IsSecondary;
                schoolInfo.Created = DateTime.Now.Date;
                //saving data
                _storage.GetRepository<ISchoolRepository>().Add(schoolInfo);
                _storage.Save();
                return RedirectToAction("Index");
            }

            return View();
        }
        
    }
}
