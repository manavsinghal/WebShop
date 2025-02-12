namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents Countries Controller.
/// </summary>
public partial class MastersFunction : FunctionBase<MastersFunction>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get Countries
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetCountriesAsync))]
	public virtual async Task<HttpResponseData> GetCountriesAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "MasterLists/Countries")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var countryUId = GetQueryStringValue("countryUId");
		var languageUId = GetQueryStringValue("languageUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetCountriesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetCountryRequest
			{
				CountryUId = String.IsNullOrEmpty(countryUId) ? Guid.Empty : Guid.Parse(countryUId),
				LanguageUId = String.IsNullOrEmpty(languageUId) ? Guid.Empty : Guid.Parse(languageUId),
				CorrelationUId = correlationUId	
			};

			var response = await _master!.GetCountriesAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.Countries);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetCountriesAsync)}", nameof(MastersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCountriesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge Countries
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeCountriesAsync))]
	public virtual async Task<HttpResponseData> MergeCountriesAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "MasterLists/Countries")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCountriesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(MastersFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var countries = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.Country>>();

				if (countries != null && countries.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeCountryRequest
					{
						Countries = countries,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._master!.MergeCountriesAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeCountriesAsync)} - {nameof(countries)} is null", nameof(MastersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeCountriesAsync)}", nameof(MastersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCountriesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
