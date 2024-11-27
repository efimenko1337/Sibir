using Sibir.Domain.Shared.ViewModels;

namespace Sibir.API.Contracts.Project.GetAllProject
{
    public record Response
    (
        ProjectViewViewModel[] Projects,
        int PageCount
    );
}
