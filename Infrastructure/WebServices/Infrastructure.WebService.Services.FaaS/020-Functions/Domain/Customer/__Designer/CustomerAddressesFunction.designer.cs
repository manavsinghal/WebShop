namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents CustomerAddresses Controller.
/// </summary>
public partial class CustomersFunction : FunctionBase<CustomersFunction>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get CustomerAddresses
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetCustomerAddressesAsync))]
	public virtual async Task<HttpResponseData> GetCustomerAddressesAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Customers/CustomerAddresses")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var customerAddressUId = GetQueryStringValue("customerAddressUId");
		var customerUId = GetQueryStringValue("customerUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetCustomerAddressRequest
			{
				CustomerAddressUId = String.IsNullOrEmpty(customerAddressUId) ? Guid.Empty : Guid.Parse(customerAddressUId),
				CustomerUId = String.IsNullOrEmpty(customerUId) ? Guid.Empty : Guid.Parse(customerUId),
				CorrelationUId = correlationUId	
			};

			var response = await _customer!.GetCustomerAddressesAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.CustomerAddresses);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetCustomerAddressesAsync)}", nameof(CustomersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge CustomerAddresses
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeCustomerAddressesAsync))]
	public virtual async Task<HttpResponseData> MergeCustomerAddressesAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Customers/CustomerAddresses")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(CustomersFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var customerAddresses = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.CustomerAddress>>();

				if (customerAddresses != null && customerAddresses.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeCustomerAddressRequest
					{
						CustomerAddresses = customerAddresses,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._customer!.MergeCustomerAddressesAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeCustomerAddressesAsync)} - {nameof(customerAddresses)} is null", nameof(CustomersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeCustomerAddressesAsync)}", nameof(CustomersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
