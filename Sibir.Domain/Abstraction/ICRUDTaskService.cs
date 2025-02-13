using CSharpFunctionalExtensions;
using Sibir.Domain.Shared;
using Sibir.Domain.Shared.ViewModels;

namespace Sibir.Domain.Abstraction;

public interface ICRUDTaskService
{
    Task<Result<Guid, Error>> CreateTask(TaskCreateViewModel viewModel);
    Task<Result<Guid, Error>> DeleteTask(Guid id);
    Task<Result<(TaskViewViewModel[], int), Error>> GetFilteredTasksCreatedByEmployee(int Page, Guid employeeId, string status, string title, string objectOfSorting, bool sotrDirection);
    Task<Result<(TaskViewViewModel[], int), Error>> GetFilteredTasksPerformendByEmployee(int Page, Guid employeeId, string status, string title, string objectOfSorting, bool sotrDirection);
    Task<Result<(TaskViewViewModel[], int), Error>> GetProjectFilteredTasks(int Page, Guid projectId, string status, string title, string objectOfSorting, bool sotrDirection);
    Task<Result<(TaskViewViewModel[], int), Error>> GetProjectTasks(int Page, Guid projectId);
    Task<Result<(TaskViewViewModel[], int), Error>> GetTasksCreatedByEmployee(int Page, Guid employeeId);
    Task<Result<(TaskViewViewModel[], int), Error>> GetTasksPerformendByEmployee(int Page, Guid employeeId);
    Task<Result<Guid, Error>> UpdateTaskComment(Guid id, CommentViewModel commentViewModel);
    Task<Result<Guid, Error>> UpdateTaskExecuter(Guid taskId, Guid executerId);
    Task<Result<Guid, Error>> UpdateTaskPriority(Guid id, PriorityViewModel priorityViewModel);
    Task<Result<Guid, Error>> UpdateTaskStatus(Guid id, StatusViewModel statusViewModel);
    Task<Result<TaskViewViewModel, Error>> GetTaskById(Guid id);
}