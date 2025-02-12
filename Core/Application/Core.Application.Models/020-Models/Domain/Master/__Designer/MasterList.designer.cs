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
/// Represents MasterList class.
/// </summary>
/// <remarks>
/// The MasterList class.
/// </remarks>
public partial class Master : COREAPPMODELS.DomainModel<COREDOMAINDATAMODELSDOMAIN.MasterList>, COREAPPINTERFACESDOMAIN.IMaster
{
	#region Fields

	private readonly COREAPPDREPOINTERFACESDOMAIN.IMasterRepository _masterRepository;

	private readonly IServiceProvider _serviceProvider;

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the MasterList class
	/// </summary>
	/// <param name="masterRepository">The masterRepository.</param>
	/// <param name="logger">The logger.</param>
	/// <param name="cache">The cache.</param>
	/// <param name="messageHub"></param>
	/// <param name="dbAppSettings"></param>
	/// <param name="serviceProvider"></param>
	public Master(COREAPPDREPOINTERFACESDOMAIN.IMasterRepository masterRepository	
							, ILogger<COREDOMAINDATAMODELSDOMAIN.MasterList> logger
							, IDistributedCache cache
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							, COREAPPINTERFACESDBAPPSETTINGS.IDbAppSettings dbAppSettings
							, IServiceProvider serviceProvider
							): base(logger, cache, messageHub, dbAppSettings, serviceProvider)
	{
		this._masterRepository = masterRepository;
		this._serviceProvider = serviceProvider;
	}

	#endregion
	
	#region Partial Methods	

	/// <summary>
	/// Get MasterLists
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetMasterListPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetMasterListRequest request);

	/// <summary>
	/// Get MasterLists
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetMasterListPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetMasterListRequest request);

	/// <summary>
	/// Merge MasterLists
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeMasterListPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeMasterListRequest request);

	/// <summary>
	/// Merge MasterLists
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeMasterListPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeMasterListRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get MasterLists
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetMasterListResponse> GetMasterListsAsync(COREAPPDATAMODELSDOMAIN.GetMasterListRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetMasterListResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetMasterListPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetMasterListsAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.MasterLists = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.MasterList>>();
			}
			else
			{
				response = await FetchAndCacheMasterListsAsync(request, cacheKey, EnableCaching("MasterList", nameof(GetMasterListsAsync)));
			}

			var postProcessorResponse = await GetMasterListPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetMasterListsAsync)}", nameof(Master), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetMasterListResponse> FetchAndCacheMasterListsAsync(COREAPPDATAMODELSDOMAIN.GetMasterListRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetMasterListResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._masterRepository.GetMasterListsAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.MasterLists = await repositoryResponse.MasterLists.ToListAsync();

			//Decryption
			var decryptedResult = response.MasterLists.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.MasterLists = decryptedResult;
			}

			await SetCachedData(cacheKey, response.MasterLists.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetMasterListRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetMasterListRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetMasterListRequest
		{
			MatchExpression = request.MasterListUId != Guid.Empty ? (entity) => entity.MasterListUId == request.MasterListUId : null,
			CorrelationUId = request.CorrelationUId
		};

		return repositoryRequest;
	}


	/// <summary>
	/// Merge MasterLists
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeMasterListResponse> MergeMasterListsAsync(COREAPPDATAMODELSDOMAIN.MergeMasterListRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeMasterListResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// MasterList.custom.cs MergeMasterListPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeMasterListPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("MasterList");

				if (isEncryptionRequired)
				{
					//Encryption
					request.MasterLists = request.MasterLists!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeMasterListRequest
			{
				MasterLists = request.MasterLists,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.MasterLists);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._masterRepository.MergeMasterListsAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _masterRepository.SaveMasterListAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("MasterList", nameof(MergeMasterListsAsync));

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
			//// MasterList.custom.cs MergeMasterListPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeMasterListPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeMasterListsAsync)}", nameof(Master), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}


	/// <summary>
	/// Gets the Master List Items By Code.
	/// </summary>
	/// <param name="masterListCode">The master list code.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetMasterListItemResponse> GetMasterListItemsByCode(String masterListCode)
	{
		Logger.LogInfo($"{nameof(GetMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(GetMasterListsAsync), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		
		COREAPPDATAMODELSDOMAIN.GetMasterListItemResponse? response = new();

		try
		{
			var cacheKey = $"Domain.MasterList:{masterListCode}";

			var cachedData = await this.GetCachedData(cacheKey, true).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.MasterListItems = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.MasterListItem>>();
			}
			else
			{
				var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetMasterListRequest
				{
					MatchExpression = (entity) => masterListCode.Replace(" ","") == entity.Code!.Replace(" ", "")
				};

				var masterListItemsResponse = await _masterRepository.GetMasterListsAsync(repositoryRequest).ConfigureAwait(false);

				if (masterListItemsResponse.Faults!.Any())
				{
					response.Faults = masterListItemsResponse.Faults;
					response.MergeResult = response.MergeResult;
				}
				else
				{
					masterListItemsResponse.MasterLists = masterListItemsResponse.MasterLists.Include(x => x.MasterListItems);

					if (masterListItemsResponse.MasterLists != null && masterListItemsResponse.MasterLists.Any())
					{
						 var masterLists = await masterListItemsResponse.MasterLists.ToListAsync();
						 response.MasterListItems = masterLists.FirstOrDefault().MasterListItems;

						 if (response.MasterListItems != null && response.MasterListItems.Any())
						 {
							foreach (var masterListItem in response.MasterListItems)
							{
								masterListItem.MasterList = null;
							}
						 }

						 await SetCachedData(cacheKey, response.MasterListItems.ToJson(), true).ConfigureAwait(false);
					}
				 }
			 }
		}
		catch (Exception ex)
		{
			await HandleError("Error occurred", nameof(GetMasterListsAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(GetMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(GetMasterListsAsync), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}

		return response;
	}
	/// <summary>
	/// Decrypt MasterLists data
	/// </summary>
	/// <param name="masterListUId"></param>
	/// <returns></returns>
	public async Task DecryptMasterListsAsync(Guid masterListUId)
	{
		Logger.LogInfo($"{nameof(DecryptMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Master), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetMasterListRequest
			{
				MasterListUId = masterListUId
			};

			Logger.LogInfo($"{nameof(DecryptMasterListsAsync)} - Processing data for decryption", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseMasterLists = await this.GetMasterListsAsync(request).ConfigureAwait(false);

			if (responseMasterLists.MasterLists != null && responseMasterLists.MasterLists.Any())
			{
				Logger.LogInfo($"{nameof(DecryptMasterListsAsync)} - Decrypted the data", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var masterList in responseMasterLists.MasterLists)
				{
					masterList.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptMasterListsAsync)} - Set the item state to update the records back to entity", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeMasterListRequest
				{
					MasterLists = responseMasterLists.MasterLists.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeMasterListsAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptMasterListsAsync)} while saving data", nameof(Master), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptMasterListsAsync)} - Successfully processed data for decryption", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing masterLists data for decryption in {nameof(DecryptMasterListsAsync)}", nameof(Master), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Master), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetMasterListRequest request)
	{
		var cacheKey = $"Domain.MasterList:{request.MasterListUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeMasterListRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.MasterLists != null)
		{	
			var cacheKey = $"Domain.MasterList:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var masterList in request.MasterLists)
			{
				cacheKey = $"Domain.MasterList:{masterList.MasterListUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
