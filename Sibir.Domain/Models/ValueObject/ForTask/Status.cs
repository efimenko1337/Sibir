using CSharpFunctionalExtensions;
using Sibir.Domain.Shared;


namespace Sibir.Domain.Models.ValueObject.ForTask
{
    public class Status : CSharpFunctionalExtensions.ValueObject
    {
        private readonly static string ToDo = nameof(ToDo);
        private readonly static string InProgress = nameof(InProgress);
        private readonly static string Done = nameof(Done);

        private readonly static string[] _statuses = [ToDo, InProgress, Done];

        public string Value { get; } = null!;

        private Status(string value) => Value = value;
        public Status() { }

        public static Result<Status, Error> Create(string status= nameof(ToDo))
        {
            if (string.IsNullOrWhiteSpace(status))
                return Errors.General.ValueIsInvalid();
            var value = status.Trim().ToLower();
            if (!_statuses.Any(s => string.Equals(status, s, StringComparison.OrdinalIgnoreCase)))
                return Errors.General.ValueIsInvalid();
            return new Status(status);

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
