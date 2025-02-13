using CSharpFunctionalExtensions;
using Sibir.Domain.Models.ValueObject.ForTask;
using Sibir.Domain.Shared;

namespace Sibir.Domain.Abstraction
{
    public interface ITaskRepository
    {
        Task<Maybe<Guid>> Create(Models.EntityObject.Task newTask);
        Task<Maybe<Guid>> Delete(Guid taskId);
        Task<Maybe<TaskDetailsDto>> GetById(Guid taskId);
        Task<TaskDetailsDtoWithPageCount[]> GetFilteredProjectTasks(Guid projectId, int Page, Action<FilterOptions> optionsAction);
        Task<TaskDetailsDtoWithPageCount[]> GetFilteredTasksCreatedByEmployee(Guid employeeId, int Page, Action<FilterOptions> optionsAction);
        Task<TaskDetailsDtoWithPageCount[]> GetFilteredTasksPerformedByEmployee(Guid employeeId, int Page, Action<FilterOptions> optionsAction);
        Task<TaskDetailsDtoWithPageCount[]> GetProjectTasks(Guid projectId, int Page);
        Task<TaskDetailsDtoWithPageCount[]> GetTaskCreatedByEmployee(Guid employeeId, int Page);
        Task<TaskDetailsDtoWithPageCount[]> GetTaskPerformedByEmployee(Guid employeeId, int Page);
        Task<Maybe<Guid>> UpdateCommments(Guid taskId, Comment newComment);
        Task<Maybe<Guid>> UpdateExecuter(Guid taskId, Guid employeeId);
        Task<Maybe<Guid>> UpdatePriority(Guid taskId, Priority newPriority);
        Task<Maybe<Guid>> UpdateStatus(Guid taskId, Status newStatus);
    }
}