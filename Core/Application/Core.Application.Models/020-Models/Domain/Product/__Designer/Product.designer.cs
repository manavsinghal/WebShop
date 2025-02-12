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
/// Represents Product class.
/// </summary>
/// <remarks>
/// The Product class.
/// </remarks>
public partial class Product : COREAPPMODELS.DomainModel<COREDOMAINDATAMODELSDOMAIN.Product>, COREAPPINTERFACESDOMAIN.IProduct
{
	#region Fields

	private readonly COREAPPINTERFACESDOMAIN.IMaster _master;

	private readonly COREAPPDREPOINTERFACESDOMAIN.IProductRepository _productRepository;

	private readonly IServiceProvider _serviceProvider;

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the Product class
	/// </summary>
	/// <param name="master">The master.</param>
	/// <param name="productRepository">The productRepository.</param>
	/// <param name="logger">The logger.</param>
	/// <param name="cache">The cache.</param>
	/// <param name="messageHub"></param>
	/// <param name="dbAppSettings"></param>
	/// <param name="serviceProvider"></param>
	public Product(COREAPPDREPOINTERFACESDOMAIN.IProductRepository productRepository,COREAPPINTERFACESDOMAIN.IMaster master	
							, ILogger<COREDOMAINDATAMODELSDOMAIN.Product> logger
							, IDistributedCache cache
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							, COREAPPINTERFACESDBAPPSETTINGS.IDbAppSettings dbAppSettings
							, IServiceProvider serviceProvider
							): base(logger, cache, messageHub, dbAppSettings, serviceProvider)
	{
		this._master = master;
		this._productRepository = productRepository;
		this._serviceProvider = serviceProvider;
	}

	#endregion
	
	#region Partial Methods	

	/// <summary>
	/// Get Products
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetProductPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetProductRequest request);

	/// <summary>
	/// Merge Products
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeProductPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeProductRequest request);

	/// <summary>
	/// Merge Products
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeProductPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeProductRequest request);

	/// <summary>
	/// Get Products
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetProductPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetProductRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get Products
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetProductResponse> GetProductsAsync(COREAPPDATAMODELSDOMAIN.GetProductRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Product), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetProductResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetProductPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetProductsAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.Products = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.Product>>();
			}
			else
			{
				response = await FetchAndCacheProductsAsync(request, cacheKey, EnableCaching("Product", nameof(GetProductsAsync)));
			}

			var postProcessorResponse = await GetProductPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetProductsAsync)}", nameof(Product), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Product), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetProductResponse> FetchAndCacheProductsAsync(COREAPPDATAMODELSDOMAIN.GetProductRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetProductResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._productRepository.GetProductsAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			if (request.LanguageUId != Guid.Empty && request.LanguageUId != COREDOMAINDATAMODELSDOMAINENUM.Language.EnglishUS)
			{
				response.Products = repositoryResponse.Products.Include(x => x.ProductLanguages.Where(y => y.LanguageUId == request.LanguageUId)).ToList();

				if (response.Products != null)
				{
					foreach (var product in response.Products)							
					{
						if (product.ProductLanguages != null && product.ProductLanguages.Any())
						{
							product.Name = product.ProductLanguages.First().Name;
							product.ProductLanguages = null;
						}
					}
				}
			}
			else
			{
				response.Products = await repositoryResponse.Products.ToListAsync();
			}

			//Decryption
			var decryptedResult = response.Products.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.Products = decryptedResult;
			}

			await SetCachedData(cacheKey, response.Products.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetProductRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetProductRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetProductRequest
		{
			MatchExpression = request.ProductUId != Guid.Empty ? (entity) => entity.ProductUId == request.ProductUId : null,
			CorrelationUId = request.CorrelationUId
		};

		return repositoryRequest;
	}


	/// <summary>
	/// Merge Products
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeProductResponse> MergeProductsAsync(COREAPPDATAMODELSDOMAIN.MergeProductRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Product), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeProductResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// Product.custom.cs MergeProductPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeProductPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("Product");

				if (isEncryptionRequired)
				{
					//Encryption
					request.Products = request.Products!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeProductRequest
			{
				Products = request.Products,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.Products);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._productRepository.MergeProductsAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _productRepository.SaveProductAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("Product", nameof(MergeProductsAsync));

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
			//// Product.custom.cs MergeProductPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeProductPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeProductsAsync)}", nameof(Product), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Product), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt Products data
	/// </summary>
	/// <param name="productUId"></param>
	/// <returns></returns>
	public async Task DecryptProductsAsync(Guid productUId)
	{
		Logger.LogInfo($"{nameof(DecryptProductsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Product), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptProductsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetProductRequest
			{
				ProductUId = productUId
			};

			Logger.LogInfo($"{nameof(DecryptProductsAsync)} - Processing data for decryption", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseProducts = await this.GetProductsAsync(request).ConfigureAwait(false);

			if (responseProducts.Products != null && responseProducts.Products.Any())
			{
				Logger.LogInfo($"{nameof(DecryptProductsAsync)} - Decrypted the data", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var product in responseProducts.Products)
				{
					product.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptProductsAsync)} - Set the item state to update the records back to entity", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeProductRequest
				{
					Products = responseProducts.Products.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeProductsAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptProductsAsync)} while saving data", nameof(Product), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptProductsAsync)} - Successfully processed data for decryption", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing products data for decryption in {nameof(DecryptProductsAsync)}", nameof(Product), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Product), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptProductsAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptProductsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetProductRequest request)
	{
		var cacheKey = $"Domain.Product:{request.ProductUId}:{request.LanguageUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeProductRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.Products != null)
		{
			var response = await _master.GetLanguagesAsync(new COREAPPDATAMODELSDOMAIN.GetLanguageRequest()).ConfigureAwait(false);
			var cacheKey = $"Domain.Product:{Guid.Empty}:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var language in response.Languages)
			{ 
				cacheKey = $"Domain.Product:{Guid.Empty}:{language.LanguageUId}";
				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
			}

			foreach (var product in request.Products)
			{						

				if (response != null)
				{ 
					foreach (var language in response.Languages)
					{
						cacheKey = $"Domain.Product:{product.ProductUId}:{language.LanguageUId}";
						await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
					}
				}
			}
		}
	}

	#endregion
}
