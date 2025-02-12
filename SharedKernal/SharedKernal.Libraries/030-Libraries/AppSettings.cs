#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.SharedKernal.Libraries;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Application settings.
/// </summary>
public static class AppSettings
{
	#region Private Static Properties

	private static IConfiguration _config;

	private static Dictionary<String, String>? _serviceUrls;

	#endregion

	#region Constructor

	/// <summary>
	/// Initializes AppSettings.
	/// </summary>
	static AppSettings()
	{
		_config = null!;
	}

	#endregion

	#region Public Static Properties

	/// <summary>
	/// Configure
	/// </summary>
	/// <param name="configuration"></param>
	public static void Configure(IConfiguration configuration)
	{
		_config = configuration;
		_serviceUrls = null!;
	}

	/// <summary>
	/// Gets the connection string.
	/// </summary>
	/// <returns></returns>
	public static String GetConnectionString()
	{
		var value = Config.GetSection("ConnectionStrings").GetSection("WebShopDbContext").Value;
		return value!;
	}

	#region DbQueryConnectionString

	/// <summary>
	/// Gets the query connection string.
	/// </summary>
	/// <returns></returns>
	public static String GetQueryConnectionString()
	{
		var value = Config.GetSection("ConnectionStrings").GetSection("WebShopDbQueryContext").Value;
		return value!;
	}

	#endregion EndDbQueryConnectionString

	/// <summary>
	/// Gets the name of the DefaulDomain.
	/// </summary>
	/// <value>
	/// The name of the DefaulDomain.
	/// </value>
	public static String DefaulDomain
	{
		get
		{
			var value = Config.GetSection("DefaulDomain").Value;

			if (String.IsNullOrEmpty(value))
			{
				value = "accenture.com";
			}

			return value;
		}
	}

	/// <summary>
	/// Gets the machine CurrentUser
	/// </summary>
	/// <value>
	/// The machine CurrentUser
	/// </value>
	public static String CurrentUser => "oneasset-provision@accenture.com"; //$"{System.Environment.UserName ?? "Unknown"}";

	/// <summary>
	/// Gets the machine CurrentUserEmail.
	/// </summary>
	/// <value>
	/// The machine CurrentUserEmail.
	/// </value>
	public static Guid CurrentUserEmail => Guid.Parse("99999999-9999-9999-0010-000000000010"); //oneasset - provision@accenture.com

	/// <summary>
	/// Config
	/// </summary>
	public static IConfiguration Config
	{
		get
		{
			if (_config == null)
			{
				var builder = new ConfigurationBuilder()
							  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
							  .AddEnvironmentVariables()
							  .UseCryptography("appsettings");

				_config = builder.Build();

				var envName = _config.GetSection("Environment").Get<String>();
				var keyVaultUri = _config["KeyVaultUri"];
				var managedIdentity = _config["UserAssignedManagedIdentity"];

				if (keyVaultUri != null && managedIdentity != null && envName != "Sandbox" && envName != "Local")
				{
					var secretClient = new SecretClient(
											new Uri(keyVaultUri),
											new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = managedIdentity }));

					builder = new ConfigurationBuilder()
							  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
							  .AddAzureKeyVault(secretClient, new MyKeyVaultSecretManager())
							  .AddEnvironmentVariables()
							  .UseCryptography("appsettings");

					_config = builder.Build();
				}
			}

