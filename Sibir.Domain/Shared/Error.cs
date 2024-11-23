namespace Sibir.Domain.Shared
{
    public record Error(string Code, string Message);

    public static class Errors
    {
        public static class User
        {
            
        }

        public static class General
        {
            public static Error NotFound()=>
                new("404", "Not Found");


            public static Error ValueIsInvalid() =>
                new("400", "Value is invalid");

            public static Error ValueIsRequired() =>
                new("400", "Value is required");

            public static Error InvalidLength()=>
                new("400", $"Invalid length");

        }
    }
}
