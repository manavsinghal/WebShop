#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

public class Program
{
	public static void Main()
	{
		var host = new HostBuilder()
			.ConfigureFunctionsWorkerDefaults(builder =>
			{
				builder.UseMiddleware<FunctionContextMiddleware>();

				var isADIntegrationEnabled = SHAREDKERNALLIB.AppSettings.IsADIntegrationEnabled;

				if (isADIntegrationEnabled)
				{
					builder.UseMiddleware<AuthenticationMiddleware>();
					//builder.UseMiddleware<AuthorizationMiddleware>();
				}
			})
			.ConfigureAppConfiguration((context, configBuilder) =>
			{
				var handler = new HttpClientHandler
				{
					SslProtocols = System.Security.Authentication.SslProtocols.Tls12
				};

				configBuilder
					.SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
					.AddEnvironmentVariables();

				configBuilder.UseCryptography("appsettings");
			})
			.ConfigureServices((context, services) =>
			{
				services.AddSignalRCore();

				var PolicyOrigins = SHAREDKERNALLIB.AppSettings.GetValue<String>("PolicyOrigins")
											  .Split(",", StringSplitOptions.RemoveEmptyEntries |
														  StringSplitOptions.TrimEntries);

				_ = services.AddCors(options => options.AddPolicy("AllowAllOrigins",
										 builder => builder.WithOrigins(PolicyOrigins)
														   .SetIsOriginAllowedToAllowWildcardSubdomains()
														   .AllowCredentials()
														   .AllowAnyHeader()
														   .AllowAnyMethod()));

				services.Configure<JsonSerializerOptions>(jsonOptions =>
				{
					jsonOptions.PropertyNamingPolicy = null;
					jsonOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
				});

				var configRoot = context.Configuration;

				AppSettings.Configure(configRoot);

				services.AddLogging(loggingBuilder =>
				{
					loggingBuilder.AddLog4Net("log4net.config");
				});

				var provider = configRoot["DataProvider"];

				services.AddDataCacheServices();

				if (!String.IsNullOrEmpty(provider) && provider.ToUpper() == "SQLSERVER")
				{
					services.RegisterSqlServices();
				}
				else
				{
					//services.RegisterPostgreServices();
				}

				services.AddSingleton<IFunctionContextAccessor, DefaultFunctionContextAccessor>();

				services.RegisterDataServices();
				services.RegisterServices();

				services.AddHealthChecks();

				services.AddHttpClient();

				services.AddResponseCompression();
				services.AddEndpointsApiExplorer();

			})
			.Build();

		host.Run();

	}
}


