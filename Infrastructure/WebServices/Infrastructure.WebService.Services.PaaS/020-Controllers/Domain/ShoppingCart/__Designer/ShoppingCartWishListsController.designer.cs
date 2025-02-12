namespace Accenture.WebShop.Infrastructure.WebService.Services.PaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents ShoppingCartWishLists Controller.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
public partial class ShoppingCartsController : INFRAWEBSERVICESERVICES.ControllerBase<COREDOMAINDATAMODELSDOMAIN.ShoppingCart>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get ShoppingCartWishLists
	/// </summary>
	/// <returns></returns>
	[Route("ShoppingCartWishLists")]
	[HttpGet]
	public virtual async Task<IActionResult> GetShoppingCartWishListsAsync([FromQuery] Guid shoppingCartWishListUId)
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShoppingCartsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetShoppingCartWishListRequest
			{
				ShoppingCartWishListUId = shoppingCartWishListUId,
				CorrelationUId = correlationUId	
			};

			var response = await _shoppingCart!.GetShoppingCartWishListsAsync(request).ConfigureAwait(false);
			
			actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.ShoppingCartWishLists);
							
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetShoppingCartWishListsAsync)}", nameof(ShoppingCartsController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShoppingCartsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Merge ShoppingCartWishLists
	/// </summary>
	/// <returns></returns>
	[Route("ShoppingCartWishLists")]
	[HttpPost]
	public virtual async Task<IActionResult> MergeShoppingCartWishListsAsync()
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShoppingCartsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await Request.GetRawBodyStringAsync().ConfigureAwait(false);

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(ShoppingCartsController));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var shoppingCartWishLists = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.ShoppingCartWishList>>();

				if (shoppingCartWishLists != null && shoppingCartWishLists.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeShoppingCartWishListRequest
					{
						ShoppingCartWishLists = shoppingCartWishLists,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._shoppingCart!.MergeShoppingCartWishListsAsync(request).ConfigureAwait(false);

					actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeShoppingCartWishListsAsync)} - {nameof(shoppingCartWishLists)} is null", nameof(ShoppingCartsController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeShoppingCartWishListsAsync)}", nameof(ShoppingCartsController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShoppingCartsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	#endregion

	#region Private Methods

	#endregion
}
