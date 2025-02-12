#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Core.Application.Models;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// DomainModel
/// </summary>
/// <typeparam name="T"></typeparam>
public partial class DomainModel<T> : BusinessDomain<T> where T : COREDOMAINDATAMODELS.DataModelWithAudit<T>
{
	#region Fields
	protected ILogger<T> Logger { get; init; }
	protected IDistributedCache _cache;

	private readonly MESSAGEHUBINTERFACES.IMessageHub _messageHub;
	protected readonly COREAPPINTERFACESDBAPPSETTINGS.IDbAppSettings _dbAppSettings;
	private readonly IServiceProvider _serviceProvider;

	static List<COREDOMAINDATAMODELS.CacheObjectMappingConfig> cacheObjectMappingConfigJsonData = null;

	public Boolean IsEnabledCaching
	{
		get
		{
			return _dbAppSettings.Caching.IsEnabled;
		}
	}

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="DomainModel{T}"/> class.
	/// </summary>
	/// <param name="logger">The logger.</param>
	/// <param name="cache">The cache.</param>
	/// <param name="messageHub">The message hub.</param>
	/// <param name="dbAppSettings">The database application settings.</param>
	public DomainModel(ILogger<T> logger
					  , IDistributedCache cache 
					  , MESSAGEHUBINTERFACES.IMessageHub messageHub
				      , COREAPPINTERFACESDBAPPSETTINGS.IDbAppSettings dbAppSettings
					  , IServiceProvider serviceProvider) 
	{
		this.Logger = logger;
		this._cache = cache;
		this._messageHub = messageHub;
		this._dbAppSettings = dbAppSettings;
		this._serviceProvider = serviceProvider;

		LoadCacheObjectMappingConfig();
	}

	#endregion

	#region Methods

	/// <summary>
	/// Validates the data model.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="dataModels"></param>
	/// <returns></returns>
	public FaultCollection ValidateDataModel<U>(ICollection<U> dataModels)
	{
		var validationResults = new List<ValidationResult>();

		foreach (var dataModel in dataModels)
		{
			var validationContext = new ValidationContext(dataModel, null, null);

			var isValid = Validator.TryValidateObject(dataModel, validationContext, validationResults, true);

			if (!isValid)
			{
				break;
			}
		}

		if (validationResults.Any())
		{
			faults.AddRange(validationResults.Select(v => new Fault
			{
				Title = "Validation Error",
				Message = v.ErrorMessage,
				FaultType = FaultType.ValidationError,
				Severity = Severity.Critical,
				ApplicationTier = ApplicationTier.BusinessDomain
			}));
		}

		return faults;
	}


	/// <summary>
	/// Creates the exception fault.
	/// </summary>
	/// <param name="ex">The ex.</param>
	/// <returns>Fault</returns>
	protected static Fault CreateExceptionFault(Exception ex)
	{
		Fault fault = null;

		var faultType = FaultType.Exception;

		if (ex != null)
		{
			if (ex.InnerException != null && ex.InnerException.InnerException != null &&
				(ex.InnerException.InnerException is Microsoft.EntityFrameworkCore.DbUpdateException ||
				 ex.InnerException.InnerException is Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException))
			{
				faultType = FaultType.Conflict;
			}
			else
			{
				faultType = FaultType.Exception;
			}

			fault = new Fault
			{
				Title = "Exception has occurred",
				Message = ex.Message,
				FaultType = faultType,
				Severity = Severity.Critical,
				ApplicationTier = ApplicationTier.BusinessDomain,
				StackTrace = ex.StackTrace
			};

			if (ex.InnerException != null)
			{
				fault.StackTrace = ex.InnerException.ToString();
			}
		}

		return fault;
	}

	/// <summary>
	/// Handles the error.
	/// </summary>
	/// <param name="message">The message.</param>
	/// <param name="title">The title.</param>
	/// <param name="exception">The exception.</param>
	/// <returns></returns>
	protected virtual async Task<SHAREDKERNALLIB.WebShopException> HandleError(String message, String? title, Exception? exception = null)
	{
		SHAREDKERNALLIB.WebShopException handleException = null;

		if (exception != null)
		{
			handleException = new SHAREDKERNALLIB.WebShopException(message, exception);

			String innerExceptionMessage = null;

			if (exception.InnerException != null)
			{
				innerExceptionMessage = exception.InnerException.Message;
			}

			await this.SendErrorAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser
										, title
										, SHAREDKERNALRESX.WebShop.CodeComposer
											, $"{{SHAREDKERNALRESX.WebShop.Exception}} in {title} - {exception.Message} - InnerException : {innerExceptionMessage}").ConfigureAwait(false);
		}

