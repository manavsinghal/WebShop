namespace Accenture.WebShop.Infrastructure.WebService.Services.PaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents SellerPhones Controller.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
public partial class SellersController : INFRAWEBSERVICESERVICES.ControllerBase<COREDOMAINDATAMODELSDOMAIN.Seller>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get SellerPhones
	/// </summary>
	/// <returns></returns>
	[Route("SellerPhones")]
	[HttpGet]
	public virtual async Task<IActionResult> GetSellerPhonesAsync([FromQuery] Guid sellerPhoneUId, [FromQuery] Guid sellerUId)
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetSellerPhoneRequest
			{
				SellerPhoneUId = sellerPhoneUId,
				SellerUId = sellerUId,
				CorrelationUId = correlationUId	
			};

			var response = await _seller!.GetSellerPhonesAsync(request).ConfigureAwait(false);
			
			actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.SellerPhones);
							
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetSellerPhonesAsync)}", nameof(SellersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Merge SellerPhones
	/// </summary>
	/// <returns></returns>
	[Route("SellerPhones")]
	[HttpPost]
	public virtual async Task<IActionResult> MergeSellerPhonesAsync()
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await Request.GetRawBodyStringAsync().ConfigureAwait(false);

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(SellersController));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var sellerPhones = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.SellerPhone>>();

				if (sellerPhones != null && sellerPhones.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeSellerPhoneRequest
					{
						SellerPhones = sellerPhones,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._seller!.MergeSellerPhonesAsync(request).ConfigureAwait(false);

					actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeSellerPhonesAsync)} - {nameof(sellerPhones)} is null", nameof(SellersController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeSellerPhonesAsync)}", nameof(SellersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	#endregion

	#region Private Methods

	#endregion
}
