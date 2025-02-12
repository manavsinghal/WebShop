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
/// Represents ProductCategoryLanguage class.
/// </summary>
/// <remarks>
/// The ProductCategoryLanguage class.
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
	/// Get ProductCategoryLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetProductCategoryLanguagePreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetProductCategoryLanguageRequest request);

	/// <summary>
	/// Get ProductCategoryLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetProductCategoryLanguagePostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetProductCategoryLanguageRequest request);

	/// <summary>
	/// Merge ProductCategoryLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeProductCategoryLanguagePreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeProductCategoryLanguageRequest request);

	/// <summary>
	/// Merge ProductCategoryLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeProductCategoryLanguagePostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeProductCategoryLanguageRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get ProductCategoryLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetProductCategoryLanguageResponse> GetProductCategoryLanguagesAsync(COREAPPDATAMODELSDOMAIN.GetProductCategoryLanguageRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Product), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetProductCategoryLanguageResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetProductCategoryLanguagePreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetProductCategoryLanguagesAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.ProductCategoryLanguages = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.ProductCategoryLanguage>>();
			}
			else
			{
				response = await FetchAndCacheProductCategoryLanguagesAsync(request, cacheKey, EnableCaching("ProductCategoryLanguage", nameof(GetProductCategoryLanguagesAsync)));
			}

			var postProcessorResponse = await GetProductCategoryLanguagePostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetProductCategoryLanguagesAsync)}", nameof(Product), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Product), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetProductCategoryLanguageResponse> FetchAndCacheProductCategoryLanguagesAsync(COREAPPDATAMODELSDOMAIN.GetProductCategoryLanguageRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetProductCategoryLanguageResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._productRepository.GetProductCategoryLanguagesAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.ProductCategoryLanguages = await repositoryResponse.ProductCategoryLanguages.ToListAsync();

			//Decryption
			var decryptedResult = response.ProductCategoryLanguages.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.ProductCategoryLanguages = decryptedResult;
			}

			await SetCachedData(cacheKey, response.ProductCategoryLanguages.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetProductCategoryLanguageRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetProductCategoryLanguageRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetProductCategoryLanguageRequest
		{
			MatchExpression = request.ProductCategoryLanguageUId != Guid.Empty ? (entity) => entity.ProductCategoryLanguageUId == request.ProductCategoryLanguageUId : null,
			CorrelationUId = request.CorrelationUId
		};

		return repositoryRequest;
	}


	/// <summary>
	/// Merge ProductCategoryLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeProductCategoryLanguageResponse> MergeProductCategoryLanguagesAsync(COREAPPDATAMODELSDOMAIN.MergeProductCategoryLanguageRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Product), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeProductCategoryLanguageResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// ProductCategoryLanguage.custom.cs MergeProductCategoryLanguagePreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeProductCategoryLanguagePreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("ProductCategoryLanguage");

				if (isEncryptionRequired)
				{
					//Encryption
					request.ProductCategoryLanguages = request.ProductCategoryLanguages!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeProductCategoryLanguageRequest
			{
				ProductCategoryLanguages = request.ProductCategoryLanguages,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.ProductCategoryLanguages);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._productRepository.MergeProductCategoryLanguagesAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _productRepository.SaveProductCategoryLanguageAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("ProductCategoryLanguage", nameof(MergeProductCategoryLanguagesAsync));

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
			//// ProductCategoryLanguage.custom.cs MergeProductCategoryLanguagePostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeProductCategoryLanguagePostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeProductCategoryLanguagesAsync)}", nameof(Product), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Product), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt ProductCategoryLanguages data
	/// </summary>
	/// <param name="productCategoryLanguageUId"></param>
	/// <returns></returns>
	public async Task DecryptProductCategoryLanguagesAsync(Guid productCategoryLanguageUId)
	{
		Logger.LogInfo($"{nameof(DecryptProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Product), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetProductCategoryLanguageRequest
			{
				ProductCategoryLanguageUId = productCategoryLanguageUId
			};

			Logger.LogInfo($"{nameof(DecryptProductCategoryLanguagesAsync)} - Processing data for decryption", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseProductCategoryLanguages = await this.GetProductCategoryLanguagesAsync(request).ConfigureAwait(false);

			if (responseProductCategoryLanguages.ProductCategoryLanguages != null && responseProductCategoryLanguages.ProductCategoryLanguages.Any())
			{
				Logger.LogInfo($"{nameof(DecryptProductCategoryLanguagesAsync)} - Decrypted the data", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var productCategoryLanguage in responseProductCategoryLanguages.ProductCategoryLanguages)
				{
					productCategoryLanguage.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptProductCategoryLanguagesAsync)} - Set the item state to update the records back to entity", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeProductCategoryLanguageRequest
				{
					ProductCategoryLanguages = responseProductCategoryLanguages.ProductCategoryLanguages.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeProductCategoryLanguagesAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptProductCategoryLanguagesAsync)} while saving data", nameof(Product), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptProductCategoryLanguagesAsync)} - Successfully processed data for decryption", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing productCategoryLanguages data for decryption in {nameof(DecryptProductCategoryLanguagesAsync)}", nameof(Product), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Product), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetProductCategoryLanguageRequest request)
	{
		var cacheKey = $"Domain.ProductCategoryLanguage:{request.ProductCategoryLanguageUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeProductCategoryLanguageRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.ProductCategoryLanguages != null)
		{	
			var cacheKey = $"Domain.ProductCategoryLanguage:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var productCategoryLanguage in request.ProductCategoryLanguages)
			{
				cacheKey = $"Domain.ProductCategoryLanguage:{productCategoryLanguage.ProductCategoryLanguageUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
