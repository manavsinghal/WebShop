namespace Accenture.WebShop.Infrastructure.WebService.Services.PaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents Customers Controller.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/Domain/Customers")] 
public partial class CustomersController : INFRAWEBSERVICESERVICES.ControllerBase<COREDOMAINDATAMODELSDOMAIN.Customer>
{
	#region Fields

	private readonly COREAPPINTERFACESDOMAIN.ICustomer _customer;

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the Customer class
	/// </summary>
	/// <param name="customer">customer</param>
	/// <param name="logger">logger</param>
	/// <param name="messageHub"></param>
	public CustomersController(COREAPPINTERFACESDOMAIN.ICustomer customer
							, ILogger<COREDOMAINDATAMODELSDOMAIN.Customer> logger
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							) : base(logger, messageHub)
	{
		_customer = customer;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Get Customers
	/// </summary>
	/// <returns></returns>
	[Route("Customers")]
	[HttpGet]
	public virtual async Task<IActionResult> GetCustomersAsync([FromQuery] Guid customerUId)
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetCustomerRequest
			{
				CustomerUId = customerUId,
				CorrelationUId = correlationUId	
			};

			var response = await _customer!.GetCustomersAsync(request).ConfigureAwait(false);
			
			actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.Customers);
							
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetCustomersAsync)}", nameof(CustomersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Merge Customers
	/// </summary>
	/// <returns></returns>
	[Route("Customers")]
	[HttpPost]
	public virtual async Task<IActionResult> MergeCustomersAsync()
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await Request.GetRawBodyStringAsync().ConfigureAwait(false);

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(CustomersController));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var customers = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.Customer>>();

				if (customers != null && customers.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeCustomerRequest
					{
						Customers = customers,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._customer!.MergeCustomersAsync(request).ConfigureAwait(false);

					actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeCustomersAsync)} - {nameof(customers)} is null", nameof(CustomersController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeCustomersAsync)}", nameof(CustomersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	#endregion

	#region Private Methods

	#endregion
}
