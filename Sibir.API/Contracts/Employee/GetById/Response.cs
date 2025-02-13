using Sibir.Domain.Shared.ViewModels;

namespace Sibir.API.Contracts.Employee.GetById
{
    public record Response
    (
        EmployeeViewViewModel Employee
    );
}
