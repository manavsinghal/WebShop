namespace Accenture.WebShop.Infrastructure.WebService.Services.PaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents ShoppingCarts Controller.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/Domain/ShoppingCarts")] 
public partial class ShoppingCartsController : INFRAWEBSERVICESERVICES.ControllerBase<COREDOMAINDATAMODELSDOMAIN.ShoppingCart>
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
	public ShoppingCartsController(COREAPPINTERFACESDOMAIN.IShoppingCart shoppingCart
							, ILogger<COREDOMAINDATAMODELSDOMAIN.ShoppingCart> logger
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							) : base(logger, messageHub)
	{
		_shoppingCart = shoppingCart;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Get ShoppingCarts
	/// </summary>
	/// <returns></returns>
	[Route("ShoppingCarts")]
	[HttpGet]
	public virtual async Task<IActionResult> GetShoppingCartsAsync([FromQuery] Guid shoppingCartUId)
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetShoppingCartsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShoppingCartsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetShoppingCartRequest
			{
				ShoppingCartUId = shoppingCartUId,
				CorrelationUId = correlationUId	
			};

			var response = await _shoppingCart!.GetShoppingCartsAsync(request).ConfigureAwait(false);
			
			actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.ShoppingCarts);
							
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetShoppingCartsAsync)}", nameof(ShoppingCartsController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShoppingCartsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShoppingCartsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Merge ShoppingCarts
	/// </summary>
	/// <returns></returns>
	[Route("ShoppingCarts")]
	[HttpPost]
	public virtual async Task<IActionResult> MergeShoppingCartsAsync()
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShoppingCartsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShoppingCartsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await Request.GetRawBodyStringAsync().ConfigureAwait(false);

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(ShoppingCartsController));

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

					actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeShoppingCartsAsync)} - {nameof(shoppingCarts)} is null", nameof(ShoppingCartsController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeShoppingCartsAsync)}", nameof(ShoppingCartsController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShoppingCartsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShoppingCartsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	#endregion

	#region Private Methods

	#endregion
}
