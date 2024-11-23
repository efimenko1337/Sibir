using CSharpFunctionalExtensions;
using Sibir.Domain.Models.ValueObject.ForProject;

namespace Sibir.Domain.Models.EntityObject
{
    public class Project : Entity<Guid>
    {
        public Project() { }

        public Project(Guid id,
            Title title,
            Company company,
            DevelopmentTime developmentTime,
            Priority priority,
            Employee? manager)
            : base(id)
        {
            Title = title;
            Company = company;
            DevelopmentTime = developmentTime;
            Priority = priority;
            Manager = manager;
        }

        public Title Title { get; set; } = null!;
        public Company Company { get; set; } = null!;
        public DevelopmentTime DevelopmentTime { get; set; } = null!;
        public Priority Priority { get; set; } = null!;
        
        public virtual Employee? Manager { get; set; }
        public virtual Guid? ManagerId { get; set; }

        public virtual ICollection<Employee> Executers { get; set; } = [];
        public virtual ICollection<Task> Tasks { get; set; } = [];
    }
}
