namespace Accenture.WebShop.Infrastructure.WebService.Services.PaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents Languages Controller.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
public partial class MastersController : INFRAWEBSERVICESERVICES.ControllerBase<COREDOMAINDATAMODELSDOMAIN.MasterList>
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
	[Route("Languages")]
	[HttpGet]
	public virtual async Task<IActionResult> GetLanguagesAsync([FromQuery] Guid languageUId)
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MastersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetLanguageRequest
			{
				LanguageUId = languageUId,
				CorrelationUId = correlationUId	
			};

			var response = await _master!.GetLanguagesAsync(request).ConfigureAwait(false);
			
			actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.Languages);
							
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetLanguagesAsync)}", nameof(MastersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MastersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Merge Languages
	/// </summary>
	/// <returns></returns>
	[Route("Languages")]
	[HttpPost]
	public virtual async Task<IActionResult> MergeLanguagesAsync()
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MastersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await Request.GetRawBodyStringAsync().ConfigureAwait(false);

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(MastersController));

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

					actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeLanguagesAsync)} - {nameof(languages)} is null", nameof(MastersController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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

				actionResult = GetErrorResponse(fault);
			}
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);
			
			await HandleError($"Error occurred in {nameof(MergeLanguagesAsync)}", nameof(MastersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MastersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Gets the language resources.
	/// </summary>
	/// <param name="culture">The culture.</param>
	/// <returns></returns>
	[Route("languageResources/{culture}")]
	[HttpGet]	
	public  async Task<COREDOMAINDATAMODELS.LanguageResources> GetLanguageResources([FromRoute]String culture)
	{
		var resourceId = String.Empty;

		Logger.LogInfo($"{nameof(GetLanguageResources)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MastersController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
		COREDOMAINDATAMODELS.LanguageResources? response = null;

		try
		{
			SHAREDKERNALRESX.WebShop.ResourceManager.IgnoreCase = true;

			var getLanguageResponse = await _master.GetLanguagesAsync(new COREAPPDATAMODELSDOMAIN.GetLanguageRequest()).ConfigureAwait(false);
			var language = getLanguageResponse.Languages.FirstOrDefault(m => m.LanguageUId == Guid.Parse(culture));

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

			await HandleError($"Error occurred in {nameof(GetLanguageResources)}", nameof(MastersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(GetLanguageResources)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MastersController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
		}	
		
		return response;
	}
	#endregion

	#region Private Methods

	#endregion
}
