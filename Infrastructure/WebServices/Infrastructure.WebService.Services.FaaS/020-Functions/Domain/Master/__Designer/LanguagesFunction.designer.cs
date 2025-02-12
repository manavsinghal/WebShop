namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents Languages Controller.
/// </summary>
public partial class MastersFunction : FunctionBase<MastersFunction>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get Languages
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetLanguagesAsync))]
	public virtual async Task<HttpResponseData> GetLanguagesAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "MasterLists/Languages")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var languageUId = GetQueryStringValue("languageUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetLanguageRequest
			{
				LanguageUId = String.IsNullOrEmpty(languageUId) ? Guid.Empty : Guid.Parse(languageUId),
				CorrelationUId = correlationUId	
			};

			var response = await _master!.GetLanguagesAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.Languages);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetLanguagesAsync)}", nameof(MastersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge Languages
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeLanguagesAsync))]
	public virtual async Task<HttpResponseData> MergeLanguagesAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "MasterLists/Languages")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(MastersFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var languages = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.Language>>();

				if (languages != null && languages.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeLanguageRequest
					{
						Languages = languages,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._master!.MergeLanguagesAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeLanguagesAsync)} - {nameof(languages)} is null", nameof(MastersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
				}				
			}	
			else
			{
				var fault = new Fault
				{
					Title = "Exception has occurred",
					Message = "Empty content",
					FaultType = FaultType.Exception,
					Severity = Severity.Critical,
					ApplicationTier = ApplicationTier.Service,
				};

				httpResponseData = GetErrorResponse(fault);
			}
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);
			
			await HandleError($"Error occurred in {nameof(MergeLanguagesAsync)}", nameof(MastersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Gets the language resources.
	/// </summary>
	/// <param name="culture">The culture.</param>
	/// <returns></returns>
	[Function("GetLanguageResources")]
	public  async Task<HttpResponseData> GetLanguageResources([HttpTrigger(AuthorizationLevel.Function, "get", Route = "MasterLists/languageResources/{culture}")] HttpRequest req, String culture)
	{
		var resourceId = String.Empty;

		Logger.LogInfo($"{nameof(GetLanguageResources)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MastersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
		COREDOMAINDATAMODELS.LanguageResources? response = null;

		try
		{
			SHAREDKERNALRESX.WebShop.ResourceManager.IgnoreCase = true;

			var getLanguageResponse = await _master.GetLanguagesAsync(new COREAPPDATAMODELSDOMAIN.GetLanguageRequest()).ConfigureAwait(false);
			var language = getLanguageResponse.Languages.FirstOrDefault(m => m.LanguageUId == Guid.Parse($"{culture}"));

			response = new COREDOMAINDATAMODELS.LanguageResources
			{
				Culture = culture,
				Literals = new Dictionary<String, String>()
			};

			if (language != null)
			{
				if (String.IsNullOrEmpty(resourceId))
				{
					var resourceSet = SHAREDKERNALRESX.WebShop.ResourceManager.GetResourceSet(new CultureInfo(language.Culture), true, true);

					foreach (DictionaryEntry entry in resourceSet!)
					{
						response.Literals.Add(entry.Key.ToString()!, entry.Value!.ToString()!);
					}
				}
				else
				{
					var resource = SHAREDKERNALRESX.WebShop.ResourceManager.GetString(resourceId, new CultureInfo(language.Culture));

					response.Literals.Add(resourceId, resource!);
				}
			}
			else
			{
				response.Message = $"{culture} not found/supported";
			}
		}
		catch (Exception ex)
		{
			String? innerExceptionMessage = null;

			if (ex.InnerException != null)
			{
				innerExceptionMessage = ex.InnerException.Message;
			}

			await HandleError($"Error occurred in {nameof(GetLanguageResources)}", nameof(MastersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(GetLanguageResources)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MastersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
		}	
		
		return GetSuccessResponse(response);
	}

	#endregion

	#region Private Methods

	#endregion
}
