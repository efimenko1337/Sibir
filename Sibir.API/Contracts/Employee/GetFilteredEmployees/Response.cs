using Sibir.Domain.Shared.ViewModels;

namespace Sibir.API.Contracts.Employee.GetFilteredEmployees
{
    public record Response
    (
        int PageCount,
        EmployeeViewViewModel[] Employees
    );
}
