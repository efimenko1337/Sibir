using Sibir.BL.ViewModels;

namespace Sibir.API.Contracts.Project.UpdateProjectData
{
    public record Request
    (
        Guid ProjectId,
        string Title,
        ComapnyViewModel Comapny,
        DevelopmentTimeViewModel DevelopmentTime
    );
}
