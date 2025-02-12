namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents SellerPhones Controller.
/// </summary>
public partial class SellersFunction : FunctionBase<SellersFunction>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get SellerPhones
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetSellerPhonesAsync))]
	public virtual async Task<HttpResponseData> GetSellerPhonesAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Sellers/SellerPhones")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var sellerPhoneUId = GetQueryStringValue("sellerPhoneUId");
		var sellerUId = GetQueryStringValue("sellerUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetSellerPhoneRequest
			{
				SellerPhoneUId = String.IsNullOrEmpty(sellerPhoneUId) ? Guid.Empty : Guid.Parse(sellerPhoneUId),
				SellerUId = String.IsNullOrEmpty(sellerUId) ? Guid.Empty : Guid.Parse(sellerUId),
				CorrelationUId = correlationUId	
			};

			var response = await _seller!.GetSellerPhonesAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.SellerPhones);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetSellerPhonesAsync)}", nameof(SellersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge SellerPhones
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeSellerPhonesAsync))]
	public virtual async Task<HttpResponseData> MergeSellerPhonesAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Sellers/SellerPhones")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(SellersFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var sellerPhones = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.SellerPhone>>();

				if (sellerPhones != null && sellerPhones.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeSellerPhoneRequest
					{
						SellerPhones = sellerPhones,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._seller!.MergeSellerPhonesAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeSellerPhonesAsync)} - {nameof(sellerPhones)} is null", nameof(SellersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeSellerPhonesAsync)}", nameof(SellersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
