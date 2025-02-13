using CSharpFunctionalExtensions;
using Sibir.Domain.Models.ValueObject.ForEmployee;
using Sibir.Domain.Shared;
using Sibir.Domain.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sibir.BL.Mappers
{
    public static class NameMapper
    {
        public static Result<Name,Error> ViewModelToDomain(EmployeeNameViewModel viewModel) =>
             Name.Create(viewModel.FirstName,
                 viewModel.SecondName,
                 viewModel.MiddleName);

        public static EmployeeNameViewModel DomainToViewModel(Name domain) =>
        new
        (
            domain.FirstName,
            domain.SecondName,
            domain.MiddleName
        );

    }
}
