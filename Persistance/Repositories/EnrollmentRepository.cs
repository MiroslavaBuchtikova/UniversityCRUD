using Microsoft.EntityFrameworkCore;
using University.Persistance.Context;
using University.Persistance.Entities.Students;

namespace University.Persistance.Repositories
{
    public sealed class EnrollmentRepository : GenericRepository<Enrollment>
    {
        public EnrollmentRepository(UniversityDbContext dbContext) : base(dbContext)
        {
        }

        public Enrollment Get(long studentId, int index)
        {
            var student = DbContext.Students?.SingleOrDefault(w => w.Id == studentId);
            if (student.Enrollments.Count > index)
                return student.Enrollments[index];

            return null;
        }

        public void Remove(Enrollment enrollment)
        {
            DbContext.Enrollments.Remove(enrollment);
            DbContext.SaveChanges();
        }

        public void Save(Student student, Course course, Grade grade)
        {
            if (student.Enrollments == null)
            {
                student.Enrollments = new List<Enrollment>();
            }
            if (student.Enrollments.Count >= 2)
                throw new Exception("Cannot have more than 2 enrollments");

            var enrollment = new Enrollment
            {
                Student = student,
                Course = course,
                Grade = grade
            };

            DbContext.Enrollments.Add(enrollment);
            DbContext.SaveChanges();
        }

        public void Update(Enrollment enrollment, Course course, Grade grade)
        {
           var enrollmentToUpdate =  DbContext.Enrollments?.SingleOrDefault(w => w.Id == enrollment.Id);
            enrollmentToUpdate.Course = course;
            enrollmentToUpdate.Grade = grade;

            DbContext.Enrollments.Update(enrollmentToUpdate);
            DbContext.SaveChanges();
        }
    }
}
