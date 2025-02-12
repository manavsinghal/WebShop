#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Core.Application.Models.DbAppSettings;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead 

#endregion

/// <summary>
/// AppSettings from DB
/// </summary>
public partial class DbAppSettings : COREAPPINTERFACESDBAPPSETTINGS.IDbAppSettings
{
	#region Fields

	private readonly ILogger<DbAppSettings> _logger;
	private readonly IServiceProvider _serviceProvider;
	private COREDOMAINDATAMODELSDOMAIN.CommonSettings _commonSettings = default!;
	private COREDOMAINDATAMODELSDOMAIN.ProvisionServiceSettings _provisionServiceSettings = default!;
	private COREDOMAINDATAMODELSDOMAIN.MessageBusSettings _messageBusSettings = default!;
	private COREDOMAINDATAMODELSDOMAIN.WebAPIServiceSettings _webapiServiceSettings = default!;
	private COREDOMAINDATAMODELSDOMAIN.WebUIServiceSettings _webuiServiceSettings = default!;
	private COREDOMAINDATAMODELSDOMAIN.EmailSettings _emailSettings = default!;
	private COREDOMAINDATAMODELSDOMAIN.CachingSettings _cachingSettings = default!;

	#endregion

	#region Properties     

	/// <summary>
	/// Common Service Settings
	/// </summary>
	public COREDOMAINDATAMODELSDOMAIN.CommonSettings Common
	{
		get
		{
			this._commonSettings ??= this.LoadCommonSettingsAsync().GetAwaiter().GetResult();

			return _commonSettings;
		}
	}

	/// <summary>
	/// Provision Service Settings
	/// </summary>
	public COREDOMAINDATAMODELSDOMAIN.ProvisionServiceSettings ProvisionService
	{
		get
		{
			this._provisionServiceSettings ??= this.LoadProvisionServiceSettingsAsync().GetAwaiter().GetResult();

			return _provisionServiceSettings;
		}
	}

	/// <summary>
	/// Message Bus Settings
	/// </summary>
	public COREDOMAINDATAMODELSDOMAIN.MessageBusSettings MessageBus
	{
		get
		{
			this._messageBusSettings ??= this.LoadMessageBusSettingsAsync().GetAwaiter().GetResult();

			return _messageBusSettings;
		}
	}

	/// <summary>
	/// WebAPI Service Settings
	/// </summary>
	public COREDOMAINDATAMODELSDOMAIN.WebAPIServiceSettings WebAPIService
	{
		get
		{
			this._webapiServiceSettings ??= this.LoadWebAPISettingsAsync().GetAwaiter().GetResult();

			return _webapiServiceSettings;
		}
	}

	/// <summary>
	/// WebUI Service Settings
	/// </summary>
	public COREDOMAINDATAMODELSDOMAIN.WebUIServiceSettings WebUIService
	{
		get
		{
			this._webuiServiceSettings ??= this.LoadWebUISettingsAsync().GetAwaiter().GetResult();

			return _webuiServiceSettings;
		}
	}

	/// <summary>
	/// Email Settings
	/// </summary>
	public COREDOMAINDATAMODELSDOMAIN.EmailSettings Email
	{
		get
		{
			this._emailSettings ??= this.LoadEmailSettingsAsync().GetAwaiter().GetResult();

			return _emailSettings;
		}
	}

	/// <summary>
	/// Gets the caching.
	/// </summary>
	/// <value>
	/// The caching.
	/// </value>
	public COREDOMAINDATAMODELSDOMAIN.CachingSettings Caching
	{
		get
		{
			this._cachingSettings ??= this.LoadCachingSettingsAsync().GetAwaiter().GetResult();

			return _cachingSettings;
		}
	}

	#endregion

	#region Constructors

