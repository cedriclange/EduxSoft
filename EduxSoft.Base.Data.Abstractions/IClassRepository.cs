using EduxSoft.Base.Data.Entities;
using ExtCore.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EduxSoft.Base.Data.Abstractions
{
    public interface IClassRepository:IRepository
    {
        Task<IEnumerable<ClassInfo>> All();
        Task<IEnumerable<ClassInfo>> FromSection(int id);
        ClassInfo WithKey(int id);
        void Create(ClassInfo info);
        IEnumerable<ClassInfo> DropDownList();
    }
}
