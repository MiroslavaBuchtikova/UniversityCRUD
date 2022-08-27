using Bluestep.Service.SE.NaturalPerson.Persistence.Entities;
namespace University.Persistence.Entities.Students;
public class Student : EntityBase
{
    public virtual string SSN { get; set; }
    public virtual string Name { get; set; }
    public virtual string Email { get; set; }

    public virtual IList<Enrollment> Enrollments { get; set; }
    public virtual IList<Disenrollment> Disenrollments { get; set; }
}

