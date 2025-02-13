using Sibir.Domain.Shared.ViewModels;

namespace Sibir.API.Contracts.Task.GetById
{
    public record Response
    (
        TaskViewViewModel Task
    );
}
