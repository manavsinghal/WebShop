namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents Sellers Controller.
/// </summary>
public partial class SellersFunction : FunctionBase<SellersFunction>
{
	#region Fields

	private readonly COREAPPINTERFACESDOMAIN.ISeller _seller;

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the Seller class
	/// </summary>
	/// <param name="seller">seller</param>
	/// <param name="logger">logger</param>
	/// <param name="messageHub"></param>
	public SellersFunction(COREAPPINTERFACESDOMAIN.ISeller seller
							, ILogger<SellersFunction> logger
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							, IFunctionContextAccessor functionContextAccessor
							) : base(logger, messageHub, functionContextAccessor)
	{
		_seller = seller;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Get Sellers
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetSellersAsync))]
	public virtual async Task<HttpResponseData> GetSellersAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Sellers/Sellers")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var sellerUId = GetQueryStringValue("sellerUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetSellerRequest
			{
				SellerUId = String.IsNullOrEmpty(sellerUId) ? Guid.Empty : Guid.Parse(sellerUId),
				CorrelationUId = correlationUId	
			};

			var response = await _seller!.GetSellersAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.Sellers);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetSellersAsync)}", nameof(SellersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge Sellers
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeSellersAsync))]
	public virtual async Task<HttpResponseData> MergeSellersAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Sellers/Sellers")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(SellersFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var sellers = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.Seller>>();

				if (sellers != null && sellers.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeSellerRequest
					{
						Sellers = sellers,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._seller!.MergeSellersAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeSellersAsync)} - {nameof(sellers)} is null", nameof(SellersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeSellersAsync)}", nameof(SellersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
