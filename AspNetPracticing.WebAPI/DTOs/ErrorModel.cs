using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AspNetPracticing.WebAPI.DTOs
{
    public class ErrorModel
    {
        public string Title { get; set; } = string.Empty;
        public HttpStatusCode StatusCode { get; set; }
        public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();
    }
}
