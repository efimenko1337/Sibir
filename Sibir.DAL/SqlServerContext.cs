using Microsoft.EntityFrameworkCore;
using Sibir.Domain.Models.EntityObject;
using Task = Sibir.Domain.Models.EntityObject.Task;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Model = Sibir.Domain.Models.EntityObject;
using Sibir.DAL.Configurations;

namespace Sibir.DAL
{
    public class SqlServerContext(IConfiguration configuration) :DbContext
    {
        private readonly IConfiguration _configuration = configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.
                UseSqlServer(_configuration.GetConnectionString("SqlServer")).
                UseLoggerFactory(CreatingLoggerFactory()).
                EnableSensitiveDataLogging();
        }

        private static ILoggerFactory CreatingLoggerFactory() =>
            LoggerFactory.Create(builder => { builder.AddConsole(); });

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Model.Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new TaskConfiguration());
        }
    }
}
