using Sibir.Domain.Shared.ViewModels;

namespace Sibir.API.Contracts.Task.GetProjectTasks
{
    public record Response
    (
        int PageCount,
        TaskViewViewModel[] Tasks
    );
}
