using FluentValidation;
using HaitikBackend.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HaitikBackend.Application;

public static class DepandencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DepandencyInjection).Assembly;

        // MediatR — registers all IRequestHandlers, INotificationHandlers, etc.
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        // MediatR Pipeline Behaviors (order matters: first registered = outermost)
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        // FluentValidation — registers all AbstractValidator<T> from this assembly
        services.AddValidatorsFromAssembly(assembly);

        // AutoMapper — registers all Profile subclasses from this assembly
        services.AddAutoMapper(cfg => { } ,assembly);

        

        return services;
    }
}
