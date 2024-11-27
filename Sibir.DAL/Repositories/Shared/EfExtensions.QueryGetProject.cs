using Model = Sibir.Domain.Models.EntityObject;
using CSharpFunctionalExtensions;
using Sibir.Domain.Models.EntityObject;


namespace Sibir.DAL.Repositories.Shared
{
    internal static partial class EfExtensions
    {
        public static IQueryable<ProjectManagerDto> QueryGetProject(this IQueryable<Project> query, 
            IQueryable<Employee> empoyees)
        {
            return query.QueryGroupJoinProject(empoyees)
            .Select(x => new ProjectManagerDto
            {
                Project = x.Project,
                ManagerId = x.Manager != null ? x.Manager.Id : Guid.Empty,
                ManagerFirstName = x.Manager != null ? x.Manager.Name.FirstName : string.Empty,
                ManagerMiddleName = x.Manager != null ? x.Manager.Name.MiddleName : string.Empty,
                ManagerSecondName = x.Manager != null ? x.Manager.Name.SecondName : string.Empty,
            });
        }

        public static IQueryable<ProjectManagerDtoWithPageCount> QueryGetProjectWithPageCount(this IQueryable<Project> query,
            IQueryable<Employee> empoyees,
            Func<int> CountQuery
            )
        {
            return query.QueryGroupJoinProject(empoyees)
            .Select(x => new ProjectManagerDtoWithPageCount
            {
                Project = x.Project,
                ManagerId = x.Manager != null ? x.Manager.Id : Guid.Empty,
                ManagerFirstName = x.Manager != null ? x.Manager.Name.FirstName : string.Empty,
                ManagerMiddleName = x.Manager != null ? x.Manager.Name.MiddleName : string.Empty,
                ManagerSecondName = x.Manager != null ? x.Manager.Name.SecondName : string.Empty,
                TotalCount = CountQuery()
            });
        }

        private static IQueryable<GroupJoinResult> QueryGroupJoinProject(this IQueryable<Project> query,
            IQueryable<Employee> employees)
        {
            return query.GroupJoin(
                employees,
                p => p.ManagerId,
                m => m.Id,
                (p, managers) => new GroupJoinResult
                {
                    Project = p,
                    Manager = managers.DefaultIfEmpty().FirstOrDefault()
                });
        }

        private class GroupJoinResult
        {
            public Project Project { get; set; } = null!;
            public Employee? Manager { get; set; }
        }

        public class ProjectManagerDto
        {
            public Project Project { get; set; } = null!;
            public Guid ManagerId { get; set; }
            public string ManagerFirstName { get; set; } = null!;
            public string ManagerMiddleName { get; set; } = null!;
            public string ManagerSecondName { get; set; } = null!;
        }

        public class ProjectManagerDtoWithPageCount : ProjectManagerDto
        {
            public int TotalCount { get; set; }
        }


    }

}
