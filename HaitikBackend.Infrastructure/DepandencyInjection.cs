using HaitikBackend.Application.Common.Interfaces;
using HaitikBackend.Application.Common.Interfaces.BackgroundJobs;
using HaitikBackend.Application.Common.Interfaces.FileUpload;
using HaitikBackend.Application.Common.Interfaces.Import.ImporterFactory;
using HaitikBackend.Application.Common.Interfaces.OrderAssignment;
using HaitikBackend.Application.Common.Interfaces.OTP;
using HaitikBackend.Application.Common.Interfaces.PhoneNumberChecker;
using HaitikBackend.Application.Common.Interfaces.Security;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using HaitikBackend.Infrastructure.Email;
using HaitikBackend.Infrastructure.Presistence;
using HaitikBackend.Infrastructure.Presistence.UnitOfWork;
using HaitikBackend.Infrastructure.Services;
using HaitikBackend.Infrastructure.Services.FileStorage;
using HaitikBackend.Infrastructure.Services.Import;
using HaitikBackend.Infrastructure.Services.Notification;
using HaitikBackend.Infrastructure.Services.OrderAssignment;
using HaitikBackend.Infrastructure.Services.OTP;
using HaitikBackend.Infrastructure.Services.PhoneNumber;
using HaitikBackend.Infrastructure.Services.Security;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BackgroundJobsImpl = HaitikBackend.Infrastructure.BackgroundJobs.BackgroundJobs;

namespace HaitikBackend.Infrastructure;

public static class DepandencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services , IConfiguration configuration)
    {
        // Database
        services.AddDbContext<HaitikDbContext>(options =>
            options.UseSqlServer("Server=.;Database=HaitikDB;User Id=sa; Password=sa123456;TrustServerCertificate=True;",
                opt => opt.UseNetTopologySuite()));

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Services
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IPhoneNumberChecker, PhoneNumberChecker>();
        services.AddScoped<IOtpGenerator, OtpGenerator>();
        services.AddScoped<IFileStorage, FileStorage>();
        services.AddScoped<IDocumentImporterFactory, DocumentImporterFactory>();
        services.AddScoped<IOrderAssignmentService, OrderAssignmentService>();
        services.AddScoped<IBackgroundJobs, BackgroundJobsImpl>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IEmailService, EmailService>();

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
