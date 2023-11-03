using DataAccess.MsSql;
using DataAccess.Interfaces;
using DomainServices.Implementation;
using DomainServices.Interfaces;
using Email.Interfaces;
using WebApp.Interfaces;
using Infrastructure.MailHandler;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Services;
using UseCases.Order.Utils;
using UseCases;
using ApplicationServices.Implementation;
using ApplicationServices.Interfaces;
using Delivery.Interfaces;
using Delivery.Company;
using Hangfire;
using Mobile.UseCases.Order.BackgroundJobs;
using WebApp.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Domain
builder.Services.AddScoped<IOrderDomainService, OrderDomainService>();

// Infrastructure
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IBackgroundJobService, BackgroundJobService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddDbContext<IDbContext, AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CleanArchitecture"));
});
builder.Services.AddScoped<IDeliveryService, DeliveryService>();

// Application
builder.Services.AddApplicationServices();
builder.Services.AddScoped<ISecurityService, SecurityService>();

// Frameworks
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("CleanArchitecture")));
builder.Services.AddHangfireServer();

var app = builder.Build();

app.UseExceptionHandlerMiddleware();
app.MapControllers();
app.UseHangfireServer();

RecurringJob.AddOrUpdate<UpdateDeliveryStatusJob>("UpdateDeliveryStatusJob", (job) => job.ExecuteAsync(), Cron.Minutely);

app.Run();
