namespace Sibir.API.Contracts.Task.GetProjectsFilteredTasks
{
    public record Request
    (
       int Page,
       Guid ProjectId,
       string Status,
       string Titile,
       string ObjectOfSorting,
       bool SortDirction
    );
}
