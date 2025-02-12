namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents ShoppingCarts Controller.
/// </summary>
public partial class ShoppingCartsFunction : FunctionBase<ShoppingCartsFunction>
{
	#region Fields

	private readonly COREAPPINTERFACESDOMAIN.IShoppingCart _shoppingCart;

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the ShoppingCart class
	/// </summary>
	/// <param name="shoppingCart">shoppingCart</param>
	/// <param name="logger">logger</param>
	/// <param name="messageHub"></param>
	public ShoppingCartsFunction(COREAPPINTERFACESDOMAIN.IShoppingCart shoppingCart
							, ILogger<ShoppingCartsFunction> logger
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							, IFunctionContextAccessor functionContextAccessor
							) : base(logger, messageHub, functionContextAccessor)
	{
		_shoppingCart = shoppingCart;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Get ShoppingCarts
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetShoppingCartsAsync))]
	public virtual async Task<HttpResponseData> GetShoppingCartsAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ShoppingCarts/ShoppingCarts")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var shoppingCartUId = GetQueryStringValue("shoppingCartUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetShoppingCartsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShoppingCartsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetShoppingCartRequest
			{
				ShoppingCartUId = String.IsNullOrEmpty(shoppingCartUId) ? Guid.Empty : Guid.Parse(shoppingCartUId),
				CorrelationUId = correlationUId	
			};

			var response = await _shoppingCart!.GetShoppingCartsAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.ShoppingCarts);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetShoppingCartsAsync)}", nameof(ShoppingCartsFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShoppingCartsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShoppingCartsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge ShoppingCarts
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeShoppingCartsAsync))]
	public virtual async Task<HttpResponseData> MergeShoppingCartsAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "ShoppingCarts/ShoppingCarts")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShoppingCartsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShoppingCartsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(ShoppingCartsFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var shoppingCarts = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.ShoppingCart>>();

				if (shoppingCarts != null && shoppingCarts.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeShoppingCartRequest
					{
						ShoppingCarts = shoppingCarts,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._shoppingCart!.MergeShoppingCartsAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeShoppingCartsAsync)} - {nameof(shoppingCarts)} is null", nameof(ShoppingCartsFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeShoppingCartsAsync)}", nameof(ShoppingCartsFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShoppingCartsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShoppingCartsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
