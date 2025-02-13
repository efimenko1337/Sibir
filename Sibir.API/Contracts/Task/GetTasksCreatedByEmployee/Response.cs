using Sibir.Domain.Shared.ViewModels;

namespace Sibir.API.Contracts.Task.GetTasksCreatedByEmployee
{
    public record Response
    (
        int PageCount,
        TaskViewViewModel[] Tasks
    );
}
