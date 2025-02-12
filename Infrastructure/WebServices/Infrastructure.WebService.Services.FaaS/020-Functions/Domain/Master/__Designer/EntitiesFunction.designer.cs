namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents Entities Controller.
/// </summary>
public partial class MastersFunction : FunctionBase<MastersFunction>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get Entities
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetEntitiesAsync))]
	public virtual async Task<HttpResponseData> GetEntitiesAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "MasterLists/Entities")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var entityUId = GetQueryStringValue("entityUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetEntitiesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetEntityRequest
			{
				EntityUId = String.IsNullOrEmpty(entityUId) ? Guid.Empty : Guid.Parse(entityUId),
				CorrelationUId = correlationUId	
			};

			var response = await _master!.GetEntitiesAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.Entities);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetEntitiesAsync)}", nameof(MastersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetEntitiesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge Entities
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeEntitiesAsync))]
	public virtual async Task<HttpResponseData> MergeEntitiesAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "MasterLists/Entities")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeEntitiesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(MastersFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var entities = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.Entity>>();

				if (entities != null && entities.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeEntityRequest
					{
						Entities = entities,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._master!.MergeEntitiesAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeEntitiesAsync)} - {nameof(entities)} is null", nameof(MastersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeEntitiesAsync)}", nameof(MastersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeEntitiesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
