namespace Accenture.WebShop.Infrastructure.WebService.Services.PaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents ProductCategories Controller.
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
	/// Get ProductCategories
	/// </summary>
	/// <returns></returns>
	[Route("ProductCategories")]
	[HttpGet]
	public virtual async Task<IActionResult> GetProductCategoriesAsync([FromQuery] Guid productCategoryUId, [FromQuery] Guid languageUId)
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetProductCategoryRequest
			{
				ProductCategoryUId = productCategoryUId,
				LanguageUId = languageUId,
				CorrelationUId = correlationUId	
			};

			var response = await _product!.GetProductCategoriesAsync(request).ConfigureAwait(false);
			
			actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.ProductCategories);
							
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetProductCategoriesAsync)}", nameof(ProductsController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Merge ProductCategories
	/// </summary>
	/// <returns></returns>
	[Route("ProductCategories")]
	[HttpPost]
	public virtual async Task<IActionResult> MergeProductCategoriesAsync()
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await Request.GetRawBodyStringAsync().ConfigureAwait(false);

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(ProductsController));

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

					actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeProductCategoriesAsync)} - {nameof(productCategories)} is null", nameof(ProductsController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeProductCategoriesAsync)}", nameof(ProductsController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	#endregion

	#region Private Methods

	#endregion
}
