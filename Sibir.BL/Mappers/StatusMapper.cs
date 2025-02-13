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
    public static class StatusMapper
    {
        public static Result<Status, Error> ViewModelToDomain(StatusViewModel viewModel) =>
            Status.Create(viewModel.Status);

        public static StatusViewModel DomainToViewModel(Status domain) =>
        new
        (
            domain.Value
        );
    }
}
