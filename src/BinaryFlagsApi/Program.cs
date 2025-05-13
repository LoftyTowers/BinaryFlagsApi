using System;
using System.Reflection;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Configs;
using Core.DTOs;
using Core.Enums;
using Factories;
using Rules;
using Engines;
using Microsoft.AspNetCore.Hosting;

try
{

    var builder = WebApplication.CreateBuilder(args);

    // Configure Serilog
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();

    builder.Host.UseSerilog();

    // Load configuration
    builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

    // Register services
    builder.Services.Configure<FraudRulesConfig>(builder.Configuration.GetSection("FraudRules"));

    // Convention-based registration of rules
    var ruleTypes = Assembly.GetAssembly(typeof(BaseFraudRule<>))!
        .GetTypes()
        .Where(t => typeof(IBaseFraudRule).IsAssignableFrom(t) && !t.IsAbstract && !t.IsGenericType);

    foreach (var type in ruleTypes)
    {
        builder.Services.AddSingleton(typeof(IBaseFraudRule), type);
    }

    // Register the rule engine
    builder.Services.AddSingleton<IFraudRuleEngine>(provider =>
    {
        var rules = provider.GetServices<IBaseFraudRule>();
        return new FraudRuleEngine(rules);
    });

    builder.Services.AddSingleton<IPaymentRuleFactory, PaymentRuleFactory>();


    // Register controllers
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.Configure(builder.Configuration.GetSection("Kestrel"));
    });



    var app = builder.Build();

    // Configure endpoint routing
    app.UseRouting();
    app.UseAuthorization();
    app.UseSwagger();
    app.UseSwaggerUI();


    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Error setting current directory: {ex.Message}");
}
