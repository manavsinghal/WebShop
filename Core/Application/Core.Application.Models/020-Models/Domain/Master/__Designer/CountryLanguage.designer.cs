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
/// Represents CountryLanguage class.
/// </summary>
/// <remarks>
/// The CountryLanguage class.
/// </remarks>
public partial class Master : COREAPPMODELS.DomainModel<COREDOMAINDATAMODELSDOMAIN.MasterList>, COREAPPINTERFACESDOMAIN.IMaster
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	#endregion
	
	#region Partial Methods	

	/// <summary>
	/// Get CountryLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetCountryLanguagePostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetCountryLanguageRequest request);

	/// <summary>
	/// Get CountryLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetCountryLanguagePreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetCountryLanguageRequest request);

	/// <summary>
	/// Merge CountryLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeCountryLanguagePostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeCountryLanguageRequest request);

	/// <summary>
	/// Merge CountryLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeCountryLanguagePreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeCountryLanguageRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get CountryLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetCountryLanguageResponse> GetCountryLanguagesAsync(COREAPPDATAMODELSDOMAIN.GetCountryLanguageRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetCountryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetCountryLanguageResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetCountryLanguagePreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetCountryLanguagesAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.CountryLanguages = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.CountryLanguage>>();
			}
			else
			{
				response = await FetchAndCacheCountryLanguagesAsync(request, cacheKey, EnableCaching("CountryLanguage", nameof(GetCountryLanguagesAsync)));
			}

			var postProcessorResponse = await GetCountryLanguagePostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetCountryLanguagesAsync)}", nameof(Master), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCountryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetCountryLanguageResponse> FetchAndCacheCountryLanguagesAsync(COREAPPDATAMODELSDOMAIN.GetCountryLanguageRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetCountryLanguageResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._masterRepository.GetCountryLanguagesAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.CountryLanguages = await repositoryResponse.CountryLanguages.ToListAsync();

			//Decryption
			var decryptedResult = response.CountryLanguages.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.CountryLanguages = decryptedResult;
			}

			await SetCachedData(cacheKey, response.CountryLanguages.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetCountryLanguageRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetCountryLanguageRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetCountryLanguageRequest
		{
			MatchExpression = request.CountryLanguageUId != Guid.Empty ? (entity) => entity.CountryLanguageUId == request.CountryLanguageUId : null,
			CorrelationUId = request.CorrelationUId
		};

		return repositoryRequest;
	}


	/// <summary>
	/// Merge CountryLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeCountryLanguageResponse> MergeCountryLanguagesAsync(COREAPPDATAMODELSDOMAIN.MergeCountryLanguageRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCountryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeCountryLanguageResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// CountryLanguage.custom.cs MergeCountryLanguagePreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeCountryLanguagePreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("CountryLanguage");

				if (isEncryptionRequired)
				{
					//Encryption
					request.CountryLanguages = request.CountryLanguages!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeCountryLanguageRequest
			{
				CountryLanguages = request.CountryLanguages,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.CountryLanguages);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._masterRepository.MergeCountryLanguagesAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _masterRepository.SaveCountryLanguageAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("CountryLanguage", nameof(MergeCountryLanguagesAsync));

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
			//// CountryLanguage.custom.cs MergeCountryLanguagePostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeCountryLanguagePostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeCountryLanguagesAsync)}", nameof(Master), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCountryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt CountryLanguages data
	/// </summary>
	/// <param name="countryLanguageUId"></param>
	/// <returns></returns>
	public async Task DecryptCountryLanguagesAsync(Guid countryLanguageUId)
	{
		Logger.LogInfo($"{nameof(DecryptCountryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Master), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptCountryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetCountryLanguageRequest
			{
				CountryLanguageUId = countryLanguageUId
			};

			Logger.LogInfo($"{nameof(DecryptCountryLanguagesAsync)} - Processing data for decryption", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseCountryLanguages = await this.GetCountryLanguagesAsync(request).ConfigureAwait(false);

			if (responseCountryLanguages.CountryLanguages != null && responseCountryLanguages.CountryLanguages.Any())
			{
				Logger.LogInfo($"{nameof(DecryptCountryLanguagesAsync)} - Decrypted the data", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var countryLanguage in responseCountryLanguages.CountryLanguages)
				{
					countryLanguage.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptCountryLanguagesAsync)} - Set the item state to update the records back to entity", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeCountryLanguageRequest
				{
					CountryLanguages = responseCountryLanguages.CountryLanguages.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeCountryLanguagesAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptCountryLanguagesAsync)} while saving data", nameof(Master), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptCountryLanguagesAsync)} - Successfully processed data for decryption", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing countryLanguages data for decryption in {nameof(DecryptCountryLanguagesAsync)}", nameof(Master), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Master), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptCountryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptCountryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetCountryLanguageRequest request)
	{
		var cacheKey = $"Domain.CountryLanguage:{request.CountryLanguageUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeCountryLanguageRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.CountryLanguages != null)
		{	
			var cacheKey = $"Domain.CountryLanguage:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var countryLanguage in request.CountryLanguages)
			{
				cacheKey = $"Domain.CountryLanguage:{countryLanguage.CountryLanguageUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
