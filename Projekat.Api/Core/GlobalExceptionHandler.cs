using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Projekat.Application.Exceptions;
using System.Threading.Tasks;
using System;
using Projekat.Application.Logging;
using Microsoft.Extensions.Logging;
using FluentValidation;
using System.Linq;

namespace Projekat.Api.Core
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionLogger _logger;
        public GlobalExceptionHandler(RequestDelegate next, IExceptionLogger logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);

                httpContext.Response.ContentType = "application/json";
                object response = null;
                var statusCode = StatusCodes.Status500InternalServerError;

                switch (ex)
                {
                    case ValidationException val:
                        statusCode = StatusCodes.Status422UnprocessableEntity;
                        var errors = val.Errors.Select(x => new
                        {
                            x.PropertyName,
                            x.ErrorMessage
                        });
                        response = new
                        {
                            Message = "Failed due to validation errors.For more details navigate to error property-",
                            errors
                        };
                        break;
                    case EntityNotFoundException _:
                        statusCode = StatusCodes.Status404NotFound;
                        response = new
                        {
                            message = "Resource not found"
                        };
                        break;
                    case UnauthorizedUseCaseException _:
                        statusCode = StatusCodes.Status403Forbidden;
                        response = new
                        {
                            Message = "You are not allowed to execute this operation"
                        };
                        break;
                }
                httpContext.Response.StatusCode = statusCode;
                if (response != null)
                {
                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
                    return;
                }
                await Task.FromResult(httpContext.Response);
            }
        }
    }
}
