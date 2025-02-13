using Sibir.Domain.Shared.ViewModels;

namespace Sibir.API.Contracts.Task.UpdateComments
{
    public record Request
    (
        Guid TaskId,
        CommentViewModel Comment
    );
}
