using System;
using System.Collections.Generic;
using System.Text;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoftinuxBase.Security.Common.Attributes;
using SoftinuxBase.Security.Common.Enums;

namespace EduxSoft.Student.Controllers
{
    [PermissionRequirement(Permission.Admin)]
    public class StudentController : SoftinuxBase.Infrastructure.ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StudentController"/> class.
        /// </summary>
        /// <param name="storage_"></param>
        /// <param name="loggerFactory_"></param>
        public StudentController(IStorage storage_, ILoggerFactory loggerFactory_ = null) : base(storage_, loggerFactory_)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
