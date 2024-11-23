using Microsoft.EntityFrameworkCore;
using Model = Sibir.Domain.Models.EntityObject;
using Sibir.Domain.Models.ValueObject.ForProject;
using Name = Sibir.Domain.Models.ValueObject.ForEmployee.Name;
using Microsoft.EntityFrameworkCore.Internal;
using CSharpFunctionalExtensions;
using Sibir.DAL.Repositories.Shared;
using System.ComponentModel;
using Sibir.Domain.Models.EntityObject;
using static Sibir.DAL.Repositories.Shared.EfExtensions;
using System.Linq.Expressions;


namespace Sibir.DAL.Repositories
{
    public class ProjectRepository(SqlServerContext context)
    {
        private readonly int PAGE_SIZE=20;

        private readonly SqlServerContext _context = context;

        public async Task<Guid> Create(Model.Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return project.Id;
        }

        public async Task<(Model.Project,Guid,string,int)[]> GetAll(int page)
        {
            return (await _context.Projects
                .QueryGetProject(_context.Employees)
                .Select(p => new
                {
                    p.Project,
                    p.ManagerId,
                    p.ManagerFirstName,
                    p.ManagerSecondName,
                    p.ManagerMiddleName,
                    TotalCount = _context.Employees.Count()
                })
                .Skip(page*PAGE_SIZE)
                .Take(PAGE_SIZE)
                .ToArrayAsync())
                .AsEnumerable()
                .Select(r => (r.Project,
                    r.ManagerId,
                    string.Concat(r.ManagerFirstName," ",r.ManagerSecondName," ",r.ManagerMiddleName),
                    (r.TotalCount + PAGE_SIZE - 1) / PAGE_SIZE))
                .ToArray();
        }

        public async Task<Maybe<int>> EditManager(Guid projectId, Guid managerId)
        {
            try
            {
                return await _context.Projects
                    .Where(p => p.Id == projectId)
                    .ExecuteUpdateAsync(p => p.SetProperty(pp => pp.ManagerId, managerId));
            }
            catch 
            {
                return Maybe.None;
            }
        }

        public async Task<Maybe<(Model.Project, Guid, string)>> GetById(Guid id)
        {
            var DBResult = await _context.Projects
                .Where(p => p.Id == id)
                .QueryGetProject(_context.Employees)
                .FirstOrDefaultAsync();
            if (DBResult != null)
                return (DBResult.Project,
                    DBResult.ManagerId,
                    string.Concat
                    (
                        DBResult.ManagerFirstName, " ",
                        DBResult.ManagerMiddleName, " ",
                        DBResult.ManagerSecondName
                    ));
            else
                return Maybe.None;
        }
        
        public async Task<Maybe<int>> UpdateExecuter(Guid projectId, Guid[] employeesRemoveId, Guid[] employeesAddId)
        {
            int CountRow = 0;
            var Project = (await _context.Projects
                .Where(p => p.Id == projectId)
                .Include(p => p.Executers)
                .FirstOrDefaultAsync());
            if (Project == null)
                return Maybe.None;

            Project.Executers
                    .ToList()
                    .RemoveAll(e=>employeesRemoveId.Contains(e.Id));

            var EmployeesAdd = await _context.Employees
                .Where(e=>employeesAddId.Contains(e.Id))
                .ToArrayAsync();
            Project.Executers.ToList().AddRange(EmployeesAdd);

            CountRow = await _context.SaveChangesAsync();
            return CountRow;
        }

        public async Task<int> UpdatePriority(Guid projactId, Priority newPriority)
        {
            return await _context.Projects
                .Where(p => p.Id == projactId)
                .ExecuteUpdateAsync(setter=>setter.SetProperty(p=>p.Priority,newPriority));
        }

        public async Task<int> UpdateProjectData(Guid projectId,
            Title title, 
            Company company, 
            DevelopmentTime developmentTime)
        {
            return await _context.Projects
                .Where(p => p.Id == projectId)
                .ExecuteUpdateAsync(setter=> setter
                    .SetProperty(p => p.Title, title)
                    .SetProperty(p => p.Company, company)
                    .SetProperty(p => p.DevelopmentTime, developmentTime)
                );
        }

