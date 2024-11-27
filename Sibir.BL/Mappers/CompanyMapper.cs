using CSharpFunctionalExtensions;
using Sibir.Domain.Models.ValueObject.ForProject;
using Sibir.Domain.Shared;
using Sibir.Domain.Shared.ViewModels;


namespace Sibir.BL.Mappers
{
    public static class CompanyMapper
    {
        public static Result<Company, Error> ViewModelToDomain(ComapnyViewModel viewModel) =>
            Company.Create(viewModel.CompanyExecuter, viewModel.CompanyConsumer);

        public static ComapnyViewModel DomainToViewModel(Company domain) =>
            new 
            (
                CompanyConsumer: domain.Consumer,
                CompanyExecuter: domain.Executer
            );

    }
}
