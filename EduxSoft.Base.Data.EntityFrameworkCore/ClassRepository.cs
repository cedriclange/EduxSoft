using EduxSoft.Base.Data.Abstractions;
using EduxSoft.Base.Data.Entities;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace EduxSoft.Base.Data.EntityFrameworkCore
{
    public class ClassRepository : RepositoryBase<ClassInfo>, IClassRepository
    {
        public async Task<IEnumerable<ClassInfo>> All()
        {
            return await dbSet.ToListAsync();
        }

        public void Create(ClassInfo info)
        {
            dbSet.Add(info);
        }

        public async Task<IEnumerable<ClassInfo>> FromSection(int id)
        {
            return await dbSet.Where(c => c.SectionId == id)
                .OrderBy(o => o.Grade).ToListAsync();
        }

        public ClassInfo WithKey(int id)
        {
            return dbSet.FirstOrDefault(c => c.Id == id);
        }
    }
}
