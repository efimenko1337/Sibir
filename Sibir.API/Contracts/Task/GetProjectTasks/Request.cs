namespace Sibir.API.Contracts.Task.GetProjectTasks
{
    public record Request
    (
        int Page,
        Guid ProjectId
    );
}
