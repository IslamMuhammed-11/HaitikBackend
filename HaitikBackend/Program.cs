using HaitikBackend.API.Hubs;
using HaitikBackend.Application;
using HaitikBackend.Application.Abstractions;
using HaitikBackend.Authorization;
using HaitikBackend.Infrastructure;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter()
        );
    });
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    // Reference the previously defined "Bearer" security scheme.
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },

                // No scopes are required for JWT Bearer authentication.
                // This array is empty because JWT does not use OAuth scopes here.
                new string[] { }
            }
        });

    //options.OperationFilter<SwaggerAuthorizationOperationFilter>();

});

var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("Jwt:Key is not configured.");
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "HaitikBackend";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "HaitikBackendUsers";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtKey)),

            ValidateIssuer = true,

            ValidIssuer = jwtIssuer,

            ValidateAudience = true,

            ValidAudience = jwtAudience,

            ValidateLifetime = true,

            ClockSkew = TimeSpan.Zero,

            NameClaimType = ClaimTypes.NameIdentifier,

            RoleClaimType = ClaimTypes.Role
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.AddPolicy(AuthorizationPolicies.Admin, policy => policy.RequireRole("admin"));
    options.AddPolicy(AuthorizationPolicies.Driver, policy => policy.RequireRole("admin", "driver"));
    options.AddPolicy(AuthorizationPolicies.Agency, policy => policy.RequireRole("admin", "agency"));
    options.AddPolicy(AuthorizationPolicies.AgencyOwnership, policy =>
        policy.RequireAuthenticatedUser().Requirements.Add(new AgencyOwnershipRequirement()));
});
builder.Services.AddScoped<IAuthorizationHandler, AgencyOwnershipHandler>();

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter(GetRateLimitPartition(context), _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 100,
            Window = TimeSpan.FromMinutes(1),
            QueueLimit = 0
        }));
    options.AddPolicy("public", context =>
        RateLimitPartition.GetFixedWindowLimiter(GetRateLimitPartition(context), _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 30,
            Window = TimeSpan.FromMinutes(1),
            QueueLimit = 0
        }));
});

builder.Services.AddSignalR();
builder.Services.AddSingleton<IOrderTrackingNotifier, OrderTrackingNotifier>();


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<HaitikBackend.Middleware.GlobalExceptionMiddleware>();

app.UseRateLimiter();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


app.UseHangfireDashboard();

app.MapHangfireDashboard("/hangfire");


app.MapHub<DriverTrackingHub>("hubs/driver-tracking");
app.MapHub<OrderTrackingHub>("hubs/order-tracking");

app.Run();

static string GetRateLimitPartition(HttpContext context)
{
    if (context.User.Identity?.IsAuthenticated == true)
        return $"user:{context.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "unknown"}";

    return $"ip:{context.Connection.RemoteIpAddress?.ToString() ?? "unknown"}";
}
