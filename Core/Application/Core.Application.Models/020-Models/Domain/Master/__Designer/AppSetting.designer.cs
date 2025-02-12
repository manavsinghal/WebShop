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
/// Represents AppSetting class.
/// </summary>
/// <remarks>
/// The AppSetting class.
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
	/// Get AppSettings
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetAppSettingPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetAppSettingRequest request);

	/// <summary>
	/// Merge AppSettings
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeAppSettingPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeAppSettingRequest request);

	/// <summary>
	/// Get AppSettings
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetAppSettingPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetAppSettingRequest request);

	/// <summary>
	/// Merge AppSettings
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeAppSettingPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeAppSettingRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get AppSettings
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetAppSettingResponse> GetAppSettingsAsync(COREAPPDATAMODELSDOMAIN.GetAppSettingRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetAppSettingsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetAppSettingResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetAppSettingPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetAppSettingsAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.AppSettings = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.AppSetting>>();
			}
			else
			{
				response = await FetchAndCacheAppSettingsAsync(request, cacheKey, EnableCaching("AppSetting", nameof(GetAppSettingsAsync)));
			}

			var postProcessorResponse = await GetAppSettingPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetAppSettingsAsync)}", nameof(Master), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetAppSettingsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetAppSettingResponse> FetchAndCacheAppSettingsAsync(COREAPPDATAMODELSDOMAIN.GetAppSettingRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetAppSettingResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._masterRepository.GetAppSettingsAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.AppSettings = await repositoryResponse.AppSettings.ToListAsync();

			//Decryption
			var decryptedResult = response.AppSettings.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.AppSettings = decryptedResult;
			}

			await SetCachedData(cacheKey, response.AppSettings.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetAppSettingRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetAppSettingRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetAppSettingRequest
		{
			MatchExpression = request.AppSettingUId != Guid.Empty ? (entity) => entity.AppSettingUId == request.AppSettingUId : null,
			CorrelationUId = request.CorrelationUId
		};

		return repositoryRequest;
	}


	/// <summary>
	/// Merge AppSettings
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeAppSettingResponse> MergeAppSettingsAsync(COREAPPDATAMODELSDOMAIN.MergeAppSettingRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeAppSettingsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeAppSettingResponse response = new();

		var faults = new FaultCollection();

		try
		{
			//// If there are any customization required, Add custom code in the
			//// AppSetting.custom.cs MergeAppSettingPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeAppSettingPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("AppSetting");

				if (isEncryptionRequired)
				{
					//Encryption
					request.AppSettings = request.AppSettings!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeAppSettingRequest
			{
				AppSettings = request.AppSettings,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.AppSettings);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._masterRepository.MergeAppSettingsAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _masterRepository.SaveAppSettingAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("AppSetting", nameof(MergeAppSettingsAsync));

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
			//// AppSetting.custom.cs MergeAppSettingPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeAppSettingPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeAppSettingsAsync)}", nameof(Master), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeAppSettingsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt AppSettings data
	/// </summary>
	/// <param name="appSettingUId"></param>
	/// <returns></returns>
	public async Task DecryptAppSettingsAsync(Guid appSettingUId)
	{
		Logger.LogInfo($"{nameof(DecryptAppSettingsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Master), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptAppSettingsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetAppSettingRequest
			{
				AppSettingUId = appSettingUId
			};

			Logger.LogInfo($"{nameof(DecryptAppSettingsAsync)} - Processing data for decryption", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseAppSettings = await this.GetAppSettingsAsync(request).ConfigureAwait(false);

			if (responseAppSettings.AppSettings != null && responseAppSettings.AppSettings.Any())
			{
				Logger.LogInfo($"{nameof(DecryptAppSettingsAsync)} - Decrypted the data", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var appSetting in responseAppSettings.AppSettings)
				{
					appSetting.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptAppSettingsAsync)} - Set the item state to update the records back to entity", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeAppSettingRequest
				{
					AppSettings = responseAppSettings.AppSettings.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeAppSettingsAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptAppSettingsAsync)} while saving data", nameof(Master), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptAppSettingsAsync)} - Successfully processed data for decryption", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing appSettings data for decryption in {nameof(DecryptAppSettingsAsync)}", nameof(Master), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Master), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptAppSettingsAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptAppSettingsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetAppSettingRequest request)
	{
		var cacheKey = $"Domain.AppSetting:{request.AppSettingUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeAppSettingRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.AppSettings != null)
		{	
			var cacheKey = $"Domain.AppSetting:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var appSetting in request.AppSettings)
			{
				cacheKey = $"Domain.AppSetting:{appSetting.AppSettingUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
