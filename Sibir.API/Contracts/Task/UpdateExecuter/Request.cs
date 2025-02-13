namespace Sibir.API.Contracts.Task.UpdateExecuter
{
    public record Request
    (
        Guid TaskId,
        Guid EmployeeId
    );
}
