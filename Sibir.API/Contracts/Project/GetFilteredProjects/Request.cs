namespace Sibir.API.Contracts.Project.GetFilteredProjects
{
    public record Request
    (
        int Page,
        string SearchByTitle = "",
        string SearchByCompanyExecuter = "",
        string SearchByCompanyConsumer = "",
        string BeginingOfTimeRange = "", 
        string EndOfTimeRange = "", 
        string SortBy = "", 
        bool SortingDerection = true
    );
}
