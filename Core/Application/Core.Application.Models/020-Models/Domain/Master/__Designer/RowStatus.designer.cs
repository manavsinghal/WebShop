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
/// Represents RowStatus class.
/// </summary>
/// <remarks>
/// The RowStatus class.
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
	/// Merge RowStatuses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeRowStatusPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeRowStatusRequest request);

	/// <summary>
	/// Get RowStatuses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetRowStatusPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetRowStatusRequest request);

	/// <summary>
	/// Merge RowStatuses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeRowStatusPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeRowStatusRequest request);

	/// <summary>
	/// Get RowStatuses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetRowStatusPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetRowStatusRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get RowStatuses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetRowStatusResponse> GetRowStatusesAsync(COREAPPDATAMODELSDOMAIN.GetRowStatusRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetRowStatusesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetRowStatusResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetRowStatusPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetRowStatusesAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.RowStatuses = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.RowStatus>>();
			}
			else
			{
				response = await FetchAndCacheRowStatusesAsync(request, cacheKey, EnableCaching("RowStatus", nameof(GetRowStatusesAsync)));
			}

			var postProcessorResponse = await GetRowStatusPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetRowStatusesAsync)}", nameof(Master), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetRowStatusesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetRowStatusResponse> FetchAndCacheRowStatusesAsync(COREAPPDATAMODELSDOMAIN.GetRowStatusRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetRowStatusResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._masterRepository.GetRowStatusesAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.RowStatuses = await repositoryResponse.RowStatuses.ToListAsync();

			//Decryption
			var decryptedResult = response.RowStatuses.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.RowStatuses = decryptedResult;
			}

			await SetCachedData(cacheKey, response.RowStatuses.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetRowStatusRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetRowStatusRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetRowStatusRequest
		{
			MatchExpression = request.RowStatusUId != Guid.Empty ? (entity) => entity.RowStatusUId == request.RowStatusUId : null,
			CorrelationUId = request.CorrelationUId
		};

		return repositoryRequest;
	}


	/// <summary>
	/// Merge RowStatuses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeRowStatusResponse> MergeRowStatusesAsync(COREAPPDATAMODELSDOMAIN.MergeRowStatusRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeRowStatusesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeRowStatusResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// RowStatus.custom.cs MergeRowStatusPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeRowStatusPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("RowStatus");

				if (isEncryptionRequired)
				{
					//Encryption
					request.RowStatuses = request.RowStatuses!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeRowStatusRequest
			{
				RowStatuses = request.RowStatuses,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.RowStatuses);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._masterRepository.MergeRowStatusesAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _masterRepository.SaveRowStatusAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("RowStatus", nameof(MergeRowStatusesAsync));

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
			//// RowStatus.custom.cs MergeRowStatusPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeRowStatusPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeRowStatusesAsync)}", nameof(Master), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeRowStatusesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt RowStatuses data
	/// </summary>
	/// <param name="rowStatusUId"></param>
	/// <returns></returns>
	public async Task DecryptRowStatusesAsync(Guid rowStatusUId)
	{
		Logger.LogInfo($"{nameof(DecryptRowStatusesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Master), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptRowStatusesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetRowStatusRequest
			{
				RowStatusUId = rowStatusUId
			};

			Logger.LogInfo($"{nameof(DecryptRowStatusesAsync)} - Processing data for decryption", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseRowStatuses = await this.GetRowStatusesAsync(request).ConfigureAwait(false);

			if (responseRowStatuses.RowStatuses != null && responseRowStatuses.RowStatuses.Any())
			{
				Logger.LogInfo($"{nameof(DecryptRowStatusesAsync)} - Decrypted the data", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var rowStatus in responseRowStatuses.RowStatuses)
				{
					rowStatus.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptRowStatusesAsync)} - Set the item state to update the records back to entity", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeRowStatusRequest
				{
					RowStatuses = responseRowStatuses.RowStatuses.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeRowStatusesAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptRowStatusesAsync)} while saving data", nameof(Master), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptRowStatusesAsync)} - Successfully processed data for decryption", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing rowStatuses data for decryption in {nameof(DecryptRowStatusesAsync)}", nameof(Master), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Master), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptRowStatusesAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptRowStatusesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetRowStatusRequest request)
	{
		var cacheKey = $"Domain.RowStatus:{request.RowStatusUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeRowStatusRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.RowStatuses != null)
		{	
			var cacheKey = $"Domain.RowStatus:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var rowStatus in request.RowStatuses)
			{
				cacheKey = $"Domain.RowStatus:{rowStatus.RowStatusUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
