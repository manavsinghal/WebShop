namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents ProductLanguages Controller.
/// </summary>
public partial class ProductsFunction : FunctionBase<ProductsFunction>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get ProductLanguages
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetProductLanguagesAsync))]
	public virtual async Task<HttpResponseData> GetProductLanguagesAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Products/ProductLanguages")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var productLanguageUId = GetQueryStringValue("productLanguageUId");
		var productUId = GetQueryStringValue("productUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetProductLanguageRequest
			{
				ProductLanguageUId = String.IsNullOrEmpty(productLanguageUId) ? Guid.Empty : Guid.Parse(productLanguageUId),
				ProductUId = String.IsNullOrEmpty(productUId) ? Guid.Empty : Guid.Parse(productUId),
				CorrelationUId = correlationUId	
			};

			var response = await _product!.GetProductLanguagesAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.ProductLanguages);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetProductLanguagesAsync)}", nameof(ProductsFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge ProductLanguages
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeProductLanguagesAsync))]
	public virtual async Task<HttpResponseData> MergeProductLanguagesAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Products/ProductLanguages")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(ProductsFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var productLanguages = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.ProductLanguage>>();

				if (productLanguages != null && productLanguages.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeProductLanguageRequest
					{
						ProductLanguages = productLanguages,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._product!.MergeProductLanguagesAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeProductLanguagesAsync)} - {nameof(productLanguages)} is null", nameof(ProductsFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeProductLanguagesAsync)}", nameof(ProductsFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
