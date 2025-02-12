namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents Orders Controller.
/// </summary>
public partial class OrdersFunction : FunctionBase<OrdersFunction>
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
	public OrdersFunction(COREAPPINTERFACESDOMAIN.IOrder order
							, ILogger<OrdersFunction> logger
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							, IFunctionContextAccessor functionContextAccessor
							) : base(logger, messageHub, functionContextAccessor)
	{
		_order = order;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Get Orders
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetOrdersAsync))]
	public virtual async Task<HttpResponseData> GetOrdersAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Orders/Orders")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var orderUId = GetQueryStringValue("orderUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrdersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(OrdersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetOrderRequest
			{
				OrderUId = String.IsNullOrEmpty(orderUId) ? Guid.Empty : Guid.Parse(orderUId),
				CorrelationUId = correlationUId	
			};

			var response = await _order!.GetOrdersAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.Orders);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetOrdersAsync)}", nameof(OrdersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrdersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(OrdersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge Orders
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeOrdersAsync))]
	public virtual async Task<HttpResponseData> MergeOrdersAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Orders/Orders")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrdersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(OrdersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(OrdersFunction));

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

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeOrdersAsync)} - {nameof(orders)} is null", nameof(OrdersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeOrdersAsync)}", nameof(OrdersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrdersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(OrdersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
