namespace Sibir.API.Contracts.Project.EditManager
{
    public record Request
    (
        Guid ProjectId,
        Guid ManagerId
    );
}
