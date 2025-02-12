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
/// Represents AccountStatus class.
/// </summary>
/// <remarks>
/// The AccountStatus class.
/// </remarks>
public partial class Account : COREAPPMODELS.DomainModel<COREDOMAINDATAMODELSDOMAIN.Account>, COREAPPINTERFACESDOMAIN.IAccount
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	#endregion
	
	#region Partial Methods	

	/// <summary>
	/// Merge AccountStatuses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeAccountStatusPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeAccountStatusRequest request);

	/// <summary>
	/// Get AccountStatuses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetAccountStatusPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetAccountStatusRequest request);

	/// <summary>
	/// Get AccountStatuses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetAccountStatusPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetAccountStatusRequest request);

	/// <summary>
	/// Merge AccountStatuses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeAccountStatusPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeAccountStatusRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get AccountStatuses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetAccountStatusResponse> GetAccountStatusesAsync(COREAPPDATAMODELSDOMAIN.GetAccountStatusRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetAccountStatusesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Account), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetAccountStatusResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetAccountStatusPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetAccountStatusesAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.AccountStatuses = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.AccountStatus>>();
			}
			else
			{
				response = await FetchAndCacheAccountStatusesAsync(request, cacheKey, EnableCaching("AccountStatus", nameof(GetAccountStatusesAsync)));
			}

			var postProcessorResponse = await GetAccountStatusPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetAccountStatusesAsync)}", nameof(Account), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetAccountStatusesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Account), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetAccountStatusResponse> FetchAndCacheAccountStatusesAsync(COREAPPDATAMODELSDOMAIN.GetAccountStatusRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetAccountStatusResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._accountRepository.GetAccountStatusesAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.AccountStatuses = await repositoryResponse.AccountStatuses.ToListAsync();

			//Decryption
			var decryptedResult = response.AccountStatuses.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.AccountStatuses = decryptedResult;
			}

			await SetCachedData(cacheKey, response.AccountStatuses.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetAccountStatusRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetAccountStatusRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetAccountStatusRequest
		{
			MatchExpression = request.AccountStatusUId != Guid.Empty ? (entity) => entity.AccountStatusUId == request.AccountStatusUId : null,
			CorrelationUId = request.CorrelationUId
		};

		return repositoryRequest;
	}


	/// <summary>
	/// Merge AccountStatuses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeAccountStatusResponse> MergeAccountStatusesAsync(COREAPPDATAMODELSDOMAIN.MergeAccountStatusRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeAccountStatusesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Account), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeAccountStatusResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// AccountStatus.custom.cs MergeAccountStatusPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeAccountStatusPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("AccountStatus");

				if (isEncryptionRequired)
				{
					//Encryption
					request.AccountStatuses = request.AccountStatuses!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeAccountStatusRequest
			{
				AccountStatuses = request.AccountStatuses,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.AccountStatuses);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._accountRepository.MergeAccountStatusesAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _accountRepository.SaveAccountStatusAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("AccountStatus", nameof(MergeAccountStatusesAsync));

					await this.ResetCacheData(request, isEnabledEntityCaching).ConfigureAwait(false);
	
					response.MergeResult = repositoryResponse.MergeResult;

					response.Status = new COREAPPDATAMODELS.Status
					{
						Code = "Success",
						OperationStatus = COREDOMAINDATAMODELSENUM.OperationStatus.Success
					};
				}
			}

			//// If there are any customization required, Add custom code in the
			//// AccountStatus.custom.cs MergeAccountStatusPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeAccountStatusPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeAccountStatusesAsync)}", nameof(Account), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeAccountStatusesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Account), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt AccountStatuses data
	/// </summary>
	/// <param name="accountStatusUId"></param>
	/// <returns></returns>
	public async Task DecryptAccountStatusesAsync(Guid accountStatusUId)
	{
		Logger.LogInfo($"{nameof(DecryptAccountStatusesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Account), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Account), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptAccountStatusesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetAccountStatusRequest
			{
				AccountStatusUId = accountStatusUId
			};

			Logger.LogInfo($"{nameof(DecryptAccountStatusesAsync)} - Processing data for decryption", nameof(Account), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseAccountStatuses = await this.GetAccountStatusesAsync(request).ConfigureAwait(false);

			if (responseAccountStatuses.AccountStatuses != null && responseAccountStatuses.AccountStatuses.Any())
			{
				Logger.LogInfo($"{nameof(DecryptAccountStatusesAsync)} - Decrypted the data", nameof(Account), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var accountStatus in responseAccountStatuses.AccountStatuses)
				{
					accountStatus.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptAccountStatusesAsync)} - Set the item state to update the records back to entity", nameof(Account), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeAccountStatusRequest
				{
					AccountStatuses = responseAccountStatuses.AccountStatuses.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeAccountStatusesAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptAccountStatusesAsync)} while saving data", nameof(Account), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptAccountStatusesAsync)} - Successfully processed data for decryption", nameof(Account), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing accountStatuses data for decryption in {nameof(DecryptAccountStatusesAsync)}", nameof(Account), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Account), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptAccountStatusesAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptAccountStatusesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Account), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetAccountStatusRequest request)
	{
		var cacheKey = $"Domain.AccountStatus:{request.AccountStatusUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeAccountStatusRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.AccountStatuses != null)
		{	
			var cacheKey = $"Domain.AccountStatus:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var accountStatus in request.AccountStatuses)
			{
				cacheKey = $"Domain.AccountStatus:{accountStatus.AccountStatusUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
