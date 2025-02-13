using Sibir.Domain.Shared.ViewModels;

namespace Sibir.API.Contracts.Employee.EditPerosnalData
{
    public record Request
    (
        Guid Id,
        EmployeeNameViewModel Name,
        EmailViewModel Email
    );
}
