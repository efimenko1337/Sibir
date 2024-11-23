using CSharpFunctionalExtensions;
using Sibir.Domain.Shared;

namespace Sibir.Domain.Models.ValueObject.ForTask
{
    public class Comment : CSharpFunctionalExtensions.ValueObject
    {
        public static readonly int MAX_LENGHT = 500;

        public string? Value { get; }

        private Comment(string title) => Value = title;
        public Comment() { }

        public static Result<Comment, Error> Create(string comment)
        {
            if (comment.Length > MAX_LENGHT)
                return Errors.General.ValueIsInvalid();

            return new Comment(comment);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value ?? string.Empty;
        }
    }
}
