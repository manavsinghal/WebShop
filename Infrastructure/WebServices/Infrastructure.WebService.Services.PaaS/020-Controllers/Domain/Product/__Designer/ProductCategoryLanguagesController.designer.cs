namespace Accenture.WebShop.Infrastructure.WebService.Services.PaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents ProductCategoryLanguages Controller.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
public partial class ProductsController : INFRAWEBSERVICESERVICES.ControllerBase<COREDOMAINDATAMODELSDOMAIN.Product>
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
	[Route("ProductCategoryLanguages")]
	[HttpGet]
	public virtual async Task<IActionResult> GetProductCategoryLanguagesAsync([FromQuery] Guid productCategoryLanguageUId)
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetProductCategoryLanguageRequest
			{
				ProductCategoryLanguageUId = productCategoryLanguageUId,
				CorrelationUId = correlationUId	
			};

			var response = await _product!.GetProductCategoryLanguagesAsync(request).ConfigureAwait(false);
			
			actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.ProductCategoryLanguages);
							
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetProductCategoryLanguagesAsync)}", nameof(ProductsController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Merge ProductCategoryLanguages
	/// </summary>
	/// <returns></returns>
	[Route("ProductCategoryLanguages")]
	[HttpPost]
	public virtual async Task<IActionResult> MergeProductCategoryLanguagesAsync()
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await Request.GetRawBodyStringAsync().ConfigureAwait(false);

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(ProductsController));

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

					actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeProductCategoryLanguagesAsync)} - {nameof(productCategoryLanguages)} is null", nameof(ProductsController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeProductCategoryLanguagesAsync)}", nameof(ProductsController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	#endregion

	#region Private Methods

	#endregion
}
