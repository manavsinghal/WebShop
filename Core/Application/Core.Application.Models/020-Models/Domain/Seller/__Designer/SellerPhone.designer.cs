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
/// Represents SellerPhone class.
/// </summary>
/// <remarks>
/// The SellerPhone class.
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
	/// Merge SellerPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeSellerPhonePostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeSellerPhoneRequest request);

	/// <summary>
	/// Get SellerPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetSellerPhonePostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetSellerPhoneRequest request);

	/// <summary>
	/// Merge SellerPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeSellerPhonePreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeSellerPhoneRequest request);

	/// <summary>
	/// Get SellerPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetSellerPhonePreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetSellerPhoneRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get SellerPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetSellerPhoneResponse> GetSellerPhonesAsync(COREAPPDATAMODELSDOMAIN.GetSellerPhoneRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Seller), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetSellerPhoneResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetSellerPhonePreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetSellerPhonesAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.SellerPhones = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.SellerPhone>>();
			}
			else
			{
				response = await FetchAndCacheSellerPhonesAsync(request, cacheKey, EnableCaching("SellerPhone", nameof(GetSellerPhonesAsync)));
			}

			var postProcessorResponse = await GetSellerPhonePostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetSellerPhonesAsync)}", nameof(Seller), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Seller), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetSellerPhoneResponse> FetchAndCacheSellerPhonesAsync(COREAPPDATAMODELSDOMAIN.GetSellerPhoneRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetSellerPhoneResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._sellerRepository.GetSellerPhonesAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.SellerPhones = await repositoryResponse.SellerPhones.ToListAsync();

			//Decryption
			var decryptedResult = response.SellerPhones.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.SellerPhones = decryptedResult;
			}

			await SetCachedData(cacheKey, response.SellerPhones.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetSellerPhoneRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetSellerPhoneRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetSellerPhoneRequest();

		if (request.SellerPhoneUId == Guid.Empty && request.SellerUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = null;
		}
		else if (request.SellerPhoneUId == Guid.Empty && request.SellerUId != Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.SellerUId == request.SellerUId;
		}
		else if (request.SellerPhoneUId != Guid.Empty && request.SellerUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.SellerPhoneUId == request.SellerPhoneUId;
		}
		else
		{
			repositoryRequest.MatchExpression = (entity) => entity.SellerPhoneUId == request.SellerPhoneUId && entity.SellerUId == request.SellerUId;
		}

		return repositoryRequest;
	}


	/// <summary>
	/// Merge SellerPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeSellerPhoneResponse> MergeSellerPhonesAsync(COREAPPDATAMODELSDOMAIN.MergeSellerPhoneRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Seller), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeSellerPhoneResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// SellerPhone.custom.cs MergeSellerPhonePreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeSellerPhonePreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("SellerPhone");

				if (isEncryptionRequired)
				{
					//Encryption
					request.SellerPhones = request.SellerPhones!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeSellerPhoneRequest
			{
				SellerPhones = request.SellerPhones,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.SellerPhones);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._sellerRepository.MergeSellerPhonesAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _sellerRepository.SaveSellerPhoneAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("SellerPhone", nameof(MergeSellerPhonesAsync));

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
			//// SellerPhone.custom.cs MergeSellerPhonePostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeSellerPhonePostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeSellerPhonesAsync)}", nameof(Seller), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Seller), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt SellerPhones data
	/// </summary>
	/// <param name="sellerPhoneUId"></param>
	/// <returns></returns>
	public async Task DecryptSellerPhonesAsync(Guid sellerPhoneUId)
	{
		Logger.LogInfo($"{nameof(DecryptSellerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Seller), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptSellerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetSellerPhoneRequest
			{
				SellerPhoneUId = sellerPhoneUId
			};

			Logger.LogInfo($"{nameof(DecryptSellerPhonesAsync)} - Processing data for decryption", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseSellerPhones = await this.GetSellerPhonesAsync(request).ConfigureAwait(false);

			if (responseSellerPhones.SellerPhones != null && responseSellerPhones.SellerPhones.Any())
			{
				Logger.LogInfo($"{nameof(DecryptSellerPhonesAsync)} - Decrypted the data", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var sellerPhone in responseSellerPhones.SellerPhones)
				{
					sellerPhone.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptSellerPhonesAsync)} - Set the item state to update the records back to entity", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeSellerPhoneRequest
				{
					SellerPhones = responseSellerPhones.SellerPhones.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeSellerPhonesAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptSellerPhonesAsync)} while saving data", nameof(Seller), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptSellerPhonesAsync)} - Successfully processed data for decryption", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing sellerPhones data for decryption in {nameof(DecryptSellerPhonesAsync)}", nameof(Seller), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Seller), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptSellerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptSellerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetSellerPhoneRequest request)
	{
		var cacheKey = $"Domain.SellerPhone:{request.SellerPhoneUId}:{request.SellerUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeSellerPhoneRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.SellerPhones != null)
		{	
			var cacheKey = $"Domain.SellerPhone:{Guid.Empty}:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var sellerPhone in request.SellerPhones)
			{
				cacheKey = $"Domain.SellerPhone:{sellerPhone.SellerPhoneUId}:{Guid.Empty}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

				cacheKey = $"Domain.SellerPhone:{Guid.Empty}:{sellerPhone.SellerUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
