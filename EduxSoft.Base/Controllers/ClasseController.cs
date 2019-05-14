using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EduxSoft.Base.Data.Abstractions;
using EduxSoft.Base.Data.Entities;
using EduxSoft.Base.ViewModels.Classe;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace EduxSoft.Base.Controllers
{
    public class ClasseController : SoftinuxBase.Infrastructure.ControllerBase
    {
        public ClasseController(IStorage storage_, ILoggerFactory loggerFactory_ = null) : base(storage_, loggerFactory_)
        {
        }
        [Route("configuration/classe")]
        [HttpGet]
        public async Task<IActionResult> Index( int? sectionId)
        {
            var model = new IndexClassModel();
            if (sectionId != null)
            {
                model.Classes = await _storage.GetRepository<IClassRepository>().FromSection(sectionId.Value);
                
                return View(model);
            }

            model.Classes = await _storage.GetRepository<IClassRepository>().All();
            return View(model);
        }

        [Route("configuration/classe/add")]
        public IActionResult Create()
        {
            var model = new CreateClassModel();
            model.SectionsDrop = _storage.GetRepository<ISectionRepository>().DropDownList();
            return View(model);
        }

       [HttpPost]
       [Route("configuration/classe/add-new")]
       public IActionResult AddClass(CreateClassModel model)
       {
            var data = new ClassInfo();
            data.Grade = model.Grade;
            data.Name = model.Name;
            data.SectionId = model.SectionId;
            if (model.Tag == true)
            {
                data.IsPrimary = false;
                data.IsSecondary = true;
            }
            else
            {
                data.IsPrimary = true;
                data.IsSecondary = false;
            }
            _storage.GetRepository<IClassRepository>().Create(data);
            _storage.Save();
            return RedirectToAction("Index");
       }
    }
}
