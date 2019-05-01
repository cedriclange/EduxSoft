using ExtCore.Data.Entities.Abstractions;
using System;

namespace EduxSoft.Base.Data.Entities
{
    public class SchoolInfo : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsSecondary { get; set; }
        public DateTime Created { get; set; }
    }
}
