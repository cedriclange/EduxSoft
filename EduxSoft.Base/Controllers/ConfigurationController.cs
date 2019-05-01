using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoftinuxBase.Security.Common.Attributes;
using SoftinuxBase.Security.Common.Enums;

namespace EduxSoft.Base.Controllers
{
    [PermissionRequirement(Permission.Read)]
    public class ConfigurationController : SoftinuxBase.Infrastructure.ControllerBase
    {
        private readonly ILogger logger;
        public ConfigurationController(IStorage storage_, ILoggerFactory loggerFactory_ = null) : base(storage_, loggerFactory_)
        {
            logger = loggerFactory_.CreateLogger(GetType().FullName);
            logger.LogInformation("oups");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => View());
        }
    }
}
