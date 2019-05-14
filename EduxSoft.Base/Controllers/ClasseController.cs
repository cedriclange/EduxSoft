using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EduxSoft.Base.Data.Abstractions;
using EduxSoft.Base.ViewModels.Classe;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
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
    }
}
