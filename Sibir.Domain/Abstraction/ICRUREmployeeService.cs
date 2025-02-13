using CSharpFunctionalExtensions;
using Sibir.Domain.Shared;
using Sibir.Domain.Shared.ViewModels;

namespace Sibir.Domain.Abstraction;

public interface ICRUREmployeeService
{
    Task<Result<Guid, Error>> Create(EmployeeCreateViewModel viewModel);
    Task<Result<Guid, Error>> EditEmployeePersonalData(Guid Id, EmployeeNameViewModel nameViewModel, EmailViewModel emailViewModel);
    Task<Result<Guid, Error>> EditRole(Guid id, RoleViewModel roleViewModel);
    Task<Result<(EmployeeViewViewModel[], int), Error>> GetAllEmployees(int Page);
    Task<Result<EmployeeViewViewModel, Error>> GetEmployeeById(Guid id);
    Task<Result<(EmployeeViewViewModel[], int), Error>> GetFilteredEmployee(int Page, EmployeeNameViewModel name, RoleViewModel role);
    Task<Result<EmployeeViewViewModel[], Error>> GetEmployeesByProject(Guid projectId);
}