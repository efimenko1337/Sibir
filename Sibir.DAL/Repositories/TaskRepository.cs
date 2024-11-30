using Model = Sibir.Domain.Models.EntityObject;
using Sibir.Domain.Models.ValueObject.ForTask;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Sibir.DAL.Repositories.Shared;
using System.Linq.Expressions;
using Sibir.Domain.Shared;
using Sibir.Domain.Abstraction;

namespace Sibir.DAL.Repositories
{
    public class TaskRepository(SqlServerContext context) : ITaskRepository
    {
        private readonly SqlServerContext _context = context;

        private readonly int PAGE_SIZE = 20;

        public async Task<Guid> Create(Model.Task newTask)
        {
            await _context.Tasks.AddAsync(newTask);
            await _context.SaveChangesAsync();
            return newTask.Id;
        }

        public async Task<Maybe<Guid>> Delete(Guid taskId)
        {
            var rowCount = await _context.Tasks.Where(t => t.Id == taskId).ExecuteDeleteAsync();
            if (rowCount == 1)
                return taskId;
            else
                return Maybe.None;
        }

        public async Task<Maybe<TaskDetailsDto>> GetById(Guid taskId)
        {
            var DbResult = await _context.Tasks
                .Where(t => t.Id == taskId)
                .QueryGetTask(_context.Employees, _context.Projects)
                .FirstOrDefaultAsync();
            if (DbResult == null)
                return Maybe.None;
            else
                return DbResult;
        }

        public async Task<TaskDetailsDtoWithPageCount[]> GetTaskPerformedByEmployee(Guid employeeId, int Page)
        {
            var SelectorQuery = _context.Tasks
                    .Where(t => t.ExecuterId == employeeId);

            var DbResult = await SelectorQuery
                .QueryGetTaskWithPageCount(_context.Employees,
                        _context.Projects,
                        () => SelectorQuery.Count())
                .Skip(Page * PAGE_SIZE)
                .Take(PAGE_SIZE)
                .ToArrayAsync();

            return DbResult;
        }

        public async Task<TaskDetailsDtoWithPageCount[]> GetTaskCreatedByEmployee(Guid employeeId, int Page)
        {
            var SelectorQuery = _context.Tasks
                    .Where(t => t.CreaterId == employeeId);

            var DbResult = await SelectorQuery
                .QueryGetTaskWithPageCount(_context.Employees,
                        _context.Projects,
                        () => SelectorQuery.Count())
                .Skip(Page * PAGE_SIZE)
                .Take(PAGE_SIZE)
                .ToArrayAsync();

            return DbResult;
        }

        public async Task<TaskDetailsDtoWithPageCount[]> GetProjectTasks(Guid projectId, int Page)
        {
            var SelectorQuery = _context.Tasks
                    .Where(t => t.ProjectId == projectId);

            var DbResult = await SelectorQuery
                .QueryGetTaskWithPageCount(_context.Employees,
                        _context.Projects,
                        () => SelectorQuery.Count())
                .Skip(Page * PAGE_SIZE)
                .Take(PAGE_SIZE)
                .ToArrayAsync();

            return DbResult;
        }

        public async Task<Maybe<Guid>> UpdateExecuter(Guid taskId, Guid employeeId)
        {
            try
            {
                var rowCount = await _context.Tasks
                    .Where(t => t.Id == taskId)
                    .ExecuteUpdateAsync(setter => setter.
                        SetProperty(t => t.ExecuterId, employeeId));
                if (rowCount == 1)
                    return taskId;
                else
                    return Maybe.None;
            }
            catch
            {
                return Maybe.None;
            }

        }

        public async Task<Maybe<Guid>> UpdateStatus(Guid taskId, Status newStatus)
        {
            var rowCount = await _context.Tasks
                    .Where(t => t.Id == taskId)
                    .ExecuteUpdateAsync(setter => setter.
                        SetProperty(t => t.Status, newStatus));
            if (rowCount == 1)
                return taskId;
            else
                return Maybe.None;
        }

        public async Task<Maybe<Guid>> UpdatePriority(Guid taskId, Priority newPriority)
        {
            var rowCount = await _context.Tasks
                    .Where(t => t.Id == taskId)
                    .ExecuteUpdateAsync(setter => setter.
                        SetProperty(t => t.Priority, newPriority));
            if (rowCount == 1)
                return taskId;
            else
                return Maybe.None;
        }

        public async Task<Maybe<Guid>> UpdateCommments(Guid taskId, Comment newComment)
        {
            var rowCount = await _context.Tasks
                    .Where(t => t.Id == taskId)
                    .ExecuteUpdateAsync(setter => setter.
                        SetProperty(t => t.Comment, newComment));
            if (rowCount == 1)
                return taskId;
            else
                return Maybe.None;
        }

        public async Task<TaskDetailsDtoWithPageCount[]> GetFilteredTasksCreatedByEmployee(Guid employeeId,
            int Page,
            Action<FilterOptions> optionsAction) => await Filter(Page,
                optionsAction,
                t => t.CreaterId == employeeId);

        public async Task<TaskDetailsDtoWithPageCount[]> GetFilteredTasksPerformedByEmployee(Guid employeeId,
            int Page,
            Action<FilterOptions> optionsAction) => await Filter(Page,
                optionsAction,
                t => t.ExecuterId == employeeId);

        public async Task<TaskDetailsDtoWithPageCount[]> GetFilteredProjectTasks(Guid projectId,
            int Page,
            Action<FilterOptions> optionsAction) => await Filter(Page,
                optionsAction,
                t => t.ProjectId == projectId);

        private async Task<TaskDetailsDtoWithPageCount[]> Filter(int Page,
            Action<FilterOptions> optionsAction,
            Expression<Func<Model.Task, bool>> predicate)
        {
            FilterOptions Options = new();
            optionsAction(Options);


            if (!string.IsNullOrEmpty(Options.Status))
                predicate = predicate.AndAlso(t => t.Status.Value == Options.Status);

            if (!string.IsNullOrEmpty(Options.TitlePart))
                predicate = predicate.AndAlso(t => t.Title.Value == Options.TitlePart);

            var SelectQuery = _context.Tasks
                .Where(predicate);

            var BeginQuery = SelectQuery
                .QueryGetTaskWithPageCount(_context.Employees,
                    _context.Projects,
                    () => SelectQuery.Count())
                .Skip(Page * PAGE_SIZE)
                .Take(PAGE_SIZE);

            IQueryable<TaskDetailsDtoWithPageCount> MiddleQuery;

            if (Options.SubjectOfSorting == SubjectOfSorting.Priority)
                if (Options.SortDirection)
                    MiddleQuery = BeginQuery.OrderBy(t => t.Task.Priority.Value);
                else
                    MiddleQuery = BeginQuery.OrderByDescending(t => t.Task.Priority.Value);
            else
                MiddleQuery = BeginQuery;

            return await MiddleQuery.ToArrayAsync();

        }
    }
}
