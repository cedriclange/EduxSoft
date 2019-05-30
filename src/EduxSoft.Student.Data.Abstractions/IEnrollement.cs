using EduxSoft.Student.Data.Entities;
using ExtCore.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduxSoft.Student.Data.Abstractions
{
    public interface IEnrollement :IRepository
    {
        void Enroll(Enrollement model);
    }
}
