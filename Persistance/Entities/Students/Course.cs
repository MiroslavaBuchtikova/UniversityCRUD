using Bluestep.Service.SE.NaturalPerson.Persistence.Entities;

namespace University.Persistance.Entities.Students
{
    public class Course : EntityBase
    {
        public virtual string Name { get; protected set; }
        public virtual int Credits { get; protected set; }
    }
}
