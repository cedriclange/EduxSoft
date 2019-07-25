using EduxSoft.Base.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduxSoft.Student.ViewModels
{
    public class CreateStudentModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string StudentNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public int ClassID { get; set; }
        public IEnumerable<ClassInfo> ClassDrops { get; set; }
    }
}
