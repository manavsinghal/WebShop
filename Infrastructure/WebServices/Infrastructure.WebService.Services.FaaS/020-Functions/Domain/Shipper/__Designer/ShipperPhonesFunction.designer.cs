namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents ShipperPhones Controller.
/// </summary>
public partial class ShippersFunction : FunctionBase<ShippersFunction>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get ShipperPhones
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetShipperPhonesAsync))]
	public virtual async Task<HttpResponseData> GetShipperPhonesAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Shippers/ShipperPhones")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var shipperPhoneUId = GetQueryStringValue("shipperPhoneUId");
		var shipperUId = GetQueryStringValue("shipperUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetShipperPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShippersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetShipperPhoneRequest
			{
				ShipperPhoneUId = String.IsNullOrEmpty(shipperPhoneUId) ? Guid.Empty : Guid.Parse(shipperPhoneUId),
				ShipperUId = String.IsNullOrEmpty(shipperUId) ? Guid.Empty : Guid.Parse(shipperUId),
				CorrelationUId = correlationUId	
			};

			var response = await _shipper!.GetShipperPhonesAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.ShipperPhones);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetShipperPhonesAsync)}", nameof(ShippersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShipperPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShippersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge ShipperPhones
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeShipperPhonesAsync))]
	public virtual async Task<HttpResponseData> MergeShipperPhonesAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Shippers/ShipperPhones")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShipperPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShippersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(ShippersFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var shipperPhones = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.ShipperPhone>>();

				if (shipperPhones != null && shipperPhones.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeShipperPhoneRequest
					{
						ShipperPhones = shipperPhones,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._shipper!.MergeShipperPhonesAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeShipperPhonesAsync)} - {nameof(shipperPhones)} is null", nameof(ShippersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeShipperPhonesAsync)}", nameof(ShippersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShipperPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShippersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
