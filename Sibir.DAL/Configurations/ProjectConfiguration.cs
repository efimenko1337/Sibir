using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models=Sibir.Domain.Models.EntityObject;
using Sibir.Domain.Models.ValueObject.ForProject;

namespace Sibir.DAL.Configurations
{
    internal class ProjectConfiguration : IEntityTypeConfiguration<Models.Project>
    {
        public void Configure(EntityTypeBuilder<Models.Project> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Id).
                HasDefaultValueSql("NEWID()");

            builder.
                HasOne(p => p.Manager).
                WithMany(e=>e.ManagedProjects).
                HasForeignKey(p=>p.ManagerId);

            builder.ComplexProperty(p => p.Title, builder =>
            {
                builder.Property(t => t.Value).
                       HasColumnName(nameof(Title)).
                       HasMaxLength(Title.MAX_LENGHT).
                       IsRequired();
            });

            builder.ComplexProperty(p => p.Company, builder =>
            {
                builder.Property(c=>c.Consumer).
                    HasMaxLength(Company.MAX_LENGHT).
                    IsRequired();

                builder.Property(c=>c.Executer).
                    HasMaxLength(Company.MAX_LENGHT).
                    IsRequired();
            });

            builder.ComplexProperty(p => p.DevelopmentTime, builder =>
            {
                builder.Property(dt => dt.StartDate).
                    HasColumnType("date").
                    IsRequired();

                builder.Property(dt => dt.FinishDate).
                    HasColumnType("date");
            });

            builder.ComplexProperty(t => t.Priority, builder =>
            {
                builder.Property(p => p.Value).
                    HasColumnName(nameof(Priority)).
                    IsRequired();
            });
        }
    }
}
