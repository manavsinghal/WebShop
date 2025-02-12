namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents ShoppingCartWishLists Controller.
/// </summary>
public partial class ShoppingCartsFunction : FunctionBase<ShoppingCartsFunction>
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
	[Function(nameof(GetShoppingCartWishListsAsync))]
	public virtual async Task<HttpResponseData> GetShoppingCartWishListsAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ShoppingCarts/ShoppingCartWishLists")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var shoppingCartWishListUId = GetQueryStringValue("shoppingCartWishListUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShoppingCartsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetShoppingCartWishListRequest
			{
				ShoppingCartWishListUId = String.IsNullOrEmpty(shoppingCartWishListUId) ? Guid.Empty : Guid.Parse(shoppingCartWishListUId),
				CorrelationUId = correlationUId	
			};

			var response = await _shoppingCart!.GetShoppingCartWishListsAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.ShoppingCartWishLists);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetShoppingCartWishListsAsync)}", nameof(ShoppingCartsFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShoppingCartsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge ShoppingCartWishLists
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeShoppingCartWishListsAsync))]
	public virtual async Task<HttpResponseData> MergeShoppingCartWishListsAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "ShoppingCarts/ShoppingCartWishLists")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShoppingCartsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(ShoppingCartsFunction));

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

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeShoppingCartWishListsAsync)} - {nameof(shoppingCartWishLists)} is null", nameof(ShoppingCartsFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeShoppingCartWishListsAsync)}", nameof(ShoppingCartsFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShoppingCartsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
