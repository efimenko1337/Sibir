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
    public static class RoleMapper
    {
        public static Result<Role,Error> ViewModelToDomain(RoleViewModel viewModel) =>
            Role.Create(viewModel.Role);

        public static RoleViewModel DomainToViewModel(Role domain) =>
        new
        (
            domain.Value
        );
    }
}
