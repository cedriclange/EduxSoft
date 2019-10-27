using EduxSoft.Base.Data.Entities;
using EduxSoft.Student.Data.Abstractions;
using EduxSoft.Student.Data.Entities;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace EduxSoft.Student.Data.EF
{
    public class StudentRepository : RepositoryBase<StudentEntity>, IStudentRepository
    {
        public void Add(StudentEntity model)
        {
            dbSet.Add(model);
        }

        public async Task<IEnumerable<StudentEntity>> All()
        {
            return await dbSet.OrderBy(o=>o.Surname)
                                .ToListAsync();
            
        }

        public IQueryable<StudentEntity> FromClass(int id)
        {
            var data = from s in dbSet
                       join en in storageContext.Set<Enrollement>()
                       on s.Id equals en.StudentId
                       where en.ClassId == id
                       orderby s.Surname
                       select s;
            return data;
        }
        public async Task<List<StudentEntity>> AllByClassId(int id)
        {
            var model = FromClass(id);
            return await model.ToListAsync();
        }
       
        public async Task<int> CountStudent()
        {
            return await dbSet.CountAsync();
        }

        public void Detele(StudentEntity model)
        {
            
            dbSet.Remove(model);
        }

        public void Edit(StudentEntity model)
        {
            dbSet.Update(model);
        }

        public async Task<StudentEntity> WithKey(int id)
        {
            return await dbSet.FirstOrDefaultAsync(c=>c.Id == id);
        }
        public async Task<StudentEntity> LastAdded(){

            return await dbSet.LastOrDefaultAsync();
        }
    }
}
