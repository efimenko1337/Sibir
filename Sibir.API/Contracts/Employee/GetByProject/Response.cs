using Sibir.Domain.Shared.ViewModels;

namespace Sibir.API.Contracts.Employee.GetByProject
{
    public record Response
    (
        EmployeeViewViewModel[] Employees
    );
}
