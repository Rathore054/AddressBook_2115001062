using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using NLog.Config;
using NLog.Web;
using BusinessLayer.Mapping;
using NLog;
using RepositoryLayer.Context;
using System;
using BusinessLayer.Interface;

using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Middleware.Authenticator;
using System.Text;
using Microsoft.Extensions.Options;using FluentValidation.AspNetCore;
using FluentValidation;
using AutoMapper;
using Middleware.Email;


var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();
try
{
    logger.Info("Application is starting...");
    var builder = WebApplication.CreateBuilder(args);

    // Database Connection
    builder.Services.AddDbContext<ContextRL>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 41))));
    //dependency injection
    builder.Services.AddScoped<IAddressBookBL, AddressBookBL>();
    builder.Services.AddScoped<IAddressBookRL, AddressBookRL>();
    builder.Services.AddScoped<IUserBookBL, UserBookBL>();
    builder.Services.AddScoped<IUserBookRL, UserBookRL>();
    builder.Services.AddScoped<JWTToken>();
    builder.Services.AddScoped<EmailService>();
    // Add services to the container
    builder.Services.AddControllers();
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    // Add AutoMapper
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // Add FluentValidation
    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddValidatorsFromAssemblyContaining<EntryValidator>(); // Ensure Validator is correctly registered



    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Application startup failed.");
    throw;
}
finally
{
    LogManager.Shutdown();
}