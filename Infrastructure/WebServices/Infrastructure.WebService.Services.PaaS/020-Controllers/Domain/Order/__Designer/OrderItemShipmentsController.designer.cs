namespace Accenture.WebShop.Infrastructure.WebService.Services.PaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents OrderItemShipments Controller.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
public partial class OrdersController : INFRAWEBSERVICESERVICES.ControllerBase<COREDOMAINDATAMODELSDOMAIN.Order>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get OrderItemShipments
	/// </summary>
	/// <returns></returns>
	[Route("OrderItemShipments")]
	[HttpGet]
	public virtual async Task<IActionResult> GetOrderItemShipmentsAsync([FromQuery] Guid orderItemShipmentUId)
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(OrdersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetOrderItemShipmentRequest
			{
				OrderItemShipmentUId = orderItemShipmentUId,
				CorrelationUId = correlationUId	
			};

			var response = await _order!.GetOrderItemShipmentsAsync(request).ConfigureAwait(false);
			
			actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.OrderItemShipments);
							
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetOrderItemShipmentsAsync)}", nameof(OrdersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(OrdersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Merge OrderItemShipments
	/// </summary>
	/// <returns></returns>
	[Route("OrderItemShipments")]
	[HttpPost]
	public virtual async Task<IActionResult> MergeOrderItemShipmentsAsync()
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(OrdersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await Request.GetRawBodyStringAsync().ConfigureAwait(false);

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(OrdersController));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var orderItemShipments = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.OrderItemShipment>>();

				if (orderItemShipments != null && orderItemShipments.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeOrderItemShipmentRequest
					{
						OrderItemShipments = orderItemShipments,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._order!.MergeOrderItemShipmentsAsync(request).ConfigureAwait(false);

					actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeOrderItemShipmentsAsync)} - {nameof(orderItemShipments)} is null", nameof(OrdersController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeOrderItemShipmentsAsync)}", nameof(OrdersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(OrdersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	#endregion

	#region Private Methods

	#endregion
}
