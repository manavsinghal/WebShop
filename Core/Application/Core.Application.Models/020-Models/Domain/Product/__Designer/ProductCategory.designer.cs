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
/// Represents ProductCategory class.
/// </summary>
/// <remarks>
/// The ProductCategory class.
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
	/// Get ProductCategories
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetProductCategoryPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetProductCategoryRequest request);

	/// <summary>
	/// Merge ProductCategories
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeProductCategoryPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeProductCategoryRequest request);

	/// <summary>
	/// Get ProductCategories
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetProductCategoryPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetProductCategoryRequest request);

	/// <summary>
	/// Merge ProductCategories
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeProductCategoryPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeProductCategoryRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get ProductCategories
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetProductCategoryResponse> GetProductCategoriesAsync(COREAPPDATAMODELSDOMAIN.GetProductCategoryRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Product), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetProductCategoryResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetProductCategoryPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetProductCategoriesAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.ProductCategories = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.ProductCategory>>();
			}
			else
			{
				response = await FetchAndCacheProductCategoriesAsync(request, cacheKey, EnableCaching("ProductCategory", nameof(GetProductCategoriesAsync)));
			}

			var postProcessorResponse = await GetProductCategoryPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetProductCategoriesAsync)}", nameof(Product), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Product), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetProductCategoryResponse> FetchAndCacheProductCategoriesAsync(COREAPPDATAMODELSDOMAIN.GetProductCategoryRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetProductCategoryResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._productRepository.GetProductCategoriesAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			if (request.LanguageUId != Guid.Empty && request.LanguageUId != COREDOMAINDATAMODELSDOMAINENUM.Language.EnglishUS)
			{
				response.ProductCategories = repositoryResponse.ProductCategories.Include(x => x.ProductCategoryLanguages.Where(y => y.LanguageUId == request.LanguageUId)).ToList();

				if (response.ProductCategories != null)
				{
					foreach (var productCategory in response.ProductCategories)							
					{
						if (productCategory.ProductCategoryLanguages != null && productCategory.ProductCategoryLanguages.Any())
						{
							productCategory.Name = productCategory.ProductCategoryLanguages.First().Name;
							productCategory.Description = productCategory.ProductCategoryLanguages.First().Description;
							productCategory.ProductCategoryLanguages = null;
						}
					}
				}
			}
			else
			{
				response.ProductCategories = await repositoryResponse.ProductCategories.ToListAsync();
			}

			//Decryption
			var decryptedResult = response.ProductCategories.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.ProductCategories = decryptedResult;
			}

			await SetCachedData(cacheKey, response.ProductCategories.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetProductCategoryRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetProductCategoryRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetProductCategoryRequest
		{
			MatchExpression = request.ProductCategoryUId != Guid.Empty ? (entity) => entity.ProductCategoryUId == request.ProductCategoryUId : null,
			CorrelationUId = request.CorrelationUId
		};

		return repositoryRequest;
	}


	/// <summary>
	/// Merge ProductCategories
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeProductCategoryResponse> MergeProductCategoriesAsync(COREAPPDATAMODELSDOMAIN.MergeProductCategoryRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Product), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeProductCategoryResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// ProductCategory.custom.cs MergeProductCategoryPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeProductCategoryPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("ProductCategory");

				if (isEncryptionRequired)
				{
					//Encryption
					request.ProductCategories = request.ProductCategories!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeProductCategoryRequest
			{
				ProductCategories = request.ProductCategories,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.ProductCategories);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._productRepository.MergeProductCategoriesAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _productRepository.SaveProductCategoryAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("ProductCategory", nameof(MergeProductCategoriesAsync));

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
			//// ProductCategory.custom.cs MergeProductCategoryPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeProductCategoryPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeProductCategoriesAsync)}", nameof(Product), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Product), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt ProductCategories data
	/// </summary>
	/// <param name="productCategoryUId"></param>
	/// <returns></returns>
	public async Task DecryptProductCategoriesAsync(Guid productCategoryUId)
	{
		Logger.LogInfo($"{nameof(DecryptProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Product), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetProductCategoryRequest
			{
				ProductCategoryUId = productCategoryUId
			};

			Logger.LogInfo($"{nameof(DecryptProductCategoriesAsync)} - Processing data for decryption", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseProductCategories = await this.GetProductCategoriesAsync(request).ConfigureAwait(false);

			if (responseProductCategories.ProductCategories != null && responseProductCategories.ProductCategories.Any())
			{
				Logger.LogInfo($"{nameof(DecryptProductCategoriesAsync)} - Decrypted the data", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var productCategory in responseProductCategories.ProductCategories)
				{
					productCategory.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptProductCategoriesAsync)} - Set the item state to update the records back to entity", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeProductCategoryRequest
				{
					ProductCategories = responseProductCategories.ProductCategories.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeProductCategoriesAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptProductCategoriesAsync)} while saving data", nameof(Product), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptProductCategoriesAsync)} - Successfully processed data for decryption", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing productCategories data for decryption in {nameof(DecryptProductCategoriesAsync)}", nameof(Product), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Product), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetProductCategoryRequest request)
	{
		var cacheKey = $"Domain.ProductCategory:{request.ProductCategoryUId}:{request.LanguageUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeProductCategoryRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.ProductCategories != null)
		{
			var response = await _master.GetLanguagesAsync(new COREAPPDATAMODELSDOMAIN.GetLanguageRequest()).ConfigureAwait(false);
			var cacheKey = $"Domain.ProductCategory:{Guid.Empty}:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var language in response.Languages)
			{ 
				cacheKey = $"Domain.ProductCategory:{Guid.Empty}:{language.LanguageUId}";
				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
			}

			foreach (var productCategory in request.ProductCategories)
			{						

				if (response != null)
				{ 
					foreach (var language in response.Languages)
					{
						cacheKey = $"Domain.ProductCategory:{productCategory.ProductCategoryUId}:{language.LanguageUId}";
						await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
					}
				}
			}
		}
	}

	#endregion
}
