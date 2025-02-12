namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents RowStatuses Controller.
/// </summary>
public partial class MastersFunction : FunctionBase<MastersFunction>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get RowStatuses
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetRowStatusesAsync))]
	public virtual async Task<HttpResponseData> GetRowStatusesAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "MasterLists/RowStatuses")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var rowStatusUId = GetQueryStringValue("rowStatusUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetRowStatusesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetRowStatusRequest
			{
				RowStatusUId = String.IsNullOrEmpty(rowStatusUId) ? Guid.Empty : Guid.Parse(rowStatusUId),
				CorrelationUId = correlationUId	
			};

			var response = await _master!.GetRowStatusesAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.RowStatuses);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetRowStatusesAsync)}", nameof(MastersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetRowStatusesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge RowStatuses
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeRowStatusesAsync))]
	public virtual async Task<HttpResponseData> MergeRowStatusesAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "MasterLists/RowStatuses")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeRowStatusesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(MastersFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var rowStatuses = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.RowStatus>>();

				if (rowStatuses != null && rowStatuses.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeRowStatusRequest
					{
						RowStatuses = rowStatuses,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._master!.MergeRowStatusesAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeRowStatusesAsync)} - {nameof(rowStatuses)} is null", nameof(MastersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeRowStatusesAsync)}", nameof(MastersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeRowStatusesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
