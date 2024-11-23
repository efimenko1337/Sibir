using CSharpFunctionalExtensions;
using Sibir.Domain.Shared;

namespace Sibir.Domain.Models.ValueObject.ForProject
{
    public class Company : CSharpFunctionalExtensions.ValueObject
    {
        public static readonly int MAX_LENGHT = 100;

        public string Executer { get; } = null!;
        public string Consumer { get; } = null!;

        private Company(string executer, string consumer)
        {
            Executer= executer;
            Consumer= consumer;
        }
        public Company() { }

        public static Result<Company,Error> Create(string executer, string consumer)
        {
            if (string.IsNullOrWhiteSpace(executer) || 
                string.IsNullOrWhiteSpace(consumer))
                return Errors.General.ValueIsRequired();
            if (executer.Length>MAX_LENGHT || 
                consumer.Length>MAX_LENGHT)
                return Errors.General.InvalidLength();
            return new Company(executer, consumer);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Executer;
            yield return Consumer;
        }
    }
}
