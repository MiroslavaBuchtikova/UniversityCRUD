using Bluestep.Service.SE.NaturalPerson.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace University.Persistance.Entities.Students
{
    public class Student : EntityBase
    {
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }

        public virtual IList<Enrollment> Enrollments { get; set; }
        public virtual IList<Disenrollment> Disenrollments { get; set; }
    }
}
