using Bluestep.Service.SE.NaturalPerson.Persistence.Entities;
namespace University.Persistance.Entities.Students;
public class Enrollment : EntityBase
{
    public virtual Student Student { get; set; }
    public virtual Course Course { get; set; }
    public virtual Grade Grade { get; set; }
}
public enum Grade
{
    A = 1,
    B = 2,
    C = 3,
    D = 4,
    F = 5
}

