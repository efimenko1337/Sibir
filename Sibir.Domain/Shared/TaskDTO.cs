using Sibir.Domain.Models.ValueObject.ForEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = Sibir.Domain.Models.EntityObject;

namespace Sibir.Domain.Shared;

public class TaskDetailsDto
{
    public Model.Task Task { get; set; } = null!;
    public Guid CreatorId { get; set; }
    public Name CreatorName { get; set; } = null!;
    public Guid? ExecuterId { get; set; }
    public Name? ExecuterName { get; set; } 
    public Guid ProjectId { get; set; }
    public string ProjectTitle { get; set; } = null!;
}

public class TaskDetailsDtoWithPageCount : TaskDetailsDto
{
    public int TotalCount { get; set; }
}
