using HaitikBackend.Application.Abstractions;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.DomainServices.OrderAssignment;
using HaitikBackend.Domain.DomainServices.OrderAssignmentService;
using HaitikBackend.Infrastructure.Implementaions;
using HaitikBackend.Infrastructure.Presistence;
using HaitikBackend.Infrastructure.Presistence.UnitOfWork;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BackgroundJobsImpl = HaitikBackend.Infrastructure.BackgroundJobs.BackgroundJobs;

namespace HaitikBackend.Infrastructure;

public static class DepandencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        services.AddDbContext<HaitikDbContext>(options =>
            options.UseSqlServer("Server=.;Database=HaitikDB;User Id=sa; Password=sa123456;TrustServerCertificate=True;",
                opt => opt.UseNetTopologySuite()));

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Services
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<ITrackingTokenGenerator, TrackingTokenGenerator>();
        services.AddSingleton<ITrackingTokenHasher, TrackingTokenHasher>();
        services.AddScoped<IPhoneNumberChecker, PhoneNumberChecker>();
        services.AddScoped<IOtpGenerator, OtpGenerator>();
        services.AddScoped<IFileStorage, FileStorage>();
        services.AddScoped<IDocumentImporter, DocumentImporter>();
        services.AddScoped<IOrderAssignmentService, OrderAssignmentService>();
        services.AddScoped<IBackgroundJobs, BackgroundJobsImpl>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IOrderOwnershipService, OrderOwnershipService>();

        services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));

        // Hangfire
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseRecommendedSerializerSettings()
            .UseSimpleAssemblyNameTypeSerializer()
            .UseSqlServerStorage("Server=.;Database=HaitikDB;User Id=sa; Password=sa123456;TrustServerCertificate=True;"));

        services.AddHangfireServer();



        return services;
    }
}
