using CSharpFunctionalExtensions;
using Sibir.Domain.Shared;

namespace Sibir.Domain.Models.ValueObject.ForProject
{
    public class Priority : CSharpFunctionalExtensions.ValueObject
    {
        public static readonly int MAX_VALUE = 10;

        public int Value { get; }

        private Priority(int priority) => Value = priority;
        public Priority() { }

        public static Result<Priority,Error> Create(int priority)
        {
            if (priority > MAX_VALUE && priority < 0)
                return Errors.General.InvalidLength();

            return new Priority(priority);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
