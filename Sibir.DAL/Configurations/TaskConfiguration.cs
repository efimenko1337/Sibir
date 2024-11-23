using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Sibir.Domain.Models.EntityObject.Task;
using Sibir.Domain.Models.ValueObject.ForTask;

namespace Sibir.DAL.Configurations
{
    internal class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).
                HasDefaultValueSql("NEWID()");

            builder.
                HasOne(t => t.Project).
                WithMany(p => p.Tasks).
                HasForeignKey(t=>t.ProjectId).
                IsRequired();

            builder.
                HasOne(t => t.Creater).
                WithMany(e => e.CreatedTasks).
                HasForeignKey(t=>t.CreaterId).
                IsRequired(); ;

            builder.
                HasOne(t => t.Executer).
                WithMany(e => e.ExecutableTasks).
                HasForeignKey(t=>t.ExecuterId);

            builder.ComplexProperty(t=>t.Title, builder=>
            {
                builder.Property(t => t.Value).
                    HasColumnName(nameof(Title)).
                    HasMaxLength(Title.MAX_LENGHT).
                    IsRequired();
            });

            builder.ComplexProperty(t => t.Priority, builder =>
            {
                builder.Property(p => p.Value).
                    HasColumnName(nameof(Priority)).
                    IsRequired();
            });

            builder.ComplexProperty(t => t.Comment, builder => 
            {
                builder.Property(c => c!.Value).
                    HasColumnName(nameof(Comment)).
                    HasMaxLength(Comment.MAX_LENGHT).
                    IsRequired(false);
            });

            builder.ComplexProperty(t => t.Status, builder =>
            {
                builder.Property(s => s.Value).
                    HasColumnName(nameof(Status)).
                    HasMaxLength(15).
                    IsRequired();
            });
        }
    }
}
