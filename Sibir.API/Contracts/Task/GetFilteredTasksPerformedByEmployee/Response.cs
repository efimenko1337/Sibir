using Sibir.Domain.Shared.ViewModels;

namespace Sibir.API.Contracts.Task.GetFilteredTasksPerformedByEmployee
{
    public record Response
    (
        int Page,
        TaskViewViewModel[] Tasks
    );
}
