namespace Accenture.WebShop.Infrastructure.WebService.Services.PaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents Orders Controller.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/Domain/Orders")] 
public partial class OrdersController : INFRAWEBSERVICESERVICES.ControllerBase<COREDOMAINDATAMODELSDOMAIN.Order>
{
	#region Fields

	private readonly COREAPPINTERFACESDOMAIN.IOrder _order;

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the Order class
	/// </summary>
	/// <param name="order">order</param>
	/// <param name="logger">logger</param>
	/// <param name="messageHub"></param>
	public OrdersController(COREAPPINTERFACESDOMAIN.IOrder order
							, ILogger<COREDOMAINDATAMODELSDOMAIN.Order> logger
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							) : base(logger, messageHub)
	{
		_order = order;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Get Orders
	/// </summary>
	/// <returns></returns>
	[Route("Orders")]
	[HttpGet]
	public virtual async Task<IActionResult> GetOrdersAsync([FromQuery] Guid orderUId)
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrdersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(OrdersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetOrderRequest
			{
				OrderUId = orderUId,
				CorrelationUId = correlationUId	
			};

			var response = await _order!.GetOrdersAsync(request).ConfigureAwait(false);
			
			actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.Orders);
							
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetOrdersAsync)}", nameof(OrdersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrdersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(OrdersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Merge Orders
	/// </summary>
	/// <returns></returns>
	[Route("Orders")]
	[HttpPost]
	public virtual async Task<IActionResult> MergeOrdersAsync()
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrdersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(OrdersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await Request.GetRawBodyStringAsync().ConfigureAwait(false);

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(OrdersController));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var orders = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.Order>>();

				if (orders != null && orders.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeOrderRequest
					{
						Orders = orders,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._order!.MergeOrdersAsync(request).ConfigureAwait(false);

					actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeOrdersAsync)} - {nameof(orders)} is null", nameof(OrdersController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeOrdersAsync)}", nameof(OrdersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrdersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(OrdersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	#endregion

	#region Private Methods

	#endregion
}