        public async Task<(Model.Project, Guid, string, int)[]> GetFilteredProjects(int page, 
            Action<FilterOptions> optionsAction)
        {
            var Options = new FilterOptions();
            optionsAction(Options);

            #region assembly conditions
            Expression<Func<Project,bool>> Conditions = project => true;
            if (!string.IsNullOrEmpty(Options.TitlePart))
                Conditions = Conditions.AndAlso(project => project.Title.Value.ToLower().
                    Contains(Options.TitlePart.Trim().ToLower()));

            if (!string.IsNullOrEmpty(Options.CompanyConsumerPart  ))
                Conditions = Conditions.AndAlso(project => project.Company.Consumer.ToLower()
                    .Contains(Options.CompanyConsumerPart.Trim().ToLower()));

            if (!string.IsNullOrEmpty(Options.CompanyExecuterPart))
                Conditions = Conditions.AndAlso(project => project.Company.Executer.ToLower()
                    .Contains(Options.CompanyExecuterPart.Trim().ToLower()));

            if (Options.BeginingOfTimeRange != null)
                Conditions = Conditions.AndAlso(project =>  project.DevelopmentTime
                    .StartDate >= Options.BeginingOfTimeRange);

            if (Options.EndOfTimeRange != null)
                Conditions = Conditions.AndAlso(project => project.DevelopmentTime
                    .FinishDate <= Options.EndOfTimeRange);
            #endregion assembly conditions

            var BeginQuery = _context.Projects
                .Where(Conditions)
                .QueryGetProject(_context.Employees)
                .Skip(page * PAGE_SIZE)
                .Take(PAGE_SIZE);

            IQueryable<ProjectManagerDto> MiddleQuery;

            switch (Options.SubjectOfSorting)
            {

                case SubjectOfSorting.None:
                    if (Options.SortDirection)
                    {
                        MiddleQuery = BeginQuery.OrderBy(p=>p.Project.Id);
                    }
                    else
                    {
                        MiddleQuery = BeginQuery.OrderByDescending(p => p.Project.Id);
                    }
                    break;

                case SubjectOfSorting.Title:
                    if (Options.SortDirection)
                    {
                        MiddleQuery = BeginQuery.OrderBy(p => p.Project.Title);
                    }
                    else
                    {
                        MiddleQuery = BeginQuery.OrderByDescending(p => p.Project.Title);
                    }
                    break;

                case SubjectOfSorting.ExecuterCompany:
                    if (Options.SortDirection)
                    {
                        MiddleQuery = BeginQuery.OrderBy(p => p.Project.Company.Executer);
                    }
                    else
                    {
                        MiddleQuery = BeginQuery.OrderByDescending(p => p.Project.Company.Executer);
                    }
                    break;


                case SubjectOfSorting.ConsumerCompany:
                    if (Options.SortDirection)
                    {
                        MiddleQuery = BeginQuery.OrderBy(p => p.Project.Company.Consumer);
                    }
                    else
                    {
                        MiddleQuery = BeginQuery.OrderByDescending(p => p.Project.Company.Consumer);
                    }
                    break;

                case SubjectOfSorting.DateOfStart:
                    if (Options.SortDirection)
                    {
                        MiddleQuery = BeginQuery.OrderBy(p => p.Project.DevelopmentTime.StartDate);
                    }
                    else
                    {
                        MiddleQuery = BeginQuery.OrderByDescending(p => p.Project.DevelopmentTime.StartDate);
                    }
                    break;

                case SubjectOfSorting.DateOfFinish:
                    if (Options.SortDirection)
                    {
                        MiddleQuery = BeginQuery.OrderBy(p => p.Project.DevelopmentTime.FinishDate);
                    }
                    else
                    {
                        MiddleQuery = BeginQuery.OrderByDescending(p => p.Project.DevelopmentTime.FinishDate);
                    }
                    break;


                case SubjectOfSorting.Priority:
                    if (Options.SortDirection)
                    {
                        MiddleQuery = BeginQuery.OrderBy(p => p.Project.Priority.Value);
                    }
                    else
                    {
                        MiddleQuery = BeginQuery.OrderByDescending(p => p.Project.Priority.Value);
                    }
                    break;

                default:
                    MiddleQuery=BeginQuery;
                    break;
            }

            return (await MiddleQuery
                .Select(p => new
                {
                    p.Project,
                    p.ManagerId,
                    p.ManagerFirstName,
                    p.ManagerSecondName,
                    p.ManagerMiddleName,
                    TotalCount = _context.Employees.Count()
                })
                .ToArrayAsync())
                .AsEnumerable()
                .Select(p => 
                (
                    p.Project,
                    p.ManagerId,
                    string.Concat(p.ManagerFirstName, " ", p.ManagerSecondName, " ", p.ManagerMiddleName),
                    p.TotalCount
                ))
                .ToArray();
        }
    }
}
