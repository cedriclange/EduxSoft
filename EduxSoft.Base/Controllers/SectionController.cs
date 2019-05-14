using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EduxSoft.Base.Data.Abstractions;
using EduxSoft.Base.Data.Entities;
using EduxSoft.Base.ViewModels.Classe;
using EduxSoft.Base.ViewModels.Section;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EduxSoft.Base.Controllers
{
    public class SectionController : SoftinuxBase.Infrastructure.ControllerBase
    {
        public SectionController(IStorage storage_, ILoggerFactory loggerFactory_ = null) : base(storage_, loggerFactory_)
        {
        }
        [Route("configuration/section")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new IndexSectionModel();
            model.Sections = await _storage.GetRepository<ISectionRepository>().All();
            return View(model);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetClasse(int sectionId)
        {
            var model = new IndexClassModel();
            model.Classes = await _storage.GetRepository<IClassRepository>().FromSection(sectionId);
            return PartialView(model);
        }

        [Route("configuration/section/add-section")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSection(CreateSectionModel model)
        {
            if (ModelState.IsValid)
            {
                var section = new SectionInfo();
                section.Name = model.Name;
                section.Created = DateTime.Now;
                _storage.GetRepository<ISectionRepository>().Create(section);
                _storage.Save();
                return RedirectToAction("Index");
            }
            return PartialView(model);
        }

    }
}
