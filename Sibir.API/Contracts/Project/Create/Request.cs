using Sibir.Domain.Shared.ViewModels;

namespace Sibir.API.Contracts.Project.Create
{
    public record Request
    (
        ProjectCreateViewModel ProjectCreate
    );
}
