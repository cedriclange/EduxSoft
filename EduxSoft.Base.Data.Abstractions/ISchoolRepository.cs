using EduxSoft.Base.Data.Entities;
using ExtCore.Data.Abstractions;

namespace EduxSoft.Base.Data.Abstractions
{
    public interface ISchoolRepository: IRepository
    {
        void Add(SchoolInfo info);
        void Edit(SchoolInfo info);
        SchoolInfo GetOne();
    }
}
