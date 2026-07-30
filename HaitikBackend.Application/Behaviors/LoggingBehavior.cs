using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace HaitikBackend.Application.Behaviors;

internal class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{

    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        var stopwatch = new Stopwatch();

        stopwatch.Start();

        _logger.LogInformation("Handling Request {request}", requestName);

        var response = await next();

        stopwatch.Stop();

        _logger.LogInformation("Handled Request {requestName} int {ms} ms", requestName, stopwatch.ElapsedMilliseconds);

        return response;
    }
}
