using EduxSoft.Student.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduxSoft.Student.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<StudentEntity> Students { get; set; }
    }
}
