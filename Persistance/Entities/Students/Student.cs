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

        protected Student()
        {
        }

        public Student(string name, string email)
            : this()
        {
            Name = name;
            Email = email;
        }

        public Enrollment GetEnrollment(int index)
        {
            if (Enrollments?.Count > index)
                return Enrollments[index];

            return null;
        }

        public virtual void RemoveEnrollment(Enrollment enrollment)
        {
            Enrollments.Remove(enrollment);
        }

        public virtual void AddDisenrollmentComment(Enrollment enrollment, string comment)
        {
            var disenrollment = new Disenrollment(enrollment.Student, enrollment.Course, comment);
            if (Disenrollments == null)
            {
                Disenrollments = new List<Disenrollment>();
            }
            Disenrollments.Add(disenrollment);
        }

        public virtual void Enroll(Course course, Grade grade)
        {
            if (Enrollments == null)
            {
                Enrollments = new List<Enrollment>();
            }
            if (Enrollments.Count >= 2)
                throw new Exception("Cannot have more than 2 enrollments");

            var enrollment = new Enrollment(this, course, grade);
            Enrollments.Add(enrollment);
        }
    }
}
