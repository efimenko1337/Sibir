using CSharpFunctionalExtensions;
using Sibir.Domain.Models.ValueObject.ForEmployee;

namespace Sibir.Domain.Models.EntityObject
{
    public class Employee : Entity<Guid>
    {
        public Employee() { }

        public Employee(Guid id,
            Name name,
            Email email,
            Role role)
            : base(id)
        {
            Name = name;
            Email = email;
            Role = role;
        }

        public Name Name { get; set; } = null!;
        public Email Email { get; set; } = null!;
        public Role Role { get; set; } = null!;

        public ICollection<Task> ExecutableTasks { get; set; } = [];
        public ICollection<Task> CreatedTasks { get; set; } = [];
        public ICollection<Project> ExecutableProjects { get; set; } = [];
        public ICollection<Project> ManagedProjects { get; set; } = [];
    }
}