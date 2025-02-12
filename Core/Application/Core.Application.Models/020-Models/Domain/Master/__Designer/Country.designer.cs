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
/// Represents Country class.
/// </summary>
/// <remarks>
/// The Country class.
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
	/// Merge Countries
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeCountryPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeCountryRequest request);

	/// <summary>
	/// Get Countries
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetCountryPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetCountryRequest request);

	/// <summary>
	/// Get Countries
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetCountryPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetCountryRequest request);

	/// <summary>
	/// Merge Countries
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeCountryPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeCountryRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get Countries
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetCountryResponse> GetCountriesAsync(COREAPPDATAMODELSDOMAIN.GetCountryRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetCountriesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetCountryResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetCountryPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetCountriesAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.Countries = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.Country>>();
			}
			else
			{
				response = await FetchAndCacheCountriesAsync(request, cacheKey, EnableCaching("Country", nameof(GetCountriesAsync)));
			}

			var postProcessorResponse = await GetCountryPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetCountriesAsync)}", nameof(Master), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCountriesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetCountryResponse> FetchAndCacheCountriesAsync(COREAPPDATAMODELSDOMAIN.GetCountryRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetCountryResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._masterRepository.GetCountriesAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			if (request.LanguageUId != Guid.Empty && request.LanguageUId != COREDOMAINDATAMODELSDOMAINENUM.Language.EnglishUS)
			{
				response.Countries = repositoryResponse.Countries.Include(x => x.CountryLanguages.Where(y => y.LanguageUId == request.LanguageUId)).ToList();

				if (response.Countries != null)
				{
					foreach (var country in response.Countries)							
					{
						if (country.CountryLanguages != null && country.CountryLanguages.Any())
						{
							country.Name = country.CountryLanguages.First().Name;
							country.CountryLanguages = null;
						}
					}
				}
			}
			else
			{
				response.Countries = await repositoryResponse.Countries.ToListAsync();
			}

			//Decryption
			var decryptedResult = response.Countries.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.Countries = decryptedResult;
			}

			await SetCachedData(cacheKey, response.Countries.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetCountryRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetCountryRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetCountryRequest
		{
			MatchExpression = request.CountryUId != Guid.Empty ? (entity) => entity.CountryUId == request.CountryUId : null,
			CorrelationUId = request.CorrelationUId
		};

		return repositoryRequest;
	}


	/// <summary>
	/// Merge Countries
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeCountryResponse> MergeCountriesAsync(COREAPPDATAMODELSDOMAIN.MergeCountryRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCountriesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeCountryResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// Country.custom.cs MergeCountryPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeCountryPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("Country");

				if (isEncryptionRequired)
				{
					//Encryption
					request.Countries = request.Countries!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeCountryRequest
			{
				Countries = request.Countries,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.Countries);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._masterRepository.MergeCountriesAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _masterRepository.SaveCountryAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("Country", nameof(MergeCountriesAsync));

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
			//// Country.custom.cs MergeCountryPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeCountryPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeCountriesAsync)}", nameof(Master), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCountriesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt Countries data
	/// </summary>
	/// <param name="countryUId"></param>
	/// <returns></returns>
	public async Task DecryptCountriesAsync(Guid countryUId)
	{
		Logger.LogInfo($"{nameof(DecryptCountriesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Master), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptCountriesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetCountryRequest
			{
				CountryUId = countryUId
			};

			Logger.LogInfo($"{nameof(DecryptCountriesAsync)} - Processing data for decryption", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseCountries = await this.GetCountriesAsync(request).ConfigureAwait(false);

			if (responseCountries.Countries != null && responseCountries.Countries.Any())
			{
				Logger.LogInfo($"{nameof(DecryptCountriesAsync)} - Decrypted the data", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var country in responseCountries.Countries)
				{
					country.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptCountriesAsync)} - Set the item state to update the records back to entity", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeCountryRequest
				{
					Countries = responseCountries.Countries.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeCountriesAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptCountriesAsync)} while saving data", nameof(Master), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptCountriesAsync)} - Successfully processed data for decryption", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing countries data for decryption in {nameof(DecryptCountriesAsync)}", nameof(Master), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Master), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptCountriesAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptCountriesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetCountryRequest request)
	{
		var cacheKey = $"Domain.Country:{request.CountryUId}:{request.LanguageUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeCountryRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.Countries != null)
		{
			var response = await this.GetLanguagesAsync(new COREAPPDATAMODELSDOMAIN.GetLanguageRequest()).ConfigureAwait(false);
			var cacheKey = $"Domain.Country:{Guid.Empty}:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var language in response.Languages)
			{ 
				cacheKey = $"Domain.Country:{Guid.Empty}:{language.LanguageUId}";
				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
			}

			foreach (var country in request.Countries)
			{						

				if (response != null)
				{ 
					foreach (var language in response.Languages)
					{
						cacheKey = $"Domain.Country:{country.CountryUId}:{language.LanguageUId}";
						await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
					}
				}
			}
		}
	}

	#endregion
}
