using CSharpFunctionalExtensions;
using Sibir.Domain.Models.EntityObject;
using Sibir.Domain.Models.ValueObject.ForEmployee;

namespace Sibir.Domain.Abstraction
{
    public interface IEmployeeRepository
    {
        Task<Guid> Create(Employee newEmployee);
        Task<Maybe<Guid>> EditPersonalData(Guid id, Name name, Email email);
        Task<Maybe<Guid>> EditRole(Guid id, Role role);
        Task<(Employee, int)[]> GetAll(int Page);
        Task<Maybe<Employee>> GetById(Guid id);
        Task<Maybe<Employee[]>> GetByProject(Guid ProjectId);
        Task<(Employee, int)[]> GetFilteredEmployees(int Page, string FirstName, string SecondName, string MiddleName, string Role);
    }
}