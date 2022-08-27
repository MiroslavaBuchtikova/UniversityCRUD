
using University.Persistence.Context;
namespace University.Persistence.Repositories;

public abstract class GenericRepository<T>
{
    protected UniversityDbContext DbContext { get; }

    public GenericRepository(UniversityDbContext dbContext)
    {
        DbContext = dbContext;
    }
}
