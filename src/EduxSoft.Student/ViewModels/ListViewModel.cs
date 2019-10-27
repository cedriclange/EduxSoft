using EduxSoft.Base.Data.Entities;
using EduxSoft.Student.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduxSoft.Student.ViewModels
{
    public class ListViewModel
    {
        public IEnumerable<StudentEntity> Students { get; set; }
        public IEnumerable<ClassInfo> ClassDrops { get; set; }
        public IEnumerable<SectionInfo> sectionDrops { get; set; }
    }
}
