namespace Accenture.WebShop.Infrastructure.WebService.Services.PaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents ProductLanguages Controller.
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
	/// Get ProductLanguages
	/// </summary>
	/// <returns></returns>
	[Route("ProductLanguages")]
	[HttpGet]
	public virtual async Task<IActionResult> GetProductLanguagesAsync([FromQuery] Guid productLanguageUId, [FromQuery] Guid productUId)
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetProductLanguageRequest
			{
				ProductLanguageUId = productLanguageUId,
				ProductUId = productUId,
				CorrelationUId = correlationUId	
			};

			var response = await _product!.GetProductLanguagesAsync(request).ConfigureAwait(false);
			
			actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.ProductLanguages);
							
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetProductLanguagesAsync)}", nameof(ProductsController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Merge ProductLanguages
	/// </summary>
	/// <returns></returns>
	[Route("ProductLanguages")]
	[HttpPost]
	public virtual async Task<IActionResult> MergeProductLanguagesAsync()
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await Request.GetRawBodyStringAsync().ConfigureAwait(false);

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(ProductsController));

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

					actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeProductLanguagesAsync)} - {nameof(productLanguages)} is null", nameof(ProductsController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeProductLanguagesAsync)}", nameof(ProductsController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	#endregion

	#region Private Methods

	#endregion
}
