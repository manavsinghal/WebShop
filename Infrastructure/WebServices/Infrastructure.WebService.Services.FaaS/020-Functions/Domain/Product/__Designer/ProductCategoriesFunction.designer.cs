namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents ProductCategories Controller.
/// </summary>
public partial class ProductsFunction : FunctionBase<ProductsFunction>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get ProductCategories
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetProductCategoriesAsync))]
	public virtual async Task<HttpResponseData> GetProductCategoriesAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Products/ProductCategories")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var productCategoryUId = GetQueryStringValue("productCategoryUId");
		var languageUId = GetQueryStringValue("languageUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetProductCategoryRequest
			{
				ProductCategoryUId = String.IsNullOrEmpty(productCategoryUId) ? Guid.Empty : Guid.Parse(productCategoryUId),
				LanguageUId = String.IsNullOrEmpty(languageUId) ? Guid.Empty : Guid.Parse(languageUId),
				CorrelationUId = correlationUId	
			};

			var response = await _product!.GetProductCategoriesAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.ProductCategories);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetProductCategoriesAsync)}", nameof(ProductsFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge ProductCategories
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeProductCategoriesAsync))]
	public virtual async Task<HttpResponseData> MergeProductCategoriesAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Products/ProductCategories")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(ProductsFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var productCategories = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.ProductCategory>>();

				if (productCategories != null && productCategories.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeProductCategoryRequest
					{
						ProductCategories = productCategories,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._product!.MergeProductCategoriesAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeProductCategoriesAsync)} - {nameof(productCategories)} is null", nameof(ProductsFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeProductCategoriesAsync)}", nameof(ProductsFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
