using System.Threading.Tasks;
using EduxSoft.Base.Data.Abstractions;
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
        
    }
}
