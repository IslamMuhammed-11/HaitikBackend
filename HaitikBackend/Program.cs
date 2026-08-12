using HaitikBackend.API.Hubs;
using HaitikBackend.Application;
using HaitikBackend.Infrastructure;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();


builder.Services.AddInfrastructure();
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

app.MapControllers();

app.UseMiddleware<HaitikBackend.Middleware.GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.UseHangfireDashboard();

app.MapHangfireDashboard("/hangfire");


app.MapHub<DriverTrackingHub>("/hubs/driver-tracking");

app.Run();
