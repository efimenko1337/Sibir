using CSharpFunctionalExtensions;
using Sibir.Domain.Models.ValueObject.ForEmployee;
using Sibir.Domain.Models.ValueObject.ForTask;
using Sibir.Domain.Shared;
using Sibir.Domain.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = Sibir.Domain.Models.EntityObject;

namespace Sibir.BL.Mappers
{
    public static class TaskMapper
    {
        public static Result<Model.Task,Error> ViewModelToDomain(TaskCreateViewModel viewModel)
        {
            var TaskValue = new List<Result<object,Error>>
            {
                TitleMapper.ViewModelToDomain(viewModel.Title),
                PriorityMapper.ViewModelToDomain(viewModel.Priority),
                CommentMapper.ViewModelToDomain(viewModel.Comment),
                StatusMapper.ViewModelToDomain(viewModel.Status)
            };

            foreach(var item in TaskValue) 
                if(item.IsFailure)
                    return item.Error;

            return new Model.Task
            {
                Title = (Title)TaskValue[0].Value,
                Priority = (Priority)TaskValue[1].Value,
                Comment = (Comment)TaskValue[2].Value,
                Status = (Status)TaskValue[3].Value,
                CreaterId=viewModel.CreatorId,
                ProjectId=viewModel.ProjectId
            };
        }

        public static TaskViewViewModel DomainToViewModel(Model.Task domain,
            Guid creatorId, Name creatorName,
            Guid? executerId, Name? executerName,
            Guid projectId, string titleProject) =>
        new
        (
            Id: domain.Id,
            Title: TitleMapper.DomainToViewModel(domain.Title),
            Priority: PriorityMapper.DomainToViewModel(domain.Priority),
            Comment: CommentMapper.DomainToViewModel(domain.Comment),
            Status: StatusMapper.DomainToViewModel(domain.Status),
            CreatorId: creatorId,
            CreatorName: NameMapper.DomainToViewModel(creatorName),
            ExecuterId: executerId ?? Guid.Empty,
            ExecuterName: executerName != null ? NameMapper.DomainToViewModel(executerName) : new("undefind", "undefind", "undefind"),
            ProjectId: projectId,
            ProjectTitle: new (titleProject)
        );
        
    }
}
