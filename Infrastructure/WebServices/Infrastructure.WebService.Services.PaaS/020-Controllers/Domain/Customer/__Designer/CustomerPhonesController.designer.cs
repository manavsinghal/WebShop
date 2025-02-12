namespace Accenture.WebShop.Infrastructure.WebService.Services.PaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents CustomerPhones Controller.
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
	/// Get CustomerPhones
	/// </summary>
	/// <returns></returns>
	[Route("CustomerPhones")]
	[HttpGet]
	public virtual async Task<IActionResult> GetCustomerPhonesAsync([FromQuery] Guid customerPhoneUId, [FromQuery] Guid customerUId)
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetCustomerPhoneRequest
			{
				CustomerPhoneUId = customerPhoneUId,
				CustomerUId = customerUId,
				CorrelationUId = correlationUId	
			};

			var response = await _customer!.GetCustomerPhonesAsync(request).ConfigureAwait(false);
			
			actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.CustomerPhones);
							
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetCustomerPhonesAsync)}", nameof(CustomersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Merge CustomerPhones
	/// </summary>
	/// <returns></returns>
	[Route("CustomerPhones")]
	[HttpPost]
	public virtual async Task<IActionResult> MergeCustomerPhonesAsync()
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await Request.GetRawBodyStringAsync().ConfigureAwait(false);

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(CustomersController));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var customerPhones = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.CustomerPhone>>();

				if (customerPhones != null && customerPhones.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeCustomerPhoneRequest
					{
						CustomerPhones = customerPhones,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._customer!.MergeCustomerPhonesAsync(request).ConfigureAwait(false);

					actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeCustomerPhonesAsync)} - {nameof(customerPhones)} is null", nameof(CustomersController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeCustomerPhonesAsync)}", nameof(CustomersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	#endregion

	#region Private Methods

	#endregion
}
