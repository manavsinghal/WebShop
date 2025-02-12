namespace Accenture.WebShop.Infrastructure.WebService.Services.PaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents ShipperPhones Controller.
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
	/// Get ShipperPhones
	/// </summary>
	/// <returns></returns>
	[Route("ShipperPhones")]
	[HttpGet]
	public virtual async Task<IActionResult> GetShipperPhonesAsync([FromQuery] Guid shipperPhoneUId, [FromQuery] Guid shipperUId)
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetShipperPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShippersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetShipperPhoneRequest
			{
				ShipperPhoneUId = shipperPhoneUId,
				ShipperUId = shipperUId,
				CorrelationUId = correlationUId	
			};

			var response = await _shipper!.GetShipperPhonesAsync(request).ConfigureAwait(false);
			
			actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.ShipperPhones);
							
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetShipperPhonesAsync)}", nameof(ShippersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShipperPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShippersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Merge ShipperPhones
	/// </summary>
	/// <returns></returns>
	[Route("ShipperPhones")]
	[HttpPost]
	public virtual async Task<IActionResult> MergeShipperPhonesAsync()
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShipperPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShippersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await Request.GetRawBodyStringAsync().ConfigureAwait(false);

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(ShippersController));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var shipperPhones = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.ShipperPhone>>();

				if (shipperPhones != null && shipperPhones.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeShipperPhoneRequest
					{
						ShipperPhones = shipperPhones,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._shipper!.MergeShipperPhonesAsync(request).ConfigureAwait(false);

					actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeShipperPhonesAsync)} - {nameof(shipperPhones)} is null", nameof(ShippersController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeShipperPhonesAsync)}", nameof(ShippersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShipperPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShippersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	#endregion

	#region Private Methods

	#endregion
}
