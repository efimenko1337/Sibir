using CSharpFunctionalExtensions;
using Sibir.BL.Mappers;
using Sibir.Domain.Abstraction;
using Sibir.Domain.Shared;
using Sibir.Domain.Shared.ViewModels;

namespace Sibir.BL.Services
{
    public class CRUDTaskService(ITaskRepository repository) : ICRUDTaskService
    {
        private readonly ITaskRepository _taskRepository = repository;

        public async Task<Result<Guid, Error>> CreateTask(TaskCreateViewModel viewModel)
        {
            var NewTask = TaskMapper.ViewModelToDomain(viewModel);

            if (NewTask.IsFailure)
                return NewTask.Error;

            var DbResult = await _taskRepository.Create(NewTask.Value);
            if (DbResult.HasNoValue)
                return new Error("404", "asdasd");

            return DbResult.Value;
        }

        public async Task<Result<Guid, Error>> DeleteTask(Guid id)
        {
            var DbResult = await _taskRepository.Delete(id);

            if (DbResult.HasNoValue)
                return new Error("404", "Task not found");

            return DbResult.Value;
        }

        public async Task<Result<Guid, Error>> UpdateTaskComment(Guid id, CommentViewModel commentViewModel)
        {
            var newComment = CommentMapper.ViewModelToDomain(commentViewModel);

            if (newComment.IsFailure)
                return newComment.Error;

            var DbResult = await _taskRepository.UpdateCommments(id, newComment.Value);

            if (DbResult.HasNoValue)
                return new Error("404", "Task not found");

            return DbResult.Value;
        }

        public async Task<Result<Guid, Error>> UpdateTaskExecuter(Guid taskId, Guid executerId)
        {
            var DbResult = await _taskRepository.UpdateExecuter(taskId, executerId);

            if (DbResult.HasNoValue)
                return new Error("404", "Task or exceuter not found");

            return DbResult.Value;
        }

        public async Task<Result<Guid, Error>> UpdateTaskPriority(Guid id, PriorityViewModel priorityViewModel)
        {
            var newPriority = PriorityMapper.ViewModelToDomain(priorityViewModel);

            if (newPriority.IsFailure)
                return newPriority.Error;

            var DbResult = await _taskRepository.UpdatePriority(id, newPriority.Value);

            if (DbResult.HasNoValue)
                return new Error("404", "Task not found");

            return DbResult.Value;
        }

        public async Task<Result<Guid, Error>> UpdateTaskStatus(Guid id, StatusViewModel statusViewModel)
        {
            var newStatus = StatusMapper.ViewModelToDomain(statusViewModel);

            if (newStatus.IsFailure)
                return newStatus.Error;

            var DbResult = await _taskRepository.UpdateStatus(id, newStatus.Value);

            if (DbResult.HasNoValue)
                return new Error("404", "Task not found");

            return DbResult.Value;
        }

        public async Task<Result<(TaskViewViewModel[], int), Error>> GetProjectTasks(int Page, Guid projectId)
        {
            var DbResult = await _taskRepository.GetProjectTasks(projectId, Page);

            var TaskViewModels = new TaskViewViewModel[DbResult.Length];

            for (int i = 0; i < DbResult.Length; i++)
                TaskViewModels[i] = TaskMapper.DomainToViewModel(DbResult[i].Task,
                    DbResult[i].CreatorId, DbResult[i].CreatorName,
                    DbResult[i].ExecuterId, DbResult[i].ExecuterName,
                    DbResult[i].ProjectId, DbResult[i].ProjectTitle);

            return (TaskViewModels, DbResult.Length == 0 ? 0 : DbResult[0].TotalCount);
        }

        public async Task<Result<(TaskViewViewModel[], int), Error>> GetTasksCreatedByEmployee(int Page, Guid employeeId)
        {
            var DbResult = await _taskRepository.GetTaskCreatedByEmployee(employeeId, Page);

            var TaskViewModels = new TaskViewViewModel[DbResult.Length];

            for (int i = 0; i < DbResult.Length; i++)
                TaskViewModels[i] = TaskMapper.DomainToViewModel(DbResult[i].Task,
                    DbResult[i].CreatorId, DbResult[i].CreatorName,
                    DbResult[i].ExecuterId, DbResult[i].ExecuterName,
                    DbResult[i].ProjectId, DbResult[i].ProjectTitle);

            return (TaskViewModels, DbResult.Length == 0 ? 0 : DbResult[0].TotalCount);
        }

