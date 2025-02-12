namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents Accounts Controller.
/// </summary>
public partial class AccountsFunction : FunctionBase<AccountsFunction>
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
	public AccountsFunction(COREAPPINTERFACESDOMAIN.IAccount account
							, ILogger<AccountsFunction> logger
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							, IFunctionContextAccessor functionContextAccessor
							) : base(logger, messageHub, functionContextAccessor)
	{
		_account = account;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Get Accounts
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetAccountsAsync))]
	public virtual async Task<HttpResponseData> GetAccountsAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Accounts/Accounts")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var accountUId = GetQueryStringValue("accountUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(AccountsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetAccountRequest
			{
				AccountUId = String.IsNullOrEmpty(accountUId) ? Guid.Empty : Guid.Parse(accountUId),
				CorrelationUId = correlationUId	
			};

			var response = await _account!.GetAccountsAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.Accounts);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetAccountsAsync)}", nameof(AccountsFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(AccountsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge Accounts
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeAccountsAsync))]
	public virtual async Task<HttpResponseData> MergeAccountsAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Accounts/Accounts")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(AccountsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(AccountsFunction));

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

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeAccountsAsync)} - {nameof(accounts)} is null", nameof(AccountsFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeAccountsAsync)}", nameof(AccountsFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(AccountsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Get current Account
	/// </summary>
	/// <returns></returns>
	[Function("GetCurrentAccountAsync")]
	public virtual async Task<HttpResponseData> GetCurrentAccountAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Accounts/CurrentAccount")] HttpRequest req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetCurrentAccountAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(AccountsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetAccountRequest
			{
				EmailId = this.GetUserContext(),
				CorrelationUId = correlationUId
			};

			var response = await _account!.GetCurrentAccountAsync(request).ConfigureAwait(false);

			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.Accounts);
			
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);

			await HandleError($"Error occurred in {nameof(GetCurrentAccountAsync)}", nameof(AccountsFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCurrentAccountAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(AccountsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
