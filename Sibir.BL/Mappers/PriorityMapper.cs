using CSharpFunctionalExtensions;
using Sibir.Domain.Models.ValueObject.ForTask;
using Sibir.Domain.Shared;
using Sibir.Domain.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sibir.BL.Mappers
{
    public static class PriorityMapper
    {
        public static Result<Priority, Error> ViewModelToDomain(PriorityViewModel viewModel) =>
            Priority.Create(viewModel.Priority);

        public static PriorityViewModel DomainToViewModel(Priority domain) =>
        new
        (
            domain.Value
        );

    }
}
