namespace Sibir.API.Contracts.Task.GetTaskPerformedByEmployee
{
    public record Request
    (
        int Page,
        Guid EmployeeId
    );
}
