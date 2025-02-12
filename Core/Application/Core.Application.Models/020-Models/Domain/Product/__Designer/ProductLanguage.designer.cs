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
/// Represents ProductLanguage class.
/// </summary>
/// <remarks>
/// The ProductLanguage class.
/// </remarks>
public partial class Product : COREAPPMODELS.DomainModel<COREDOMAINDATAMODELSDOMAIN.Product>, COREAPPINTERFACESDOMAIN.IProduct
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	#endregion
	
	#region Partial Methods	

	/// <summary>
	/// Get ProductLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetProductLanguagePostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetProductLanguageRequest request);

	/// <summary>
	/// Merge ProductLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeProductLanguagePostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeProductLanguageRequest request);

	/// <summary>
	/// Get ProductLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetProductLanguagePreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetProductLanguageRequest request);

	/// <summary>
	/// Merge ProductLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeProductLanguagePreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeProductLanguageRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get ProductLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetProductLanguageResponse> GetProductLanguagesAsync(COREAPPDATAMODELSDOMAIN.GetProductLanguageRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Product), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetProductLanguageResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetProductLanguagePreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetProductLanguagesAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.ProductLanguages = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.ProductLanguage>>();
			}
			else
			{
				response = await FetchAndCacheProductLanguagesAsync(request, cacheKey, EnableCaching("ProductLanguage", nameof(GetProductLanguagesAsync)));
			}

			var postProcessorResponse = await GetProductLanguagePostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetProductLanguagesAsync)}", nameof(Product), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Product), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetProductLanguageResponse> FetchAndCacheProductLanguagesAsync(COREAPPDATAMODELSDOMAIN.GetProductLanguageRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetProductLanguageResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._productRepository.GetProductLanguagesAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.ProductLanguages = await repositoryResponse.ProductLanguages.ToListAsync();

			//Decryption
			var decryptedResult = response.ProductLanguages.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.ProductLanguages = decryptedResult;
			}

			await SetCachedData(cacheKey, response.ProductLanguages.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetProductLanguageRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetProductLanguageRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetProductLanguageRequest();

		if (request.ProductLanguageUId == Guid.Empty && request.ProductUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = null;
		}
		else if (request.ProductLanguageUId == Guid.Empty && request.ProductUId != Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.ProductUId == request.ProductUId;
		}
		else if (request.ProductLanguageUId != Guid.Empty && request.ProductUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.ProductLanguageUId == request.ProductLanguageUId;
		}
		else
		{
			repositoryRequest.MatchExpression = (entity) => entity.ProductLanguageUId == request.ProductLanguageUId && entity.ProductUId == request.ProductUId;
		}

		return repositoryRequest;
	}


	/// <summary>
	/// Merge ProductLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeProductLanguageResponse> MergeProductLanguagesAsync(COREAPPDATAMODELSDOMAIN.MergeProductLanguageRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Product), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeProductLanguageResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// ProductLanguage.custom.cs MergeProductLanguagePreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeProductLanguagePreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("ProductLanguage");

				if (isEncryptionRequired)
				{
					//Encryption
					request.ProductLanguages = request.ProductLanguages!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeProductLanguageRequest
			{
				ProductLanguages = request.ProductLanguages,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.ProductLanguages);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._productRepository.MergeProductLanguagesAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _productRepository.SaveProductLanguageAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("ProductLanguage", nameof(MergeProductLanguagesAsync));

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
			//// ProductLanguage.custom.cs MergeProductLanguagePostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeProductLanguagePostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeProductLanguagesAsync)}", nameof(Product), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Product), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt ProductLanguages data
	/// </summary>
	/// <param name="productLanguageUId"></param>
	/// <returns></returns>
	public async Task DecryptProductLanguagesAsync(Guid productLanguageUId)
	{
		Logger.LogInfo($"{nameof(DecryptProductLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Product), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptProductLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetProductLanguageRequest
			{
				ProductLanguageUId = productLanguageUId
			};

			Logger.LogInfo($"{nameof(DecryptProductLanguagesAsync)} - Processing data for decryption", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseProductLanguages = await this.GetProductLanguagesAsync(request).ConfigureAwait(false);

			if (responseProductLanguages.ProductLanguages != null && responseProductLanguages.ProductLanguages.Any())
			{
				Logger.LogInfo($"{nameof(DecryptProductLanguagesAsync)} - Decrypted the data", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var productLanguage in responseProductLanguages.ProductLanguages)
				{
					productLanguage.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptProductLanguagesAsync)} - Set the item state to update the records back to entity", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeProductLanguageRequest
				{
					ProductLanguages = responseProductLanguages.ProductLanguages.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeProductLanguagesAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptProductLanguagesAsync)} while saving data", nameof(Product), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptProductLanguagesAsync)} - Successfully processed data for decryption", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing productLanguages data for decryption in {nameof(DecryptProductLanguagesAsync)}", nameof(Product), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Product), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptProductLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptProductLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetProductLanguageRequest request)
	{
		var cacheKey = $"Domain.ProductLanguage:{request.ProductLanguageUId}:{request.ProductUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeProductLanguageRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.ProductLanguages != null)
		{	
			var cacheKey = $"Domain.ProductLanguage:{Guid.Empty}:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var productLanguage in request.ProductLanguages)
			{
				cacheKey = $"Domain.ProductLanguage:{productLanguage.ProductLanguageUId}:{Guid.Empty}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

				cacheKey = $"Domain.ProductLanguage:{Guid.Empty}:{productLanguage.ProductUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
