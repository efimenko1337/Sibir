namespace Sibir.API.Contracts.Task.GetFilteredTasksPerformedByEmployee
{
    public record Request
    (
       int Page,
       Guid EmployeeId,
       string Status,
       string Titile,
       string ObjectOfSorting,
       bool SortDirction
    );
}
