namespace Sibir.API.Contracts.Project.UpdateExecuters
{
    public record Request
    (
        Guid ProjectId,
        Guid[] EmployeesRemoveId,
        Guid[] EmployeesAddId
    );
}
