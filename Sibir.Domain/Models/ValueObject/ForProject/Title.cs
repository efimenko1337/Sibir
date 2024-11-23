using CSharpFunctionalExtensions;
using Sibir.Domain.Shared;

namespace Sibir.Domain.Models.ValueObject.ForProject
{
    public class Title: CSharpFunctionalExtensions.ValueObject
    {
        public static readonly int MAX_LENGHT = 200;

        public string Value { get; } = null!;

        private Title(string title)  => Value = title;
        public Title() { }

        public static Result<Title,Error> Create(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return Errors.General.ValueIsInvalid();
            if(title.Length > MAX_LENGHT)
                return Errors.General.ValueIsInvalid();

            return new Title(title);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
