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
/// Represents ShipperPhone class.
/// </summary>
/// <remarks>
/// The ShipperPhone class.
/// </remarks>
public partial class Shipper : COREAPPMODELS.DomainModel<COREDOMAINDATAMODELSDOMAIN.Shipper>, COREAPPINTERFACESDOMAIN.IShipper
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	#endregion
	
	#region Partial Methods	

	/// <summary>
	/// Merge ShipperPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeShipperPhonePostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeShipperPhoneRequest request);

	/// <summary>
	/// Merge ShipperPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeShipperPhonePreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeShipperPhoneRequest request);

	/// <summary>
	/// Get ShipperPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetShipperPhonePreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetShipperPhoneRequest request);

	/// <summary>
	/// Get ShipperPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetShipperPhonePostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetShipperPhoneRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get ShipperPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetShipperPhoneResponse> GetShipperPhonesAsync(COREAPPDATAMODELSDOMAIN.GetShipperPhoneRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetShipperPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Shipper), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetShipperPhoneResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetShipperPhonePreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetShipperPhonesAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.ShipperPhones = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.ShipperPhone>>();
			}
			else
			{
				response = await FetchAndCacheShipperPhonesAsync(request, cacheKey, EnableCaching("ShipperPhone", nameof(GetShipperPhonesAsync)));
			}

			var postProcessorResponse = await GetShipperPhonePostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetShipperPhonesAsync)}", nameof(Shipper), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShipperPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Shipper), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetShipperPhoneResponse> FetchAndCacheShipperPhonesAsync(COREAPPDATAMODELSDOMAIN.GetShipperPhoneRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetShipperPhoneResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._shipperRepository.GetShipperPhonesAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.ShipperPhones = await repositoryResponse.ShipperPhones.ToListAsync();

			//Decryption
			var decryptedResult = response.ShipperPhones.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.ShipperPhones = decryptedResult;
			}

			await SetCachedData(cacheKey, response.ShipperPhones.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetShipperPhoneRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetShipperPhoneRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetShipperPhoneRequest();

		if (request.ShipperPhoneUId == Guid.Empty && request.ShipperUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = null;
		}
		else if (request.ShipperPhoneUId == Guid.Empty && request.ShipperUId != Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.ShipperUId == request.ShipperUId;
		}
		else if (request.ShipperPhoneUId != Guid.Empty && request.ShipperUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.ShipperPhoneUId == request.ShipperPhoneUId;
		}
		else
		{
			repositoryRequest.MatchExpression = (entity) => entity.ShipperPhoneUId == request.ShipperPhoneUId && entity.ShipperUId == request.ShipperUId;
		}

		return repositoryRequest;
	}


	/// <summary>
	/// Merge ShipperPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeShipperPhoneResponse> MergeShipperPhonesAsync(COREAPPDATAMODELSDOMAIN.MergeShipperPhoneRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShipperPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Shipper), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeShipperPhoneResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// ShipperPhone.custom.cs MergeShipperPhonePreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeShipperPhonePreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("ShipperPhone");

				if (isEncryptionRequired)
				{
					//Encryption
					request.ShipperPhones = request.ShipperPhones!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeShipperPhoneRequest
			{
				ShipperPhones = request.ShipperPhones,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.ShipperPhones);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._shipperRepository.MergeShipperPhonesAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _shipperRepository.SaveShipperPhoneAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("ShipperPhone", nameof(MergeShipperPhonesAsync));

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
			//// ShipperPhone.custom.cs MergeShipperPhonePostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeShipperPhonePostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeShipperPhonesAsync)}", nameof(Shipper), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShipperPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Shipper), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt ShipperPhones data
	/// </summary>
	/// <param name="shipperPhoneUId"></param>
	/// <returns></returns>
	public async Task DecryptShipperPhonesAsync(Guid shipperPhoneUId)
	{
		Logger.LogInfo($"{nameof(DecryptShipperPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Shipper), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptShipperPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetShipperPhoneRequest
			{
				ShipperPhoneUId = shipperPhoneUId
			};

			Logger.LogInfo($"{nameof(DecryptShipperPhonesAsync)} - Processing data for decryption", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseShipperPhones = await this.GetShipperPhonesAsync(request).ConfigureAwait(false);

			if (responseShipperPhones.ShipperPhones != null && responseShipperPhones.ShipperPhones.Any())
			{
				Logger.LogInfo($"{nameof(DecryptShipperPhonesAsync)} - Decrypted the data", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var shipperPhone in responseShipperPhones.ShipperPhones)
				{
					shipperPhone.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptShipperPhonesAsync)} - Set the item state to update the records back to entity", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeShipperPhoneRequest
				{
					ShipperPhones = responseShipperPhones.ShipperPhones.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeShipperPhonesAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptShipperPhonesAsync)} while saving data", nameof(Shipper), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptShipperPhonesAsync)} - Successfully processed data for decryption", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing shipperPhones data for decryption in {nameof(DecryptShipperPhonesAsync)}", nameof(Shipper), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Shipper), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptShipperPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptShipperPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetShipperPhoneRequest request)
	{
		var cacheKey = $"Domain.ShipperPhone:{request.ShipperPhoneUId}:{request.ShipperUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeShipperPhoneRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.ShipperPhones != null)
		{	
			var cacheKey = $"Domain.ShipperPhone:{Guid.Empty}:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var shipperPhone in request.ShipperPhones)
			{
				cacheKey = $"Domain.ShipperPhone:{shipperPhone.ShipperPhoneUId}:{Guid.Empty}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

				cacheKey = $"Domain.ShipperPhone:{Guid.Empty}:{shipperPhone.ShipperUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
