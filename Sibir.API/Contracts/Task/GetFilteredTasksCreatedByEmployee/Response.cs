using Sibir.Domain.Shared.ViewModels;

namespace Sibir.API.Contracts.Task.GetFilteredTasksCreatedByEmployee
{
    public record Response
    (
        int PageCount,
        TaskViewViewModel[] Tasks
    );
}
