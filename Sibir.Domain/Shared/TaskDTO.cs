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
    public string CreatorFirstName { get; set; } = null!;
    public string CreatorMiddleName { get; set; } = null!;
    public string CreatorSecondName { get; set; } = null!;
    public Guid ExecuterId { get; set; }
    public string ExecuterFirstName { get; set; } = null!;
    public string ExecuterMiddleName { get; set; } = null!;
    public string ExecuterSecondName { get; set; } = null!;
    public Guid ProjectId { get; set; }
    public string ProjectTitle { get; set; } = null!;
}

public class TaskDetailsDtoWithPageCount : TaskDetailsDto
{
    public int TotalCount { get; set; }
}
