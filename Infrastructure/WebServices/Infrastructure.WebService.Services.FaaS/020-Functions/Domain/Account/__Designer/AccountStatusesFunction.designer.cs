namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents AccountStatuses Controller.
/// </summary>
public partial class AccountsFunction : FunctionBase<AccountsFunction>
{
	#region Fields

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Get AccountStatuses
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetAccountStatusesAsync))]
	public virtual async Task<HttpResponseData> GetAccountStatusesAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Accounts/AccountStatuses")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var accountStatusUId = GetQueryStringValue("accountStatusUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetAccountStatusesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(AccountsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetAccountStatusRequest
			{
				AccountStatusUId = String.IsNullOrEmpty(accountStatusUId) ? Guid.Empty : Guid.Parse(accountStatusUId),
				CorrelationUId = correlationUId	
			};

			var response = await _account!.GetAccountStatusesAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.AccountStatuses);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetAccountStatusesAsync)}", nameof(AccountsFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetAccountStatusesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(AccountsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge AccountStatuses
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeAccountStatusesAsync))]
	public virtual async Task<HttpResponseData> MergeAccountStatusesAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Accounts/AccountStatuses")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeAccountStatusesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(AccountsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(AccountsFunction));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var accountStatuses = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.AccountStatus>>();

				if (accountStatuses != null && accountStatuses.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeAccountStatusRequest
					{
						AccountStatuses = accountStatuses,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._account!.MergeAccountStatusesAsync(request).ConfigureAwait(false);

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeAccountStatusesAsync)} - {nameof(accountStatuses)} is null", nameof(AccountsFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeAccountStatusesAsync)}", nameof(AccountsFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeAccountStatusesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(AccountsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
