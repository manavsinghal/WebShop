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
/// Represents ShoppingCartWishList class.
/// </summary>
/// <remarks>
/// The ShoppingCartWishList class.
/// </remarks>
public partial class ShoppingCart : COREAPPMODELS.DomainModel<COREDOMAINDATAMODELSDOMAIN.ShoppingCart>, COREAPPINTERFACESDOMAIN.IShoppingCart
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	#endregion
	
	#region Partial Methods	

	/// <summary>
	/// Merge ShoppingCartWishLists
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeShoppingCartWishListPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeShoppingCartWishListRequest request);

	/// <summary>
	/// Get ShoppingCartWishLists
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetShoppingCartWishListPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetShoppingCartWishListRequest request);

	/// <summary>
	/// Merge ShoppingCartWishLists
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeShoppingCartWishListPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeShoppingCartWishListRequest request);

	/// <summary>
	/// Get ShoppingCartWishLists
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetShoppingCartWishListPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetShoppingCartWishListRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get ShoppingCartWishLists
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetShoppingCartWishListResponse> GetShoppingCartWishListsAsync(COREAPPDATAMODELSDOMAIN.GetShoppingCartWishListRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShoppingCart), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetShoppingCartWishListResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetShoppingCartWishListPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetShoppingCartWishListsAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.ShoppingCartWishLists = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.ShoppingCartWishList>>();
			}
			else
			{
				response = await FetchAndCacheShoppingCartWishListsAsync(request, cacheKey, EnableCaching("ShoppingCartWishList", nameof(GetShoppingCartWishListsAsync)));
			}

			var postProcessorResponse = await GetShoppingCartWishListPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetShoppingCartWishListsAsync)}", nameof(ShoppingCart), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShoppingCart), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetShoppingCartWishListResponse> FetchAndCacheShoppingCartWishListsAsync(COREAPPDATAMODELSDOMAIN.GetShoppingCartWishListRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetShoppingCartWishListResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._shoppingCartRepository.GetShoppingCartWishListsAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.ShoppingCartWishLists = await repositoryResponse.ShoppingCartWishLists.ToListAsync();

			//Decryption
			var decryptedResult = response.ShoppingCartWishLists.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.ShoppingCartWishLists = decryptedResult;
			}

			await SetCachedData(cacheKey, response.ShoppingCartWishLists.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetShoppingCartWishListRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetShoppingCartWishListRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetShoppingCartWishListRequest
		{
			MatchExpression = request.ShoppingCartWishListUId != Guid.Empty ? (entity) => entity.ShoppingCartWishListUId == request.ShoppingCartWishListUId : null,
			CorrelationUId = request.CorrelationUId
		};

		return repositoryRequest;
	}


	/// <summary>
	/// Merge ShoppingCartWishLists
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeShoppingCartWishListResponse> MergeShoppingCartWishListsAsync(COREAPPDATAMODELSDOMAIN.MergeShoppingCartWishListRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShoppingCart), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeShoppingCartWishListResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// ShoppingCartWishList.custom.cs MergeShoppingCartWishListPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeShoppingCartWishListPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("ShoppingCartWishList");

				if (isEncryptionRequired)
				{
					//Encryption
					request.ShoppingCartWishLists = request.ShoppingCartWishLists!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeShoppingCartWishListRequest
			{
				ShoppingCartWishLists = request.ShoppingCartWishLists,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.ShoppingCartWishLists);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._shoppingCartRepository.MergeShoppingCartWishListsAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _shoppingCartRepository.SaveShoppingCartWishListAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("ShoppingCartWishList", nameof(MergeShoppingCartWishListsAsync));

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
			//// ShoppingCartWishList.custom.cs MergeShoppingCartWishListPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeShoppingCartWishListPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeShoppingCartWishListsAsync)}", nameof(ShoppingCart), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShoppingCart), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt ShoppingCartWishLists data
	/// </summary>
	/// <param name="shoppingCartWishListUId"></param>
	/// <returns></returns>
	public async Task DecryptShoppingCartWishListsAsync(Guid shoppingCartWishListUId)
	{
		Logger.LogInfo($"{nameof(DecryptShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShoppingCart), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(ShoppingCart), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetShoppingCartWishListRequest
			{
				ShoppingCartWishListUId = shoppingCartWishListUId
			};

			Logger.LogInfo($"{nameof(DecryptShoppingCartWishListsAsync)} - Processing data for decryption", nameof(ShoppingCart), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseShoppingCartWishLists = await this.GetShoppingCartWishListsAsync(request).ConfigureAwait(false);

			if (responseShoppingCartWishLists.ShoppingCartWishLists != null && responseShoppingCartWishLists.ShoppingCartWishLists.Any())
			{
				Logger.LogInfo($"{nameof(DecryptShoppingCartWishListsAsync)} - Decrypted the data", nameof(ShoppingCart), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var shoppingCartWishList in responseShoppingCartWishLists.ShoppingCartWishLists)
				{
					shoppingCartWishList.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptShoppingCartWishListsAsync)} - Set the item state to update the records back to entity", nameof(ShoppingCart), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeShoppingCartWishListRequest
				{
					ShoppingCartWishLists = responseShoppingCartWishLists.ShoppingCartWishLists.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeShoppingCartWishListsAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptShoppingCartWishListsAsync)} while saving data", nameof(ShoppingCart), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptShoppingCartWishListsAsync)} - Successfully processed data for decryption", nameof(ShoppingCart), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing shoppingCartWishLists data for decryption in {nameof(DecryptShoppingCartWishListsAsync)}", nameof(ShoppingCart), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(ShoppingCart), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShoppingCart), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetShoppingCartWishListRequest request)
	{
		var cacheKey = $"Domain.ShoppingCartWishList:{request.ShoppingCartWishListUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeShoppingCartWishListRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.ShoppingCartWishLists != null)
		{	
			var cacheKey = $"Domain.ShoppingCartWishList:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var shoppingCartWishList in request.ShoppingCartWishLists)
			{
				cacheKey = $"Domain.ShoppingCartWishList:{shoppingCartWishList.ShoppingCartWishListUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
