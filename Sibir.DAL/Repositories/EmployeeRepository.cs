using Sibir.Domain.Models.ValueObject.ForEmployee;
using Sibir.Domain.Models.EntityObject;
using Microsoft.EntityFrameworkCore;
using CSharpFunctionalExtensions;
using Sibir.DAL.Repositories.Shared;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Sibir.DAL.Repositories
{
    public class EmployeeRepository(SqlServerContext context)
    {
        private readonly SqlServerContext _context = context;

        private readonly int PAGE_SIZE = 20;

        public async Task<Guid> Create(Employee newEmployee)
        {
            await _context.Employees.AddAsync(newEmployee);
            await _context.SaveChangesAsync();
            return newEmployee.Id;
        }

        public async Task<Maybe<Guid>> EditPersonalData(Guid id, Name name, Email email)
        {
            var CountRow = await _context.Employees
                .Where(e => e.Id == id)
                .ExecuteUpdateAsync(setter=>setter
                    .SetProperty(e=>e.Name,name)
                    .SetProperty(e=>e.Email,email));
            if (CountRow == 1)
                return id;
            else
                return Maybe.None;
        }

        public async Task<Maybe<Guid>> EditRole(Guid id, Role role)
        {
            var CountRow = await _context.Employees
                .Where(e => e.Id == id)
                .ExecuteUpdateAsync(setter=> setter.
                    SetProperty(e=>e.Role,role));
            if(CountRow == 1)
                return id;
            else
                return Maybe.None;
        }

        public async Task<(Employee, int)[]> GetAll(int Page)
        {
             return ( await _context.Employees
                .Select(e => new
                {
                    Employee = e,
                    TotalCount = _context.Employees.Count()
                })
                .Skip(Page*PAGE_SIZE)
                .Take(PAGE_SIZE)
                .ToArrayAsync())
                .AsEnumerable()
                .Select(res=>
                (
                    res.Employee,
                    res.TotalCount
                ))
                .ToArray();
        }

        public async Task<Maybe<Employee>> GetById(Guid id)
        {
            var Employee = await _context.Employees.FirstOrDefaultAsync(e=>e.Id == id);
            if (Employee == null)
                return Maybe.None;
            else
                return Employee;
        }

        public async Task<Maybe<Employee[]>> GetByProject(Guid ProjectId)
        {
            var DBResult = await _context.Projects
                .Where(p=>p.Id == ProjectId)
                .Select(p=> new
                {
                    p.Manager,
                    p.Executers
                })
                .FirstOrDefaultAsync();
            if (DBResult == null)
                return Maybe.None;

            if(DBResult.Manager != null)
                DBResult.Executers.Add(DBResult.Manager);

            return DBResult.Executers.ToArray();
        }

        public async Task<(Employee, int)[]> GetFilteredEmployees(int Page,
            string FirstName, 
            string SecondName, 
            string MiddleName, 
            string Role)
        {

            Expression<Func<Employee,bool>> predicate = e => true;
            if (FirstName != string.Empty)
                predicate = predicate.AndAlso(e=>
                    e.Name.FirstName.ToLower().Contains(FirstName.ToLower().Trim()));

            if (SecondName != string.Empty)
                predicate = predicate.AndAlso(e =>
                    e.Name.SecondName.ToLower().Contains(SecondName.ToLower().Trim()));

            if (MiddleName != string.Empty)
                predicate = predicate.AndAlso(e =>
                    e.Name.MiddleName.ToLower().Contains(MiddleName.ToLower().Trim()));

            if (Role != string.Empty)
                predicate = predicate.AndAlso(e =>
                    e.Role.Value.Contains(Role));

            return (await _context.Employees
                .Where(predicate)
                .Select(e => new
                {
                    Employee = e,
                    TotalCount =  _context.Employees.Where(predicate).Count()
                })
                .ToArrayAsync())
                .AsEnumerable()
                .Select(res=>
                (
                    res.Employee,
                    res.TotalCount
                ))
                .ToArray();
        }
    }
}
