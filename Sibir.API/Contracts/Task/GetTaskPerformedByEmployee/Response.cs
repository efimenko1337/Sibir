using Sibir.Domain.Shared.ViewModels;

namespace Sibir.API.Contracts.Task.GetTaskPerformedByEmployee
{
    public record Response
    (
        int PageCount,
        TaskViewViewModel[] Tasks
    );
}
