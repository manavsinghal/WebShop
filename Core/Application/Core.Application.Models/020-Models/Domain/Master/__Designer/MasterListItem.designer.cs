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
/// Represents MasterListItem class.
/// </summary>
/// <remarks>
/// The MasterListItem class.
/// </remarks>
public partial class Master : COREAPPMODELS.DomainModel<COREDOMAINDATAMODELSDOMAIN.MasterList>, COREAPPINTERFACESDOMAIN.IMaster
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	#endregion
	
	#region Partial Methods	

	/// <summary>
	/// Merge MasterListItems
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeMasterListItemPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeMasterListItemRequest request);

	/// <summary>
	/// Merge MasterListItems
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeMasterListItemPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeMasterListItemRequest request);

	/// <summary>
	/// Get MasterListItems
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetMasterListItemPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetMasterListItemRequest request);

	/// <summary>
	/// Get MasterListItems
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetMasterListItemPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetMasterListItemRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get MasterListItems
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetMasterListItemResponse> GetMasterListItemsAsync(COREAPPDATAMODELSDOMAIN.GetMasterListItemRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetMasterListItemsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetMasterListItemResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetMasterListItemPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetMasterListItemsAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.MasterListItems = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.MasterListItem>>();
			}
			else
			{
				response = await FetchAndCacheMasterListItemsAsync(request, cacheKey, EnableCaching("MasterListItem", nameof(GetMasterListItemsAsync)));
			}

			var postProcessorResponse = await GetMasterListItemPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetMasterListItemsAsync)}", nameof(Master), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetMasterListItemsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetMasterListItemResponse> FetchAndCacheMasterListItemsAsync(COREAPPDATAMODELSDOMAIN.GetMasterListItemRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetMasterListItemResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._masterRepository.GetMasterListItemsAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.MasterListItems = await repositoryResponse.MasterListItems.ToListAsync();

			//Decryption
			var decryptedResult = response.MasterListItems.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.MasterListItems = decryptedResult;
			}

			await SetCachedData(cacheKey, response.MasterListItems.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetMasterListItemRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetMasterListItemRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetMasterListItemRequest();

		if (request.MasterListItemUId == Guid.Empty && request.MasterListUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = null;
		}
		else if (request.MasterListItemUId == Guid.Empty && request.MasterListUId != Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.MasterListUId == request.MasterListUId;
		}
		else if (request.MasterListItemUId != Guid.Empty && request.MasterListUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.MasterListItemUId == request.MasterListItemUId;
		}
		else
		{
			repositoryRequest.MatchExpression = (entity) => entity.MasterListItemUId == request.MasterListItemUId && entity.MasterListUId == request.MasterListUId;
		}

		return repositoryRequest;
	}


	/// <summary>
	/// Merge MasterListItems
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeMasterListItemResponse> MergeMasterListItemsAsync(COREAPPDATAMODELSDOMAIN.MergeMasterListItemRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeMasterListItemsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeMasterListItemResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// MasterListItem.custom.cs MergeMasterListItemPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeMasterListItemPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("MasterListItem");

				if (isEncryptionRequired)
				{
					//Encryption
					request.MasterListItems = request.MasterListItems!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeMasterListItemRequest
			{
				MasterListItems = request.MasterListItems,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.MasterListItems);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._masterRepository.MergeMasterListItemsAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _masterRepository.SaveMasterListItemAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("MasterListItem", nameof(MergeMasterListItemsAsync));

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
			//// MasterListItem.custom.cs MergeMasterListItemPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeMasterListItemPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeMasterListItemsAsync)}", nameof(Master), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeMasterListItemsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt MasterListItems data
	/// </summary>
	/// <param name="masterListItemUId"></param>
	/// <returns></returns>
	public async Task DecryptMasterListItemsAsync(Guid masterListItemUId)
	{
		Logger.LogInfo($"{nameof(DecryptMasterListItemsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Master), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptMasterListItemsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetMasterListItemRequest
			{
				MasterListItemUId = masterListItemUId
			};

			Logger.LogInfo($"{nameof(DecryptMasterListItemsAsync)} - Processing data for decryption", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseMasterListItems = await this.GetMasterListItemsAsync(request).ConfigureAwait(false);

			if (responseMasterListItems.MasterListItems != null && responseMasterListItems.MasterListItems.Any())
			{
				Logger.LogInfo($"{nameof(DecryptMasterListItemsAsync)} - Decrypted the data", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var masterListItem in responseMasterListItems.MasterListItems)
				{
					masterListItem.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptMasterListItemsAsync)} - Set the item state to update the records back to entity", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeMasterListItemRequest
				{
					MasterListItems = responseMasterListItems.MasterListItems.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeMasterListItemsAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptMasterListItemsAsync)} while saving data", nameof(Master), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptMasterListItemsAsync)} - Successfully processed data for decryption", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing masterListItems data for decryption in {nameof(DecryptMasterListItemsAsync)}", nameof(Master), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Master), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptMasterListItemsAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptMasterListItemsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetMasterListItemRequest request)
	{
		var cacheKey = $"Domain.MasterListItem:{request.MasterListItemUId}:{request.MasterListUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeMasterListItemRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.MasterListItems != null)
		{	
			var cacheKey = $"Domain.MasterListItem:{Guid.Empty}:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var masterListItem in request.MasterListItems)
			{
				cacheKey = $"Domain.MasterListItem:{masterListItem.MasterListItemUId}:{Guid.Empty}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

				cacheKey = $"Domain.MasterListItem:{Guid.Empty}:{masterListItem.MasterListUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
