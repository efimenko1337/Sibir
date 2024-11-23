using CSharpFunctionalExtensions;
using Sibir.Domain.Shared;

namespace Sibir.Domain.Models.ValueObject.ForProject
{
    public class DevelopmentTime : CSharpFunctionalExtensions.ValueObject
    {
        public DateOnly StartDate { get; }
        public DateOnly FinishDate { get; }

        private DevelopmentTime(DateOnly startDate,DateOnly finishDate)
        {
            StartDate=startDate;
            FinishDate=finishDate;
        }
        public DevelopmentTime() { }

        public static Result<DevelopmentTime, Error> Create(DateOnly startDate, DateOnly finishDate)
        {
            if (finishDate < DateOnly.FromDateTime(DateTime.Now))
                return new Error("400", "The end date of development cannot be set earlier than today");
            if(finishDate<startDate)
                return new Error("400", "The end date of development cannot be earlier than the start date of development");

            return new DevelopmentTime(startDate, finishDate);
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return StartDate;
            yield return FinishDate!;
        }
    }
}
