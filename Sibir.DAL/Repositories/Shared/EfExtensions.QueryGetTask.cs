using Sibir.Domain.Models.ValueObject.ForEmployee;
using Sibir.Domain.Shared;
using Model = Sibir.Domain.Models.EntityObject;

namespace Sibir.DAL.Repositories.Shared
{
    internal static partial class EfExtensions
    {
        public static IQueryable<TaskDetailsDto> QueryGetTask(this IQueryable<Model.Task> query,
            IQueryable<Model.Employee> employees,
            IQueryable<Model.Project> projects)
        {
            return query
                .GroupJoin(
                    employees,
                    t => t.CreaterId,
                    e => e.Id,
                    (task, creators) => new { Task = task, Creator = creators.DefaultIfEmpty().FirstOrDefault() }
                )
                .GroupJoin(
                    employees,
                    t => t.Task.ExecuterId,
                    e => e.Id,
                    (t, executers) => new { t.Task, t.Creator, Executer = executers.DefaultIfEmpty().FirstOrDefault() }
                )
                .GroupJoin(
                    projects,
                    t => t.Task.ProjectId,
                    p => p.Id,
                    (t, project) => new
                    {
                        t.Task,
                        t.Creator,
                        t.Executer,
                        Project = project.DefaultIfEmpty().FirstOrDefault()
                    }
                )
                .Select(x => new TaskDetailsDto
                {
                    Task = x.Task,
                    CreatorId = x.Creator != null ? x.Creator.Id : Guid.Empty,
                    CreatorName = Name.Create
                    (
                        x.Creator != null ? x.Creator.Name.FirstName : string.Empty,
                        x.Creator != null ? x.Creator.Name.MiddleName : string.Empty,
                        x.Creator != null ? x.Creator.Name.SecondName : string.Empty
                    ).Value,
                    ExecuterId = x.Executer != null ? x.Executer.Id : Guid.Empty,
                    ExecuterName = Name.Create
                    (
                        x.Executer != null ? x.Executer.Name.FirstName : string.Empty,
                        x.Executer != null ? x.Executer.Name.MiddleName : string.Empty,
                        x.Executer != null ? x.Executer.Name.SecondName : string.Empty
                    ).Value,
                    ProjectId = x.Project != null ? x.Project.Id : Guid.Empty,
                    ProjectTitle = x.Project != null ? x.Project.Title.Value : string.Empty
                });
        }

        public static IQueryable<TaskDetailsDtoWithPageCount> QueryGetTaskWithPageCount(this IQueryable<Model.Task> query,
            IQueryable<Model.Employee> employees,
            IQueryable<Model.Project> projects,
            Func<int> CountQuery)
        {
            return query
                .GroupJoin(
                    employees,
                    t => t.CreaterId,
                    e => e.Id,
                    (task, creators) => new { Task = task, Creator = creators.DefaultIfEmpty().FirstOrDefault() }
                )
                .GroupJoin(
                    employees,
                    t => t.Task.ExecuterId,
                    e => e.Id,
                    (t, executers) => new { t.Task, t.Creator, Executer = executers.DefaultIfEmpty().FirstOrDefault() }
                )
                .GroupJoin(
                    projects,
                    t => t.Task.ProjectId,
                    p => p.Id,
                    (t, project) => new
                    {
                        t.Task,
                        t.Creator,
                        t.Executer,
                        Project = project.DefaultIfEmpty().FirstOrDefault()
                    }
                )
                .Select(x => new TaskDetailsDtoWithPageCount
                {
                    Task = x.Task,
                    CreatorId = x.Creator != null ? x.Creator.Id : Guid.Empty,
                    CreatorName = Name.Create
                    (
                        x.Creator != null ? x.Creator.Name.FirstName : string.Empty,
                        x.Creator != null ? x.Creator.Name.MiddleName : string.Empty,
                        x.Creator != null ? x.Creator.Name.SecondName : string.Empty
                    ).Value,
                    ExecuterId = x.Executer != null ? x.Executer.Id : Guid.Empty,
                    ExecuterName = Name.Create
                    (
                        x.Executer != null ? x.Executer.Name.FirstName : string.Empty,
                        x.Executer != null ? x.Executer.Name.MiddleName : string.Empty,
                        x.Executer != null ? x.Executer.Name.SecondName : string.Empty
                    ).Value,
                    ProjectId = x.Project != null ? x.Project.Id : Guid.Empty,
                    ProjectTitle = x.Project != null ? x.Project.Title.Value : string.Empty,
                    TotalCount = CountQuery()
                });
        }

        
    }
}
