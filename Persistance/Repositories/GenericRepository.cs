
using University.Persistance.Context;

namespace University.Persistance.Repositories
{
    public abstract class GenericRepository<T>
    {
        protected UniversityDbContext DbContext { get; }

        public GenericRepository(UniversityDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}