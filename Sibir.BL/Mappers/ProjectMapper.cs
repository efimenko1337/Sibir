using CSharpFunctionalExtensions;
using Sibir.BL.ViewModels;
using Sibir.Domain.Models.ValueObject.ForProject;
using Sibir.Domain.Shared;
using Model = Sibir.Domain.Models.EntityObject;

namespace Sibir.BL.Mappers
{
    internal static class ProjectMapper
    {
        public static Result<Model.Project,Error> ViewModelToDomain(ProjectCreateViewModel viewModel)
        {
            var ProjectValue = new List<Result<object, Error>>
            {
                Title.Create(viewModel.Title),
                CompanyMapper.ViewModelToDomain(new (viewModel.CompanyExecuter,viewModel.CompanyConsumer)),
                DevelopmentTimeMapper.ViewModelToDomain(new (viewModel.DateOfStart, viewModel.DateOfFinish)),
                Priority.Create(viewModel.Priority)
            };

            foreach (var item in ProjectValue)
            {
                if (item.IsFailure)
                    return item.Error;
            }

            return new Model.Project
            {
                Title = (Title)ProjectValue[0].Value,
                Company = (Company)ProjectValue[1].Value,
                DevelopmentTime = (DevelopmentTime)ProjectValue[2].Value,
                Priority = (Priority)ProjectValue[3].Value,
            };
        }

        public static ProjectViewViewModel DomainToViewModel(Model.Project domain,Guid ManagerId, string ManagerName) =>
        new
        (
            Id: domain.Id,
            Tittle: domain.Title.Value,
            ExecuterCompany: domain.Company.Executer,
            ConsumerComapny: domain.Company.Consumer,
            DateOfStart: domain.DevelopmentTime.StartDate.ToString("yyyy-MM-dd"),
            DateOfFinish: domain.DevelopmentTime.FinishDate.ToString("yyyy-MM-dd"),
            Priority: domain.Priority.Value,
            ManagerId: ManagerId,
            ManagerName: ManagerName
        );
    }
}
