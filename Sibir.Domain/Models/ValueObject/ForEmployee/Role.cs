using CSharpFunctionalExtensions;
using Sibir.Domain.Shared;

namespace Sibir.Domain.Models.ValueObject.ForEmployee
{
    public class Role : CSharpFunctionalExtensions.ValueObject
    {
        private readonly static string Supervisor = nameof(Supervisor);
        private readonly static string ProjectManager = nameof(ProjectManager);
        private readonly static string Employee = nameof(Employee);

        private readonly static string[] _roles = [Supervisor,ProjectManager,Employee];

        public string Value { get; } = null!;

        private Role(string value) => Value=value;
        public Role() { }

        public static Result<Role,Error> Create(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
                return Errors.General.ValueIsInvalid();
            var value=role.Trim().ToLower();
            if(!_roles.Any(r=>string.Equals(role,r,StringComparison.OrdinalIgnoreCase)))
                return Errors.General.ValueIsInvalid();
            return new Role(role);
            
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
