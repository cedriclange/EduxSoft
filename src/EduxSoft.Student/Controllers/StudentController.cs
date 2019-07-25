using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EduxSoft.Base.Data.Abstractions;
using EduxSoft.Student.Data.Abstractions;
using EduxSoft.Student.Data.Entities;
using EduxSoft.Student.ViewModels;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoftinuxBase.Security.Common.Attributes;
using SoftinuxBase.Security.Common.Enums;

namespace EduxSoft.Student.Controllers
{
    [PermissionRequirement(Permission.Write)]
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

        [Route("/student/enrollement")]
        public IActionResult Create()
        {
            var model = new CreateStudentModel();
            model.ClassDrops = _storage.GetRepository<IClassRepository>().DropDownList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Enroll(CreateStudentModel data)
        {
            if (ModelState.IsValid)
            {
                var model = new StudentEntity();
                model.Surname = data.Surname;
                model.MiddleName = data.MiddleName;
                model.FirstName = data.FirstName;
                model.Address = data.Address;
                model.DateOfBirth = data.DateOfBirth;
                model.StudentNumber = data.StudentNumber;
                model.Created = DateTime.Now;
                _storage.GetRepository<IStudentRepository>().Add(model);
                await _storage.SaveAsync();

                var enroll = new Enrollement();
                enroll.ClassId = data.ClassID;
                enroll.StudentId = model.Id;
                enroll.Created = DateTime.Now;

                _storage.GetRepository<IEnrollement>().Enroll(enroll);
                await _storage.SaveAsync();

                return RedirectToAction("Index");
            }

            return View(data);
        }      
    }
}
