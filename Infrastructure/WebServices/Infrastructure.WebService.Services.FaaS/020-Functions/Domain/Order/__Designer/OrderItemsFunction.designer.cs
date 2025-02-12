namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents OrderItems Controller.
/// </summary>
public partial class OrdersFunction : FunctionBase<OrdersFunction>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get OrderItems
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetOrderItemsAsync))]
	public virtual async Task<HttpResponseData> GetOrderItemsAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Orders/OrderItems")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var orderItemUId = GetQueryStringValue("orderItemUId");
		var orderUId = GetQueryStringValue("orderUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrderItemsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(OrdersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetOrderItemRequest
			{
				OrderItemUId = String.IsNullOrEmpty(orderItemUId) ? Guid.Empty : Guid.Parse(orderItemUId),
				OrderUId = String.IsNullOrEmpty(orderUId) ? Guid.Empty : Guid.Parse(orderUId),
				CorrelationUId = correlationUId	
			};

			var response = await _order!.GetOrderItemsAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.OrderItems);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetOrderItemsAsync)}", nameof(OrdersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrderItemsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(OrdersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge OrderItems
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeOrderItemsAsync))]
	public virtual async Task<HttpResponseData> MergeOrderItemsAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Orders/OrderItems")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrderItemsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(OrdersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(OrdersFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var orderItems = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.OrderItem>>();

				if (orderItems != null && orderItems.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeOrderItemRequest
					{
						OrderItems = orderItems,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._order!.MergeOrderItemsAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeOrderItemsAsync)} - {nameof(orderItems)} is null", nameof(OrdersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeOrderItemsAsync)}", nameof(OrdersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrderItemsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(OrdersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