			return _config;
		}
	}

	/// <summary>
	/// Gets the name of the ARCAPIBaseUrl.
	/// </summary>
	/// <value>
	/// The name of the ARCAPIBaseUrl.
	/// </value>
	public static String ARCAPIBaseUrl
	{
		get
		{
			var value = Config.GetSection("ARC-APIBaseUrl").Value;
			return value!;
		}
	}

	/// <summary>
	/// Gets the service URL.
	/// </summary>
	/// <param name="key">The key.</param>
	/// <returns></returns>
	public static async Task<String> GetServiceUrl(String key)
	{
		if (_serviceUrls == null)
		{
			var baseUrl = Config.GetSection("BaseUrl").Value;
			var serviceUrlPath = Config.GetSection("ServiceUrlPath").Value;
			String? serviceUrlsData;

			if (baseUrl != null)
			{
				var httpClient = GetHttpClient();
				serviceUrlsData = await httpClient.GetStringAsync(serviceUrlPath).ConfigureAwait(false);
			}
			else
			{
				var currentPath = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName;
				var serviceUrlFullPath = Path.Combine(currentPath!, serviceUrlPath!);
				serviceUrlsData = File.ReadAllText(serviceUrlFullPath);
			}

			_serviceUrls = serviceUrlsData.ToObject<Dictionary<String, String>>()!;
		}

		var serviceUrl = _serviceUrls!.ContainsKey(key) ? _serviceUrls[key] : String.Empty;

		return serviceUrl;
	}

	/// <summary>
	/// Gets the name of the application/datasource.
	/// </summary>
	/// <value>
	/// The name of the application/DataSource.
	/// </value>
	public static String AppName
	{
		get
		{
			var value = Config.GetSection("AppName").Value;

			if (String.IsNullOrEmpty(value))
			{
				value = "WebShop";
			}

			return value;
		}
	}

	/// <summary>
	/// Gets AppWebUrl
	/// </summary>
	/// <value>
	/// The AppWebUrl
	/// </value>
	public static String AppWebUrl
	{
		get
		{
			var value = Config.GetSection("AppWebUrl").Value;

			if (String.IsNullOrEmpty(value))
			{
				value = Environment == "Prod"
					? $"https://reinventionconsole.accenture.com/provision/"
					: $"https://reinventionconsole-{Environment}.accenture.com/provision/";
			}

			return value;
		}
	}

	/// <summary>
	/// IsPrivateMethodNotificationEnabled
	/// </summary>
	/// <returns></returns>
	public static Boolean IsServiceTierNotificationEnabled
	{
		get
		{
			_ = Boolean.TryParse(Config.GetValue<String>("MessageHub:IsServiceTierNotificationEnabled"), out var value);

			return value;
		}
	}

	/// <summary>
	/// IsPrivateMethodNotificationEnabled
	/// </summary>
	/// <returns></returns>
	public static Boolean IsBusinesTierNotificationEnabled
	{
		get
		{
			_ = Boolean.TryParse(Config.GetValue<String>("MessageHub:IsBusinesTierNotificationEnabled"), out var value);

			return value;
		}
	}

	/// <summary>
	/// IsPrivateMethodNotificationEnabled
	/// </summary>
	/// <returns></returns>
	public static Boolean IsDataAccessTierNotificationEnabled
	{
		get
		{
			_ = Boolean.TryParse(Config.GetValue<String>("MessageHub:IsDataAccessTierNotificationEnabled"), out var value);

			return value;
		}
	}

	/// <summary>
	/// IsPrivateMethodNotificationEnabled
	/// </summary>
	/// <returns></returns>
	public static Boolean IsPrivateMethodNotificationEnabled
	{
		get
		{
			_ = Boolean.TryParse(Config.GetValue<String>("MessageHub:IsPrivateMethodNotificationEnabled"), out var value);

			return value;
		}
	}

	/// <summary>
	/// Distributed Cache Provider => SqlServer, Memory, Redis
	/// </summary>
	public static String DataCacheProvider
	{
		get
		{
			var dataCache = Config.GetSection("DataCache");
			var cacheProviderType = dataCache.GetValue<String>("CacheProviderType");

			return cacheProviderType!;
		}
	}

	/// <summary>
	/// Sql Server Connection String for SqlServer Distributed Cache
	/// </summary>
	public static String DataCacheSqlConnectionString
	{
		get
		{
			var dataCache = Config.GetSection("DataCache");
			var sqlConnectionString = dataCache.GetValue<String>("SqlConnectionString");

			return sqlConnectionString!;
		}
	}

	/// <summary>
	/// Sql Server Table Nmae for SqlServer Distributed Cache
	/// </summary>
	public static String DataCacheSqlTableName
	{
		get
		{
			var dataCache = Config.GetSection("DataCache");
			var sqlTableName = dataCache.GetValue<String>("SqlTableName");

			return sqlTableName!;
		}
	}

	/// <summary>
	/// Sql Server Schema Name for SqlServer Distributed Cache
	/// </summary>
	public static String DataCacheSqlSchemaName
	{
		get
		{
			var dataCache = Config.GetSection("DataCache").GetValue<String>("SqlSchemaName");
			return dataCache!;
		}
	}

	/// <summary>
	/// Redis Connection String for Distributed Cache
	/// </summary>
	public static String DataCacheRedisConnectionString
	{
		get
		{
			var dataCache = Config.GetSection("DataCache");
			var redisCacheProvider = dataCache.GetSection("RedisCacheProvider");
			var redisConnectionString = redisCacheProvider.GetValue<String>("RedisConnectionString");

			return redisConnectionString!;
		}
	}

	/// <summary>
	/// Redis Instance for Distributed Cache
	/// </summary>
	public static String DataCacheRedisInstanceName
	{
		get
		{
			var dataCache = Config.GetSection("DataCache");
			var redisCacheProvider = dataCache.GetSection("RedisCacheProvider");
			var redisInstanceName = redisCacheProvider.GetValue<String>("RedisInstanceName");

			return redisInstanceName!;
		}
	}

	/// <summary>
	/// Gets the cache configuration file path.
	/// </summary>
	/// <value>
	/// The cache configuration file path.
	/// </value>
	public static String CachingConfigFilePath
	{
		get
		{
			var obfuscator = Config.GetSection("DataCache");
			var configFilePath = obfuscator.GetValue<String>("ConfigFilePath");

			return configFilePath!;
		}
	}

	/// <summary>
	/// Gets the IsADIntegrationEnabled.
	/// </summary>
	/// <value>
	/// The value of IsADIntegrationEnabled.
	/// </value>
	public static Boolean IsADIntegrationEnabled
	{
		get
		{
			_ = Boolean.TryParse(Config.GetSection("IsADIntegrationEnabled").Value, out var value);

			return value;
		}
	}

	/// <summary>
	/// CurrentApp
	/// </summary>
	public static String CurrentApp { get; set; } = "WebShop";

	/// <summary>
	/// CurrentDateTime
	/// </summary>
	public static DateTime CurrentDateTime { get; set; } = DateTime.UtcNow;

	/// <summary>
	/// CurrentCorrelationUId
	/// </summary>
	public static Guid CurrentCorrelationUId { get; set; } = Guid.NewGuid();

	/// <summary>
	/// Gets the CacheAbsoluteExpiration of the Application.
	/// </summary>
	/// <value>
	/// The CacheAbsoluteExpiration.
	/// </value>
	public static Int32 CacheAbsoluteExpiration
	{
		get
		{
			var value = 0;
			var cacheAbsoluteExpiration = Config.GetSection("CacheAbsoluteExpiration").Value;

			if (!String.IsNullOrEmpty(cacheAbsoluteExpiration))
			{
				value = Int32.Parse(cacheAbsoluteExpiration);
			}

			return value;
		}
	}

	/// <summary>
	/// Gets the CacheSlidingExpiration of the Application.
	/// </summary>
	/// <value>
	/// The CacheSlidingExpiration.
	/// </value>
	public static Int32 CacheSlidingExpiration
	{
		get
		{
			var value = 0;
			var cacheSlidingExpiration = Config.GetSection("CacheSlidingExpiration").Value;

			if (!String.IsNullOrEmpty(cacheSlidingExpiration))
			{
				value = Int32.Parse(cacheSlidingExpiration);
			}

			return value;
		}
	}

	/// <summary>
	/// Gets the ApplicationSuffix of the Application.
	/// </summary>
	/// <value>
	/// The ApplicationSuffix.
	/// </value>
	public static String ApplicationSuffix
	{
		get
		{
			var value = String.Empty;
			var applicationSuffix = Config.GetSection("ApplicationSuffix").Value;

			if (!String.IsNullOrEmpty(applicationSuffix))
			{
				value = applicationSuffix;
			}

			return value;
		}
	}

	/// <summary>
	/// Gets a value indicating whether this instance is fortress configuration available.
	/// </summary>
	/// <value>
	///   <c>true</c> if this instance is fortress configuration available; otherwise, <c>false</c>.
	/// </value>
	public static Boolean IsFortressConfigAvailable
	{
		get
		{
			var fortress = Config.GetSection("Fortress");
			var value = fortress.GetSection("ConfigurationFile").Value;

			return value != null;
		}
	}

	/// <summary>
	/// Gets or sets the TestDataPath path.
	/// </summary>
	/// <value>
	/// The TestDataPath path.
	/// </value>
	public static String TestDataPath
	{
		get
		{
			var value = Config.GetSection("TestDataPath").Value;
			return value!;
		}
	}

	/// <summary>
	/// Gets the log verbosity.
	/// </summary>
	/// <value>
	/// The log verbosity.
	/// </value>
	public static String LogVerbosity
	{
		get
		{
			var value = Config.GetSection("LogVerbosity").Value;

			if (String.IsNullOrEmpty(value))
			{
				value = "Summary";
			}

			return value;
		}
	}

	/// <summary>
	/// Environment
	/// </summary>
	/// <value>
	/// <c>true</c> if this instance is Scheduler, <c>false</c>.
	/// </value>
	public static String Environment
	{
		get
		{
			var value = Config.GetSection("Environment").Value;

			if (String.IsNullOrEmpty(value))
			{
				value = "SANDBOX";
			}

			return value.ToUpper();
		}
	}

	/// <summary>
	/// Gets a value indicating whether EnvironmentComposerConsoleLoggerIsEnabled is enabled
	/// </summary>
	/// <value>
	///   <c>true</c> if the console logger is enabled, <c>false</c>.
	/// </value>
	public static Boolean EnvironmentComposerConsoleLoggerIsEnabled
	{
		get
		{
			var value = false;

			var isEnabled = Config.GetSection("Logging:EnvironmentComposerConsole:IsEnabled").Value;

			if (!String.IsNullOrEmpty(isEnabled))
			{
				value = Convert.ToBoolean(isEnabled);
			}

			return value;
		}
	}

	/// <summary>
	/// Gets a value indicating whether EnvironmentComposerSignalRLoggerIsEnabled is enabled
	/// </summary>
	/// <value>
	///   <c>true</c> if the SignalR logger is enabled, <c>false</c>.
	/// </value>
	public static Boolean EnvironmentComposerSignalRLoggerIsEnabled
	{
		get
		{
			var value = false;

			var isEnabled = Config.GetSection("Logging:EnvironmentComposerSignalR:IsEnabled").Value;

			if (!String.IsNullOrEmpty(isEnabled))
			{
				value = Convert.ToBoolean(isEnabled);
			}

			return value;
		}
	}

	/// <summary>
	/// MessageHub default topic
	/// </summary>
	public static String EnvironmentComposerMessageHubDefaultTopic
	{
		get
		{
			var value = String.Empty;
			var topic = Config.GetSection("Logging:EnvironmentComposerSignalR:DefaultTopic").Value;

			if (!String.IsNullOrEmpty(topic))
			{
				value = topic;
			}

			return value;
		}
	}

	/// <summary>
	/// Gets a value indicating whether AssetConfigurationValidationService is enabled
	/// </summary>
	/// <value>
	///   <c>true</c> if this instance is Scheduler, <c>false</c>.
	/// </value>
	public static Boolean AssetConfigurationValidationServiceIsEnabled
	{
		get
		{
			var value = false;

			var isEnabled = Config.GetSection("AssetConfigurationValidationService:IsEnabled").Value;

			if (!String.IsNullOrEmpty(isEnabled))
			{
				value = Convert.ToBoolean(isEnabled);
			}

			return value;
		}
	}

	/// <summary>
	/// Uid for the Provision Service
	/// </summary>
	public static List<Guid> ProvisionServiceInstances
	{
		get
		{
			var value = new List<Guid>();

			var serviceInstances = Config.GetSection("ProvisionService:ProvisionServiceInstances");

			serviceInstances?.Bind(value);

			return value;
		}
	}

	/// <summary>
	/// Gets the workspace client request URI.
	/// </summary>
	/// <value>
	/// The workspace client request URI.
	/// </value>
	public static String EnvironmentComposerClientRequestUri
	{
		get
		{
			return Config.GetSection("AzureAD:RequestUri").Value!;
		}
	}

	/// <summary>
	/// Gets the workspace client Auth URI.
	/// </summary>
	/// <value>
	/// The workspace client Auth URI.
	/// </value>
	public static String EnvironmentComposerClientAuthUri
	{
		get
		{
			return Config.GetSection("AzureAD:AuthURI").Value!;
		}
	}

	/// <summary>
	/// Gets the asset client authentication client identifier.
	/// </summary>
	/// <value>
	/// The asset client authentication client identifier.
	/// </value>
	public static String EnvironmentComposerClientId
	{
		get
		{
			return Config.GetSection("AzureAD:ClientId").Value!;
		}
	}

	/// <summary>
	/// Gets the asset client authentication client secret.
	/// </summary>
	/// <value>
	/// The asset client authentication client secret.
	/// </value>
	public static String EnvironmentComposerClientSecret
	{
		get
		{
			var secret = Config.GetSection("AzureAD:Secret").Value!;

			if (secret.StartsWith("{"))
			{
				secret = Config.GetValue<String>(secret.TrimStart('{').TrimEnd('}'));
			}

			return secret!;
		}
	}

	/// <summary>
	/// Gets the asset client scope.
	/// </summary>
	/// <value>
	/// The asset client scope.
	/// </value>
	public static String EnvironmentComposerResource
	{
		get
		{
			return Config.GetSection("AzureAD:Resource").Value!;
		}
	}

	/// <summary>
	/// Gets the environment composer scopes.
	/// </summary>
	/// <value>
	/// The environment composer scopes.
	/// </value>
	public static String EnvironmentComposerScopes
	{
		get
		{
			return Config.GetSection("AzureAD:Scopes").Value!;
		}
	}

	/// <summary>
	/// Gets the environment composer tenant.
	/// </summary>
	/// <value>
	/// The environment composer tenant.
	/// </value>
	public static String EnvironmentComposerTenant
	{
		get
		{
			return Config.GetSection("AzureAD:TenantId").Value!;
		}
	}

	/// <summary>
	/// Gets the photo picker client identifier.
	/// </summary>
	/// <value>
	/// The photo picker client identifier.
	/// </value>
	public static String PhotoPickerClientId
	{
		get
		{
			return Config.GetSection("AzureAD:PhotoPickerClientId").Value!;
		}
	}

	/// <summary>
	/// Gets the photo picker client secret.
	/// </summary>
	/// <value>
	/// The photo picker client secret.
	/// </value>
	public static String PhotoPickerClientSecret
	{
		get
		{
			var photoPickerClientSecret = Config.GetSection("AzureAD:PhotoPickerClientSecret").Value!;

			if (photoPickerClientSecret.StartsWith("{"))
			{
				photoPickerClientSecret = Config.GetValue<String>(photoPickerClientSecret.TrimStart('{').TrimEnd('}'));
			}

			return photoPickerClientSecret!;
		}
	}

	/// <summary>
	/// Gets the graph API.
	/// </summary>
	/// <value>
	/// The graph API.
	/// </value>
	public static String GraphAPI
	{
		get
		{
			return Config.GetSection("GraphAPI:GraphAPIUrl").Value!;
		}
	}

	/// <summary>
	/// Gets the graph API get member of.
	/// </summary>
	/// <value>
	/// The graph API get member of.
	/// </value>
	public static String GraphAPIGetMemberOf
	{
		get
		{
			return Config.GetSection("GraphAPI:GetMemberOf").Value!;
		}
	}

	/// <summary>
	/// Gets the graph API create group.
	/// </summary>
	/// <value>
	/// The graph API create group.
	/// </value>
	public static String GraphAPICreateGroup
	{
		get
		{
			return Config.GetSection("GraphAPI:CreateGroup").Value!;
		}
	}

	/// <summary>
	/// Gets the graph API photo picker end point.
	/// </summary>
	/// <value>
	/// The graph API photo picker end point.
	/// </value>
	public static String GraphAPIPhotoPickerEndPoint
	{
		get
		{
			return Config.GetSection("GraphAPI:PhotoPickerEndPoint").Value!;
		}
	}

	/// <summary>
	/// Gets the graph API batching endpoint.
	/// </summary>
	/// <value>
	/// The graph API batching endpoint.
	/// </value>
	public static String GraphAPIBatchingEndpoint
	{
		get
		{
			return Config.GetSection("GraphAPI:BatchingEndpoint").Value!;
		}
	}

	/// <summary>
	/// Gets the graph API batch request URL.
	/// </summary>
	/// <value>
	/// The graph API batch request URL.
	/// </value>
	public static String GraphAPIBatchRequestUrl
	{
		get
		{
			return Config.GetSection("GraphAPI:BatchRequestUrl").Value!;
		}
	}

	/// <summary>
	/// Gets the azure key vault provider user assigned managed identity.
	/// </summary>
	/// <value>
	/// The azure key vault provider user assigned managed identity.
	/// </value>
	public static String AzureKeyVaultProviderUserAssignedManagedIdentity
	{
		get
		{
			return Config.GetSection("AzureKeyVaultProvider:UserAssignedManagedIdentity").Value!;
		}
	}

	/// <summary>
	/// Gets the azure key vault provider key vault URI.
	/// </summary>
	/// <value>
	/// The azure key vault provider key vault URI.
	/// </value>
	public static String AzureKeyVaultProviderKeyVaultUri
	{
		get
		{
			return Config.GetSection("AzureKeyVaultProvider:KeyVaultUri").Value!;
		}
	}

	/// <summary>
	/// Gets the azure key vault provider azure key.
	/// </summary>
	/// <value>
	/// The azure key vault provider azure key.
	/// </value>
	public static String AzureKeyVaultProviderAzureKey
	{
		get
		{
			return Config.GetSection("AzureKeyVaultProvider:AzureKey").Value!;
		}
	}

	/// <summary>
	/// Gets the ProvisionServiceIPAddress.
	/// </summary>
	/// <value>
	/// The ProvisionServiceIPAddress.
	/// </value>
	public static String ProvisionServiceIPAddress
	{
		get
		{
			var value = Config.GetSection("ProvisionServiceIPAddress").Value;
			return value!;
		}
	}

	/// <summary>
	/// Gets the EmailSecurityKey.
	/// </summary>
	/// <value>
	/// The EmailSecurityKey.
	/// </value>
	public static String EmailSecurityKey
	{
		get
		{
			var value = Config.GetSection("EmailSecurityKey").Value;
			return value!;
		}
	}

	/// <summary>
	/// Gets the EmailSecurityKey.
	/// </summary>
	/// <value>
	/// The EmailSecurityKey.
	/// </value>
	public static String AESKey
	{
		get
		{
			var value = Config.GetSection("OAAA-Key").Value;
			return value!;
		}
	}

	/// <summary>
	/// Gets the EmailSecurityKey.
	/// </summary>
	/// <value>
	/// The EmailSecurityKey.
	/// </value>
	public static String AESVector
	{
		get
		{
			var value = Config.GetSection("OAAA-Vector").Value;
			return value!;
		}
	}

	/// <summary>
	/// Gets the ProvisionServicePort.
	/// </summary>
	/// <value>
	/// The ProvisionServicePort.
	/// </value>
	public static String ProvisionServicePort
	{
		get
		{
			var value = Config.GetSection("ProvisionServicePort").Value;
			return value!;
		}
	}

	/// <summary>
	/// AzureCommunicationServicesProviderConnectionString
	/// </summary>
	public static String AcsProviderConnectionString
	{
		get
		{
			var value = Config.GetSection("Email:AcsProvider:ConnectionString").Value;
			return value!;
		}
	}

	/// <summary>
	/// Folder to drop email files
	/// </summary>
	public static String DebugProviderFolderPath
	{
		get
		{
			var value = Config.GetSection("Email:DebugProvider:FolderPath").Value;
			return value!;
		}
	}

	/// <summary>
	/// EmailProviderType
	/// </summary>
	public static String EmailProviderType
	{
		get
		{
			var value = Config.GetSection("Email:EmailProviderType").Value;
			return value!;
		}
	}

	/// <summary>
	/// EmailFrom
	/// </summary>
	public static String EmailFrom
	{
		get
		{
			var value = Config.GetSection("Email:From").Value;
			return value!;
		}
	}

	/// <summary>
	/// Replacement Environments
	/// </summary>
	public static IEnumerable<String> EmailReplacementEnvironments
	{
		get
		{
			var value = Config.GetSection("Email:ReplacementEnvironmentCsv").Value;

			if (String.IsNullOrEmpty(value))
			{
				value = @"DEV,DEVTEST,PREPROD,STAGE,SANDBOX,DEMO,APT,UAT";
			}

			return String.IsNullOrEmpty(value!) ? Enumerable.Empty<String>() : value.Split(",");
		}
	}

	/// <summary>
	/// ReplacementToEmailId
	/// </summary>
	public static IEnumerable<String> AllowedEmailDomainsCsv
	{
		get
		{
			var value = Config.GetSection("Email:AllowedEmailDomainsCsv").Value;
			return String.IsNullOrEmpty(value) ? Enumerable.Empty<String>() : value.Split(",");
		}
	}

	/// <summary>
	/// ReplacementToEmailId
	/// </summary>
	public static IEnumerable<String> EmailReplacementToEmailIds
	{
		get
		{
			var value = Config.GetSection("Email:ReplacementToEmailId").Value;
			return String.IsNullOrEmpty(value) ? Enumerable.Empty<String>() : value.Split(";");
		}
	}

	/// <summary>
	/// ReplacementCcEmailId
	/// </summary>
	public static IEnumerable<String> EmailReplacementCcEmailIds
	{
		get
		{
			var value = Config.GetSection("Email:ReplacementCcEmailId").Value;
			return String.IsNullOrEmpty(value) ? Enumerable.Empty<String>() : value.Split(";");
		}
	}

	/// <summary>
	/// ReplacementCcEmailId
	/// </summary>
	public static IEnumerable<String> EmailReplacementBccEmailIds
	{
		get
		{
			var value = Config.GetSection("Email:ReplacementBccEmailId").Value;
			return String.IsNullOrEmpty(value) ? Enumerable.Empty<String>() : value.Split(";");
		}
	}

	/// <summary>
	/// ExchangeOnlineProviderSecretKey
	/// </summary>
	public static String O365ProviderSecretKey
	{
		get
		{
			var value = Config.GetSection("Email:O365Provider:SecretKey").Value;
			return value!;
		}
	}

	/// <summary>
	/// ExchangeOnlineProviderClientId
	/// </summary>
	public static String O365ProviderClientId
	{
		get
		{
			var value = Config.GetSection("Email:O365Provider:ClientId").Value;
			return value!;
		}
	}

	/// <summary>
	/// ExchangeOnlineProviderTenantId
	/// </summary>
	public static String O365ProviderTenantId
	{
		get
		{
			var value = Config.GetSection("Email:O365Provider:TenantId").Value;
			return value!;
		}
	}

	/// <summary>
	/// OneAssetProviderEndpoint
	/// </summary>
	public static String OneAssetProviderEndpoint
	{
		get
		{
			var value = Config.GetSection("Email:OneAssetProvider:Endpoint").Value;
			return value!;
		}
	}

	/// <summary>
	/// OneAssetProviderClientId
	/// </summary>
	public static String OneAssetProviderClientId
	{
		get
		{
			var value = Config.GetSection("Email:OneAssetProvider:ClientId").Value;
			return value!;
		}
	}

	/// <summary>
	/// OneAssetProviderClientSecret
	/// </summary>
	public static String OneAssetProviderClientSecret
	{
		get
		{
			var value = Config.GetSection("Email:OneAssetProvider:ClientSecret").Value;
			return value!;
		}
	}

	/// <summary>
	/// OneAssetProviderTenantId
	/// </summary>
	public static String OneAssetProviderTenantId
	{
		get
		{
			var value = Config.GetSection("Email:OneAssetProvider:TenantId").Value;
			return value!;
		}
	}

	/// <summary>
	/// OneAssetProviderScope
	/// </summary>
	public static String OneAssetProviderScope
	{
		get
		{
			var value = Config.GetSection("Email:OneAssetProvider:Scope").Value;
			return value!;
		}
	}

	/// <summary>
	/// OneAssetProviderEmailActionUId
	/// </summary>
	public static String OneAssetProviderEmailActionUId
	{
		get
		{
			var value = Config.GetSection("Email:OneAssetProvider:EmailActionUId").Value;
			return value!;
		}
	}

	/// <summary>
	/// OneAssetProviderCreatedByApp
	/// </summary>
	public static String OneAssetProviderCreatedByApp
	{
		get
		{
			var value = Config.GetSection("Email:OneAssetProvider:CreatedByApp").Value;
			return value!;
		}
	}

	/// <summary>
	/// OneAssetProviderCreatedByApp
	/// </summary>
	public static Boolean OneAssetProviderEncodeBody
	{
		get
		{
			_ = Boolean.TryParse(Config.GetSection("Email:OneAssetProvider:EncodeBody").Value, out var value);

			return value;
		}
	}

	/// <summary>
	/// IsExtendedFailedLoggingEnabled
	/// </summary>
	public static Boolean EmailIsExtendedFailedLoggingEnabled
	{
		get
		{
			var value = false;

			var isExtendedFailedLoggingEnabled = Config.GetSection("Email:IsExtendedFailedLoggingEnabled").Value;

			if (!String.IsNullOrEmpty(isExtendedFailedLoggingEnabled))
			{
				value = Convert.ToBoolean(isExtendedFailedLoggingEnabled);
			}

			return value;
		}
	}

	/// <summary>
	/// SmtpProviderUserId
	/// </summary>
	public static String SmtpProviderUserId
	{
		get
		{
			var value = Config.GetSection("Email:SmtpProvider:UserId").Value;
			return value!;
		}
	}

	/// <summary>
	/// SmtpProviderPassword
	/// </summary>
	public static String SmtpProviderPassword
	{
		get
		{
			var value = Config.GetSection("Email:SmtpProvider:Password").Value;
			return value!;
		}
	}

	/// <summary>
	/// SmtpProviderHost
	/// </summary>
	public static String SmtpProviderHost
	{
		get
		{
			var value = Config.GetSection("Email:SmtpProvider:Host").Value;
			return value!;
		}
	}

	/// <summary>
	/// SmtpProviderPort
	/// </summary>
	public static Int32 SmtpProviderPort
	{
		get
		{
			var value = Config.GetSection("Email:SmtpProvider:Port").Value;
			return Int32.Parse(value!);
		}
	}

	/// <summary>
	/// MessageBusServiceBusConnectionString
	/// </summary>
	public static String MessageBusServiceBusConnectionString
	{
		get
		{
			var value = Config.GetSection("ConnectionStrings").GetSection("MessageBusServiceBus").Value;
			return value!;
		}
	}

	/// <summary>
	/// MessageBusServiceBusConnectionString
	/// </summary>
	public static String BlobDistributedMutexStorageConnectionString
	{
		get
		{
			var value = Config.GetSection("ConnectionStrings").GetSection("BlobDistributedMutexStorage").Value;
			return value!;
		}
	}

	/// <summary>
	/// Gets the obfuscator section.
	/// </summary>
	/// <value>
	/// The obfuscator encryption key.
	/// </value>
	public static IConfigurationSection ObfuscatorSection
	{
		get
		{
			return Config.GetSection("Obfuscator");
		}
	}

	/// <summary>
	/// Gets a value indicating whether Obfuscator is enabled
	/// </summary>
	/// <value>
	///   <c>true</c> if Obfuscator is enabled, <c>false</c>.
	/// </value>
	public static Boolean ObfuscatorIsEnabled
	{
		get
		{
			var value = false;

			var isEnabled = Config.GetSection("Obfuscator:IsEnabled").Value;

			if (!String.IsNullOrEmpty(isEnabled))
			{
				value = Convert.ToBoolean(isEnabled);
			}

			return value;
		}
	}

	/// <summary>
	/// Gets a value indicating Obfuscator SecrectProviderType
	/// </summary>
	/// <value>
	/// <c>true</c> Obfuscator SecrectProviderType, <c>false</c>.
	/// </value>
	public static String ObfuscatorSecrectProviderType
	{
		get
		{
			var obfuscator = Config.GetSection("Obfuscator");
			var secrectProviderType = obfuscator.GetValue<String>("SecrectProviderType");

			return secrectProviderType!;
		}
	}

	/// <summary>
	/// Gets the obfuscator key vault URI.
	/// </summary>
	/// <value>
	/// The obfuscator key vault URI.
	/// </value>
	public static String ObfuscatorKeyVaultUri
	{
		get
		{
			var obfuscator = Config.GetSection("Obfuscator:AzureKeyVaultProvider");
			var keyVaultUri = obfuscator.GetValue<String>("KeyVaultUri");

			return keyVaultUri!;
		}
	}

	/// <summary>
	/// Gets the obfuscator key vault Managed Identity.
	/// </summary>
	/// <value>
	/// The obfuscator key vault Managed Identity.
	/// </value>
	public static String ObfuscatorKeyVaultUAManagedIdentity
	{
		get
		{
			var obfuscator = Config.GetSection("Obfuscator:AzureKeyVaultProvider");
			var keyVaultUri = obfuscator.GetValue<String>("UserAssignedManagedIdentity");

			return keyVaultUri!;
		}
	}

	/// <summary>
	/// Gets the obfuscator key vault key vault secret.
	/// </summary>
	/// <value>
	/// The obfuscator key vault key vault secret.
	/// </value>
	public static String ObfuscatorKeyVaultSecretKey
	{
		get
		{
			var obfuscator = Config.GetSection("Obfuscator:AzureKeyVaultProvider");
			var keyVaultSecret = obfuscator.GetValue<String>("SecretKey");

			return keyVaultSecret!;
		}
	}

	/// <summary>
	/// Gets the obfuscator encryption key.
	/// </summary>
	/// <value>
	/// The obfuscator encryption key.
	/// </value>
	public static String ObfuscatorEncryptionKey
	{
		get
		{
			var obfuscator = Config.GetSection("Obfuscator:CustomSecretProvider");
			var encryptionKey = obfuscator.GetValue<String>("EncryptionKey");

			return encryptionKey!;
		}
	}

	/// <summary>
	/// Gets the obfuscator encryption keys.
	/// </summary>
	/// <value>
	/// The obfuscator encryption key.
	/// </value>
	public static Dictionary<String, String> ObfuscatorEncryptionKeys
	{
		get
		{
			var obfuscator = Config.GetSection("Obfuscator:CustomSecretProvider");

			var encryptionKeys = obfuscator.GetChildren();

			var secretKeys = new Dictionary<String, String>();

			foreach (var secretKey in encryptionKeys)
			{
				secretKeys.Add(secretKey.Key, secretKey.Value!);
			}

			return secretKeys;
		}
	}

	/// <summary>
	/// Gets the obfuscator configuration file path.
	/// </summary>
	/// <value>
	/// The obfuscator configuration file path.
	/// </value>
	public static String ObfuscatorConfigFilePath
	{
		get
		{
			var obfuscator = Config.GetSection("Obfuscator");
			var configFilePath = obfuscator.GetValue<String>("ConfigFilePath");

			return configFilePath!;
		}
	}

	/// <summary>
	/// Gets the service bus connection string.
	/// </summary>
	/// <value>
	/// The service bus connection string.
	/// </value>
	public static String ServiceBusConnectionString
	{
		get
		{
			var value = Config.GetSection("EventHub:ServiceBusHubProvider:ServiceBusConnectionString").Value;
			return value!;
		}
	}

	/// <summary>
	/// Gets the URL rewrite rule path.
	/// </summary>
	/// <value>
	/// The URL rewrite rule path.
	/// </value>
	public static String UrlRewriteRulePath
	{
		get
		{
			var value = String.Empty;
			var applicationSuffix = Config.GetSection("UrlRewriteRulePath").Value;

			if (!String.IsNullOrEmpty(applicationSuffix))
			{
				value = applicationSuffix;
			}

			return value;
		}
	}

	/// <summary>
	/// Gets the AES KEY ID.
	/// </summary>
	/// <value>
	/// The AESKEYID.
	/// </value>
	public static String AESKeyId
	{
		get
		{
			var value = String.Empty;
			var aesKeyId = Config.GetSection("AESKEYID").Value;

			if (!String.IsNullOrEmpty(aesKeyId))
			{
				value = aesKeyId;
			}

			return value;
		}
	}

	/// <summary>
	/// Gets the IV KEY ID.
	/// </summary>
	/// <value>
	/// The IVKEYID.
	/// </value>
	public static String IVKeyId
	{
		get
		{
			var value = String.Empty;
			var ivKeyId = Config.GetSection("IVKEYID").Value;

			if (!String.IsNullOrEmpty(ivKeyId))
			{
				value = ivKeyId;
			}

			return value;
		}
	}

	/// <summary>
	/// Gets the get searched user URI.
	/// </summary>
	/// <value>
	/// The get searched user URI.
	/// </value>
	public static String GetSearchedUserURI
	{
		get
		{
			return Config.GetSection("GraphAPI:GetSearchedUserURI").Value!;
		}
	}

	/// <summary>
	/// Gets the get searched group URI.
	/// </summary>
	/// <value>
	/// The get searched group URI.
	/// </value>
	public static String GetSearchedGroupURI
	{
		get
		{
			return Config.GetSection("GraphAPI:GetSearchedGroupURI").Value!;
		}
	}

	/// <summary>
	/// Gets the graph API get user email identifier.
	/// </summary>
	/// <value>
	/// The graph API get user email identifier.
	/// </value>
	public static String GraphAPIGetUserEmailId
	{
		get
		{
			return Config.GetSection("GraphAPI:GetUserEmailId").Value!;
		}
	}

	/// <summary>
	/// AssetCodeAzureFileSharePath
	/// </summary>
	/// <value>
	/// <c>true</c> if this instance is Scheduler, <c>false</c>.
	/// </value>
	public static String AssetCodeAzureFileSharePath
	{
		get
		{
			var value = String.Empty;
			var AzureFilePath = Config.GetSection("AssetCodeAzureFileSharePath").Value;

			if (!String.IsNullOrEmpty(AzureFilePath))
			{
				value = AzureFilePath;
			}

			return value;
		}
	}

	/// <summary>
	/// AzureStorageAccount
	/// </summary>
	public static String AzureStorageAccount
	{
		get
		{
			var value = String.Empty;
			var azureStorageAccount = Config.GetSection("AzureStorageAccount").Value;

			if (!String.IsNullOrEmpty(azureStorageAccount))
			{
				value = azureStorageAccount;
			}

			return value;
		}
	}

	/// <summary>
	/// AzureServiceBus
	/// </summary>
	public static String AzureServiceBus
	{
		get
		{
			var value = String.Empty;
			var azureServiceBus = Config.GetSection("AzureServiceBus").Value;

			if (!String.IsNullOrEmpty(azureServiceBus))
			{
				value = azureServiceBus;
			}

			return value;
		}
	}

	/// <summary>
	/// ManagedIdentityClientId
	/// </summary>
	public static String ManagedIdentityClientId
	{
		get
		{
			var value = String.Empty;
			var managedIdentityClientId = Config.GetSection("UserAssignedManagedIdentity").Value;

			if (!String.IsNullOrEmpty(managedIdentityClientId))
			{
				value = managedIdentityClientId;
			}

			return value;
		}
	}

	#endregion

	#region Static Methods

	/// <summary>
	/// GetValue
	/// </summary>
	public static T GetValue<T>(String key)
	{
		var value = Config.GetValue<T>(key!);
		return value!;
	}

	/// <summary>
	/// Represents GetHttpClient method.
	/// </summary>
	private static HttpClient GetHttpClient()
	{
		var httpClient = new HttpClient();
		var baseUrl = Config.GetSection("BaseUrl").Value;

		if (httpClient.BaseAddress == null)
		{
			httpClient.BaseAddress = new Uri(baseUrl!);
		}

		return httpClient;
	}

	/// <summary>
	/// Gets the GetAppSettingValue secret.
	/// </summary>
	/// <value>
	/// The GetAppSettingValue secret.
	/// </value>
	public static String? GetAppSettingValue(String key)
	{
		var secret = Config.GetValue<String>(key);

		if (secret != null && secret.StartsWith("{"))
		{
			secret = Config.GetValue<String>(secret.TrimStart('{').TrimEnd('}'));
		}

		return secret;
	}

	/// <summary>
	/// TestApiHealthURL
	/// </summary>
	public static String TestApiHealthURL
	{
		get
		{
			var value = Config.GetSection("TestApiHealthURL").Value;
			return value!;
		}
	}

	/// <summary>
	/// AzureBlobStorageConnectionString
	/// </summary>
	public static String AzureBlobStorageConnectionString
	{
		get
		{
			var azureAD = Config.GetSection("AzureAD");
			var azureBlobStorage = azureAD.GetSection("AzureBlobStorage");
			var azureBlobConnectionString = azureBlobStorage.GetValue<String>("ConnectionString");

			return azureBlobConnectionString!;
		}
	}

	/// <summary>
	/// AzureBlobStorageContainerName
	/// </summary>
	public static String AzureBlobStorageContainerName
	{
		get
		{
			var azureAD = Config.GetSection("AzureAD");
			var azureBlobStorage = azureAD.GetSection("AzureBlobStorage");
			var azureBlobContainerName = azureBlobStorage.GetValue<String>("ContainerName");

			return azureBlobContainerName!;
		}
	}

	/// <summary>
	/// AzureBlobfilepath
	/// </summary>
	public static String AzureBlobfilepath
	{
		get
		{
			var azureAD = Config.GetSection("AzureAD");
			var azureBlobStorage = azureAD.GetSection("AzureBlobStorage");
			var value = azureBlobStorage.GetSection("blobName").Value;
			return value!;
		}
	}
	#endregion
}

