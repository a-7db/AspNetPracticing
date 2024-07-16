using AspNetPracticing.WebAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;

namespace AspNetPracticing.WebAPI.ProgramServices
{
    public static class ErrorResponseService
    {
        public static ApiBehaviorOptions ErrorResult(this ApiBehaviorOptions options)
        {
            options.InvalidModelStateResponseFactory = httpContent =>
            {
                List<string> errors = httpContent.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

                return new BadRequestObjectResult(new ErrorModel
                {
                    Title = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = errors
                });
            };

            return options;
        }
    }
}
