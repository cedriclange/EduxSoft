using System;
using System.Collections;
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
        [Route("/student")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new IndexViewModel();
            model.Student = await _storage.GetRepository<IStudentRepository>().LastAdded();
            return View(model);
        }

        [Route("/student/enrollement")]
        public IActionResult Create()
        {
            var model = new CreateStudentModel();
            model.ClassDrops = _storage.GetRepository<IClassRepository>().DropDownList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        [Route("/student/list")]
        [HttpGet]
        public async Task<IActionResult> ListStudent()
        {
            //ViewData["NameParam"] = string.IsNullOrEmpty(SortOrder) ? "name_desc" : "";
            //ViewData["sectionParam"] = string.IsNullOrEmpty(SortOrder) ? "section_desc" : "";
            //ViewData["classParam"] = string.IsNullOrEmpty(SortOrder) ? "class_desc" : "";

            var model = new ListViewModel();
            model.Students = await this._storage.GetRepository<IStudentRepository>().All();
            model.ClassDrops = this._storage.GetRepository<IClassRepository>().DropDownList();
            model.sectionDrops = this._storage.GetRepository<ISectionRepository>().DropDownList();
            return await Task.Run(() => View(model));
        }
        
    }
}
