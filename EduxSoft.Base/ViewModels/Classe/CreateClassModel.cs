using EduxSoft.Base.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduxSoft.Base.ViewModels.Classe
{
    public class CreateClassModel
    {
        public string Name { get; set; }
        public string Grade { get; set; }
        public bool Tag { get; set; }
        public int SectionId { get; set; }
        public IEnumerable<SectionInfo> SectionsDrop { get; set; }
    }
}
