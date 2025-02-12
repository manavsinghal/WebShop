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
/// Represents Seller class.
/// </summary>
/// <remarks>
/// The Seller class.
/// </remarks>
public partial class Seller : COREAPPMODELS.DomainModel<COREDOMAINDATAMODELSDOMAIN.Seller>, COREAPPINTERFACESDOMAIN.ISeller
{
	#region Fields

	private readonly COREAPPDREPOINTERFACESDOMAIN.ISellerRepository _sellerRepository;

	private readonly IServiceProvider _serviceProvider;

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the Seller class
	/// </summary>
	/// <param name="sellerRepository">The sellerRepository.</param>
	/// <param name="logger">The logger.</param>
	/// <param name="cache">The cache.</param>
	/// <param name="messageHub"></param>
	/// <param name="dbAppSettings"></param>
	/// <param name="serviceProvider"></param>
	public Seller(COREAPPDREPOINTERFACESDOMAIN.ISellerRepository sellerRepository	
							, ILogger<COREDOMAINDATAMODELSDOMAIN.Seller> logger
							, IDistributedCache cache
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							, COREAPPINTERFACESDBAPPSETTINGS.IDbAppSettings dbAppSettings
							, IServiceProvider serviceProvider
							): base(logger, cache, messageHub, dbAppSettings, serviceProvider)
	{
		this._sellerRepository = sellerRepository;
		this._serviceProvider = serviceProvider;
	}

	#endregion
	
	#region Partial Methods	

	/// <summary>
	/// Get Sellers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetSellerPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetSellerRequest request);

	/// <summary>
	/// Merge Sellers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeSellerPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeSellerRequest request);

	/// <summary>
	/// Get Sellers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetSellerPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetSellerRequest request);

	/// <summary>
	/// Merge Sellers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeSellerPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeSellerRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get Sellers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetSellerResponse> GetSellersAsync(COREAPPDATAMODELSDOMAIN.GetSellerRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Seller), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetSellerResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetSellerPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetSellersAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.Sellers = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.Seller>>();
			}
			else
			{
				response = await FetchAndCacheSellersAsync(request, cacheKey, EnableCaching("Seller", nameof(GetSellersAsync)));
			}

			var postProcessorResponse = await GetSellerPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetSellersAsync)}", nameof(Seller), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Seller), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetSellerResponse> FetchAndCacheSellersAsync(COREAPPDATAMODELSDOMAIN.GetSellerRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetSellerResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._sellerRepository.GetSellersAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.Sellers = await repositoryResponse.Sellers.ToListAsync();

			//Decryption
			var decryptedResult = response.Sellers.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.Sellers = decryptedResult;
			}

			await SetCachedData(cacheKey, response.Sellers.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetSellerRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetSellerRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetSellerRequest
		{
			MatchExpression = request.SellerUId != Guid.Empty ? (entity) => entity.SellerUId == request.SellerUId : null,
			CorrelationUId = request.CorrelationUId
		};

		return repositoryRequest;
	}


	/// <summary>
	/// Merge Sellers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeSellerResponse> MergeSellersAsync(COREAPPDATAMODELSDOMAIN.MergeSellerRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Seller), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeSellerResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// Seller.custom.cs MergeSellerPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeSellerPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("Seller");

				if (isEncryptionRequired)
				{
					//Encryption
					request.Sellers = request.Sellers!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeSellerRequest
			{
				Sellers = request.Sellers,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.Sellers);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._sellerRepository.MergeSellersAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _sellerRepository.SaveSellerAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("Seller", nameof(MergeSellersAsync));

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
			//// Seller.custom.cs MergeSellerPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeSellerPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeSellersAsync)}", nameof(Seller), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Seller), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt Sellers data
	/// </summary>
	/// <param name="sellerUId"></param>
	/// <returns></returns>
	public async Task DecryptSellersAsync(Guid sellerUId)
	{
		Logger.LogInfo($"{nameof(DecryptSellersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Seller), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptSellersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetSellerRequest
			{
				SellerUId = sellerUId
			};

			Logger.LogInfo($"{nameof(DecryptSellersAsync)} - Processing data for decryption", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseSellers = await this.GetSellersAsync(request).ConfigureAwait(false);

			if (responseSellers.Sellers != null && responseSellers.Sellers.Any())
			{
				Logger.LogInfo($"{nameof(DecryptSellersAsync)} - Decrypted the data", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var seller in responseSellers.Sellers)
				{
					seller.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptSellersAsync)} - Set the item state to update the records back to entity", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeSellerRequest
				{
					Sellers = responseSellers.Sellers.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeSellersAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptSellersAsync)} while saving data", nameof(Seller), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptSellersAsync)} - Successfully processed data for decryption", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing sellers data for decryption in {nameof(DecryptSellersAsync)}", nameof(Seller), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Seller), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptSellersAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptSellersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetSellerRequest request)
	{
		var cacheKey = $"Domain.Seller:{request.SellerUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeSellerRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.Sellers != null)
		{	
			var cacheKey = $"Domain.Seller:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var seller in request.Sellers)
			{
				cacheKey = $"Domain.Seller:{seller.SellerUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
