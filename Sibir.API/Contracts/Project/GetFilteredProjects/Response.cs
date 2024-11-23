using Sibir.BL.ViewModels;

namespace Sibir.API.Contracts.Project.GetFilteredProjects
{
    public record Response
    (
        ProjectViewViewModel[] Projects,
        int PageCount
    );

}
