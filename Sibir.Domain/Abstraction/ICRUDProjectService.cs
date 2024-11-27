using CSharpFunctionalExtensions;
using Sibir.Domain.Shared;
using Sibir.Domain.Shared.ViewModels;

namespace Sibir.Domain.Abstraction
{
    public interface ICRUDProjectService
    {
        Task<Result<Guid, Error>> CreateProject(ProjectCreateViewModel projectCreate);
        Task<Result<Guid, Error>> EditProjectManager(Guid projectId, Guid managerId);
        Task<Result<(ProjectViewViewModel[], int), Error>> GetAllProject(int page);
        Task<Result<(ProjectViewViewModel[], int), Error>> GetFilteredProject(int page, string? titlePart, string? companyConsumerPart, string? companyExecuterPart, DateOnly? beginingOfTimeRange, DateOnly? endOfTimeRange, string? subjectOfSorting, bool sortDirection = true);
        Task<Result<ProjectViewViewModel, Error>> GetProjectById(Guid id);
        Task<Result<Guid, Error>> UpdateProjectData(Guid projectId, string title, ComapnyViewModel comapny, DevelopmentTimeViewModel developmentTime);
        Task<Result<int, Error>> UpdateProjectExecuters(Guid projectId, Guid[] employeesRemoveId, Guid[] employeesAddId);
        Task<Result<Guid, Error>> UpdateProjectPriority(Guid projectId, int newPriority);
    }
}