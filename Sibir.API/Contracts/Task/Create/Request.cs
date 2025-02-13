using Sibir.Domain.Shared.ViewModels;

namespace Sibir.API.Contracts.Task.Create
{
    public record Request
    (
        TaskCreateViewModel Task
    );
}
