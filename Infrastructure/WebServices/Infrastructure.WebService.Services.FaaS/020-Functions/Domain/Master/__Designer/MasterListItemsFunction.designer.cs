namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents MasterListItems Controller.
/// </summary>
public partial class MastersFunction : FunctionBase<MastersFunction>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get MasterListItems
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetMasterListItemsAsync))]
	public virtual async Task<HttpResponseData> GetMasterListItemsAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "MasterLists/MasterListItems")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var masterListItemUId = GetQueryStringValue("masterListItemUId");
		var masterListUId = GetQueryStringValue("masterListUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetMasterListItemsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetMasterListItemRequest
			{
				MasterListItemUId = String.IsNullOrEmpty(masterListItemUId) ? Guid.Empty : Guid.Parse(masterListItemUId),
				MasterListUId = String.IsNullOrEmpty(masterListUId) ? Guid.Empty : Guid.Parse(masterListUId),
				CorrelationUId = correlationUId	
			};

			var response = await _master!.GetMasterListItemsAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.MasterListItems);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetMasterListItemsAsync)}", nameof(MastersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetMasterListItemsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge MasterListItems
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeMasterListItemsAsync))]
	public virtual async Task<HttpResponseData> MergeMasterListItemsAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "MasterLists/MasterListItems")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeMasterListItemsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(MastersFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var masterListItems = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.MasterListItem>>();

				if (masterListItems != null && masterListItems.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeMasterListItemRequest
					{
						MasterListItems = masterListItems,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._master!.MergeMasterListItemsAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeMasterListItemsAsync)} - {nameof(masterListItems)} is null", nameof(MastersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeMasterListItemsAsync)}", nameof(MastersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeMasterListItemsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
