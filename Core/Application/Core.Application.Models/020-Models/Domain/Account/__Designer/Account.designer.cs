#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
  
#endregion       

namespace Accenture.WebShop.Core.Application.Models.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents Account class.
/// </summary>
/// <remarks>
/// The Account class.
/// </remarks>
public partial class Account : COREAPPMODELS.DomainModel<COREDOMAINDATAMODELSDOMAIN.Account>, COREAPPINTERFACESDOMAIN.IAccount
{
	#region Fields

	private readonly COREAPPDREPOINTERFACESDOMAIN.IAccountRepository _accountRepository;

	private readonly IServiceProvider _serviceProvider;

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the Account class
	/// </summary>
	/// <param name="accountRepository">The accountRepository.</param>
	/// <param name="logger">The logger.</param>
	/// <param name="cache">The cache.</param>
	/// <param name="messageHub"></param>
	/// <param name="dbAppSettings"></param>
	/// <param name="serviceProvider"></param>
	public Account(COREAPPDREPOINTERFACESDOMAIN.IAccountRepository accountRepository	
							, ILogger<COREDOMAINDATAMODELSDOMAIN.Account> logger
							, IDistributedCache cache
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							, COREAPPINTERFACESDBAPPSETTINGS.IDbAppSettings dbAppSettings
							, IServiceProvider serviceProvider
							): base(logger, cache, messageHub, dbAppSettings, serviceProvider)
	{
		this._accountRepository = accountRepository;
		this._serviceProvider = serviceProvider;
	}

	#endregion
	
	#region Partial Methods	

	/// <summary>
	/// Merge Accounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeAccountPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeAccountRequest request);

	/// <summary>
	/// Get Accounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetAccountPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetAccountRequest request);

	/// <summary>
	/// Merge Accounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeAccountPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeAccountRequest request);

	/// <summary>
	/// Get Accounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetAccountPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetAccountRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get Accounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetAccountResponse> GetAccountsAsync(COREAPPDATAMODELSDOMAIN.GetAccountRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Account), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetAccountResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetAccountPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var repositoryRequest = CreateRepositoryRequest(request);
			var repositoryResponse = await this._accountRepository.GetAccountsAsync(repositoryRequest).ConfigureAwait(false);
			
			if (repositoryResponse.Faults!.Any())
			{
				faults = repositoryResponse.Faults;
			}
			else
			{
				response.MergeResult = repositoryResponse.MergeResult;

				response.Accounts = await repositoryResponse.Accounts.ToListAsync().ConfigureAwait(false);
				
				//Decryption
				var decryptedResult = response.Accounts.DeObfuscate(new { AccessRole = new String[] { "All" } });

				if (decryptedResult != null)
				{
					response.Accounts = decryptedResult;
				}
			}

			var postProcessorResponse = await GetAccountPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetAccountsAsync)}", nameof(Account), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Account), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetAccountResponse> FetchAndCacheAccountsAsync(COREAPPDATAMODELSDOMAIN.GetAccountRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetAccountResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._accountRepository.GetAccountsAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.Accounts = await repositoryResponse.Accounts.ToListAsync();

			//Decryption
			var decryptedResult = response.Accounts.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.Accounts = decryptedResult;
			}

			await SetCachedData(cacheKey, response.Accounts.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetAccountRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetAccountRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetAccountRequest
		{
			MatchExpression = request.AccountUId != Guid.Empty ? (entity) => entity.AccountUId == request.AccountUId : null,
			CorrelationUId = request.CorrelationUId
		};

		return repositoryRequest;
	}


	/// <summary>
	/// Merge Accounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeAccountResponse> MergeAccountsAsync(COREAPPDATAMODELSDOMAIN.MergeAccountRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Account), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeAccountResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// Account.custom.cs MergeAccountPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeAccountPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("Account");

				if (isEncryptionRequired)
				{
					//Encryption
					request.Accounts = request.Accounts!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeAccountRequest
			{
				Accounts = request.Accounts,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.Accounts);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._accountRepository.MergeAccountsAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _accountRepository.SaveAccountAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{
					response.MergeResult = repositoryResponse.MergeResult;

					response.Status = new COREAPPDATAMODELS.Status
					{
						Code = "Success",
						OperationStatus = COREDOMAINDATAMODELSENUM.OperationStatus.Success
					};
				}
			}

			//// If there are any customization required, Add custom code in the
			//// Account.custom.cs MergeAccountPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeAccountPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeAccountsAsync)}", nameof(Account), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Account), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Get current Account
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetAccountResponse> GetCurrentAccountAsync(COREAPPDATAMODELSDOMAIN.GetAccountRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetCurrentAccountAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Account), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.GetAccountResponse response = new();

		try
		{
			if (!String.IsNullOrEmpty(request.EmailId))
			{
				var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetAccountRequest
				{
					MatchExpression = (entity) => entity.EmailId == request.EmailId
				};
				var repositoryResponse = await this._accountRepository.GetAccountsAsync(repositoryRequest).ConfigureAwait(false);

				if (repositoryResponse.Faults!.Any())
				{
					faults = repositoryResponse.Faults;
				}
				else
				{
					response.MergeResult = repositoryResponse.MergeResult;

					response.Accounts = repositoryResponse.Accounts.ToList().OrderBy(x=>x.SortOrder);

					//Decryption
					var decryptedResult = response.Accounts.DeObfuscate(new { AccessRole = new String[] { "All" } });

					if (decryptedResult != null)
					{
						response.Accounts = decryptedResult;
					}
				}
			}
			else
			{
				response.Accounts = new List<COREDOMAINDATAMODELSDOMAIN.Account>();
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(GetCurrentAccountAsync)}", nameof(Account), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCurrentAccountAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Account), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}
	/// <summary>
	/// Decrypt Accounts data
	/// </summary>
	/// <param name="accountUId"></param>
	/// <returns></returns>
	public async Task DecryptAccountsAsync(Guid accountUId)
	{
		Logger.LogInfo($"{nameof(DecryptAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Account), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Account), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetAccountRequest
			{
				AccountUId = accountUId
			};

			Logger.LogInfo($"{nameof(DecryptAccountsAsync)} - Processing data for decryption", nameof(Account), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseAccounts = await this.GetAccountsAsync(request).ConfigureAwait(false);

			if (responseAccounts.Accounts != null && responseAccounts.Accounts.Any())
			{
				Logger.LogInfo($"{nameof(DecryptAccountsAsync)} - Decrypted the data", nameof(Account), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var account in responseAccounts.Accounts)
				{
					account.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptAccountsAsync)} - Set the item state to update the records back to entity", nameof(Account), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeAccountRequest
				{
					Accounts = responseAccounts.Accounts.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeAccountsAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptAccountsAsync)} while saving data", nameof(Account), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptAccountsAsync)} - Successfully processed data for decryption", nameof(Account), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing accounts data for decryption in {nameof(DecryptAccountsAsync)}", nameof(Account), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Account), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Account), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	#endregion
}
