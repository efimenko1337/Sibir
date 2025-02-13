using Sibir.Domain.Shared.ViewModels;

namespace Sibir.API.Contracts.Employee.GetFilteredEmployees
{
    public record Request
    (
        int Page,
        EmployeeNameViewModel Name,
        RoleViewModel Role
    );
}
