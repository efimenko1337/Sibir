using Sibir.Domain.Shared.ViewModels;

namespace Sibir.API.Contracts.Employee.Create
{
    public record Request
    (
        EmployeeNameViewModel Name,
        string Role
    );
}
