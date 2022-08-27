using System.Reflection;
using Microsoft.EntityFrameworkCore;
using University.Persistence.Entities.Students;

namespace University.Persistence.Context
{
    public class UniversityDbContext : DbContext
    {
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Disenrollment> Disenrollments { get; set; }
        public UniversityDbContext(DbContextOptions options) :
            base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}