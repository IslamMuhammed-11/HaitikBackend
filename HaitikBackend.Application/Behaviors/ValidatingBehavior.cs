using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaitikBackend.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validtor = _validators.ToArray();

        if (validtor.Length == 0)
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(validtor.Select(e => e.ValidateAsync(context, cancellationToken)));

        var faliures = validationResults.SelectMany(e => e.Errors).Where(fail => fail is not null);

        //if(faliures.Any())
        //Throw or Handle

        return await next();
    }
}