	/// <summary>
	/// ctor
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="master"></param>
	public DbAppSettings(ILogger<DbAppSettings> logger, IServiceProvider serviceProvider)
	{
		this._logger = logger;
		this._serviceProvider = serviceProvider;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Get AppSettings value from the DB
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="componentName"></param>
	/// <param name="key"></param>
	/// <returns></returns>
	public async Task<T?> GetValueAsync<T>(String componentName, String key)
	{
		_logger.LogInfo($"{nameof(GetValueAsync)} ({componentName}, {key}) {SHAREDKERNALRESX.WebShop.Begin}", nameof(DbAppSettings), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);

		try
		{
			using var serviceProviderScope = _serviceProvider.CreateScope();

			var master = serviceProviderScope.ServiceProvider.GetRequiredService<COREAPPDREPOINTERFACESDOMAIN.IMasterRepository>();
			var getAppSettingRequest = new COREAPPDATAREPOMODELSDOMAIN.GetAppSettingRequest
			{
				MatchExpression = a => a.ComponentName.Equals(componentName) && a.KeyName.Equals(key)
			};

			var getAppSettingResponse = await master.GetAppSettingsAsync(getAppSettingRequest).ConfigureAwait(false);

			if (getAppSettingResponse.Faults != null && getAppSettingResponse.Faults.Any())
			{
				_logger.LogFault($"{nameof(GetValueAsync)}", $"{nameof(DbAppSettings)}", getAppSettingResponse.Faults, SHAREDKERNALLIB.ApplicationTier.Service);
				return default!;
			}

			var appSetting = await getAppSettingResponse.AppSettings.FirstOrDefaultAsync().ConfigureAwait(false);

			if (appSetting != null)
			{
				return (T?)Convert.ChangeType(appSetting.Value, typeof(T));
			}
		}
		catch (Exception ex)
		{
			_logger.LogFault($"{nameof(GetValueAsync)}", $"{nameof(DbAppSettings)}", ex, SHAREDKERNALLIB.ApplicationTier.Service);
		}
		finally
		{
			_logger.LogInfo($"{nameof(GetValueAsync)} ({componentName}, {key}) {SHAREDKERNALRESX.WebShop.End}", nameof(DbAppSettings), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
		}

		return default!;
	}

	/// <summary>
	/// Get AppSettings value from the DB
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="componentName"></param>
	/// <param name="key"></param>
	/// <returns></returns>
	public async Task<COREDOMAINDATAMODELSDOMAIN.AppSetting> GetAsync(String componentName, String key)
	{
		_logger.LogInfo($"{nameof(GetAsync)} ({componentName}, {key}) {SHAREDKERNALRESX.WebShop.Begin}", nameof(DbAppSettings), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);

		try
		{
			using var serviceProviderScope = _serviceProvider.CreateScope();

			var master = serviceProviderScope.ServiceProvider.GetRequiredService<COREAPPDREPOINTERFACESDOMAIN.IMasterRepository>();
			var getAppSettingRequest = new COREAPPDATAREPOMODELSDOMAIN.GetAppSettingRequest
			{
				MatchExpression = a => a.ComponentName.Equals(componentName) && a.KeyName.Equals(key)
			};

			var getAppSettingResponse = await master.GetAppSettingsAsync(getAppSettingRequest).ConfigureAwait(false);

			if (getAppSettingResponse.Faults != null && getAppSettingResponse.Faults.Any())
			{
				_logger.LogFault($"{nameof(GetAsync)}", $"{nameof(DbAppSettings)}", getAppSettingResponse.Faults, SHAREDKERNALLIB.ApplicationTier.Service);
				return default!;
			}

			var appSetting = await getAppSettingResponse.AppSettings.FirstOrDefaultAsync().ConfigureAwait(false);

			if (appSetting != null)
			{
				return appSetting;
			}
		}
		catch (Exception ex)
		{
			_logger.LogFault($"{nameof(GetAsync)}", $"{nameof(DbAppSettings)}", ex, SHAREDKERNALLIB.ApplicationTier.Service);
		}
		finally
		{
			_logger.LogInfo($"{nameof(GetAsync)} ({componentName}, {key}) {SHAREDKERNALRESX.WebShop.End}", nameof(DbAppSettings), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
		}

		return default!;
	}

	/// <summary>
	/// Get AppSettings value from the DB
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="componentName"></param>
	/// <returns></returns>
	public async Task<IEnumerable<COREDOMAINDATAMODELSDOMAIN.AppSetting>> GetAsync(String componentName)
	{
		_logger.LogInfo($"{nameof(GetAsync)} Component: {componentName} {SHAREDKERNALRESX.WebShop.Begin}", nameof(DbAppSettings), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);

		try
		{
			using var serviceProviderScope = _serviceProvider.CreateScope();

			var master = serviceProviderScope.ServiceProvider.GetRequiredService<COREAPPDREPOINTERFACESDOMAIN.IMasterRepository>();
			var getAppSettingRequest = new COREAPPDATAREPOMODELSDOMAIN.GetAppSettingRequest
			{
				MatchExpression = a => a.ComponentName.Equals(componentName)
			};

			var getAppSettingResponse = await master.GetAppSettingsAsync(getAppSettingRequest).ConfigureAwait(false);

			if (getAppSettingResponse.Faults != null && getAppSettingResponse.Faults.Any())
			{
				_logger.LogFault($"{nameof(GetAsync)}", $"{nameof(DbAppSettings)}", getAppSettingResponse.Faults, SHAREDKERNALLIB.ApplicationTier.Service);
				return default!;
			}

			var appSetting = await getAppSettingResponse.AppSettings.ToListAsync().ConfigureAwait(false);

			if (appSetting != null)
			{
				return appSetting;
			}
		}
		catch (Exception ex)
		{
			_logger.LogFault($"Error occurred in {nameof(GetAsync)}", $"{nameof(DbAppSettings)}", ex, SHAREDKERNALLIB.ApplicationTier.Service);
		}
		finally
		{
			_logger.LogInfo($"{nameof(GetAsync)} Component: {componentName} {SHAREDKERNALRESX.WebShop.End}", nameof(DbAppSettings), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
		}

		return default!;
	}

	/// <summary>
	/// Set AppSettings value in the DB
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="componentName"></param>
	/// <param name="key"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	public async Task<Boolean> SetValueAsync(String componentName, String key, String value)
	{
		_logger.LogInfo($"{nameof(SetValueAsync)} ({componentName}, {key}) {SHAREDKERNALRESX.WebShop.Begin}", nameof(DbAppSettings), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);

		try
		{
			using var serviceProviderScope = _serviceProvider.CreateScope();

			var master = serviceProviderScope.ServiceProvider.GetRequiredService<COREAPPDREPOINTERFACESDOMAIN.IMasterRepository>();

			var appSetting = new COREDOMAINDATAMODELSDOMAIN.AppSetting
			{
				ComponentName = componentName,
				KeyName = key,
				Value = value,
				ItemState = COREDOMAINDATAMODELSENUM.ItemState.Added
			};
			appSetting.SetDefaultAuditFields();

			var appSettings = new List<COREDOMAINDATAMODELSDOMAIN.AppSetting>
			{
				appSetting
			};

			var mergeAppSettingRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeAppSettingRequest
			{
				AppSettings = appSettings
			};

			var mergeAppSettingResponse = await master.MergeAppSettingsAsync(mergeAppSettingRequest).ConfigureAwait(false);

			_ = await master.SaveAppSettingAsync().ContinueWith(t =>
			{
				if (t.IsFaulted && t.Exception != null)
				{
					mergeAppSettingResponse.Faults.Add(new Fault { Message = t.Exception.Message });
				}

				return Task.CompletedTask;

			}).ConfigureAwait(false);

			if (mergeAppSettingResponse.Faults != null && mergeAppSettingResponse.Faults.Any())
			{
				_logger.LogFault($"{nameof(SetValueAsync)}", $"{nameof(DbAppSettings)}", mergeAppSettingResponse.Faults, SHAREDKERNALLIB.ApplicationTier.Service);
				return false;
			}

			return true;
		}
		catch (Exception ex)
		{
			_logger.LogFault($"{nameof(SetValueAsync)}", $"{nameof(DbAppSettings)}", ex, SHAREDKERNALLIB.ApplicationTier.Service);
		}
		finally
		{
			_logger.LogInfo($"{nameof(SetValueAsync)} ({componentName}, {key}) {SHAREDKERNALRESX.WebShop.End}", nameof(DbAppSettings), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
		}

		return false;
	}

	/// <summary>
	/// Set AppSettings value in the DB
	/// </summary>
	/// <param name="appSetting"></param>
	/// <returns></returns>
	public async Task<Boolean> SetAsync(COREDOMAINDATAMODELSDOMAIN.AppSetting appSetting)
	{
		_logger.LogInfo($"{nameof(SetAsync)} ({appSetting.ComponentName}, {appSetting.KeyName}) {SHAREDKERNALRESX.WebShop.Begin}", nameof(DbAppSettings), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);

		try
		{
			using var serviceProviderScope = _serviceProvider.CreateScope();

			var master = serviceProviderScope.ServiceProvider.GetRequiredService<COREAPPDREPOINTERFACESDOMAIN.IMasterRepository>();
			appSetting.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;

			var appSettings = new List<COREDOMAINDATAMODELSDOMAIN.AppSetting>
			{
				appSetting
			};

			var mergeAppSettingRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeAppSettingRequest
			{
				AppSettings = appSettings
			};

			var mergeAppSettingResponse = await master.MergeAppSettingsAsync(mergeAppSettingRequest).ConfigureAwait(false);

			_ = await master.SaveAppSettingAsync().ContinueWith(t =>
			{
				if (t.IsFaulted && t.Exception != null)
				{
					mergeAppSettingResponse.Faults.Add(new Fault { Message = t.Exception.Message });
				}

				return Task.CompletedTask;

			}).ConfigureAwait(false);

			if (mergeAppSettingResponse.Faults != null && mergeAppSettingResponse.Faults.Any())
			{
				_logger.LogFault($"Error occured in {nameof(SetAsync)}", $"{nameof(DbAppSettings)}", mergeAppSettingResponse.Faults, SHAREDKERNALLIB.ApplicationTier.Service);
				return false;
			}

			return true;
		}
		catch (Exception ex)
		{
			_logger.LogFault($"Error occured in {nameof(SetAsync)}", $"{nameof(DbAppSettings)}", ex, SHAREDKERNALLIB.ApplicationTier.Service);
		}
		finally
		{
			_logger.LogInfo($"{nameof(SetAsync)} ({appSetting.ComponentName}, {appSetting.KeyName}) {SHAREDKERNALRESX.WebShop.End}", nameof(DbAppSettings), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
		}

		return false;
	}

	/// <summary>
	/// Refresh settings from DB
	/// </summary>
	/// <returns></returns>
	public async Task RefreshAsync()
	{
		_logger.LogInfo($"{nameof(RefreshAsync)}  {SHAREDKERNALRESX.WebShop.Begin}", nameof(DbAppSettings), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);

		try
		{
			if (this._commonSettings != null)
			{
				this._commonSettings = await this.LoadCommonSettingsAsync().ConfigureAwait(false);
			}

			if (this._provisionServiceSettings != null)
			{
				this._provisionServiceSettings = await this.LoadProvisionServiceSettingsAsync().ConfigureAwait(false);
			}

			if (this._webapiServiceSettings != null)
			{
				this._webapiServiceSettings = await this.LoadWebAPISettingsAsync().ConfigureAwait(false);
			}

			if (this._emailSettings != null)
			{
				this._emailSettings = await this.LoadEmailSettingsAsync().ConfigureAwait(false);
			}

			if (this._webuiServiceSettings != null)
			{
				this._webuiServiceSettings = await this.LoadWebUISettingsAsync().ConfigureAwait(false);
			}

			if (this._messageBusSettings != null)
			{
				this._messageBusSettings = await this.LoadMessageBusSettingsAsync().ConfigureAwait(false);
			}

			if (this._cachingSettings != null)
			{
				this._cachingSettings = await this.LoadCachingSettingsAsync().ConfigureAwait(false);
			}

		}
		catch (Exception ex)
		{
			_logger.LogFault($"Error occured in {nameof(RefreshAsync)}", $"{nameof(DbAppSettings)}", ex, SHAREDKERNALLIB.ApplicationTier.Service);
		}
		finally
		{
			_logger.LogInfo($"{nameof(RefreshAsync)}  {SHAREDKERNALRESX.WebShop.End}", nameof(DbAppSettings), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
		}
	}

	/// <summary>
	/// Refresh Appsettings from DB
	/// </summary>
	/// <returns></returns>
	public async Task RefreshAppSettingsAsync()
	{
		_logger.LogInfo($"{nameof(RefreshAsync)}  {SHAREDKERNALRESX.WebShop.Begin}", nameof(DbAppSettings), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);

		try
		{
			this._commonSettings = await this.LoadCommonSettingsAsync().ConfigureAwait(false);

			this._webapiServiceSettings = await this.LoadWebAPISettingsAsync().ConfigureAwait(false);

			this._cachingSettings = await this.LoadCachingSettingsAsync().ConfigureAwait(false);

			this._provisionServiceSettings = await this.LoadProvisionServiceSettingsAsync().ConfigureAwait(false);

			this._emailSettings = await this.LoadEmailSettingsAsync().ConfigureAwait(false);

			this._webuiServiceSettings = await this.LoadWebUISettingsAsync().ConfigureAwait(false);

			this._messageBusSettings = await this.LoadMessageBusSettingsAsync().ConfigureAwait(false);

		}
		catch (Exception ex)
		{
			_logger.LogFault($"Error occured in {nameof(RefreshAsync)}", $"{nameof(DbAppSettings)}", ex, SHAREDKERNALLIB.ApplicationTier.Service);
		}
		finally
		{
			_logger.LogInfo($"{nameof(RefreshAsync)}  {SHAREDKERNALRESX.WebShop.End}", nameof(DbAppSettings), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Populate Message Bus Settings
	/// </summary>
	/// <returns></returns>
	private async Task<COREDOMAINDATAMODELSDOMAIN.MessageBusSettings> LoadMessageBusSettingsAsync()
	{
		var appSettings = await this.GetAsync("MessageBus").ConfigureAwait(false);

		var maxAutoLockRenewalDurationInHours = GetInt32ValueOrDefault(appSettings, "MaxAutoLockRenewalDurationInHours");

		// TODO: Revisit default values
		return new COREDOMAINDATAMODELSDOMAIN.MessageBusSettings
		{
			ProviderType = GetValueOrDefault(appSettings, "ProviderType") ?? "ServiceBus",
			QueueName = GetValueOrDefault(appSettings, "QueueName"),
			TopicName = GetValueOrDefault(appSettings, "TopicName"),
			MaxAutoLockRenewalDurationInHours = maxAutoLockRenewalDurationInHours == default ? 2 : maxAutoLockRenewalDurationInHours
		};
	}

	/// <summary>
	/// Populate Common Settings
	/// </summary>
	/// <returns></returns>
	private async Task<COREDOMAINDATAMODELSDOMAIN.CommonSettings> LoadCommonSettingsAsync()
	{
		var appSettings = await this.GetAsync("Common").ConfigureAwait(false);
		var appWebUrl = AppSettings.AppWebUrl.TrimEnd('/');

		return new COREDOMAINDATAMODELSDOMAIN.CommonSettings
		{
			Environment = GetValueOrDefault(appSettings, "Environment.SubjectPrefix"),
			AppName = GetValueOrDefault(appSettings, "AppName"),
			AppCode = GetValueOrDefault(appSettings, "AppCode"),
			SupportTeamEmailId = GetValueOrDefault(appSettings, "SupportTeamEmailId"),
			SupportTeamName = GetValueOrDefault(appSettings, "SupportTeamName"),
			AssetVersionEditFormUrl = CombineUrl(appWebUrl, GetValueOrDefault(appSettings, "AssetVersionEditFormUrl")),
			EnvironmentEditFormUrl = CombineUrl(appWebUrl, GetValueOrDefault(appSettings, "EnvironmentEditFormUrl")),
			EnvironmentAssetVersionProvisionStatusPageUrl = CombineUrl(appWebUrl, GetValueOrDefault(appSettings, "EnvironmentAssetVersionProvisionStatusPageUrl")),
			AssetVersionAssessmentStatusPageUrl = CombineUrl(appWebUrl, GetValueOrDefault(appSettings, "AssetVersionAssessmentStatusPageUrl")),
			AssetEditPageUrl = CombineUrl(appWebUrl, GetValueOrDefault(appSettings, "AssetEditPageUrl")),
			ProvisionServiceEditFormUrl = CombineUrl(appWebUrl, GetValueOrDefault(appSettings, "ProvisionServiceEditFormUrl")),
			AppReadOnlyModeStartOn = GetValueOrDefault(appSettings, "AppReadOnlyModeStartOn"),
			AppReadOnlyModeEndOn = GetValueOrDefault(appSettings, "AppReadOnlyModeEndOn"),
			AppReadOnlyModeExceptionForUser = GetValueOrDefault(appSettings, "AppReadOnlyModeExceptionForUser"),
			IsManagedIdentityEnabledForStorageAccount = GetBooleanValueOrDefault(appSettings, "IsManagedIdentityEnabledForStorageAccount"),
			IsManagedIdentityEnabledForServiceBus = GetBooleanValueOrDefault(appSettings, "IsManagedIdentityEnabledForServiceBus"),
			EntityEventConfigData = GetValueOrDefault(appSettings, "EntityEventConfigData"),
			ARCECBackendScript = GetGuidValueOrDefault(appSettings, "ARCECBackendScript"),
			CacheObjectMappingConfigData = GetValueOrDefault(appSettings, "CacheObjectMappingConfigData"),
		};
	}

	/// <summary>
	/// Populate ProvisionService Settings
	/// </summary>
	/// <returns></returns>
	private async Task<COREDOMAINDATAMODELSDOMAIN.ProvisionServiceSettings> LoadProvisionServiceSettingsAsync()
	{
		var appSettings = await this.GetAsync("ProvisionService").ConfigureAwait(false);

		var workerIntervalInSecs = GetInt32ValueOrDefault(appSettings, "WorkerIntervalInSecs");
		var retryIntervalInMins = GetInt32ValueOrDefault(appSettings, "RetryIntervalInMins");
		var queueMessageRetryIntervalInMins = GetInt32ValueOrDefault(appSettings, "QueueMessageRetryIntervalInMins");
		var reminderIntervalInHrs = GetInt32ValueOrDefault(appSettings, "ReminderIntervalInHrs");
		var statusUpdateIntervalInMins = GetInt32ValueOrDefault(appSettings, "StatusUpdateIntervalInMins");
		var emailRetryCount = GetInt32ValueOrDefault(appSettings, "EmailRetryCount");
		var assetValidationIntervalInMins = GetInt32ValueOrDefault(appSettings, "AssetValidationService.IntervalInMins");
		var emailServiceIntervalInMins = GetInt32ValueOrDefault(appSettings, "EmailService.IntervalInMins");
		var emailServiceRetryIntervalInMins = GetInt32ValueOrDefault(appSettings, "EmailService.RetryIntervalInMins");
		var accountServiceIntervalInMins = GetInt32ValueOrDefault(appSettings, "AccountServiceIntervalInMins");
		var dbEventNotificationServiceIntervalInMins = GetInt32ValueOrDefault(appSettings, "DbEventNotificationServiceIntervalInMins");
		var userLastLoggedInDays = GetInt32ValueOrDefault(appSettings, "UserLastLoggedInDays");
		var purgeEmailQueueAfterDays = GetInt32ValueOrDefault(appSettings, "PurgeEmailQueueAfterDays");
		var assessmentStatusUpdateServiceIntervalInMins = GetInt32ValueOrDefault(appSettings, "AssessmentStatusUpdateServiceIntervalInMins");
		var assessmentRequestServiceIntervalInMins = GetInt32ValueOrDefault(appSettings, "AssessmentRequestService.IntervalInMins");
		var obfuscatorServiceIntervalInMins = GetInt32ValueOrDefault(appSettings, "ObfuscatorService.IntervalInMins");
		var obfuscatorServiceRetryIntervalInMins = GetInt32ValueOrDefault(appSettings, "ObfuscatorService.RetryIntervalInMins");
		var deObfuscatorServiceIntervalInMins = GetInt32ValueOrDefault(appSettings, "DeObfuscatorService.IntervalInMins");
		var deObfuscatorServiceRetryIntervalInMins = GetInt32ValueOrDefault(appSettings, "DeObfuscatorService.RetryIntervalInMins");
		var eCOnboardingServiceIntervalInMins = GetInt32ValueOrDefault(appSettings, "ECOnboardingServiceIntervalInMins");

		// TODO: Revisit Default Values
		var result = new COREDOMAINDATAMODELSDOMAIN.ProvisionServiceSettings
		{
			ProvisionBody = await LoadTemplateAsync(appSettings, "ProvisionBody", "ProvisionBodyFilePath").ConfigureAwait(false),
			ProvisionSubject = GetValueOrDefault(appSettings, "ProvisionSubject"),
			ProvisionSubjectPrefix = GetValueOrDefault(appSettings, "ProvisionSubjectPrefix"),
			ProvisionSubjectSuffix = GetValueOrDefault(appSettings, "ProvisionSubjectSuffix"),
			ProvisionAdditionalToAddress = GetValueOrDefault(appSettings, "ProvisionAdditionalToAddress"),
			ProvisionAdditionalCcAddress = GetValueOrDefault(appSettings, "ProvisionAdditionalCcAddress"),

			DecommissionBody = await LoadTemplateAsync(appSettings, "DecommissionBody", "DecommissionBodyFilePath").ConfigureAwait(false),
			DecommissionSubject = GetValueOrDefault(appSettings, "DecommissionSubject"),
			DecommissionSubjectPrefix = GetValueOrDefault(appSettings, "DecommissionSubjectPrefix"),
			DecommissionSubjectSuffix = GetValueOrDefault(appSettings, "DecommissionSubjectSuffix"),
			DecommissionAdditionalToAddress = GetValueOrDefault(appSettings, "DecommissionAdditionalToAddress"),
			DecommissionAdditionalCcAddress = GetValueOrDefault(appSettings, "DecommissionAdditionalCcAddress"),

			SelfServiceProvisionBody = await LoadTemplateAsync(appSettings, "SelfServiceProvisionBody", "SelfServiceProvisionBodyFilePath").ConfigureAwait(false),
			SelfServiceProvisionSubject = GetValueOrDefault(appSettings, "SelfServiceProvisionSubject"),
			SelfServiceProvisionSubjectPrefix = GetValueOrDefault(appSettings, "SelfServiceProvisionSubjectPrefix"),
			SelfServiceProvisionSubjectSuffix = GetValueOrDefault(appSettings, "SelfServiceProvisionSubjectSuffix"),
			SelfServiceProvisionAdditionalToAddress = GetValueOrDefault(appSettings, "SelfServiceProvisionAdditionalToAddress"),
			SelfServiceProvisionAdditionalCcAddress = GetValueOrDefault(appSettings, "SelfServiceProvisionAdditionalCcAddress"),

			SelfServiceDecommissionBody = await LoadTemplateAsync(appSettings, "SelfServiceDecommissionBody", "SelfServiceDecommissionBodyFilePath").ConfigureAwait(false),
			SelfServiceDecommissionSubject = GetValueOrDefault(appSettings, "SelfServiceDecommissionSubject"),
			SelfServiceDecommissionSubjectPrefix = GetValueOrDefault(appSettings, "SelfServiceDecommissionSubjectPrefix"),
			SelfServiceDecommissionSubjectSuffix = GetValueOrDefault(appSettings, "SelfServiceDecommissionSubjectSuffix"),
			SelfServiceDecommissionAdditionalToAddress = GetValueOrDefault(appSettings, "SelfServiceDecommissionAdditionalToAddress"),
			SelfServiceDecommissionAdditionalCcAddress = GetValueOrDefault(appSettings, "SelfServiceDecommissionAdditionalCcAddress"),

			ProvisionReminderBody = await LoadTemplateAsync(appSettings, "ProvisionReminderBody", "ProvisionReminderBodyFilePath").ConfigureAwait(false),
			ProvisionReminderSubject = GetValueOrDefault(appSettings, "ProvisionReminderSubject"),
			ProvisionReminderSubjectPrefix = GetValueOrDefault(appSettings, "ProvisionReminderSubjectPrefix"),
			ProvisionReminderSubjectSuffix = GetValueOrDefault(appSettings, "ProvisionReminderSubjectSuffix"),
			ProvisionReminderAdditionalToAddress = GetValueOrDefault(appSettings, "ProvisionReminderAdditionalToAddress"),
			ProvisionReminderAdditionalCcAddress = GetValueOrDefault(appSettings, "ProvisionReminderAdditionalCcAddress"),

			DecommissionReminderBody = await LoadTemplateAsync(appSettings, "DecommissionReminderBody", "DecommissionReminderBodyFilePath").ConfigureAwait(false),
			DecommissionReminderSubject = GetValueOrDefault(appSettings, "DecommissionReminderSubject"),
			DecommissionReminderSubjectPrefix = GetValueOrDefault(appSettings, "DecommissionReminderSubjectPrefix"),
			DecommissionReminderSubjectSuffix = GetValueOrDefault(appSettings, "DecommissionReminderSubjectSuffix"),
			DecommissionReminderAdditionalToAddress = GetValueOrDefault(appSettings, "DecommissionReminderAdditionalToAddress"),
			DecommissionReminderAdditionalCcAddress = GetValueOrDefault(appSettings, "DecommissionReminderAdditionalCcAddress"),

			SelfServiceProvisionReminderBody = await LoadTemplateAsync(appSettings, "SelfServiceProvisionReminderBody", "SelfServiceProvisionReminderBodyFilePath").ConfigureAwait(false),
			SelfServiceProvisionReminderSubject = GetValueOrDefault(appSettings, "SelfServiceProvisionReminderSubject"),
			SelfServiceProvisionReminderSubjectPrefix = GetValueOrDefault(appSettings, "SelfServiceProvisionReminderSubjectPrefix"),
			SelfServiceProvisionReminderSubjectSuffix = GetValueOrDefault(appSettings, "SelfServiceProvisionReminderSubjectSuffix"),
			SelfServiceProvisionReminderAdditionalToAddress = GetValueOrDefault(appSettings, "SelfServiceProvisionReminderAdditionalToAddress"),
			SelfServiceProvisionReminderAdditionalCcAddress = GetValueOrDefault(appSettings, "SelfServiceProvisionReminderAdditionalCcAddress"),

			SelfServiceDecommissionReminderBody = await LoadTemplateAsync(appSettings, "SelfServiceDecommissionReminderBody", "SelfServiceDecommissionReminderBodyFilePath").ConfigureAwait(false),
			SelfServiceDecommissionReminderSubject = GetValueOrDefault(appSettings, "SelfServiceDecommissionReminderSubject"),
			SelfServiceDecommissionReminderSubjectPrefix = GetValueOrDefault(appSettings, "SelfServiceDecommissionReminderSubjectPrefix"),
			SelfServiceDecommissionReminderSubjectSuffix = GetValueOrDefault(appSettings, "SelfServiceDecommissionReminderSubjectSuffix"),
			SelfServiceDecommissionReminderAdditionalToAddress = GetValueOrDefault(appSettings, "SelfServiceDecommissionReminderAdditionalToAddress"),
			SelfServiceDecommissionReminderAdditionalCcAddress = GetValueOrDefault(appSettings, "SelfServiceDecommissionReminderAdditionalCcAddress"),

			ProvisioningHelpPageContent = GetValueOrDefault(appSettings, "ProvisioningHelpPageContent"),

			ProvisionServiceEmailTemplateCss = await LoadTemplateAsync(appSettings, "EmailTemplateCss", "EmailTemplateCssFilePath").ConfigureAwait(false),
			ProvisionServiceMutexContainerName = GetValueOrDefault(appSettings, "MutexContainerName"),
			ProvisionReminderServiceMutexBlobName = GetValueOrDefault(appSettings, "ReminderService.MutexBlobName"),
			ProvisionRetryServiceMutexBlobName = GetValueOrDefault(appSettings, "RetryService.MutexBlobName"),
			QueueMessageRetryServiceMutexBlobName = GetValueOrDefault(appSettings, "QueueMessageRetryService.MutexBlobName"),
			ProvisionStatusUpdateServiceMutexBlobName = GetValueOrDefault(appSettings, "StatusUpdateService.MutexBlobName"),
			ExcludeProvisionServiceParameterCsvFromEmail = GetValueOrDefault(appSettings, "ExcludeProvisionServiceParameterCSVFromEmail"),
			AssetValidationIntervalInMins = assetValidationIntervalInMins == default ? 300 : assetValidationIntervalInMins,
			WorkerIntervalInSecs = workerIntervalInSecs == default ? 300 : workerIntervalInSecs,
			RetryIntervalInMins = retryIntervalInMins == default ? 120 : retryIntervalInMins,
			QueueMessageRetryIntervalInMins = queueMessageRetryIntervalInMins == default ? 120 : queueMessageRetryIntervalInMins,
			ReminderIntervalInHrs = reminderIntervalInHrs == default ? 24 : reminderIntervalInHrs,
			StatusUpdateIntervalInMins = statusUpdateIntervalInMins == default ? 120 : statusUpdateIntervalInMins,
			IsEnabled = GetBooleanValueOrDefault(appSettings, "IsEnabled"),
			IsRetryEnabled = GetBooleanValueOrDefault(appSettings, "IsRetryEnabled"),
			IsQueueMessageRetryEnabled = GetBooleanValueOrDefault(appSettings, "IsQueueMessageRetryEnabled"),
			IsReminderEnabled = GetBooleanValueOrDefault(appSettings, "IsReminderEnabled"),
			IsStatusUpdateEnabled = GetBooleanValueOrDefault(appSettings, "IsStatusUpdateEnabled"),
			IsProvisionSelfServiceEmailEnabled = GetBooleanValueOrDefault(appSettings, "IsProvisionSelfServiceEmailEnabled"),
			IsAssetValidationEnabled = GetBooleanValueOrDefault(appSettings, "IsAssetValidationEnabled"),
			IsAssetValidationEmailEnabled = GetBooleanValueOrDefault(appSettings, "AssetValidationService.IsEmailEnabled"),
			IsAssessmentEmailEnabledToUser = GetBooleanValueOrDefault(appSettings, "IsAssessmentEmailEnabledToUser"),
			IsAssetVersionEmailValidationEnabled = GetBooleanValueOrDefault(appSettings, "AssetValidationService.IsAssetVersionEmailValidationEnabled"),
			EmailRetryCount = emailRetryCount == default ? 5 : emailRetryCount,
			AssetValidationServiceMailTemplateTemplate = await LoadTemplateAsync(appSettings, "AssetValidationService.MailTemplate.Template", "AssetValidationService.MailTemplate.TemplateFilePath").ConfigureAwait(false),
			AssetValidationServiceMailTemplateSubjectPrefix = GetValueOrDefault(appSettings, "AssetValidationService.MailTemplate.SubjectPrefix"),
			AssetValidationServiceMailTemplateSubjectSuffix = GetValueOrDefault(appSettings, "AssetValidationService.MailTemplate.SubjectSuffix"),
			AssetValidationServiceMutexBlobName = GetValueOrDefault(appSettings, "AssetValidationService.MutexBlobName"),
			AssetValidationServiceEmailColumns = GetValueOrDefault(appSettings, "AssetValidationServiceEmailColumns"),

			EmailServiceMutexBlobName = GetValueOrDefault(appSettings, "EmailService.MutexBlobName"),
			IsEmailServiceEnabled = GetBooleanValueOrDefault(appSettings, "EmailService.IsEnabled"),
			EmailServiceIntervalInMins = emailServiceIntervalInMins,
			EmailServiceRetryIntervalInMins = emailServiceRetryIntervalInMins,

			AccountServiceMutexBlobName = GetValueOrDefault(appSettings, "AccountService.MutexBlobName"),
			IsAccountServiceEnabled = GetBooleanValueOrDefault(appSettings, "IsAccountServiceEnabled"),
			AccountServiceIntervalInMins = accountServiceIntervalInMins,
			UserLastLoggedInDays = userLastLoggedInDays,
			DbEventNotificationServiceMutexBlobName = GetValueOrDefault(appSettings, "DbEventNotificationService.MutexBlobName"),
			IsDbEventNotificationServiceEnabled = GetBooleanValueOrDefault(appSettings, "IsDbEventNotificationServiceEnabled"),
			DbEventNotificationServiceIntervalInMins = dbEventNotificationServiceIntervalInMins == default ? 5 : dbEventNotificationServiceIntervalInMins,
			PurgeEmailQueueAfterDays = purgeEmailQueueAfterDays,

			AssessmentStatusUpdateServiceMutexBlobName = GetValueOrDefault(appSettings, "AssessmentStatusUpdateService.MutexBlobName"),
			IsAssessmentStatusUpdateServiceEnabled = GetBooleanValueOrDefault(appSettings, "IsAssessmentStatusUpdateServiceEnabled"),
			AssessmentStatusUpdateServiceIntervalInMins = assessmentStatusUpdateServiceIntervalInMins == default ? 5 : assessmentStatusUpdateServiceIntervalInMins,

			AssessmentRequestServiceMutexBlobName = GetValueOrDefault(appSettings, "AssessmentRequestService.MutexBlobName"),
			IsAssessmentRequestServiceEnabled = GetBooleanValueOrDefault(appSettings, "IsAssessmentRequestServiceEnabled"),
			AssessmentRequestServiceIntervalInMins = assessmentRequestServiceIntervalInMins == default ? 5 : assessmentRequestServiceIntervalInMins,

			ObfuscatorServiceMutexBlobName = GetValueOrDefault(appSettings, "ObfuscatorService.MutexBlobName"),
			IsObfuscatorServiceEnabled = GetBooleanValueOrDefault(appSettings, "ObfuscatorService.IsEnabled"),
			ObfuscatorServiceIntervalInMins = obfuscatorServiceIntervalInMins,
			ObfuscatorServiceRetryIntervalInMins = obfuscatorServiceRetryIntervalInMins,

			StorageProvider = GetValueOrDefault(appSettings, "StorageProvider"),
			ContainerName = GetValueOrDefault(appSettings, "ContainerName"),

			IsECOnboardingServiceEnabled = GetBooleanValueOrDefault(appSettings, "IsECOnboardingServiceEnabled"),
			ECOnboardingServiceMutexBlobName = GetValueOrDefault(appSettings, "ProvisionService.ECOnboardingServiceMutexBlobName"),
			ECOnboardingServiceIntervalInMins = eCOnboardingServiceIntervalInMins == default ? 5 : eCOnboardingServiceIntervalInMins,
			ECOnboardingServiceSubject = GetValueOrDefault(appSettings, "ECOnboardingServiceSubject"),
			ECOnboardingServiceToAddress = GetValueOrDefault(appSettings, "ECOnboardingServiceToAddress"),
			ECOnboardingServiceCcAddress = GetValueOrDefault(appSettings, "ECOnboardingServiceCcAddress"),
			ECOnboardingServiceBody = GetValueOrDefault(appSettings, "ECOnboardingServiceBody"),

		};

		return result;
	}

	/// <summary>
	/// Load Template 
	/// </summary>
	/// <param name="appSettings"></param>
	/// <param name="key"></param>
	/// <returns></returns>
	private static async Task<String> LoadTemplateAsync(IEnumerable<COREDOMAINDATAMODELSDOMAIN.AppSetting> appSettings, String templateKey, String templateFilePathKey)
	{
		var template = appSettings.GetValue<String>(templateKey) ?? String.Empty;

		if (String.IsNullOrEmpty(template))
		{
			var templateFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!, appSettings.GetValue<String>(templateFilePathKey) ?? String.Empty);

			if (!String.IsNullOrEmpty(templateFilePath))
			{
				template = await File.ReadAllTextAsync(templateFilePath).ConfigureAwait(false);
			}
		}

		return template;
	}

	/// <summary>
	/// Populate WebAPI Settings
	/// </summary>
	/// <returns></returns>
	private async Task<COREDOMAINDATAMODELSDOMAIN.WebAPIServiceSettings> LoadWebAPISettingsAsync()
	{
		var appSettings = await this.GetAsync("WebAPI").ConfigureAwait(false);

		// TODO: Default values ?
		return new COREDOMAINDATAMODELSDOMAIN.WebAPIServiceSettings
		{
			BatchSize = GetInt32ValueOrDefault(appSettings, "BatchSize"),
			RetryMessageBusCount = GetInt32ValueOrDefault(appSettings, "RetryMessageBusCount"),
			RequiredProvisionStatusForLiveEnvironment = GetValueOrDefault(appSettings, "RequiredProvisionStatusForLiveEnvironment"),
			ObfuscatorConfigJsonPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!, GetValueOrDefault(appSettings, "ObfuscatorConfigJsonPath")),
			EmailTemplateCssFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!, GetValueOrDefault(appSettings, "EmailTemplateCssFilePath")),
			IsAssetVersionLockedCheckRequired = GetBooleanValueOrDefault(appSettings, "IsAssetVersionLockedCheckRequired"),
			AssetVersionOwnerRole = GetValueOrDefault(appSettings, "AssetVersionOwnerRole"),
			AssetVersionContactRole = GetValueOrDefault(appSettings, "AssetVersionContactRole"),
			AssetVersionProvisionTeamRole = GetValueOrDefault(appSettings, "AssetVersionProvisionTeamRole"),
			IncludeAssetRbac = GetBooleanValueOrDefault(appSettings, "IncludeAssetRbac"),
			IncludeEnvironmentRbac = GetBooleanValueOrDefault(appSettings, "IncludeEnvironmentRbac"),
			RequestAssessmentMailTemplate = GetValueOrDefault(appSettings, "RequestAssessmentMailTemplate"),
			StorageProvider = GetValueOrDefault(appSettings, "StorageProvider"),
			ContainerName = GetValueOrDefault(appSettings, "ContainerName"),
			CodeRepositoryFolderName = GetValueOrDefault(appSettings, "CodeRepositoryFolderName"),
			IsZipPasswordEnabled = GetBooleanValueOrDefault(appSettings, "IsZipPasswordEnabled"),
			UserGuideFolderName = GetValueOrDefault(appSettings, "UserGuideFolderName"),
			ARCECDataSyncService = GetGuidValueOrDefault(appSettings, GetValueOrDefault(appSettings, "ARCECDataSyncService") ?? "00000000-0000-0000-0000-000000000000"),
			ARCECApp = GetGuidValueOrDefault(appSettings, GetValueOrDefault(appSettings, "ARCECApp") ?? "00000000-0000-0000-0000-000000000000"),
			IsShowCreatedByModifiedByEmailId = GetBooleanValueOrDefault(appSettings, "IsShowCreatedByModifiedByEmailId"),
			AssetVersionArtifactFolderName = GetValueOrDefault(appSettings, "AssetVersionArtifactFolderName"),
			ValidateHeadersForAPI = GetBooleanValueOrDefault(appSettings, "ValidateHeadersForAPI"),
			QBExternalSystemUId = GetValueOrDefault(appSettings, "QBExternalSystemUId")
		};

	}

	/// <summary>
	/// Populate WebUI Settings
	/// </summary>
	/// <returns></returns>
	private async Task<COREDOMAINDATAMODELSDOMAIN.WebUIServiceSettings> LoadWebUISettingsAsync()
	{
		var appSettings = await this.GetAsync("WebUI").ConfigureAwait(false);

		return new COREDOMAINDATAMODELSDOMAIN.WebUIServiceSettings
		{
			MaintenanceEndDate = appSettings.GetValue<DateTime?>("MaintenanceEndDate"),
			MaintenanceExceptionForUser = GetValueOrDefault(appSettings, "MaintenanceExceptionForUser"),
			IsAssetVersionAssessmentFeatureEnabled = GetBooleanValueOrDefault(appSettings, "IsAssetVersionAssessmentFeatureEnabled"),
			IsAssetVersionQuestionnaireFeatureEnabled = GetBooleanValueOrDefault(appSettings, "IsAssetVersionQuestionnaireFeatureEnabled"),
			IsAssetVersionAssessmentAutoLoadFeatureEnabled = GetBooleanValueOrDefault(appSettings, "IsAssetVersionAssessmentAutoLoadFeatureEnabled"),
			IsAutoAssignQuestionnaireOnLoadFeatureEnabled = GetBooleanValueOrDefault(appSettings, "IsAutoAssignQuestionnaireOnLoadFeatureEnabled"),
			IsAssetVersionComponentDependencyFeatureEnabled = GetBooleanValueOrDefault(appSettings, "IsAssetVersionComponentDependencyFeatureEnabled"),
			IsMultipleTabLogoutFeatureEnabled = GetBooleanValueOrDefault(appSettings, "IsMultipleTabLogoutFeatureEnabled")
		};

	}

	/// <summary>
	/// Loads the caching settings asynchronous.
	/// </summary>
	/// <returns></returns>
	private async Task<COREDOMAINDATAMODELSDOMAIN.CachingSettings> LoadCachingSettingsAsync()
	{
		var appSettings = await this.GetAsync("Caching").ConfigureAwait(false);

		return new COREDOMAINDATAMODELSDOMAIN.CachingSettings
		{
			AbsoluteExpirationInMinutes = GetInt32ValueOrDefault(appSettings, "AbsoluteExpirationInMinutes"),
			SlidingExpirationInMinutes = GetInt32ValueOrDefault(appSettings, "SlidingExpirationInMinutes"),
			IsEnabled = GetBooleanValueOrDefault(appSettings, "IsEnabled"),
		};
	}

	/// <summary>
	/// Populate Email Settings
	/// </summary>
	/// <returns></returns>
	private async Task<COREDOMAINDATAMODELSDOMAIN.EmailSettings> LoadEmailSettingsAsync()
	{
		var appSettings = await this.GetAsync("Email").ConfigureAwait(false);

		return new COREDOMAINDATAMODELSDOMAIN.EmailSettings
		{
			EmailProviderType = GetValueOrDefault(appSettings, "EmailProviderType"),
			From = GetValueOrDefault(appSettings, "From"),
			DebugProviderFolderPath = GetValueOrDefault(appSettings, "DebugProvider.FolderPath"),
			OneAssetProviderEndpoint = GetValueOrDefault(appSettings, "OneAssetProvider.Endpoint"),
			OneAssetProviderClientId = GetValueOrDefault(appSettings, "OneAssetProvider.ClientId"),
			OneAssetProviderClientSecret = GetValueOrDefault(appSettings, "OneAssetProvider.ClientSecret"),
			OneAssetProviderTenantId = GetValueOrDefault(appSettings, "OneAssetProvider.TenantId"),
			OneAssetProviderScope = GetValueOrDefault(appSettings, "OneAssetProvider.Scope"),
			OneAssetProviderEmailActionUId = GetValueOrDefault(appSettings, "OneAssetProvider.EmailActionUId"),
			OneAssetProviderCreatedByApp = GetValueOrDefault(appSettings, "OneAssetProvider.CreatedByApp"),
			OneAssetProviderEncodeBody = GetBooleanValueOrDefault(appSettings, "OneAssetProvider.EncodeBody"),
			AllowedEmailDomainsCsv = GetValueOrDefault(appSettings, "AllowedEmailDomainsCsv"),
			ReplacementEnvironmentCsv = GetValueOrDefault(appSettings, "ReplacementEnvironmentCsv"),
			ReplacementToEmailId = GetValueOrDefault(appSettings, "ReplacementToEmailId"),
			ReplacementCcEmailId = GetValueOrDefault(appSettings, "ReplacementCcEmailId"),
			ReplacementBccEmailId = GetValueOrDefault(appSettings, "ReplacementBccEmailId"),
			IsExtendedFailedLoggingEnabled = GetBooleanValueOrDefault(appSettings, "IsExtendedFailedLoggingEnabled"), // Logs PII
		};

	}

	#endregion

	#region Helper Methods

	/// <summary>
	/// Gets the value or default.
	/// </summary>
	/// <param name="appSettings"></param>
	/// <param name="key"></param>
	/// <returns></returns>
	private string GetValueOrDefault(IEnumerable<COREDOMAINDATAMODELSDOMAIN.AppSetting> appSettings, String key)
	{
		return appSettings.GetValue<string>(key) ?? string.Empty;
	}

	/// <summary>
	/// Gets the boolean value or default.
	/// </summary>
	/// <param name="appSettings"></param>
	/// <param name="key"></param>
	/// <returns></returns>
	private Boolean GetBooleanValueOrDefault(IEnumerable<COREDOMAINDATAMODELSDOMAIN.AppSetting> appSettings, String key)
	{
		return Boolean.Parse(appSettings.GetValue<String>(key) ?? "false");
	}

	/// <summary>
	/// Gets the int32 value or default.
	/// </summary>
	/// <param name="appSettings"></param>
	/// <param name="key"></param>
	/// <returns></returns>
	private Int32 GetInt32ValueOrDefault(IEnumerable<COREDOMAINDATAMODELSDOMAIN.AppSetting> appSettings, String key)
	{
		return Int32.Parse(appSettings.GetValue<String>(key) ?? "0");
	}

	/// <summary>
	/// Gets the GUID value or default.
	/// </summary>
	/// <param name="appSettings"></param>
	/// <param name="key"></param>
	/// <returns></returns>
	private Guid GetGuidValueOrDefault(IEnumerable<COREDOMAINDATAMODELSDOMAIN.AppSetting> appSettings, String key)
	{
		return Guid.Parse(appSettings.GetValue<String>(key) ?? "00000000-0000-0000-0000-000000000000");
	}

	/// <summary>
	/// Combines the URL.
	/// </summary>
	/// <param name="baseUrl"></param>
	/// <param name="relativeUrl"></param>
	/// <returns></returns>
	private String CombineUrl(String baseUrl, String relativeUrl)
	{
		return $"{baseUrl}/{relativeUrl.TrimStart('/')}";
	}

	#endregion
}


