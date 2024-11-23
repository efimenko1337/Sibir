using Sibir.BL.ViewModels;

namespace Sibir.API.Contracts.Project.GetAllProject
{
    public record Response
    (
        ProjectViewViewModel[] Projects,
        int PageCount
    );
}
