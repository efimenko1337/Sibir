using CSharpFunctionalExtensions;
using Sibir.Domain.Models.EntityObject;
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
    public static class EmployeeMapper
    {
        public static Result<Employee, Error> ViewModelToDoamin(EmployeeCreateViewModel viewModel)
        {
            var EmployeeValue = new List<Result<object, Error>>
            {
                NameMapper.ViewModelToDomain(viewModel.Name),
                EmailMapper.ViewModelToDomain(viewModel.Email),
                RoleMapper.ViewModelToDomain(viewModel.Role),
            };

            foreach (var item in EmployeeValue)
                if (item.IsFailure)
                    return item.Error;

            return new Employee
            {
                Name = (Name)EmployeeValue[0].Value,
                Email = (Email)EmployeeValue[1].Value,
                Role = (Role)EmployeeValue[2].Value,
            };
        }

        public static EmployeeViewViewModel DomainToViewModel(Employee doamin) =>
        new
        (
            doamin.Id,
            NameMapper.DomainToViewModel(doamin.Name),
            EmailMapper.DoaminToVewModel(doamin.Email),
            RoleMapper.DomainToViewModel(doamin.Role)
        );
    }
}
