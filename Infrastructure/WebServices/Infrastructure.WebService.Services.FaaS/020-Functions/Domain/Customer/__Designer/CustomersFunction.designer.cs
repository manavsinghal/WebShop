namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents Customers Controller.
/// </summary>
public partial class CustomersFunction : FunctionBase<CustomersFunction>
{
	#region Fields

	private readonly COREAPPINTERFACESDOMAIN.ICustomer _customer;

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the Customer class
	/// </summary>
	/// <param name="customer">customer</param>
	/// <param name="logger">logger</param>
	/// <param name="messageHub"></param>
	public CustomersFunction(COREAPPINTERFACESDOMAIN.ICustomer customer
							, ILogger<CustomersFunction> logger
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							, IFunctionContextAccessor functionContextAccessor
							) : base(logger, messageHub, functionContextAccessor)
	{
		_customer = customer;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Get Customers
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetCustomersAsync))]
	public virtual async Task<HttpResponseData> GetCustomersAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Customers/Customers")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var customerUId = GetQueryStringValue("customerUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetCustomerRequest
			{
				CustomerUId = String.IsNullOrEmpty(customerUId) ? Guid.Empty : Guid.Parse(customerUId),
				CorrelationUId = correlationUId	
			};

			var response = await _customer!.GetCustomersAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.Customers);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetCustomersAsync)}", nameof(CustomersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge Customers
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeCustomersAsync))]
	public virtual async Task<HttpResponseData> MergeCustomersAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Customers/Customers")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(CustomersFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var customers = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.Customer>>();

				if (customers != null && customers.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeCustomerRequest
					{
						Customers = customers,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._customer!.MergeCustomersAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeCustomersAsync)} - {nameof(customers)} is null", nameof(CustomersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeCustomersAsync)}", nameof(CustomersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
