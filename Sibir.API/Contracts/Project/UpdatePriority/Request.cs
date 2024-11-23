namespace Sibir.API.Contracts.Project.UpdatePriority
{
    public record Request
    (
        Guid ProjectId,
        int NewPriority
    );
}
