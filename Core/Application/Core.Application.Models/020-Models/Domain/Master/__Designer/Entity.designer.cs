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
/// Represents Entity class.
/// </summary>
/// <remarks>
/// The Entity class.
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
	/// Get Entities
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetEntityPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetEntityRequest request);

	/// <summary>
	/// Get Entities
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetEntityPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetEntityRequest request);

	/// <summary>
	/// Merge Entities
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeEntityPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeEntityRequest request);

	/// <summary>
	/// Merge Entities
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeEntityPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeEntityRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get Entities
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetEntityResponse> GetEntitiesAsync(COREAPPDATAMODELSDOMAIN.GetEntityRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetEntitiesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetEntityResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetEntityPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetEntitiesAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.Entities = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.Entity>>();
			}
			else
			{
				response = await FetchAndCacheEntitiesAsync(request, cacheKey, EnableCaching("Entity", nameof(GetEntitiesAsync)));
			}

			var postProcessorResponse = await GetEntityPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetEntitiesAsync)}", nameof(Master), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetEntitiesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetEntityResponse> FetchAndCacheEntitiesAsync(COREAPPDATAMODELSDOMAIN.GetEntityRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetEntityResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._masterRepository.GetEntitiesAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.Entities = await repositoryResponse.Entities.ToListAsync();

			//Decryption
			var decryptedResult = response.Entities.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.Entities = decryptedResult;
			}

			await SetCachedData(cacheKey, response.Entities.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetEntityRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetEntityRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetEntityRequest
		{
			MatchExpression = request.EntityUId != Guid.Empty ? (entity) => entity.EntityUId == request.EntityUId : null,
			CorrelationUId = request.CorrelationUId
		};

		return repositoryRequest;
	}


	/// <summary>
	/// Merge Entities
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeEntityResponse> MergeEntitiesAsync(COREAPPDATAMODELSDOMAIN.MergeEntityRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeEntitiesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeEntityResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// Entity.custom.cs MergeEntityPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeEntityPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("Entity");

				if (isEncryptionRequired)
				{
					//Encryption
					request.Entities = request.Entities!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeEntityRequest
			{
				Entities = request.Entities,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.Entities);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._masterRepository.MergeEntitiesAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _masterRepository.SaveEntityAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("Entity", nameof(MergeEntitiesAsync));

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
			//// Entity.custom.cs MergeEntityPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeEntityPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeEntitiesAsync)}", nameof(Master), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeEntitiesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Master), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt Entities data
	/// </summary>
	/// <param name="entityUId"></param>
	/// <returns></returns>
	public async Task DecryptEntitiesAsync(Guid entityUId)
	{
		Logger.LogInfo($"{nameof(DecryptEntitiesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Master), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptEntitiesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetEntityRequest
			{
				EntityUId = entityUId
			};

			Logger.LogInfo($"{nameof(DecryptEntitiesAsync)} - Processing data for decryption", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseEntities = await this.GetEntitiesAsync(request).ConfigureAwait(false);

			if (responseEntities.Entities != null && responseEntities.Entities.Any())
			{
				Logger.LogInfo($"{nameof(DecryptEntitiesAsync)} - Decrypted the data", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var entity in responseEntities.Entities)
				{
					entity.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptEntitiesAsync)} - Set the item state to update the records back to entity", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeEntityRequest
				{
					Entities = responseEntities.Entities.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeEntitiesAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptEntitiesAsync)} while saving data", nameof(Master), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptEntitiesAsync)} - Successfully processed data for decryption", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing entities data for decryption in {nameof(DecryptEntitiesAsync)}", nameof(Master), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Master), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptEntitiesAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptEntitiesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Master), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetEntityRequest request)
	{
		var cacheKey = $"Domain.Entity:{request.EntityUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeEntityRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.Entities != null)
		{	
			var cacheKey = $"Domain.Entity:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var entity in request.Entities)
			{
				cacheKey = $"Domain.Entity:{entity.EntityUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
