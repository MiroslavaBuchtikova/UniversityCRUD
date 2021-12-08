using Microsoft.EntityFrameworkCore;
using University.Persistance.Context;
using University.Persistance.Entities.Students;

namespace University.Persistance.Repositories
{
    public sealed class DisenrollmentRepository : GenericRepository<Disenrollment>
    {
        public DisenrollmentRepository(UniversityDbContext dbContext) : base(dbContext)
        {
        }

        public void Add(Disenrollment disenrollment)
        {
            DbContext.Disenrollments.Add(disenrollment);
            DbContext.SaveChanges();
        }
        public List<Disenrollment> GetList()
        {
            return DbContext.Disenrollments.ToList();
        }
        public Disenrollment Get(long disenrollmentId)
        {
            return DbContext.Disenrollments.SingleOrDefault(w=>w.Id == disenrollmentId).ToList();
        }
        public void Update(Disenrollment disenrollment)
        {
            DbContext.Disenrollments.Update(disenrollment);
            DbContext.SaveChanges();
        }
        public void Remove(long id, long courseId)
        {
            var disenrollment = DbContext.Disenrollments.SingleOrDefault(s=>s.Student.Id == id && s.Course.Id == courseId);
            DbContext.Remove(disenrollment);
            DbContext.SaveChanges();
        }
    }
}
