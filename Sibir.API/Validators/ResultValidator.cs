using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Sibir.Domain.Shared;

namespace Sibir.API.Validators
{
    public static class ResultValidator
    {
        public static IActionResult Validate(this Result<object,Error> result)=>
            result.Error.Code switch
            {
                "400" => new BadRequestObjectResult(result.Error.Message),
                "404" => new NotFoundObjectResult(result.Error.Message),
                _ => throw new Exception("Unknown status code"),
            };
    }
}
