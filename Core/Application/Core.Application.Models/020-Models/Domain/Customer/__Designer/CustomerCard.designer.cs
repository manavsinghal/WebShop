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
/// Represents CustomerCard class.
/// </summary>
/// <remarks>
/// The CustomerCard class.
/// </remarks>
public partial class Customer : COREAPPMODELS.DomainModel<COREDOMAINDATAMODELSDOMAIN.Customer>, COREAPPINTERFACESDOMAIN.ICustomer
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	#endregion
	
	#region Partial Methods	

	/// <summary>
	/// Merge CustomerCards
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeCustomerCardPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeCustomerCardRequest request);

	/// <summary>
	/// Get CustomerCards
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetCustomerCardPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetCustomerCardRequest request);

	/// <summary>
	/// Merge CustomerCards
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeCustomerCardPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeCustomerCardRequest request);

	/// <summary>
	/// Get CustomerCards
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetCustomerCardPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetCustomerCardRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get CustomerCards
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetCustomerCardResponse> GetCustomerCardsAsync(COREAPPDATAMODELSDOMAIN.GetCustomerCardRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerCardsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Customer), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetCustomerCardResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetCustomerCardPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetCustomerCardsAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.CustomerCards = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.CustomerCard>>();
			}
			else
			{
				response = await FetchAndCacheCustomerCardsAsync(request, cacheKey, EnableCaching("CustomerCard", nameof(GetCustomerCardsAsync)));
			}

			var postProcessorResponse = await GetCustomerCardPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetCustomerCardsAsync)}", nameof(Customer), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerCardsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Customer), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetCustomerCardResponse> FetchAndCacheCustomerCardsAsync(COREAPPDATAMODELSDOMAIN.GetCustomerCardRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetCustomerCardResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._customerRepository.GetCustomerCardsAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.CustomerCards = await repositoryResponse.CustomerCards.ToListAsync();

			//Decryption
			var decryptedResult = response.CustomerCards.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.CustomerCards = decryptedResult;
			}

			await SetCachedData(cacheKey, response.CustomerCards.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetCustomerCardRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetCustomerCardRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetCustomerCardRequest();

		if (request.CustomerCardUId == Guid.Empty && request.CustomerUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = null;
		}
		else if (request.CustomerCardUId == Guid.Empty && request.CustomerUId != Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.CustomerUId == request.CustomerUId;
		}
		else if (request.CustomerCardUId != Guid.Empty && request.CustomerUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.CustomerCardUId == request.CustomerCardUId;
		}
		else
		{
			repositoryRequest.MatchExpression = (entity) => entity.CustomerCardUId == request.CustomerCardUId && entity.CustomerUId == request.CustomerUId;
		}

		return repositoryRequest;
	}


	/// <summary>
	/// Merge CustomerCards
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeCustomerCardResponse> MergeCustomerCardsAsync(COREAPPDATAMODELSDOMAIN.MergeCustomerCardRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerCardsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Customer), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeCustomerCardResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// CustomerCard.custom.cs MergeCustomerCardPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeCustomerCardPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("CustomerCard");

				if (isEncryptionRequired)
				{
					//Encryption
					request.CustomerCards = request.CustomerCards!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeCustomerCardRequest
			{
				CustomerCards = request.CustomerCards,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.CustomerCards);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._customerRepository.MergeCustomerCardsAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _customerRepository.SaveCustomerCardAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("CustomerCard", nameof(MergeCustomerCardsAsync));

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
			//// CustomerCard.custom.cs MergeCustomerCardPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeCustomerCardPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeCustomerCardsAsync)}", nameof(Customer), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerCardsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Customer), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt CustomerCards data
	/// </summary>
	/// <param name="customerCardUId"></param>
	/// <returns></returns>
	public async Task DecryptCustomerCardsAsync(Guid customerCardUId)
	{
		Logger.LogInfo($"{nameof(DecryptCustomerCardsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Customer), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptCustomerCardsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetCustomerCardRequest
			{
				CustomerCardUId = customerCardUId
			};

			Logger.LogInfo($"{nameof(DecryptCustomerCardsAsync)} - Processing data for decryption", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseCustomerCards = await this.GetCustomerCardsAsync(request).ConfigureAwait(false);

			if (responseCustomerCards.CustomerCards != null && responseCustomerCards.CustomerCards.Any())
			{
				Logger.LogInfo($"{nameof(DecryptCustomerCardsAsync)} - Decrypted the data", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var customerCard in responseCustomerCards.CustomerCards)
				{
					customerCard.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptCustomerCardsAsync)} - Set the item state to update the records back to entity", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeCustomerCardRequest
				{
					CustomerCards = responseCustomerCards.CustomerCards.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeCustomerCardsAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptCustomerCardsAsync)} while saving data", nameof(Customer), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptCustomerCardsAsync)} - Successfully processed data for decryption", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing customerCards data for decryption in {nameof(DecryptCustomerCardsAsync)}", nameof(Customer), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Customer), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptCustomerCardsAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptCustomerCardsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetCustomerCardRequest request)
	{
		var cacheKey = $"Domain.CustomerCard:{request.CustomerCardUId}:{request.CustomerUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeCustomerCardRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.CustomerCards != null)
		{	
			var cacheKey = $"Domain.CustomerCard:{Guid.Empty}:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var customerCard in request.CustomerCards)
			{
				cacheKey = $"Domain.CustomerCard:{customerCard.CustomerCardUId}:{Guid.Empty}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

				cacheKey = $"Domain.CustomerCard:{Guid.Empty}:{customerCard.CustomerUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
