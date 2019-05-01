using EduxSoft.Base.Data.Entities;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduxSoft.Base.Data.EntityFrameworkCore
{
    public class EntityRegistar : IEntityRegistrar
    {
        public void RegisterEntities(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<SchoolInfo>(etb =>
            {
                etb.ToTable("tbl_SchoolInfo");
                etb.HasKey(e => e.Id);
                etb.Property(e => e.Id).ValueGeneratedOnAdd();
                etb.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(50)");
                etb.Property(e => e.Address)
                .HasColumnType("varchar(100)");

            }
            );
            modelbuilder.Entity<SectionInfo>(etb =>
            {
                etb.ToTable("tbl_SectionInfo");
                etb.HasKey(e => e.Id);
                etb.Property(e => e.Id).ValueGeneratedOnAdd();
                etb.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(50)");

                etb.HasMany(e => e.Classes)
                .WithOne(o => o.Section)
                .HasForeignKey(f => f.SectionId);
            });
            modelbuilder.Entity<ClassInfo>(etb =>
            {
                etb.ToTable("ClassInfos");
                etb.HasKey(e => e.Id);
                etb.Property(e => e.Id).ValueGeneratedOnAdd();

            });
        }
    }
}
