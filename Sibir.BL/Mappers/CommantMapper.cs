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
    public static class CommentMapper
    {
        public static Result<Comment,Error> ViewModelToDomain(CommentViewModel viewModel) =>
            Comment.Create(viewModel.Comment);

        public static CommentViewModel DomainToViewModel(Comment domain) =>
        new
        (
            domain.Value ?? ""
        );
    }
}
