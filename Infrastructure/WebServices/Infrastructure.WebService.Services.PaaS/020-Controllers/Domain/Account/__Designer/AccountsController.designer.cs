namespace Accenture.WebShop.Infrastructure.WebService.Services.PaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents Accounts Controller.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/Domain/Accounts")] 
public partial class AccountsController : INFRAWEBSERVICESERVICES.ControllerBase<COREDOMAINDATAMODELSDOMAIN.Account>
{
	#region Fields

	private readonly COREAPPINTERFACESDOMAIN.IAccount _account;

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the Account class
	/// </summary>
	/// <param name="account">account</param>
	/// <param name="logger">logger</param>
	/// <param name="messageHub"></param>
	public AccountsController(COREAPPINTERFACESDOMAIN.IAccount account
							, ILogger<COREDOMAINDATAMODELSDOMAIN.Account> logger
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							) : base(logger, messageHub)
	{
		_account = account;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Get Accounts
	/// </summary>
	/// <returns></returns>
	[Route("Accounts")]
	[HttpGet]
	public virtual async Task<IActionResult> GetAccountsAsync([FromQuery] Guid accountUId)
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(AccountsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetAccountRequest
			{
				AccountUId = accountUId,
				CorrelationUId = correlationUId	
			};

			var response = await _account!.GetAccountsAsync(request).ConfigureAwait(false);
			
			actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.Accounts);
							
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetAccountsAsync)}", nameof(AccountsController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(AccountsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Merge Accounts
	/// </summary>
	/// <returns></returns>
	[Route("Accounts")]
	[HttpPost]
	public virtual async Task<IActionResult> MergeAccountsAsync()
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(AccountsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await Request.GetRawBodyStringAsync().ConfigureAwait(false);

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(AccountsController));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var accounts = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.Account>>();

				if (accounts != null && accounts.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeAccountRequest
					{
						Accounts = accounts,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._account!.MergeAccountsAsync(request).ConfigureAwait(false);

					actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeAccountsAsync)} - {nameof(accounts)} is null", nameof(AccountsController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeAccountsAsync)}", nameof(AccountsController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(AccountsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Get current Account
	/// </summary>
	/// <returns></returns>
	[Route("CurrentAccount")]
	[HttpGet]
	public virtual async Task<IActionResult> GetCurrentAccountAsync()
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetCurrentAccountAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(AccountsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetAccountRequest
			{
				EmailId = this.GetUserContext(),
				CorrelationUId = correlationUId
			};

			var response = await _account!.GetCurrentAccountAsync(request).ConfigureAwait(false);

			actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.Accounts);
			
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);

			await HandleError($"Error occurred in {nameof(GetCurrentAccountAsync)}", nameof(AccountsController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCurrentAccountAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(AccountsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	#endregion

	#region Private Methods

	#endregion
}
