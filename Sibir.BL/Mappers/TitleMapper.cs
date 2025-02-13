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
    public static class TitleMapper
    {
        public static Result<Title, Error> ViewModelToDomain(TitleViewModel viewModel) =>
            Title.Create(viewModel.Title);

        public static TitleViewModel DomainToViewModel(Title domain) =>
        new
        (
            domain.Value
        );
    }
}
