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
/// Represents ShoppingCart class.
/// </summary>
/// <remarks>
/// The ShoppingCart class.
/// </remarks>
public partial class ShoppingCart : COREAPPMODELS.DomainModel<COREDOMAINDATAMODELSDOMAIN.ShoppingCart>, COREAPPINTERFACESDOMAIN.IShoppingCart
{
	#region Fields

	private readonly IServiceProvider _serviceProvider;

	private readonly COREAPPDREPOINTERFACESDOMAIN.IShoppingCartRepository _shoppingCartRepository;

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the ShoppingCart class
	/// </summary>
	/// <param name="shoppingCartRepository">The shoppingCartRepository.</param>
	/// <param name="logger">The logger.</param>
	/// <param name="cache">The cache.</param>
	/// <param name="messageHub"></param>
	/// <param name="dbAppSettings"></param>
	/// <param name="serviceProvider"></param>
	public ShoppingCart(COREAPPDREPOINTERFACESDOMAIN.IShoppingCartRepository shoppingCartRepository	
							, ILogger<COREDOMAINDATAMODELSDOMAIN.ShoppingCart> logger
							, IDistributedCache cache
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							, COREAPPINTERFACESDBAPPSETTINGS.IDbAppSettings dbAppSettings
							, IServiceProvider serviceProvider
							): base(logger, cache, messageHub, dbAppSettings, serviceProvider)
	{
		this._serviceProvider = serviceProvider;
		this._shoppingCartRepository = shoppingCartRepository;
	}

	#endregion
	
	#region Partial Methods	

	/// <summary>
	/// Get ShoppingCarts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetShoppingCartPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetShoppingCartRequest request);

	/// <summary>
	/// Merge ShoppingCarts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeShoppingCartPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeShoppingCartRequest request);

	/// <summary>
	/// Get ShoppingCarts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetShoppingCartPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetShoppingCartRequest request);

	/// <summary>
	/// Merge ShoppingCarts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeShoppingCartPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeShoppingCartRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get ShoppingCarts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetShoppingCartResponse> GetShoppingCartsAsync(COREAPPDATAMODELSDOMAIN.GetShoppingCartRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetShoppingCartsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShoppingCart), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetShoppingCartResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetShoppingCartPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetShoppingCartsAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.ShoppingCarts = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.ShoppingCart>>();
			}
			else
			{
				response = await FetchAndCacheShoppingCartsAsync(request, cacheKey, EnableCaching("ShoppingCart", nameof(GetShoppingCartsAsync)));
			}

			var postProcessorResponse = await GetShoppingCartPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetShoppingCartsAsync)}", nameof(ShoppingCart), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShoppingCartsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShoppingCart), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetShoppingCartResponse> FetchAndCacheShoppingCartsAsync(COREAPPDATAMODELSDOMAIN.GetShoppingCartRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetShoppingCartResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._shoppingCartRepository.GetShoppingCartsAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.ShoppingCarts = await repositoryResponse.ShoppingCarts.ToListAsync();

			//Decryption
			var decryptedResult = response.ShoppingCarts.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.ShoppingCarts = decryptedResult;
			}

			await SetCachedData(cacheKey, response.ShoppingCarts.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetShoppingCartRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetShoppingCartRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetShoppingCartRequest
		{
			MatchExpression = request.ShoppingCartUId != Guid.Empty ? (entity) => entity.ShoppingCartUId == request.ShoppingCartUId : null,
			CorrelationUId = request.CorrelationUId
		};

		return repositoryRequest;
	}


	/// <summary>
	/// Merge ShoppingCarts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeShoppingCartResponse> MergeShoppingCartsAsync(COREAPPDATAMODELSDOMAIN.MergeShoppingCartRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShoppingCartsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShoppingCart), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeShoppingCartResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// ShoppingCart.custom.cs MergeShoppingCartPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeShoppingCartPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("ShoppingCart");

				if (isEncryptionRequired)
				{
					//Encryption
					request.ShoppingCarts = request.ShoppingCarts!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeShoppingCartRequest
			{
				ShoppingCarts = request.ShoppingCarts,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.ShoppingCarts);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._shoppingCartRepository.MergeShoppingCartsAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _shoppingCartRepository.SaveShoppingCartAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("ShoppingCart", nameof(MergeShoppingCartsAsync));

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
			//// ShoppingCart.custom.cs MergeShoppingCartPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeShoppingCartPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeShoppingCartsAsync)}", nameof(ShoppingCart), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShoppingCartsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShoppingCart), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt ShoppingCarts data
	/// </summary>
	/// <param name="shoppingCartUId"></param>
	/// <returns></returns>
	public async Task DecryptShoppingCartsAsync(Guid shoppingCartUId)
	{
		Logger.LogInfo($"{nameof(DecryptShoppingCartsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShoppingCart), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(ShoppingCart), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptShoppingCartsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetShoppingCartRequest
			{
				ShoppingCartUId = shoppingCartUId
			};

			Logger.LogInfo($"{nameof(DecryptShoppingCartsAsync)} - Processing data for decryption", nameof(ShoppingCart), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseShoppingCarts = await this.GetShoppingCartsAsync(request).ConfigureAwait(false);

			if (responseShoppingCarts.ShoppingCarts != null && responseShoppingCarts.ShoppingCarts.Any())
			{
				Logger.LogInfo($"{nameof(DecryptShoppingCartsAsync)} - Decrypted the data", nameof(ShoppingCart), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var shoppingCart in responseShoppingCarts.ShoppingCarts)
				{
					shoppingCart.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptShoppingCartsAsync)} - Set the item state to update the records back to entity", nameof(ShoppingCart), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeShoppingCartRequest
				{
					ShoppingCarts = responseShoppingCarts.ShoppingCarts.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeShoppingCartsAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptShoppingCartsAsync)} while saving data", nameof(ShoppingCart), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptShoppingCartsAsync)} - Successfully processed data for decryption", nameof(ShoppingCart), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing shoppingCarts data for decryption in {nameof(DecryptShoppingCartsAsync)}", nameof(ShoppingCart), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(ShoppingCart), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptShoppingCartsAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptShoppingCartsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShoppingCart), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetShoppingCartRequest request)
	{
		var cacheKey = $"Domain.ShoppingCart:{request.ShoppingCartUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeShoppingCartRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.ShoppingCarts != null)
		{	
			var cacheKey = $"Domain.ShoppingCart:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var shoppingCart in request.ShoppingCarts)
			{
				cacheKey = $"Domain.ShoppingCart:{shoppingCart.ShoppingCartUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
