using CSharpFunctionalExtensions;
using Sibir.Domain.Models.ValueObject.ForProject;
using Sibir.Domain.Shared;
using Sibir.Domain.Shared.ViewModels;

namespace Sibir.BL.Mappers
{
    public static class DevelopmentTimeMapper
    {
        public static Result<DevelopmentTime, Error> ViewModelToDomain(DevelopmentTimeViewModel viewModel)
        {
            if (DateOnly.TryParse(viewModel.DateOfStart, out var DateOfStart) &&
                DateOnly.TryParse(viewModel.DateOfFinish, out var DateOfFinish))
            {
                return DevelopmentTime.Create(DateOfStart, DateOfFinish);
            }
            else
                return new Error("400", "Failur date format.");
        }

        public static DevelopmentTimeViewModel DomainToViewModel(DevelopmentTime domain) =>
        new 
        (
            domain.StartDate.ToString("yyyy-MM-dd"),
            domain.FinishDate.ToString("yyyy-MM-dd")
        );
    }
}
