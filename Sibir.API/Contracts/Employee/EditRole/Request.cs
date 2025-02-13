using Sibir.Domain.Shared.ViewModels;

namespace Sibir.API.Contracts.Employee.EditRole
{
    public record Request
    (
        Guid Id,
        RoleViewModel Role
    );
}
