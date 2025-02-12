namespace Accenture.WebShop.Infrastructure.WebService.Services.PaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents SellerAddresses Controller.
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
	/// Get SellerAddresses
	/// </summary>
	/// <returns></returns>
	[Route("SellerAddresses")]
	[HttpGet]
	public virtual async Task<IActionResult> GetSellerAddressesAsync([FromQuery] Guid sellerAddressUId, [FromQuery] Guid sellerUId)
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetSellerAddressRequest
			{
				SellerAddressUId = sellerAddressUId,
				SellerUId = sellerUId,
				CorrelationUId = correlationUId	
			};

			var response = await _seller!.GetSellerAddressesAsync(request).ConfigureAwait(false);
			
			actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.SellerAddresses);
							
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetSellerAddressesAsync)}", nameof(SellersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Merge SellerAddresses
	/// </summary>
	/// <returns></returns>
	[Route("SellerAddresses")]
	[HttpPost]
	public virtual async Task<IActionResult> MergeSellerAddressesAsync()
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await Request.GetRawBodyStringAsync().ConfigureAwait(false);

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(SellersController));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var sellerAddresses = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.SellerAddress>>();

				if (sellerAddresses != null && sellerAddresses.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeSellerAddressRequest
					{
						SellerAddresses = sellerAddresses,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._seller!.MergeSellerAddressesAsync(request).ConfigureAwait(false);

					actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeSellerAddressesAsync)} - {nameof(sellerAddresses)} is null", nameof(SellersController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeSellerAddressesAsync)}", nameof(SellersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	#endregion

	#region Private Methods

	#endregion
}
