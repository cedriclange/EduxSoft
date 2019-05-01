using EduxSoft.Base.Data.Abstractions;
using EduxSoft.Base.Data.Entities;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduxSoft.Base.Data.EntityFrameworkCore
{
    public class SchoolRepository : RepositoryBase<SchoolInfo>, ISchoolRepository
    {
        public void Add(SchoolInfo info)
        {
            dbSet.Add(info);
        }

        public void Edit(SchoolInfo info)
        {
            storageContext.Entry(info).State = EntityState.Modified;
        }

        public SchoolInfo WithKey(int id)
        {
            return dbSet.Find(id);
        }
    }
}
