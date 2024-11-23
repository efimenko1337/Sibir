using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sibir.Domain.Models.EntityObject;
using Sibir.Domain.Models.ValueObject.ForEmployee;

namespace Sibir.DAL.Configurations
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).
                HasDefaultValueSql("NEWID()");

            builder.
                HasMany(e=>e.ExecutableProjects).
                WithMany(p=>p.Executers).
                UsingEntity("ExecutersProjects");

            builder.ComplexProperty(e => e.Name, builder =>
            {
                builder.Property(n=>n.FirstName).
                    IsRequired().
                    HasMaxLength(Name.MAX_LENGHT);

                builder.Property(n=>n.SecondName).
                    IsRequired().
                    HasMaxLength(Name.MAX_LENGHT);

                builder.Property(n=>n.MiddleName).
                    IsRequired().
                    HasMaxLength(Name.MAX_LENGHT);
            });

            builder.ComplexProperty(e=>e.Role,builder=>
            {
                builder.Property(r => r.Value).
                    HasColumnName(nameof(Role)).
                    HasMaxLength(15).
                    IsRequired();
            });

            builder.ComplexProperty(e => e.Email,builder =>
            {
                builder.Property(e => e.Value).
                    HasColumnName(nameof(Email)).
                    HasMaxLength(Email.MAX_LENGHT).
                    IsRequired();
            });
        }
    }
}
