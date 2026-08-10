using HaitikBackend.Domain.Interfaces.UnitOfWork;
using HaitikBackend.Infrastructure.Presistence;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HaitikBackend.Infrastructure;

public static class DepandencyInjection
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {

        services.AddDbContext<HaitikDbContext>(options => options.UseSqlServer(opt => opt.UseNetTopologySuite()));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddHangfire(config => config
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseRecommendedSerializerSettings()
        .UseSimpleAssemblyNameTypeSerializer()
        .UseSqlServerStorage("")
        );

        services.AddHangfireServer();


        return services;
    }
}
