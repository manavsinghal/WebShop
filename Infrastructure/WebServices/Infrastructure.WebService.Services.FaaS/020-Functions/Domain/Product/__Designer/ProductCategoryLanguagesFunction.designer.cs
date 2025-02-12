namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents ProductCategoryLanguages Controller.
/// </summary>
public partial class ProductsFunction : FunctionBase<ProductsFunction>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get ProductCategoryLanguages
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetProductCategoryLanguagesAsync))]
	public virtual async Task<HttpResponseData> GetProductCategoryLanguagesAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Products/ProductCategoryLanguages")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var productCategoryLanguageUId = GetQueryStringValue("productCategoryLanguageUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetProductCategoryLanguageRequest
			{
				ProductCategoryLanguageUId = String.IsNullOrEmpty(productCategoryLanguageUId) ? Guid.Empty : Guid.Parse(productCategoryLanguageUId),
				CorrelationUId = correlationUId	
			};

			var response = await _product!.GetProductCategoryLanguagesAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.ProductCategoryLanguages);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetProductCategoryLanguagesAsync)}", nameof(ProductsFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge ProductCategoryLanguages
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeProductCategoryLanguagesAsync))]
	public virtual async Task<HttpResponseData> MergeProductCategoryLanguagesAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Products/ProductCategoryLanguages")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(ProductsFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var productCategoryLanguages = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.ProductCategoryLanguage>>();

				if (productCategoryLanguages != null && productCategoryLanguages.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeProductCategoryLanguageRequest
					{
						ProductCategoryLanguages = productCategoryLanguages,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._product!.MergeProductCategoryLanguagesAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeProductCategoryLanguagesAsync)} - {nameof(productCategoryLanguages)} is null", nameof(ProductsFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeProductCategoryLanguagesAsync)}", nameof(ProductsFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
