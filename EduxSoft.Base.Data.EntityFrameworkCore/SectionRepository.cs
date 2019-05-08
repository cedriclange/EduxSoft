using EduxSoft.Base.Data.Abstractions;
using EduxSoft.Base.Data.Entities;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduxSoft.Base.Data.EntityFrameworkCore
{
    public class SectionRepository : RepositoryBase<SectionInfo>, ISectionRepository
    {
        public async Task<IEnumerable<SectionInfo>> All()
        {
            return await dbSet.Include(e=>e.Classes).ToListAsync();
        }

        public void Create(SectionInfo section)
        {
            dbSet.Add(section);
        }

        public void Edit(SectionInfo section)
        {
            storageContext.Entry(section).State = EntityState.Modified;
        }

        public async Task<SectionInfo> WithKey(int id)
        {
            return await dbSet.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