		Logger.LogFault(message, "DBException", exception);

		return handleException;
	}

	/// <summary>
	/// Gets the Encryption status
	/// </summary>
	/// <param name="entityName"></param>
	/// <returns></returns>
	protected Boolean GetEncryptionStatus(String entityName)
	{
		var isEncryptionEnabled = false;
		var obfuscatorConfigJsonFilePath = _dbAppSettings.WebAPIService.ObfuscatorConfigJsonPath;

		if (File.Exists(obfuscatorConfigJsonFilePath))
		{
			var jsonSerializerOptions = new JsonSerializerOptions
			{
				ReadCommentHandling = JsonCommentHandling.Skip,
				AllowTrailingCommas = true
			};

			jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

			var obfuscatorConfigJsonData = JsonSerializer.Deserialize<ObfuscatorConfig>(File.ReadAllText(obfuscatorConfigJsonFilePath), jsonSerializerOptions)!;

			if (obfuscatorConfigJsonData != null && obfuscatorConfigJsonData.Entities != null && obfuscatorConfigJsonData.Entities.Any(x => x.Name == entityName))
			{
				isEncryptionEnabled = true;
			}
		}
		else
		{
			var currentDirectoryObfuscatorConfigJsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), SHAREDKERNALLIB.AppSettings.ObfuscatorConfigFilePath);

			if (File.Exists(currentDirectoryObfuscatorConfigJsonFilePath))
			{
				var jsonSerializerOptions = new JsonSerializerOptions
				{
					ReadCommentHandling = JsonCommentHandling.Skip,
					AllowTrailingCommas = true
				};

				jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

				var obfuscatorConfigJsonData = JsonSerializer.Deserialize<ObfuscatorConfig>(File.ReadAllText(currentDirectoryObfuscatorConfigJsonFilePath), jsonSerializerOptions)!;

				if (obfuscatorConfigJsonData != null && obfuscatorConfigJsonData.Entities != null && obfuscatorConfigJsonData.Entities.Any(x => x.Name == entityName))
				{
					isEncryptionEnabled = true;
				}
			}
		}

		return isEncryptionEnabled;
	}

	#region MessageHub Methods

	/// <summary>
	/// Send private information message to eventhub
	/// </summary>
	/// <param name="topic"></param>
	/// <param name="component"></param>
	/// <param name="title"></param>
	/// <param name="description"></param>
	/// <param name="parameters"></param>
	/// <returns></returns>
	protected async Task SendPrivateInfoAsync(MESSAGEHUBENUMS.Topic topic
											  , String component
											  , String title
											  , String description
											  , params Tuple<String, String>[] parameters)
	{
		if (SHAREDKERNALLIB.AppSettings.IsBusinesTierNotificationEnabled &&
			SHAREDKERNALLIB.AppSettings.IsPrivateMethodNotificationEnabled)
		{
			var message = new MESSAGEHUBMODELS.Message
			{
				Component = component,
				Title = title,
				Description = description,
				LogLevel = MESSAGEHUBENUMS.LogLevel.Information,
				IsPrivate = true,
				Sublevel = 2,
				Parameters = parameters
			};

			await this.SendMessageAsync(new List<MESSAGEHUBENUMS.Topic>() { topic }, message).ConfigureAwait(false);
		}
	}

	/// <summary>
	/// Send public information message to eventhub
	/// </summary>
	/// <param name="topic"></param>
	/// <param name="component"></param>
	/// <param name="title"></param>
	/// <param name="description"></param>
	/// <param name="parameters"></param>
	/// <returns></returns>
	protected async Task SendInfoAsync(MESSAGEHUBENUMS.Topic topic
								   , String component
								   , String title
								   , String description
								   , params Tuple<String, String>[] parameters)
	{
		var message = new MESSAGEHUBMODELS.Message
		{
			Component = component,
			Title = title,
			Description = description,
			LogLevel = MESSAGEHUBENUMS.LogLevel.Information,
			Sublevel = 0,
			Parameters = parameters
		};

		await this.SendMessageAsync(new List<MESSAGEHUBENUMS.Topic>() { topic }, message).ConfigureAwait(false);
	}

	/// <summary>
	/// Send private/public error message to eventhub
	/// </summary>
	/// <param name="topic"></param>
	/// <param name="component"></param>
	/// <param name="title"></param>
	/// <param name="description"></param>
	/// <param name="parameters"></param>
	/// <returns></returns>
	protected async Task SendErrorAsync(MESSAGEHUBENUMS.Topic topic
								   , String component
								   , String title
								   , String description
								   , params Tuple<String, String>[] parameters)
	{
		var message = new MESSAGEHUBMODELS.Message
		{
			Component = component,
			Title = title,
			Description = description,
			LogLevel = MESSAGEHUBENUMS.LogLevel.Error,
			Sublevel = 1,
			Parameters = parameters
		};

		await this.SendMessageAsync(new List<MESSAGEHUBENUMS.Topic>() { topic }, message).ConfigureAwait(false);
	}


	/// <summary>
	/// Send public information message to eventhub on multiple topics
	/// </summary>
	/// <param name="topic"></param>
	/// <param name="message"></param>
	/// <returns></returns>
	protected async Task SendMessageAsync(IEnumerable<MESSAGEHUBENUMS.Topic> topics
									  , MESSAGEHUBMODELS.Message message)
	{
		if (SHAREDKERNALLIB.AppSettings.IsBusinesTierNotificationEnabled)
		{
			message = message ?? new MESSAGEHUBMODELS.Message();

			message.Service = SHAREDKERNALLIB.AppSettings.AppName;
			message.Tier = MESSAGEHUBENUMS.Tier.BDL;
			message.RaisedOn = DateTime.UtcNow;
			message.RaisedBy = System.Environment.UserName;
			message.Level = 3;
			message.Operation = MESSAGEHUBENUMS.Operation.Begin;

			await this._messageHub.SendMessageAsync(topics, message).ConfigureAwait(false);
		}
	}

	/// <summary>
	/// Validates the specified t.
	/// </summary>
	/// <param name="t">The t.</param>
	/// <returns></returns>
	protected virtual COREAPPDATAMODELS.ProcessorResponse Validate(T t)
	{
		var response = new COREAPPDATAMODELS.ProcessorResponse();

		//// Add custom code here if needed

		var results = new List<ValidationResult>();
		var context = new ValidationContext(t);

		Validator.TryValidateObject(t, context, results, true);

		results.ForEach(m =>
		{
			response.Faults.Add(new Fault
			{
				Message = m.ErrorMessage
			});
		});

		return response;
	}

	/// <summary>
	/// Validates the specified list.
	/// </summary>
	/// <param name="list">The list.</param>
	/// <returns></returns>
	protected virtual COREAPPDATAMODELS.ProcessorResponse Validate(IEnumerable<T> list)
	{
		var response = new COREAPPDATAMODELS.ProcessorResponse();

		//// Add custom code here if needed

		var results = new List<ValidationResult>();
		var context = new ValidationContext(list);

		foreach (var item in list)
		{
			response += Validate(item);
		}

		return response;
	}

	#endregion

	#region Caching Methods

	/// <summary>
	/// Gets the cached data.
	/// </summary>
	/// <param name="key">The key.</param>
	/// <returns></returns>
	protected async Task<String> GetCachedData(String key, Boolean isEnabledEntityCaching)
	{
		if (IsEnabledCaching && isEnabledEntityCaching)
		{
			var cacheData = await _cache.GetStringAsync(key).ConfigureAwait(false);

			if (cacheData != null && cacheData != "[]")
			{
				return cacheData;
			}
			else
			{
				return null;
			}
		}

		return null;
	}

	/// <summary>
	/// Sets the cached data.
	/// </summary>
	/// <param name="key">The key.</param>
	/// <param name="data">The data.</param>
	protected async Task SetCachedData(String key, String data, Boolean isEnabledEntityCaching)
	{
		if (IsEnabledCaching && isEnabledEntityCaching)
		{
			await _cache.SetStringAsync(key, data,
										new DistributedCacheEntryOptions
										{
											AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_dbAppSettings.Caching.AbsoluteExpirationInMinutes),
											SlidingExpiration = TimeSpan.FromMinutes(_dbAppSettings.Caching.SlidingExpirationInMinutes)
										}).ConfigureAwait(false);
		}
	}

	/// <summary>
	/// Clears the cached data.
	/// </summary>
	/// <param name="key">The key.</param>
	protected async Task ClearCachedData(String key, Boolean isEnabledEntityCaching)
	{
		if (IsEnabledCaching && isEnabledEntityCaching)
		{
			await _cache.RemoveAsync(key).ConfigureAwait(false);
		}
	}

	#endregion

	#region Read Only Method

	/// <summary>
	/// IsAppReadOnlyMode
	/// </summary>
	/// <returns></returns>
	protected async Task<Tuple<Boolean, FaultCollection>> IsAppReadOnlyModeAsync()
	{
		var isAppReadOnlyMode = false;
		var faults = new FaultCollection();

		await _dbAppSettings.RefreshAsync().ConfigureAwait(false);

		var startOn = _dbAppSettings.Common.AppReadOnlyModeStartOn;
		var endOn = _dbAppSettings.Common.AppReadOnlyModeEndOn;

		if (!String.IsNullOrEmpty(startOn) && !String.IsNullOrEmpty(endOn))
		{
			var startDate = DateTime.Parse(startOn, System.Globalization.CultureInfo.InvariantCulture);
			var endDate = DateTime.Parse(endOn, System.Globalization.CultureInfo.InvariantCulture);
			var currentDate = DateTime.UtcNow;

			if (currentDate <= endDate && currentDate >= startDate)
			{
				isAppReadOnlyMode = true;

				var fault = new Fault();
				fault.Message = SHAREDKERNALRESX.WebShop.AppReadOnlyModeMessage;
				faults.Add(fault);
			}
		}

		return Tuple.Create(isAppReadOnlyMode, faults);
	}

