using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace HaitikBackend.Application.Behaviors;

public class PerformanceBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{

    private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;
    const short LatencyMessuare = 1000;
    public PerformanceBehavior(ILogger<PerformanceBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {



        var stopwatch = new Stopwatch();

        stopwatch.Start();

        var response = await next();

        stopwatch.Stop();

        var ms = stopwatch.ElapsedMilliseconds;

        if (ms > LatencyMessuare)
        {
            string requestName = typeof(TRequest).Name;

            _logger.LogWarning("The Request {requestName} Took {ms} ms", requestName, ms);
        }

        return response;
    }
}
