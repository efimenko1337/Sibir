using Sibir.Domain.Shared.ViewModels;

namespace Sibir.API.Contracts.Employee.GetAll
{
    public record Response
    (
        EmployeeViewViewModel[] Employees,
        int PageCount
    );
}