        public async Task<Result<(TaskViewViewModel[], int), Error>> GetTasksPerformendByEmployee(int Page, Guid employeeId)
        {
            var DbResult = await _taskRepository.GetTaskPerformedByEmployee(employeeId, Page);

            var TaskViewModels = new TaskViewViewModel[DbResult.Length];

            for (int i = 0; i < DbResult.Length; i++)
                TaskViewModels[i] = TaskMapper.DomainToViewModel(DbResult[i].Task,
                    DbResult[i].CreatorId, DbResult[i].CreatorName,
                    DbResult[i].ExecuterId, DbResult[i].ExecuterName,
                    DbResult[i].ProjectId, DbResult[i].ProjectTitle);

            return (TaskViewModels, DbResult.Length == 0 ? 0 : DbResult[0].TotalCount);
        }

        public async Task<Result<(TaskViewViewModel[], int), Error>> GetFilteredTasksCreatedByEmployee(int Page, Guid employeeId,
            string status,
            string title,
            string objectOfSorting,
            bool sotrDirection)
        {
            var DbResult = await _taskRepository.GetFilteredTasksCreatedByEmployee(employeeId, Page, options =>
            {
                options.Status = status;
                options.SortDirection = sotrDirection;
                options.TitlePart = title;
                options.SubjectOfSorting = Enum.TryParse<SubjectOfSorting>(objectOfSorting, out var subjectOfSortingEnum) ?
                    subjectOfSortingEnum :
                    SubjectOfSorting.None;
            });

            var TaskViewModels = new TaskViewViewModel[DbResult.Length];

            for (int i = 0; i < DbResult.Length; i++)
                TaskViewModels[i] = TaskMapper.DomainToViewModel(DbResult[i].Task,
                    DbResult[i].CreatorId, DbResult[i].CreatorName,
                    DbResult[i].ExecuterId, DbResult[i].ExecuterName,
                    DbResult[i].ProjectId, DbResult[i].ProjectTitle);

            return (TaskViewModels, DbResult.Length == 0 ? 0 : DbResult[0].TotalCount);
        }

        public async Task<Result<(TaskViewViewModel[], int), Error>> GetFilteredTasksPerformendByEmployee(int Page, Guid employeeId,
            string status,
            string title,
            string objectOfSorting,
            bool sotrDirection)
        {
            var DbResult = await _taskRepository.GetFilteredTasksPerformedByEmployee(employeeId, Page, options =>
            {
                options.Status = status;
                options.SortDirection = sotrDirection;
                options.TitlePart = title;
                options.SubjectOfSorting = Enum.TryParse<SubjectOfSorting>(objectOfSorting, out var subjectOfSortingEnum) ?
                    subjectOfSortingEnum :
                    SubjectOfSorting.None;
            });

            var TaskViewModels = new TaskViewViewModel[DbResult.Length];

            for (int i = 0; i < DbResult.Length; i++)
                TaskViewModels[i] = TaskMapper.DomainToViewModel(DbResult[i].Task,
                    DbResult[i].CreatorId, DbResult[i].CreatorName,
                    DbResult[i].ExecuterId, DbResult[i].ExecuterName,
                    DbResult[i].ProjectId, DbResult[i].ProjectTitle);

            return (TaskViewModels, DbResult.Length == 0 ? 0 : DbResult[0].TotalCount);
        }

        public async Task<Result<(TaskViewViewModel[], int), Error>> GetProjectFilteredTasks(int Page, Guid projectId,
            string status,
            string title,
            string objectOfSorting,
            bool sotrDirection)
        {
            var DbResult = await _taskRepository.GetFilteredProjectTasks(projectId, Page, options =>
            {
                options.Status = status;
                options.SortDirection = sotrDirection;
                options.TitlePart = title;
                options.SubjectOfSorting = Enum.TryParse<SubjectOfSorting>(objectOfSorting, out var subjectOfSortingEnum) ?
                    subjectOfSortingEnum :
                    SubjectOfSorting.None;
            });

            var TaskViewModels = new TaskViewViewModel[DbResult.Length];

            for (int i = 0; i < DbResult.Length; i++)
                TaskViewModels[i] = TaskMapper.DomainToViewModel(DbResult[i].Task,
                    DbResult[i].CreatorId, DbResult[i].CreatorName,
                    DbResult[i].ExecuterId, DbResult[i].ExecuterName,
                    DbResult[i].ProjectId, DbResult[i].ProjectTitle);

            return (TaskViewModels, DbResult.Length == 0 ? 0 : DbResult[0].TotalCount);
        }

        public async Task<Result<TaskViewViewModel,Error>> GetTaskById(Guid id)
        {
            var DbResult = await _taskRepository.GetById(id);

            if (DbResult.HasNoValue)
                return Errors.General.NotFound();

            return TaskMapper.DomainToViewModel(DbResult.Value.Task,
                DbResult.Value.CreatorId,
                DbResult.Value.CreatorName,
                DbResult.Value.ExecuterId,
                DbResult.Value.ExecuterName,
                DbResult.Value.ProjectId,
                DbResult.Value.ProjectTitle);
        }
    }
}
