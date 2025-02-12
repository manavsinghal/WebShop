namespace Accenture.WebShop.Infrastructure.WebService.Services.PaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents ShipperBankAccounts Controller.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
public partial class ShippersController : INFRAWEBSERVICESERVICES.ControllerBase<COREDOMAINDATAMODELSDOMAIN.Shipper>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get ShipperBankAccounts
	/// </summary>
	/// <returns></returns>
	[Route("ShipperBankAccounts")]
	[HttpGet]
	public virtual async Task<IActionResult> GetShipperBankAccountsAsync([FromQuery] Guid shipperBankAccountUId, [FromQuery] Guid shipperUId)
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetShipperBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShippersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetShipperBankAccountRequest
			{
				ShipperBankAccountUId = shipperBankAccountUId,
				ShipperUId = shipperUId,
				CorrelationUId = correlationUId	
			};

			var response = await _shipper!.GetShipperBankAccountsAsync(request).ConfigureAwait(false);
			
			actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.ShipperBankAccounts);
							
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetShipperBankAccountsAsync)}", nameof(ShippersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShipperBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShippersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Merge ShipperBankAccounts
	/// </summary>
	/// <returns></returns>
	[Route("ShipperBankAccounts")]
	[HttpPost]
	public virtual async Task<IActionResult> MergeShipperBankAccountsAsync()
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShipperBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShippersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await Request.GetRawBodyStringAsync().ConfigureAwait(false);

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(ShippersController));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var shipperBankAccounts = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.ShipperBankAccount>>();

				if (shipperBankAccounts != null && shipperBankAccounts.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeShipperBankAccountRequest
					{
						ShipperBankAccounts = shipperBankAccounts,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._shipper!.MergeShipperBankAccountsAsync(request).ConfigureAwait(false);

					actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeShipperBankAccountsAsync)} - {nameof(shipperBankAccounts)} is null", nameof(ShippersController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeShipperBankAccountsAsync)}", nameof(ShippersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShipperBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShippersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	#endregion

	#region Private Methods

	#endregion
}
