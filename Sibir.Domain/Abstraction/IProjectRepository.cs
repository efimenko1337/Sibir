using CSharpFunctionalExtensions;
using Sibir.Domain.Models.EntityObject;
using Sibir.Domain.Models.ValueObject.ForProject;
using Sibir.Domain.Shared;

namespace Sibir.Domain.Abstraction
{ 
    public interface IProjectRepository
    {
        Task<Guid> Create(Project project);
        Task<Maybe<int>> EditManager(Guid projectId, Guid managerId);
        Task<(Project, Guid, string, int)[]> GetAll(int page);
        Task<Maybe<(Project, Guid, string)>> GetById(Guid id);
        Task<(Project, Guid, string, int)[]> GetFilteredProjects(int page, Action<FilterOptions> optionsAction);
        Task<Maybe<int>> UpdateExecuter(Guid projectId, Guid[] employeesRemoveId, Guid[] employeesAddId);
        Task<int> UpdatePriority(Guid projactId, Priority newPriority);
        Task<int> UpdateProjectData(Guid projectId, Title title, Company company, DevelopmentTime developmentTime);
    }
}