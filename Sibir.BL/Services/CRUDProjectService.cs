using CSharpFunctionalExtensions;
using Sibir.Domain.Shared;
using Sibir.BL.Mappers;
using Sibir.Domain.Models.ValueObject.ForProject;
using ValueObject = Sibir.Domain.Models.ValueObject.ForProject;
using Sibir.Domain.Abstraction;
using Sibir.Domain.Shared.ViewModels;

namespace Sibir.BL.Services
{
    public class CRUDProjectService(IProjectRepository repository) : ICRUDProjectService
    {
        private readonly IProjectRepository _repository = repository;

        public async Task<Result<Guid, Error>> CreateProject(ProjectCreateViewModel projectCreate)
        {
            var Project = ProjectMapper.ViewModelToDomain(projectCreate);
            if (Project.IsFailure)
                return Project.Error;
            return await _repository.Create(Project.Value);
        }

        public async Task<Result<(ProjectViewViewModel[], int), Error>> GetAllProject(int page)
        {
            var Result = await _repository.GetAll(page);
            var ViewModels = new ProjectViewViewModel[Result.Length];

            for (int i = 0; i < Result.Length; i++)
                ViewModels[i] = ProjectMapper.DomainToViewModel(Result[i].Item1, Result[i].Item2, Result[i].Item3);

            return (ViewModels, Result.Length > 0 ? Result[0].Item4 : 0);
        }

        public async Task<Result<Guid, Error>> EditProjectManager(Guid projectId, Guid managerId)
        {
            var RowCount = await _repository.EditManager(projectId, managerId);
            if (RowCount.HasNoValue)
                return new Error("404", "Employee not found");
            if (RowCount.Value == 1)
                return projectId;
            else
                return Errors.General.ValueIsInvalid();
        }

        public async Task<Result<ProjectViewViewModel, Error>> GetProjectById(Guid id)
        {
            var Result = await _repository.GetById(id);
            if (Result.HasNoValue)
                return Errors.General.ValueIsInvalid();
            return ProjectMapper.DomainToViewModel(Result.Value.Item1, Result.Value.Item2, Result.Value.Item3);
        }

        public async Task<Result<int, Error>> UpdateProjectExecuters(Guid projectId,
            Guid[] employeesRemoveId,
            Guid[] employeesAddId)
        {
            if (employeesAddId.Length == 0 && employeesRemoveId.Length == 0)
                return 0;

            var RowCount = await _repository.UpdateExecuter(projectId, employeesRemoveId, employeesAddId);
            if (RowCount.HasNoValue)
                return new Error("404", "Project not found");

            return RowCount.Value;
        }

        public async Task<Result<Guid, Error>> UpdateProjectPriority(Guid projectId, int newPriority)
        {
            var NewPriority = Priority.Create(newPriority);
            if (NewPriority.IsFailure)
                return NewPriority.Error;

            var Result = await _repository.UpdatePriority(projectId, NewPriority.Value);
            if (Result == 0)
                return new Error("404", "Project not found");

            return projectId;
        }

        public async Task<Result<Guid, Error>> UpdateProjectData(Guid projectId,
            string title,
            ComapnyViewModel comapny,
            DevelopmentTimeViewModel developmentTime)
        {
            var Title = ValueObject.Title.Create(title);
            var Company = CompanyMapper.ViewModelToDomain(comapny);
            var DevelopmentTime = DevelopmentTimeMapper.ViewModelToDomain(developmentTime);

            if (Company.IsFailure)
                return Company.Error;
            if (DevelopmentTime.IsFailure)
                return DevelopmentTime.Error;

            var Result = await _repository.UpdateProjectData(projectId, Title.Value, Company.Value, DevelopmentTime.Value);

            if (Result == 0)
                return new Error("404", "Project not found");

            return projectId;
        }

        public async Task<Result<(ProjectViewViewModel[], int), Error>> GetFilteredProject(int page,
            string? titlePart,
            string? companyConsumerPart,
            string? companyExecuterPart,
            DateOnly? beginingOfTimeRange,
            DateOnly? endOfTimeRange,
            string? subjectOfSorting,
            bool sortDirection = true)
        {
            var Result = await _repository.GetFilteredProjects(page, options =>
            {
                options.TitlePart = titlePart == "" ? null : titlePart;
                options.CompanyConsumerPart = companyConsumerPart == "" ? null : companyConsumerPart;
                options.CompanyExecuterPart = companyExecuterPart == "" ? null : companyExecuterPart;
                options.BeginingOfTimeRange = beginingOfTimeRange;
                options.EndOfTimeRange = endOfTimeRange;
                options.SubjectOfSorting = Enum.TryParse<SubjectOfSorting>(subjectOfSorting, out var subjectOfSortingEnum) ?
                    subjectOfSortingEnum :
                    SubjectOfSorting.None;
                options.SortDirection = sortDirection;
            });

            var ViewModels = new ProjectViewViewModel[Result.Length];
            for (int i = 0; i < Result.Length; i++)
                ViewModels[i] = ProjectMapper.DomainToViewModel(Result[i].Item1, Result[i].Item2, Result[i].Item3);

            return (ViewModels, Result.Length > 0 ? Result[0].Item4 : 0);
        }
    }
}
