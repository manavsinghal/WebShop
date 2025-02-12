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
/// Represents SellerAddress class.
/// </summary>
/// <remarks>
/// The SellerAddress class.
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
	/// Merge SellerAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeSellerAddressPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeSellerAddressRequest request);

	/// <summary>
	/// Merge SellerAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeSellerAddressPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeSellerAddressRequest request);

	/// <summary>
	/// Get SellerAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetSellerAddressPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetSellerAddressRequest request);

	/// <summary>
	/// Get SellerAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetSellerAddressPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetSellerAddressRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get SellerAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetSellerAddressResponse> GetSellerAddressesAsync(COREAPPDATAMODELSDOMAIN.GetSellerAddressRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Seller), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetSellerAddressResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetSellerAddressPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetSellerAddressesAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.SellerAddresses = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.SellerAddress>>();
			}
			else
			{
				response = await FetchAndCacheSellerAddressesAsync(request, cacheKey, EnableCaching("SellerAddress", nameof(GetSellerAddressesAsync)));
			}

			var postProcessorResponse = await GetSellerAddressPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetSellerAddressesAsync)}", nameof(Seller), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Seller), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetSellerAddressResponse> FetchAndCacheSellerAddressesAsync(COREAPPDATAMODELSDOMAIN.GetSellerAddressRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetSellerAddressResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._sellerRepository.GetSellerAddressesAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.SellerAddresses = await repositoryResponse.SellerAddresses.ToListAsync();

			//Decryption
			var decryptedResult = response.SellerAddresses.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.SellerAddresses = decryptedResult;
			}

			await SetCachedData(cacheKey, response.SellerAddresses.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetSellerAddressRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetSellerAddressRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetSellerAddressRequest();

		if (request.SellerAddressUId == Guid.Empty && request.SellerUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = null;
		}
		else if (request.SellerAddressUId == Guid.Empty && request.SellerUId != Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.SellerUId == request.SellerUId;
		}
		else if (request.SellerAddressUId != Guid.Empty && request.SellerUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.SellerAddressUId == request.SellerAddressUId;
		}
		else
		{
			repositoryRequest.MatchExpression = (entity) => entity.SellerAddressUId == request.SellerAddressUId && entity.SellerUId == request.SellerUId;
		}

		return repositoryRequest;
	}


	/// <summary>
	/// Merge SellerAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeSellerAddressResponse> MergeSellerAddressesAsync(COREAPPDATAMODELSDOMAIN.MergeSellerAddressRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Seller), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeSellerAddressResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// SellerAddress.custom.cs MergeSellerAddressPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeSellerAddressPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("SellerAddress");

				if (isEncryptionRequired)
				{
					//Encryption
					request.SellerAddresses = request.SellerAddresses!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeSellerAddressRequest
			{
				SellerAddresses = request.SellerAddresses,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.SellerAddresses);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._sellerRepository.MergeSellerAddressesAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _sellerRepository.SaveSellerAddressAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("SellerAddress", nameof(MergeSellerAddressesAsync));

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
			//// SellerAddress.custom.cs MergeSellerAddressPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeSellerAddressPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeSellerAddressesAsync)}", nameof(Seller), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Seller), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt SellerAddresses data
	/// </summary>
	/// <param name="sellerAddressUId"></param>
	/// <returns></returns>
	public async Task DecryptSellerAddressesAsync(Guid sellerAddressUId)
	{
		Logger.LogInfo($"{nameof(DecryptSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Seller), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetSellerAddressRequest
			{
				SellerAddressUId = sellerAddressUId
			};

			Logger.LogInfo($"{nameof(DecryptSellerAddressesAsync)} - Processing data for decryption", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseSellerAddresses = await this.GetSellerAddressesAsync(request).ConfigureAwait(false);

			if (responseSellerAddresses.SellerAddresses != null && responseSellerAddresses.SellerAddresses.Any())
			{
				Logger.LogInfo($"{nameof(DecryptSellerAddressesAsync)} - Decrypted the data", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var sellerAddress in responseSellerAddresses.SellerAddresses)
				{
					sellerAddress.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptSellerAddressesAsync)} - Set the item state to update the records back to entity", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeSellerAddressRequest
				{
					SellerAddresses = responseSellerAddresses.SellerAddresses.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeSellerAddressesAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptSellerAddressesAsync)} while saving data", nameof(Seller), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptSellerAddressesAsync)} - Successfully processed data for decryption", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing sellerAddresses data for decryption in {nameof(DecryptSellerAddressesAsync)}", nameof(Seller), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Seller), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetSellerAddressRequest request)
	{
		var cacheKey = $"Domain.SellerAddress:{request.SellerAddressUId}:{request.SellerUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeSellerAddressRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.SellerAddresses != null)
		{	
			var cacheKey = $"Domain.SellerAddress:{Guid.Empty}:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var sellerAddress in request.SellerAddresses)
			{
				cacheKey = $"Domain.SellerAddress:{sellerAddress.SellerAddressUId}:{Guid.Empty}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

				cacheKey = $"Domain.SellerAddress:{Guid.Empty}:{sellerAddress.SellerUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
