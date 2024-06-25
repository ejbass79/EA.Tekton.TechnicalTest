using EA.Tekton.TechnicalTest.Cross.Exceptions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Net.Mime;

namespace EA.Tekton.TechnicalTest.WebApi.Filters;

public class ExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ValidationException validationEx:
                HandleValidationException(context, validationEx);
                break;

            case NotFoundException notFoundEx:
                HandleNotFoundException(context, notFoundEx);
                break;

            case ForbiddenAccessException:
                HandleForbiddenAccessException(context);
                break;

            default:
                HandleUnknownException(context);
                break;
        }

        base.OnException(context);
    }

    private void HandleValidationException(ExceptionContext context, ValidationException exception)
    {
        var details = new ValidationProblemDetails(exception.Errors);

        context.Result = new BadRequestObjectResult(details);

        _logger.LogError(context.Exception, context.Exception.Message);
        _logger.LogError(exception.Message, exception);

        context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
        context.ExceptionHandled = true;
    }

    private void HandleInvalidModelStateException(ExceptionContext context)
    {
        var details = new ValidationProblemDetails(context.ModelState);

        context.Result = new BadRequestObjectResult(details);

        _logger.LogError(context.Exception, context.Exception.Message);

        context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
        context.ExceptionHandled = true;
    }

    private void HandleNotFoundException(ExceptionContext context, NotFoundException exception)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "The specified resource was not found.",
            Detail = exception.Message
        };

        _logger.LogError(context.Exception, context.Exception.Message);

        context.Result = new NotFoundObjectResult(details);
        context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
        context.ExceptionHandled = true;
    }

    private void HandleForbiddenAccessException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status403Forbidden,
            Title = "Forbidden"
        };

        _logger.LogError(context.Exception, context.Exception.Message);

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status403Forbidden
        };

        context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
        context.ExceptionHandled = true;
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }

        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request.",
            Detail = context.Exception.Message
        };

        _logger.LogError(context.Exception, context.Exception.Message);

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
        context.ExceptionHandled = true;
    }
}
