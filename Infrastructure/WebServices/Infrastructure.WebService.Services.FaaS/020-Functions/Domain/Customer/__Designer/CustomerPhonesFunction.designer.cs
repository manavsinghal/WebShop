namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents CustomerPhones Controller.
/// </summary>
public partial class CustomersFunction : FunctionBase<CustomersFunction>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get CustomerPhones
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetCustomerPhonesAsync))]
	public virtual async Task<HttpResponseData> GetCustomerPhonesAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Customers/CustomerPhones")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var customerPhoneUId = GetQueryStringValue("customerPhoneUId");
		var customerUId = GetQueryStringValue("customerUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetCustomerPhoneRequest
			{
				CustomerPhoneUId = String.IsNullOrEmpty(customerPhoneUId) ? Guid.Empty : Guid.Parse(customerPhoneUId),
				CustomerUId = String.IsNullOrEmpty(customerUId) ? Guid.Empty : Guid.Parse(customerUId),
				CorrelationUId = correlationUId	
			};

			var response = await _customer!.GetCustomerPhonesAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.CustomerPhones);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetCustomerPhonesAsync)}", nameof(CustomersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge CustomerPhones
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeCustomerPhonesAsync))]
	public virtual async Task<HttpResponseData> MergeCustomerPhonesAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Customers/CustomerPhones")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(CustomersFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var customerPhones = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.CustomerPhone>>();

				if (customerPhones != null && customerPhones.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeCustomerPhoneRequest
					{
						CustomerPhones = customerPhones,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._customer!.MergeCustomerPhonesAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeCustomerPhonesAsync)} - {nameof(customerPhones)} is null", nameof(CustomersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeCustomerPhonesAsync)}", nameof(CustomersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
