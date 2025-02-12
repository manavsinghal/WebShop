#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

using Azure.Monitor.OpenTelemetry.AspNetCore;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.UseCryptography("appsettings");

builder.Logging.AddLog4Net()
			   .AddConfiguration(SHAREDKERNALLIB.AppSettings.Config.GetSection("Logging"));

// Setup logging to be exported via OpenTelemetry
builder.Logging.AddOpenTelemetry(logging =>
{
	logging.IncludeFormattedMessage = true;
	logging.IncludeScopes = true;
});

if (!String.IsNullOrEmpty(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]))
{
	// Add OpenTelemetry and configure it to use Azure Monitor.
	builder.Services.AddOpenTelemetry().UseAzureMonitor();
}
builder.Services.AddOpenTelemetry().WithTracing(builder => builder.AddOtlpExporter());
builder.Services.AddOpenTelemetry().WithMetrics(builder => builder.AddOtlpExporter());

var startup = new INFRAWEBSERVICESERVICES.Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, builder.Environment);



