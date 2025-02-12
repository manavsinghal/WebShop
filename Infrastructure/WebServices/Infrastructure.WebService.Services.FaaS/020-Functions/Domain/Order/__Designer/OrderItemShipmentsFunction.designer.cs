namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents OrderItemShipments Controller.
/// </summary>
public partial class OrdersFunction : FunctionBase<OrdersFunction>
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
	[Function(nameof(GetOrderItemShipmentsAsync))]
	public virtual async Task<HttpResponseData> GetOrderItemShipmentsAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Orders/OrderItemShipments")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var orderItemShipmentUId = GetQueryStringValue("orderItemShipmentUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(OrdersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetOrderItemShipmentRequest
			{
				OrderItemShipmentUId = String.IsNullOrEmpty(orderItemShipmentUId) ? Guid.Empty : Guid.Parse(orderItemShipmentUId),
				CorrelationUId = correlationUId	
			};

			var response = await _order!.GetOrderItemShipmentsAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.OrderItemShipments);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetOrderItemShipmentsAsync)}", nameof(OrdersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(OrdersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge OrderItemShipments
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeOrderItemShipmentsAsync))]
	public virtual async Task<HttpResponseData> MergeOrderItemShipmentsAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Orders/OrderItemShipments")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(OrdersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(OrdersFunction));

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

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeOrderItemShipmentsAsync)} - {nameof(orderItemShipments)} is null", nameof(OrdersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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

				httpResponseData = GetErrorResponse(fault);
			}
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);
			
			await HandleError($"Error occurred in {nameof(MergeOrderItemShipmentsAsync)}", nameof(OrdersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(OrdersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
