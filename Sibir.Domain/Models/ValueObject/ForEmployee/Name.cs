using CSharpFunctionalExtensions;
using Sibir.Domain.Shared;

namespace Sibir.Domain.Models.ValueObject.ForEmployee
{
    public class Name : CSharpFunctionalExtensions.ValueObject
    {
        public readonly static int MAX_LENGHT = 100;  

        public string FirstName { get; } = null!;
        public string SecondName { get; } = null!;
        public string MiddleName { get; } = null!;

        private Name(string firstName,
            string secondName,
            string middleName)
        {
            FirstName = firstName;
            SecondName = secondName;
            MiddleName = middleName;
        }

        public Name() { }

        public static Result<Name,Error> Create(string firstName,
            string secondName,
            string middleName)
        {
            if (!string.IsNullOrWhiteSpace(firstName) ||
                !string.IsNullOrWhiteSpace(secondName) ||
                !string.IsNullOrWhiteSpace(middleName))
                return Errors.General.ValueIsInvalid();

            if (firstName.Length > MAX_LENGHT ||
                secondName.Length > MAX_LENGHT ||
                middleName.Length>MAX_LENGHT)
                return Errors.General.InvalidLength();

            return new Name(firstName, secondName, middleName);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return SecondName;
            yield return MiddleName;
        }

        public override string ToString()
        {
            return string.Concat(SecondName," ",FirstName," ",MiddleName);
        }
    }
}
