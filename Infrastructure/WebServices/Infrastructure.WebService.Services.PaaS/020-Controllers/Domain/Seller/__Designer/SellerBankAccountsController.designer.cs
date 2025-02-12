namespace Accenture.WebShop.Infrastructure.WebService.Services.PaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents SellerBankAccounts Controller.
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
	/// Get SellerBankAccounts
	/// </summary>
	/// <returns></returns>
	[Route("SellerBankAccounts")]
	[HttpGet]
	public virtual async Task<IActionResult> GetSellerBankAccountsAsync([FromQuery] Guid sellerBankAccountUId, [FromQuery] Guid sellerUId)
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetSellerBankAccountRequest
			{
				SellerBankAccountUId = sellerBankAccountUId,
				SellerUId = sellerUId,
				CorrelationUId = correlationUId	
			};

			var response = await _seller!.GetSellerBankAccountsAsync(request).ConfigureAwait(false);
			
			actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.SellerBankAccounts);
							
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetSellerBankAccountsAsync)}", nameof(SellersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Merge SellerBankAccounts
	/// </summary>
	/// <returns></returns>
	[Route("SellerBankAccounts")]
	[HttpPost]
	public virtual async Task<IActionResult> MergeSellerBankAccountsAsync()
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await Request.GetRawBodyStringAsync().ConfigureAwait(false);

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(SellersController));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var sellerBankAccounts = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.SellerBankAccount>>();

				if (sellerBankAccounts != null && sellerBankAccounts.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeSellerBankAccountRequest
					{
						SellerBankAccounts = sellerBankAccounts,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._seller!.MergeSellerBankAccountsAsync(request).ConfigureAwait(false);

					actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeSellerBankAccountsAsync)} - {nameof(sellerBankAccounts)} is null", nameof(SellersController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeSellerBankAccountsAsync)}", nameof(SellersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	#endregion

	#region Private Methods

	#endregion
}
