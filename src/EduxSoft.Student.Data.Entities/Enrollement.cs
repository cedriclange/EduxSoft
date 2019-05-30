using EduxSoft.Base.Data.Entities;
using ExtCore.Data.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduxSoft.Student.Data.Entities
{
    public class Enrollement : IEntity
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public DateTime Created { get; set; }
    }
}
