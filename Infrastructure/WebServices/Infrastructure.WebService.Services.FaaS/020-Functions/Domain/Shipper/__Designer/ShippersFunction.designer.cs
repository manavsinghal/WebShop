namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents Shippers Controller.
/// </summary>
public partial class ShippersFunction : FunctionBase<ShippersFunction>
{
	#region Fields

	private readonly COREAPPINTERFACESDOMAIN.IShipper _shipper;

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the Shipper class
	/// </summary>
	/// <param name="shipper">shipper</param>
	/// <param name="logger">logger</param>
	/// <param name="messageHub"></param>
	public ShippersFunction(COREAPPINTERFACESDOMAIN.IShipper shipper
							, ILogger<ShippersFunction> logger
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							, IFunctionContextAccessor functionContextAccessor
							) : base(logger, messageHub, functionContextAccessor)
	{
		_shipper = shipper;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Get Shippers
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetShippersAsync))]
	public virtual async Task<HttpResponseData> GetShippersAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Shippers/Shippers")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var shipperUId = GetQueryStringValue("shipperUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetShippersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShippersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetShipperRequest
			{
				ShipperUId = String.IsNullOrEmpty(shipperUId) ? Guid.Empty : Guid.Parse(shipperUId),
				CorrelationUId = correlationUId	
			};

			var response = await _shipper!.GetShippersAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.Shippers);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetShippersAsync)}", nameof(ShippersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShippersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShippersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge Shippers
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeShippersAsync))]
	public virtual async Task<HttpResponseData> MergeShippersAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Shippers/Shippers")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShippersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShippersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(ShippersFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var shippers = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.Shipper>>();

				if (shippers != null && shippers.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeShipperRequest
					{
						Shippers = shippers,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._shipper!.MergeShippersAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeShippersAsync)} - {nameof(shippers)} is null", nameof(ShippersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeShippersAsync)}", nameof(ShippersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShippersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShippersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
