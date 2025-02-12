namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents MasterLists Controller.
/// </summary>
public partial class MastersFunction : FunctionBase<MastersFunction>
{
	#region Fields

	private readonly COREAPPINTERFACESDOMAIN.IMaster _master;

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the MasterList class
	/// </summary>
	/// <param name="master">master</param>
	/// <param name="logger">logger</param>
	/// <param name="messageHub"></param>
	public MastersFunction(COREAPPINTERFACESDOMAIN.IMaster master
							, ILogger<MastersFunction> logger
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							, IFunctionContextAccessor functionContextAccessor
							) : base(logger, messageHub, functionContextAccessor)
	{
		_master = master;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Get MasterLists
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetMasterListsAsync))]
	public virtual async Task<HttpResponseData> GetMasterListsAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "MasterLists/MasterLists")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var masterListUId = GetQueryStringValue("masterListUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetMasterListRequest
			{
				MasterListUId = String.IsNullOrEmpty(masterListUId) ? Guid.Empty : Guid.Parse(masterListUId),
				CorrelationUId = correlationUId	
			};

			var response = await _master!.GetMasterListsAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.MasterLists);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetMasterListsAsync)}", nameof(MastersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge MasterLists
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeMasterListsAsync))]
	public virtual async Task<HttpResponseData> MergeMasterListsAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "MasterLists/MasterLists")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(MastersFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var masterLists = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.MasterList>>();

				if (masterLists != null && masterLists.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeMasterListRequest
					{
						MasterLists = masterLists,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._master!.MergeMasterListsAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeMasterListsAsync)} - {nameof(masterLists)} is null", nameof(MastersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeMasterListsAsync)}", nameof(MastersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MastersFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Gets the service version domain.
	/// </summary>
	/// <param name="masterListCode">The master list code.</param>
	/// <returns></returns>
	[Function("GetMasterListItemsByCode")]
	public async Task<IEnumerable<COREDOMAINDATAMODELSDOMAIN.MasterListItem>> GetMasterListItemsByCode([HttpTrigger(AuthorizationLevel.Function, "get", Route = "MasterLists/MasterlistItemsByCode")] HttpRequestData req)
	{
		var response = new COREAPPDATAMODELSDOMAIN.GetMasterListItemResponse();
		
		var masterListCode = GetQueryStringValue("masterListCode");
		
		Logger.LogInfo($"{nameof(GetMasterListItemsByCode)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MastersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);

		try
		{
			response = await this._master!.GetMasterListItemsByCode(masterListCode).ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			String? innerExceptionMessage = null;

			if (ex.InnerException != null)
			{
				innerExceptionMessage = ex.InnerException.Message;
			}

			await HandleError($"Error occurred in {nameof(GetMasterListItemsByCode)}", nameof(MastersFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(GetMasterListItemsByCode)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MastersFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
		}

		return response.MasterListItems;
	}

	#endregion

	#region Private Methods

	#endregion
}
