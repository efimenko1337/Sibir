using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sibir.Domain.Shared.ViewModels
{
    public record TaskViewViewModel
    (
        Guid Id,
        TitleViewModel Title,
        PriorityViewModel Priority,
        CommentViewModel Comment,
        StatusViewModel Status,
        Guid CreatorId,
        EmployeeNameViewModel CreatorName,
        Guid ExecuterId,
        EmployeeNameViewModel ExecuterName,
        Guid ProjectId,
        TitleViewModel ProjectTitle
    );
}
