using EduxSoft.Student.Data.Entities;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduxSoft.Student.Data.EF
{
    public class EntityRegistar : IEntityRegistrar
    {
        public void RegisterEntities(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<StudentEntity>(etb =>
            {
                etb.HasKey(e => e.Id);
                etb.Property(e => e.Id).ValueGeneratedOnAdd();
                etb.Property(e => e.FirstName).IsRequired();
                etb.Property(e => e.Surname).IsRequired();
                etb.Property(e=> e.DateOfBirth).IsRequired();
            });
            modelbuilder.Entity<Enrollement>(etb =>
            {
                etb.HasKey(e => e.Id);
                etb.Property(e => e.Id).ValueGeneratedOnAdd();
            });

        }
    }
}
