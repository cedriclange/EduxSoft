using ExtCore.Data.Abstractions;
using System;
using EduxSoft.Student.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace EduxSoft.Student.Data.Abstractions
{
    public interface IStudentRepository : IRepository
    {
        void Add(StudentEntity model);
        void Edit(StudentEntity model);
        void Detele(StudentEntity model);
        Task<IEnumerable<StudentEntity>> All();
        IQueryable<StudentEntity> FromClass(int id);
        Task<List<StudentEntity>> AllByClassId(int id);
        Task<StudentEntity> WithKey(int id);
        Task<int> CountStudent();
        Task<StudentEntity> LastAdded();
    }
}
