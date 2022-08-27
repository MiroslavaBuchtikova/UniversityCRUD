using Microsoft.EntityFrameworkCore;
using University.Persistence.Context;
using University.Persistence.Entities.Students;
namespace University.Persistence.Repositories;
public sealed class StudentRepository : GenericRepository<Student>
{
    public StudentRepository(UniversityDbContext dbContext) : base(dbContext)
    {
    }

    public Student GetBySSN(string ssn)
    {
        return DbContext.Students
            .Include(x => x.Enrollments)
            .ThenInclude(x => x.Course)
            .Include(x => x.Disenrollments)
            .FirstOrDefault(w => w.SSN == ssn);
    }

    public IReadOnlyList<Student> GetList(string courseName)
    {
        var result = DbContext.Students
            .Include(x => x.Enrollments)
            .ThenInclude(x => x.Course)
            .Include(x => x.Disenrollments)
            .ToList();

        if (!string.IsNullOrWhiteSpace(courseName))
        {
            result = result.Where(x => x.Enrollments.Any(e => e.Course.Name == courseName)).ToList();
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

