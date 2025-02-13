using Sibir.Domain.Shared.ViewModels;

namespace Sibir.API.Contracts.Task.GetProjectsFilteredTasks
{
    public record Response
    (
        int PageCount,
        TaskViewViewModel[] Tasks
    );
}
