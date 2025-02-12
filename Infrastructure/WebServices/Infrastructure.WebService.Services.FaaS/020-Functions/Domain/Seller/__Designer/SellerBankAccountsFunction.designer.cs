namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents SellerBankAccounts Controller.
/// </summary>
public partial class SellersFunction : FunctionBase<SellersFunction>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get SellerBankAccounts
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetSellerBankAccountsAsync))]
	public virtual async Task<HttpResponseData> GetSellerBankAccountsAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Sellers/SellerBankAccounts")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var sellerBankAccountUId = GetQueryStringValue("sellerBankAccountUId");
		var sellerUId = GetQueryStringValue("sellerUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetSellerBankAccountRequest
			{
				SellerBankAccountUId = String.IsNullOrEmpty(sellerBankAccountUId) ? Guid.Empty : Guid.Parse(sellerBankAccountUId),
				SellerUId = String.IsNullOrEmpty(sellerUId) ? Guid.Empty : Guid.Parse(sellerUId),
				CorrelationUId = correlationUId	
			};

			var response = await _seller!.GetSellerBankAccountsAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.SellerBankAccounts);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetSellerBankAccountsAsync)}", nameof(SellersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge SellerBankAccounts
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeSellerBankAccountsAsync))]
	public virtual async Task<HttpResponseData> MergeSellerBankAccountsAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Sellers/SellerBankAccounts")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(SellersFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var sellerBankAccounts = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.SellerBankAccount>>();

				if (sellerBankAccounts != null && sellerBankAccounts.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeSellerBankAccountRequest
					{
						SellerBankAccounts = sellerBankAccounts,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._seller!.MergeSellerBankAccountsAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeSellerBankAccountsAsync)} - {nameof(sellerBankAccounts)} is null", nameof(SellersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeSellerBankAccountsAsync)}", nameof(SellersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
