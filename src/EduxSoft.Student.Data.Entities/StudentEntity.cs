using ExtCore.Data.Entities.Abstractions;
using System;

namespace EduxSoft.Student.Data.Entities
{
    public class StudentEntity : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string StudentNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public DateTime Created { get; set; }

    }
}
