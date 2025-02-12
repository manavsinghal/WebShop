namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents SellerAddresses Controller.
/// </summary>
public partial class SellersFunction : FunctionBase<SellersFunction>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get SellerAddresses
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetSellerAddressesAsync))]
	public virtual async Task<HttpResponseData> GetSellerAddressesAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Sellers/SellerAddresses")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var sellerAddressUId = GetQueryStringValue("sellerAddressUId");
		var sellerUId = GetQueryStringValue("sellerUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetSellerAddressRequest
			{
				SellerAddressUId = String.IsNullOrEmpty(sellerAddressUId) ? Guid.Empty : Guid.Parse(sellerAddressUId),
				SellerUId = String.IsNullOrEmpty(sellerUId) ? Guid.Empty : Guid.Parse(sellerUId),
				CorrelationUId = correlationUId	
			};

			var response = await _seller!.GetSellerAddressesAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.SellerAddresses);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetSellerAddressesAsync)}", nameof(SellersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge SellerAddresses
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeSellerAddressesAsync))]
	public virtual async Task<HttpResponseData> MergeSellerAddressesAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Sellers/SellerAddresses")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(SellersFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var sellerAddresses = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.SellerAddress>>();

				if (sellerAddresses != null && sellerAddresses.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeSellerAddressRequest
					{
						SellerAddresses = sellerAddresses,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._seller!.MergeSellerAddressesAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeSellerAddressesAsync)} - {nameof(sellerAddresses)} is null", nameof(SellersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeSellerAddressesAsync)}", nameof(SellersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
