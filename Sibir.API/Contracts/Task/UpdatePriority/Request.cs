using Sibir.Domain.Shared.ViewModels;

namespace Sibir.API.Contracts.Task.UpdatePriority
{
    public record Request
    (
        Guid TaskId,
        PriorityViewModel Priority
    );
}
