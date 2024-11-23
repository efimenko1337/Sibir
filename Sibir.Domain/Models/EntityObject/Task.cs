using CSharpFunctionalExtensions;
using Sibir.Domain.Models.ValueObject.ForTask;

namespace Sibir.Domain.Models.EntityObject
{
    public class Task : Entity<Guid>
    {
        public Task() { }

        public Task(Guid id,
            Title title,
            Priority priority,
            Comment comment,
            Status status,
            Project project,
            Employee creater)
            : base(id)
        {
            Title = title;
            Priority = priority;
            Comment = comment;
            Status = status;
            Project = project;
            Creater = creater;
        }

        public Title Title { get; set; } =null!;
        public Priority Priority { get; set; } =null!;
        public Comment Comment { get; set; } = null!;
        public Status Status { get; set; }  = null!;

        public virtual Project Project { get; set; } =null!;
        public virtual Guid ProjectId { get; set; }

        public virtual Employee Creater { get; set; } = null!;
        public virtual Guid CreaterId { get; set; }

        public virtual Employee? Executer { get; set; }
        public virtual Guid? ExecuterId { get; set; }
    }
}