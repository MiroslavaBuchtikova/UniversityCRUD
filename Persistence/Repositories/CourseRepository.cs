using University.Persistence.Context;
using University.Persistence.Entities.Students;
namespace University.Persistence.Repositories;
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

