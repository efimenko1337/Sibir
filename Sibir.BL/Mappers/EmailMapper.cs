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
    public static class EmailMapper
    {
        public static Result<Email, Error> ViewModelToDomain(EmailViewModel viewModel) =>
            Email.Create(viewModel.Email);

        public static EmailViewModel DoaminToVewModel(Email domain) =>
        new
        (
            domain.Value
        );
    }
}
