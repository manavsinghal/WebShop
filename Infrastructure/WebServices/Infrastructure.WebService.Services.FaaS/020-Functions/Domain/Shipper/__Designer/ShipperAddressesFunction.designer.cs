namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents ShipperAddresses Controller.
/// </summary>
public partial class ShippersFunction : FunctionBase<ShippersFunction>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get ShipperAddresses
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetShipperAddressesAsync))]
	public virtual async Task<HttpResponseData> GetShipperAddressesAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Shippers/ShipperAddresses")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var shipperAddressUId = GetQueryStringValue("shipperAddressUId");
		var shipperUId = GetQueryStringValue("shipperUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetShipperAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShippersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetShipperAddressRequest
			{
				ShipperAddressUId = String.IsNullOrEmpty(shipperAddressUId) ? Guid.Empty : Guid.Parse(shipperAddressUId),
				ShipperUId = String.IsNullOrEmpty(shipperUId) ? Guid.Empty : Guid.Parse(shipperUId),
				CorrelationUId = correlationUId	
			};

			var response = await _shipper!.GetShipperAddressesAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.ShipperAddresses);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetShipperAddressesAsync)}", nameof(ShippersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShipperAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShippersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge ShipperAddresses
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeShipperAddressesAsync))]
	public virtual async Task<HttpResponseData> MergeShipperAddressesAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Shippers/ShipperAddresses")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShipperAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShippersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(ShippersFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var shipperAddresses = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.ShipperAddress>>();

				if (shipperAddresses != null && shipperAddresses.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeShipperAddressRequest
					{
						ShipperAddresses = shipperAddresses,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._shipper!.MergeShipperAddressesAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeShipperAddressesAsync)} - {nameof(shipperAddresses)} is null", nameof(ShippersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeShipperAddressesAsync)}", nameof(ShippersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShipperAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShippersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
