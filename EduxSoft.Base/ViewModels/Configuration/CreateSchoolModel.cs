using System;
using System.Collections.Generic;
using System.Text;

namespace EduxSoft.Base.ViewModels.Configuration
{
    public class CreateSchoolModel
    {
        public string Name { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsSecondary { get; set; }
        public string Address { get; set; }

    }
}
