namespace Sibir.API.Contracts.Task.GetTasksCreatedByEmployee
{
    public record Request
    (
        int Page,
        Guid EmployeeId
    );
}
