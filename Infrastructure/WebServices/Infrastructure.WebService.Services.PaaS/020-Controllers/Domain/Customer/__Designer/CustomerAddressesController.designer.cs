namespace Accenture.WebShop.Infrastructure.WebService.Services.PaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents CustomerAddresses Controller.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
public partial class CustomersController : INFRAWEBSERVICESERVICES.ControllerBase<COREDOMAINDATAMODELSDOMAIN.Customer>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get CustomerAddresses
	/// </summary>
	/// <returns></returns>
	[Route("CustomerAddresses")]
	[HttpGet]
	public virtual async Task<IActionResult> GetCustomerAddressesAsync([FromQuery] Guid customerAddressUId, [FromQuery] Guid customerUId)
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetCustomerAddressRequest
			{
				CustomerAddressUId = customerAddressUId,
				CustomerUId = customerUId,
				CorrelationUId = correlationUId	
			};

			var response = await _customer!.GetCustomerAddressesAsync(request).ConfigureAwait(false);
			
			actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.CustomerAddresses);
							
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetCustomerAddressesAsync)}", nameof(CustomersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Merge CustomerAddresses
	/// </summary>
	/// <returns></returns>
	[Route("CustomerAddresses")]
	[HttpPost]
	public virtual async Task<IActionResult> MergeCustomerAddressesAsync()
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await Request.GetRawBodyStringAsync().ConfigureAwait(false);

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(CustomersController));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var customerAddresses = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.CustomerAddress>>();

				if (customerAddresses != null && customerAddresses.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeCustomerAddressRequest
					{
						CustomerAddresses = customerAddresses,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._customer!.MergeCustomerAddressesAsync(request).ConfigureAwait(false);

					actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeCustomerAddressesAsync)} - {nameof(customerAddresses)} is null", nameof(CustomersController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeCustomerAddressesAsync)}", nameof(CustomersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	#endregion

	#region Private Methods

	#endregion
}
