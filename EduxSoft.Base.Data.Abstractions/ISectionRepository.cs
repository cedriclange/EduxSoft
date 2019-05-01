using EduxSoft.Base.Data.Entities;
using ExtCore.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduxSoft.Base.Data.Abstractions
{
    public interface ISectionRepository :IRepository
    {
        Task<IEnumerable<SectionInfo>> All();
        Task<SectionInfo> WithKey(int id);
        void Create(SectionInfo section);
        void Edit(SectionInfo section);
    }
}
