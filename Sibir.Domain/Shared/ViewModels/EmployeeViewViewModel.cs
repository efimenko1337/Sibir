using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sibir.Domain.Shared.ViewModels
{
    public record EmployeeViewViewModel
    (
        Guid Id,
        EmployeeNameViewModel Name,
        EmailViewModel Email,
        RoleViewModel Role
    );
}
