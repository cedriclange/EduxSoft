using EduxSoft.Student.Data.Abstractions;
using EduxSoft.Student.Data.Entities;
using ExtCore.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduxSoft.Student.Data.EF
{
    public class EnrollRepository : RepositoryBase<Enrollement>, IEnrollement
    {
        public void Enroll(Enrollement model)
        {
            dbSet.Add(model);
        }
    }
}
