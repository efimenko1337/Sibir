using Sibir.Domain.Shared.ViewModels;

namespace Sibir.API.Contracts.Task.UpdateStatus
{
    public record Request
    (
        Guid TaskId,
        StatusViewModel Status
    );
}
