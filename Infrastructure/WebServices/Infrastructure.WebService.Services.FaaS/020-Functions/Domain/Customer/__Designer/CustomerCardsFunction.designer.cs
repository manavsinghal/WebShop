namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents CustomerCards Controller.
/// </summary>
public partial class CustomersFunction : FunctionBase<CustomersFunction>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get CustomerCards
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetCustomerCardsAsync))]
	public virtual async Task<HttpResponseData> GetCustomerCardsAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Customers/CustomerCards")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var customerCardUId = GetQueryStringValue("customerCardUId");
		var customerUId = GetQueryStringValue("customerUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerCardsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetCustomerCardRequest
			{
				CustomerCardUId = String.IsNullOrEmpty(customerCardUId) ? Guid.Empty : Guid.Parse(customerCardUId),
				CustomerUId = String.IsNullOrEmpty(customerUId) ? Guid.Empty : Guid.Parse(customerUId),
				CorrelationUId = correlationUId	
			};

			var response = await _customer!.GetCustomerCardsAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.CustomerCards);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetCustomerCardsAsync)}", nameof(CustomersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerCardsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge CustomerCards
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeCustomerCardsAsync))]
	public virtual async Task<HttpResponseData> MergeCustomerCardsAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Customers/CustomerCards")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerCardsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(CustomersFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var customerCards = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.CustomerCard>>();

				if (customerCards != null && customerCards.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeCustomerCardRequest
					{
						CustomerCards = customerCards,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._customer!.MergeCustomerCardsAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeCustomerCardsAsync)} - {nameof(customerCards)} is null", nameof(CustomersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeCustomerCardsAsync)}", nameof(CustomersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerCardsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