#endregion


	private static void SetAuditFields<D, T1>(D auditDataModel, T1 tranDataModel)
		where D : COREDOMAINDATAMODELS.DataModelWithAudit<D>
		where T1 : COREDOMAINDATAMODELS.DataModelWithAudit<T1>
	{
		auditDataModel.CreatedByAccountUId = tranDataModel.CreatedByAccountUId;
		auditDataModel.CreatedByApp = tranDataModel.CreatedByApp;
		auditDataModel.CreatedOn = DateTime.UtcNow;
		auditDataModel.ModifiedByAccountUId = tranDataModel.ModifiedByAccountUId;
		auditDataModel.ModifiedByApp = tranDataModel.ModifiedByApp;
		auditDataModel.ModifiedOn = DateTime.UtcNow;
		auditDataModel.CorrelationUId = AppSettings.CurrentCorrelationUId;
		auditDataModel.EffectiveFrom = DateTime.UtcNow;
		auditDataModel.EffectiveTo = DateTime.UtcNow;
		auditDataModel.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Added;
	}

	#region CacheObjectMapping

	/// <summary>
	/// Load CacheObjectMappingConfig Content
	/// </summary>
	/// <returns></returns>
	private List<COREDOMAINDATAMODELS.CacheObjectMappingConfig> LoadCacheObjectMappingConfig()
	{
		var cacheObjectMappingConfigJson = _dbAppSettings.Common.CacheObjectMappingConfigData;

		if (String.IsNullOrEmpty(cacheObjectMappingConfigJson))
		{
			var cachingConfigJsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), SHAREDKERNALLIB.AppSettings.CachingConfigFilePath);

			if (File.Exists(cachingConfigJsonFilePath))
			{
				cacheObjectMappingConfigJson = File.ReadAllText(cachingConfigJsonFilePath);
			}
		}

		cacheObjectMappingConfigJsonData = new List<COREDOMAINDATAMODELS.CacheObjectMappingConfig>();

		if (!String.IsNullOrEmpty(cacheObjectMappingConfigJson))
		{
			cacheObjectMappingConfigJsonData = cacheObjectMappingConfigJson.ToObject<List<COREDOMAINDATAMODELS.CacheObjectMappingConfig>>();
		}

		return cacheObjectMappingConfigJsonData;
	}

	/// <summary>
	/// Get CacheObjectMappingConfig Content
	/// </summary>
	/// <param name="entityName"></param>
	/// <param name="operationName"></param>
	/// <returns></returns>
	protected COREDOMAINDATAMODELS.CacheObjectMappingConfig GetCacheObjectMappingConfigContent(String entityName, String operationName)
	{
		if (cacheObjectMappingConfigJsonData != null)
		{
			return cacheObjectMappingConfigJsonData.FirstOrDefault(m => m.EntityName == entityName && m.OperationName == operationName);
		}

		return new COREDOMAINDATAMODELS.CacheObjectMappingConfig();
	}

	/// <summary>
	/// EnableCaching.
	/// </summary>
	/// <param name="entityName">entityName.</param>
	/// <param name="operationName">operationName.</param>
	protected Boolean EnableCaching(String entityName, String operationName)
		{
			var cacheObjectMappingConfig = GetCacheObjectMappingConfigContent(entityName, operationName);
		
			if (cacheObjectMappingConfig != null)
			{
				return cacheObjectMappingConfig.IsCachingEnabled;
			}

			return false;
		}

	#endregion

	#region Protected Methods

	/// <summary>
    /// Handles the pre processor faults.
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// <param name="preProcessorResponse">The pre processor response.</param>
    /// <param name="response">The response.</param>
    /// <returns></returns>
	protected Boolean HandlePreProcessorFaults<T1>(COREAPPDATAMODELS.PreProcessorResponse preProcessorResponse, COREAPPDATAMODELS.Response<T1> response)
                    where T1 : COREDOMAINDATAMODELS.DataModel<T1>
    {
        if (preProcessorResponse != null && preProcessorResponse.Faults.Any())
        {
            response.Faults!.AddRange(preProcessorResponse.Faults);
            return true;
        }

        return false;
    }

	/// <summary>
    /// Handles the post processor faults.
    /// </summary>
    /// <typeparam name="T2">The type of the 2.</typeparam>
    /// <param name="postProcessorResponse">The post processor response.</param>
    /// <param name="response">The response.</param>
    /// <returns></returns>
    protected Boolean HandlePostProcessorFaults<T2>(COREAPPDATAMODELS.PostProcessorResponse postProcessorResponse, COREAPPDATAMODELS.Response<T2> response)
                        where T2 : COREDOMAINDATAMODELS.DataModel<T2>
    {
        if (postProcessorResponse != null && postProcessorResponse.Faults.Any())
        {
            response.Faults!.AddRange(postProcessorResponse.Faults);
            return true;
        }

        return false;
    }

	/// <summary>
    /// Gets the cached data if enabled.
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// <param name="request">The request.</param>
    /// <param name="cacheKey">The cache key.</param>
    /// <param name="operationName">Name of the operation.</param>
    /// <returns></returns>
    protected async Task<String> GetCachedDataIfEnabled<T1>(COREAPPDATAMODELS.Request<T1> request, String cacheKey, String operationName)
                        where T1 : COREDOMAINDATAMODELS.DataModel<T1>
    {
        var isEnabledEntityCaching = EnableCaching(typeof(T1).Name, operationName);
        return await GetCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
    }

	/// <summary>
	/// Handles the read only mode asynchronous.
	/// </summary>
	/// <typeparam name="T1">The type of the 1.</typeparam>
	/// <param name="response">The response.</param>
	/// <param name="faults">The faults.</param>
	/// <returns></returns>
	protected async Task<Boolean> HandleReadOnlyModeAsync<T1>(COREAPPDATAMODELS.Response<T1> response, FaultCollection faults) where T1 : COREDOMAINDATAMODELS.DataModel<T1>
	{
		(var isAppReadOnlyMode, faults) = await this.IsAppReadOnlyModeAsync().ConfigureAwait(false);

		if (isAppReadOnlyMode && faults.Any())
		{
			response.Faults = faults;
			return true;
		}

		return false;
	}

	#endregion



	#endregion
}

