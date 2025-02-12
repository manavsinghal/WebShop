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
/// Represents SellerBankAccount class.
/// </summary>
/// <remarks>
/// The SellerBankAccount class.
/// </remarks>
public partial class Seller : COREAPPMODELS.DomainModel<COREDOMAINDATAMODELSDOMAIN.Seller>, COREAPPINTERFACESDOMAIN.ISeller
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	#endregion
	
	#region Partial Methods	

	/// <summary>
	/// Get SellerBankAccounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetSellerBankAccountPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetSellerBankAccountRequest request);

	/// <summary>
	/// Merge SellerBankAccounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeSellerBankAccountPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeSellerBankAccountRequest request);

	/// <summary>
	/// Merge SellerBankAccounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeSellerBankAccountPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeSellerBankAccountRequest request);

	/// <summary>
	/// Get SellerBankAccounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetSellerBankAccountPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetSellerBankAccountRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get SellerBankAccounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetSellerBankAccountResponse> GetSellerBankAccountsAsync(COREAPPDATAMODELSDOMAIN.GetSellerBankAccountRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Seller), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetSellerBankAccountResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetSellerBankAccountPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetSellerBankAccountsAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.SellerBankAccounts = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.SellerBankAccount>>();
			}
			else
			{
				response = await FetchAndCacheSellerBankAccountsAsync(request, cacheKey, EnableCaching("SellerBankAccount", nameof(GetSellerBankAccountsAsync)));
			}

			var postProcessorResponse = await GetSellerBankAccountPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetSellerBankAccountsAsync)}", nameof(Seller), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Seller), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetSellerBankAccountResponse> FetchAndCacheSellerBankAccountsAsync(COREAPPDATAMODELSDOMAIN.GetSellerBankAccountRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetSellerBankAccountResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._sellerRepository.GetSellerBankAccountsAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.SellerBankAccounts = await repositoryResponse.SellerBankAccounts.ToListAsync();

			//Decryption
			var decryptedResult = response.SellerBankAccounts.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.SellerBankAccounts = decryptedResult;
			}

			await SetCachedData(cacheKey, response.SellerBankAccounts.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetSellerBankAccountRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetSellerBankAccountRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetSellerBankAccountRequest();

		if (request.SellerBankAccountUId == Guid.Empty && request.SellerUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = null;
		}
		else if (request.SellerBankAccountUId == Guid.Empty && request.SellerUId != Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.SellerUId == request.SellerUId;
		}
		else if (request.SellerBankAccountUId != Guid.Empty && request.SellerUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.SellerBankAccountUId == request.SellerBankAccountUId;
		}
		else
		{
			repositoryRequest.MatchExpression = (entity) => entity.SellerBankAccountUId == request.SellerBankAccountUId && entity.SellerUId == request.SellerUId;
		}

		return repositoryRequest;
	}


	/// <summary>
	/// Merge SellerBankAccounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeSellerBankAccountResponse> MergeSellerBankAccountsAsync(COREAPPDATAMODELSDOMAIN.MergeSellerBankAccountRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Seller), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeSellerBankAccountResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// SellerBankAccount.custom.cs MergeSellerBankAccountPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeSellerBankAccountPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("SellerBankAccount");

				if (isEncryptionRequired)
				{
					//Encryption
					request.SellerBankAccounts = request.SellerBankAccounts!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeSellerBankAccountRequest
			{
				SellerBankAccounts = request.SellerBankAccounts,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.SellerBankAccounts);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._sellerRepository.MergeSellerBankAccountsAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _sellerRepository.SaveSellerBankAccountAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("SellerBankAccount", nameof(MergeSellerBankAccountsAsync));

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
			//// SellerBankAccount.custom.cs MergeSellerBankAccountPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeSellerBankAccountPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeSellerBankAccountsAsync)}", nameof(Seller), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Seller), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt SellerBankAccounts data
	/// </summary>
	/// <param name="sellerBankAccountUId"></param>
	/// <returns></returns>
	public async Task DecryptSellerBankAccountsAsync(Guid sellerBankAccountUId)
	{
		Logger.LogInfo($"{nameof(DecryptSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Seller), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetSellerBankAccountRequest
			{
				SellerBankAccountUId = sellerBankAccountUId
			};

			Logger.LogInfo($"{nameof(DecryptSellerBankAccountsAsync)} - Processing data for decryption", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseSellerBankAccounts = await this.GetSellerBankAccountsAsync(request).ConfigureAwait(false);

			if (responseSellerBankAccounts.SellerBankAccounts != null && responseSellerBankAccounts.SellerBankAccounts.Any())
			{
				Logger.LogInfo($"{nameof(DecryptSellerBankAccountsAsync)} - Decrypted the data", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var sellerBankAccount in responseSellerBankAccounts.SellerBankAccounts)
				{
					sellerBankAccount.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptSellerBankAccountsAsync)} - Set the item state to update the records back to entity", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeSellerBankAccountRequest
				{
					SellerBankAccounts = responseSellerBankAccounts.SellerBankAccounts.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeSellerBankAccountsAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptSellerBankAccountsAsync)} while saving data", nameof(Seller), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptSellerBankAccountsAsync)} - Successfully processed data for decryption", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing sellerBankAccounts data for decryption in {nameof(DecryptSellerBankAccountsAsync)}", nameof(Seller), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Seller), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetSellerBankAccountRequest request)
	{
		var cacheKey = $"Domain.SellerBankAccount:{request.SellerBankAccountUId}:{request.SellerUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeSellerBankAccountRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.SellerBankAccounts != null)
		{	
			var cacheKey = $"Domain.SellerBankAccount:{Guid.Empty}:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var sellerBankAccount in request.SellerBankAccounts)
			{
				cacheKey = $"Domain.SellerBankAccount:{sellerBankAccount.SellerBankAccountUId}:{Guid.Empty}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

				cacheKey = $"Domain.SellerBankAccount:{Guid.Empty}:{sellerBankAccount.SellerUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
