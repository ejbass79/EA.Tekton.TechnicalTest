using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EA.Tekton.TechnicalTest.Cross.Abstractions.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<TRequest> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseCommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var name = request.GetType().Name;

        try
        {
            logger.LogInformation($"Running the request command: `{name}`");

            var result = await next();
            
            logger.LogInformation($"The request command `{name}` was executed successfully");

            return result;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"Did the request command `{name}` have errors");
            
            throw;
        }
    }
}
