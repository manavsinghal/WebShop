namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents ShipperBankAccounts Controller.
/// </summary>
public partial class ShippersFunction : FunctionBase<ShippersFunction>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get ShipperBankAccounts
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetShipperBankAccountsAsync))]
	public virtual async Task<HttpResponseData> GetShipperBankAccountsAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Shippers/ShipperBankAccounts")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var shipperBankAccountUId = GetQueryStringValue("shipperBankAccountUId");
		var shipperUId = GetQueryStringValue("shipperUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetShipperBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShippersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetShipperBankAccountRequest
			{
				ShipperBankAccountUId = String.IsNullOrEmpty(shipperBankAccountUId) ? Guid.Empty : Guid.Parse(shipperBankAccountUId),
				ShipperUId = String.IsNullOrEmpty(shipperUId) ? Guid.Empty : Guid.Parse(shipperUId),
				CorrelationUId = correlationUId	
			};

			var response = await _shipper!.GetShipperBankAccountsAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.ShipperBankAccounts);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetShipperBankAccountsAsync)}", nameof(ShippersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShipperBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShippersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge ShipperBankAccounts
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeShipperBankAccountsAsync))]
	public virtual async Task<HttpResponseData> MergeShipperBankAccountsAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Shippers/ShipperBankAccounts")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShipperBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShippersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(ShippersFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var shipperBankAccounts = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.ShipperBankAccount>>();

				if (shipperBankAccounts != null && shipperBankAccounts.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeShipperBankAccountRequest
					{
						ShipperBankAccounts = shipperBankAccounts,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._shipper!.MergeShipperBankAccountsAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeShipperBankAccountsAsync)} - {nameof(shipperBankAccounts)} is null", nameof(ShippersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeShipperBankAccountsAsync)}", nameof(ShippersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShipperBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShippersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
