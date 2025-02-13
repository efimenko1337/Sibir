using CSharpFunctionalExtensions;
using Microsoft.Identity.Client;
using Sibir.BL.Mappers;
using Sibir.Domain.Abstraction;
using Sibir.Domain.Shared;
using Sibir.Domain.Shared.ViewModels;

namespace Sibir.BL.Services
{
    public class CRUREmployeeService(IEmployeeRepository repository) : ICRUREmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository = repository;

        public async Task<Result<Guid, Error>> Create(EmployeeCreateViewModel viewModel)
        {
            var Employee = EmployeeMapper.ViewModelToDoamin(viewModel);

            if (Employee.IsFailure)
                return Employee.Error;

            return await _employeeRepository.Create(Employee.Value);
        }

        public async Task<Result<Guid, Error>> EditEmployeePersonalData(Guid Id,
            EmployeeNameViewModel nameViewModel,
            EmailViewModel emailViewModel)
        {
            var newName = NameMapper.ViewModelToDomain(nameViewModel);
            var newEmail = EmailMapper.ViewModelToDomain(emailViewModel);

            if (newName.IsFailure)
                return newName.Error;
            if (newEmail.IsFailure)
                return newEmail.Error;

            var DbResult = await _employeeRepository.EditPersonalData(Id, newName.Value, newEmail.Value);

            if (DbResult.HasNoValue)
                return new Error("404", "Employee not found");

            return DbResult.Value;
        }

        public async Task<Result<Guid, Error>> EditRole(Guid id, RoleViewModel roleViewModel)
        {
            var newRole = RoleMapper.ViewModelToDomain(roleViewModel);

            if (newRole.IsFailure)
                return newRole.Error;

            var DbResult = await _employeeRepository.EditRole(id, newRole.Value);

            if (DbResult.HasNoValue)
                return new Error("404", "Employee not found");

            return DbResult.Value;
        }

        public async Task<Result<(EmployeeViewViewModel[], int), Error>> GetAllEmployees(int Page)
        {
            var DbResult = await _employeeRepository.GetAll(Page);

            var EmployeeViewModel = new EmployeeViewViewModel[DbResult.Length];

            for (int i = 0; i < DbResult.Length; i++)
            {
                EmployeeViewModel[i] = EmployeeMapper.DomainToViewModel(DbResult[i].Item1);
            }

            return (EmployeeViewModel, DbResult.Length == 0 ? 0 : DbResult[0].Item2);
        }

        public async Task<Result<EmployeeViewViewModel, Error>> GetEmployeeById(Guid id)
        {
            var Employee = await _employeeRepository.GetById(id);

            if (Employee.HasNoValue)
                return new Error("404", "Employee not Found");

            return EmployeeMapper.DomainToViewModel(Employee.Value);
        }

        public async Task<Result<(EmployeeViewViewModel[], int), Error>> GetFilteredEmployee(int Page,
            EmployeeNameViewModel name,
            RoleViewModel role)
        {
            var DbResult = await _employeeRepository.GetFilteredEmployees(Page,
                name.FirstName,
                name.SecondName,
                name.MiddleName,
                role.Role);

            var EmployeeViewModel = new EmployeeViewViewModel[DbResult.Length];

            for (int i = 0; i < DbResult.Length; i++)
            {
                EmployeeViewModel[i] = EmployeeMapper.DomainToViewModel(DbResult[i].Item1);
            }

            return (EmployeeViewModel, DbResult.Length == 0 ? 0 : DbResult[0].Item2);
        }

        public async Task<Result<EmployeeViewViewModel[],Error>> GetEmployeesByProject(Guid ProjectId)
        {
            var DbResult = await _employeeRepository.GetByProject(ProjectId);

            if(DbResult.HasNoValue)
            {
                return new Error("404", "Project is not contains");
            }

            var Employees = DbResult.Value;

            var EmployeeViewModel = new EmployeeViewViewModel[Employees.Length];
            
            for(int i = 0;i< DbResult.Value.Length;i++)
            {
                EmployeeViewModel[i] = EmployeeMapper.DomainToViewModel(Employees[i]);
            }

            return EmployeeViewModel;
        }
    }
}
