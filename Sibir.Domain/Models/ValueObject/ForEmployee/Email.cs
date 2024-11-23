using CSharpFunctionalExtensions;
using Sibir.Domain.Shared;
using System.Text.RegularExpressions;

namespace Sibir.Domain.Models.ValueObject.ForEmployee
{
    public partial class Email: CSharpFunctionalExtensions.ValueObject
    {
        [GeneratedRegex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$")]
        private static partial Regex EmailRegex();
        public static readonly int MAX_LENGHT = 200;


        public string Value { get; } = null!;

        private Email(string mail) => Value = mail;
        public Email() { }

        public static Result<Email,Error> Create(string mail)
        {
            if (string.IsNullOrWhiteSpace(mail))
                return Errors.General.ValueIsRequired();
            if(mail.Length > MAX_LENGHT)
                return Errors.General.InvalidLength();
            if(EmailRegex().IsMatch(mail))
                return Errors.General.ValueIsRequired();
            return new Email(mail);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        
    }
}
