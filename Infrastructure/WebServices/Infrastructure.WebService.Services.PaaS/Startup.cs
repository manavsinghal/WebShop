namespace Accenture.WebShop.Infrastructure.WebService.Services.PaaS;

/// <summary>
/// Represents Startup class
/// </summary>
public class Startup
{
	#region Properties

	/// <summary>
	/// Represents ConfigRoot
	/// </summary>
	public IConfiguration ConfigRoot
	{
		get;
	}

	#endregion

	#region Constructors

	/// <summary>
	/// Represents Startup class constructor
	/// </summary>
	/// <param name="configuration"></param>
	public Startup(IConfiguration configuration)
	{
		ConfigRoot = configuration;
	}

	#endregion

	#region Methods

	/// <summary>
	/// Represents ConfigureServices method
	/// </summary>
	/// <param name="services"></param>
	public void ConfigureServices(IServiceCollection services)
	{
		_ = services.AddSignalR();
		
		var PolicyOrigins = SHAREDKERNALLIB.AppSettings.GetValue<String>("PolicyOrigins")
											  .Split(",", StringSplitOptions.RemoveEmptyEntries |
														  StringSplitOptions.TrimEntries);

		_ = services.AddCors(options => options.AddPolicy("AllowAllOrigins",
								 builder => builder.WithOrigins(PolicyOrigins)
												   .SetIsOriginAllowedToAllowWildcardSubdomains()
												   .AllowCredentials()
												   .AllowAnyHeader()
												   .AllowAnyMethod()));

		var isADIntegrationEnabled = SHAREDKERNALLIB.AppSettings.IsADIntegrationEnabled;

		if (isADIntegrationEnabled)
		{
			if (ConfigRoot["AzureAD:ClientId"] != null)
			{
				_ = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
						.AddMicrosoftIdentityWebApi(ConfigRoot, "AzureAD");
				_ = services.AddAuthorization();
			}
			else
			{
				isADIntegrationEnabled = false;
			}
		}

		var provider = ConfigRoot["DataProvider"];
		services.AddDataCacheServices();

		if (!String.IsNullOrEmpty(provider) && provider.ToUpper() == "SQLSERVER")
		{
			services.RegisterSqlServices();
			_ = services.AddHealthChecks()
			.AddTypeActivatedCheck<SqlConnectionHealthCheck>("SqlConnectionHealthCheck",
			[SHAREDKERNALLIB.AppSettings.Config.GetConnectionString("WebShopDbContext")!]);
		}
		else
		{
			//services.RegisterPostgreServices();
		}

		_ = services.Configure<HealthCheckPublisherOptions>(options =>
		{
			options.Delay = TimeSpan.FromSeconds(20);
			options.Period = TimeSpan.FromMinutes(3);
		});

		_ = services.AddSingleton<IHealthCheckPublisher, HealthCheckPublisher>()
					.AddHealthChecks()
					.AddCheck("PublisherHealthCheck", () => HealthCheckResult.Healthy("Publisher health check is healthy"))
					.AddCheck<CpuHealthCheck>("CpuHealthCheck", HealthStatus.Healthy)
					.AddCheck<ApiHealthCheck>("ApiHealthCheck", HealthStatus.Healthy)
					.AddCheck<MemoryHealthCheck>("MemoryHealthCheck", HealthStatus.Healthy)
					.AddCheck<RadisHealthCheck>("RadisHealthCheck", HealthStatus.Healthy);


		_ = services.AddHealthChecks()
			.AddTypeActivatedCheck<StorageProviderHealthCheck>("StorageProviderHealthCheck",
																[SHAREDKERNALLIB.AppSettings.AzureBlobStorageConnectionString,
																 SHAREDKERNALLIB.AppSettings.AzureBlobStorageContainerName,
																 SHAREDKERNALLIB.AppSettings.AzureBlobfilepath]);

		services.RegisterDataServices();
		//_ = services.AddSingleton<COREAPPINTERFACESMSGBUS.IMessageBus, COREAPPMODELSMSGBUS.MessageBus>();

		//_ = services.AddScoped<COREAPPINTERFACESCOMMUNICATION.IEmailCommunicationClient, SHAREDBACKENDSERVICESLIB.EmailCommunicationClient>();

		services.RegisterServices();

		_ = services.AddHealthChecks();

		_ = services.AddHttpClient();

		_ = services.AddControllers().AddOData(opt => opt.Select().Expand().Filter().OrderBy());

		_ = services.AddControllers().AddJsonOptions(jsonOptions =>
			{
				jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
				jsonOptions.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
			});

		_ = services.AddResponseCompression();

		_ = services.AddApiVersioning(ver =>
			{
				ver.DefaultApiVersion = new ApiVersion(1, 0);
				ver.AssumeDefaultVersionWhenUnspecified = true;
				ver.ReportApiVersions = true;
			});

		_ = services.AddEndpointsApiExplorer();
		_ = services.AddSwaggerGen();

		_ = services.AddRateLimiter(options =>
		{
			options.AddPolicy<string, AEHRateLimiter>("aeh");

			//options.AddPolicy<AEHRateLimiter>("aeh", (policy) =>
			//{
			//    return new AEHRateLimiter(builder.Configuration, myOptions);
			//});
		});

		_ = services.AddOpenApi();
	}

	/// <summary>
	/// Represents Configure method
	/// </summary>
	/// <param name="app"></param>
	/// <param name="env"></param>
	public void Configure(WebApplication app, IWebHostEnvironment env)
	{
		if (!String.IsNullOrEmpty(SHAREDKERNALLIB.AppSettings.ApplicationSuffix))
		{
			_ = app.UsePathBase(SHAREDKERNALLIB.AppSettings.ApplicationSuffix);
		}
		//if (env.IsDevelopment())
		//{
			_ = app.UseSwagger();
			_ = app.UseSwaggerUI();
		//}

		_ = app.MapScalarApiReference();
		_ = app.UseCors("AllowAllOrigins");

		if (SHAREDKERNALLIB.AppSettings.IsADIntegrationEnabled)
		{
			_ = app.UseAuthentication();
			_ = app.UseRouting();
			_ = app.UseAuthorization();
			_ = app.UseHttpsRedirection();
			_ = app.MapControllers();
		}
		else
		{
			_ = app.UseRouting();
			_ = app.UseAuthorization();
			_ = app.UseHttpsRedirection();
			_ = app.MapControllers().WithMetadata(new AllowAnonymousAttribute());
		}

		var options = new RewriteOptions()
		  .Add(INFRAWEBSERVICESHAREDLIB.EntityViewRewriteRule.RewriteRequest);

		_ = app.UseRewriter(options);

		_ = app.MapHealthChecks("health", new HealthCheckOptions
		{
			ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
		});

		_ = app.MapControllers().RequireRateLimiting("aeh");

		_ = app.UseRateLimiter();

		app.Run();
	}

	#endregion
}


