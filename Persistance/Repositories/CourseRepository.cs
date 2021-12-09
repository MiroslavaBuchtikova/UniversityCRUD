using University.Persistance.Context;
using University.Persistance.Entities.Students;
namespace University.Persistance.Repositories;
public sealed class CourseRepository : GenericRepository<Student>
{
    public CourseRepository(UniversityDbContext dbContext) : base(dbContext)
    {
    }

    public Course GetByName(string name)
    {
        return DbContext.Courses
            .SingleOrDefault(x => x.Name == name);
    }
}

