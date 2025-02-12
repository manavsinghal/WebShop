namespace Accenture.WebShop.Infrastructure.WebService.Services.PaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents MasterLists Controller.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/Domain/MasterLists")] 
public partial class MastersController : INFRAWEBSERVICESERVICES.ControllerBase<COREDOMAINDATAMODELSDOMAIN.MasterList>
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
	public MastersController(COREAPPINTERFACESDOMAIN.IMaster master
							, ILogger<COREDOMAINDATAMODELSDOMAIN.MasterList> logger
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							) : base(logger, messageHub)
	{
		_master = master;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Get MasterLists
	/// </summary>
	/// <returns></returns>
	[Route("MasterLists")]
	[HttpGet]
	public virtual async Task<IActionResult> GetMasterListsAsync([FromQuery] Guid masterListUId)
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MastersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetMasterListRequest
			{
				MasterListUId = masterListUId,
				CorrelationUId = correlationUId	
			};

			var response = await _master!.GetMasterListsAsync(request).ConfigureAwait(false);
			
			actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.MasterLists);
							
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetMasterListsAsync)}", nameof(MastersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MastersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Merge MasterLists
	/// </summary>
	/// <returns></returns>
	[Route("MasterLists")]
	[HttpPost]
	public virtual async Task<IActionResult> MergeMasterListsAsync()
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MastersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await Request.GetRawBodyStringAsync().ConfigureAwait(false);

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(MastersController));

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

					actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeMasterListsAsync)} - {nameof(masterLists)} is null", nameof(MastersController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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

				actionResult = GetErrorResponse(fault);
			}
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);
			
			await HandleError($"Error occurred in {nameof(MergeMasterListsAsync)}", nameof(MastersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MastersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Gets the service version domain.
	/// </summary>
	/// <param name="masterListCode">The master list code.</param>
	/// <returns></returns>
	[Route("MasterlistItemsByCode")]
	[HttpGet]
	public async Task<IEnumerable<COREDOMAINDATAMODELSDOMAIN.MasterListItem>> GetMasterListItemsByCode([FromQuery] String masterListCode)
	{
		var response = new COREAPPDATAMODELSDOMAIN.GetMasterListItemResponse();

		Logger.LogInfo($"{nameof(GetMasterListItemsByCode)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MastersController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);

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

			await HandleError($"Error occurred in {nameof(GetMasterListItemsByCode)}", nameof(MastersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(GetMasterListItemsByCode)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MastersController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
		}

		return response.MasterListItems;
	}
	#endregion

	#region Private Methods

	#endregion
}
