using University.Persistance.Context;
using University.Persistance.Entities.Students;

namespace University.Persistance.Repositories
{
    public sealed class StudentRepository : GenericRepository<Student>
    {
        public StudentRepository(UniversityDbContext dbContext) : base(dbContext)
        {
        }

        public Student GetById(long id)
        {
            return DbContext.Students?.FirstOrDefault(w => w.Id == id);
        }

        public IReadOnlyList<Student> GetList(string enrolledIn)
        {
            var result = DbContext.Students.ToList();

            if (!string.IsNullOrWhiteSpace(enrolledIn))
            {
                result = result.Where(x => x.Enrollments.Any(e => e.Course.Name == enrolledIn)).ToList();
            }


            return result;
        }

        public void Save(Student student)
        {
            DbContext.Update(student);
            DbContext.SaveChanges();
        }

        public void Delete(Student student)
        {
            DbContext.Remove(student);
            DbContext.SaveChanges();
        }
    }
}
