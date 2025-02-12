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
/// Represents ShipperAddress class.
/// </summary>
/// <remarks>
/// The ShipperAddress class.
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
	/// Get ShipperAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetShipperAddressPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetShipperAddressRequest request);

	/// <summary>
	/// Merge ShipperAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeShipperAddressPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeShipperAddressRequest request);

	/// <summary>
	/// Merge ShipperAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeShipperAddressPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeShipperAddressRequest request);

	/// <summary>
	/// Get ShipperAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetShipperAddressPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetShipperAddressRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get ShipperAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetShipperAddressResponse> GetShipperAddressesAsync(COREAPPDATAMODELSDOMAIN.GetShipperAddressRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetShipperAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Shipper), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetShipperAddressResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetShipperAddressPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetShipperAddressesAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.ShipperAddresses = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.ShipperAddress>>();
			}
			else
			{
				response = await FetchAndCacheShipperAddressesAsync(request, cacheKey, EnableCaching("ShipperAddress", nameof(GetShipperAddressesAsync)));
			}

			var postProcessorResponse = await GetShipperAddressPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetShipperAddressesAsync)}", nameof(Shipper), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShipperAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Shipper), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetShipperAddressResponse> FetchAndCacheShipperAddressesAsync(COREAPPDATAMODELSDOMAIN.GetShipperAddressRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetShipperAddressResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._shipperRepository.GetShipperAddressesAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.ShipperAddresses = await repositoryResponse.ShipperAddresses.ToListAsync();

			//Decryption
			var decryptedResult = response.ShipperAddresses.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.ShipperAddresses = decryptedResult;
			}

			await SetCachedData(cacheKey, response.ShipperAddresses.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetShipperAddressRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetShipperAddressRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetShipperAddressRequest();

		if (request.ShipperAddressUId == Guid.Empty && request.ShipperUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = null;
		}
		else if (request.ShipperAddressUId == Guid.Empty && request.ShipperUId != Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.ShipperUId == request.ShipperUId;
		}
		else if (request.ShipperAddressUId != Guid.Empty && request.ShipperUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.ShipperAddressUId == request.ShipperAddressUId;
		}
		else
		{
			repositoryRequest.MatchExpression = (entity) => entity.ShipperAddressUId == request.ShipperAddressUId && entity.ShipperUId == request.ShipperUId;
		}

		return repositoryRequest;
	}


	/// <summary>
	/// Merge ShipperAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeShipperAddressResponse> MergeShipperAddressesAsync(COREAPPDATAMODELSDOMAIN.MergeShipperAddressRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShipperAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Shipper), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeShipperAddressResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// ShipperAddress.custom.cs MergeShipperAddressPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeShipperAddressPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("ShipperAddress");

				if (isEncryptionRequired)
				{
					//Encryption
					request.ShipperAddresses = request.ShipperAddresses!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeShipperAddressRequest
			{
				ShipperAddresses = request.ShipperAddresses,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.ShipperAddresses);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._shipperRepository.MergeShipperAddressesAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _shipperRepository.SaveShipperAddressAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("ShipperAddress", nameof(MergeShipperAddressesAsync));

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
			//// ShipperAddress.custom.cs MergeShipperAddressPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeShipperAddressPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeShipperAddressesAsync)}", nameof(Shipper), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShipperAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Shipper), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt ShipperAddresses data
	/// </summary>
	/// <param name="shipperAddressUId"></param>
	/// <returns></returns>
	public async Task DecryptShipperAddressesAsync(Guid shipperAddressUId)
	{
		Logger.LogInfo($"{nameof(DecryptShipperAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Shipper), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptShipperAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetShipperAddressRequest
			{
				ShipperAddressUId = shipperAddressUId
			};

			Logger.LogInfo($"{nameof(DecryptShipperAddressesAsync)} - Processing data for decryption", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseShipperAddresses = await this.GetShipperAddressesAsync(request).ConfigureAwait(false);

			if (responseShipperAddresses.ShipperAddresses != null && responseShipperAddresses.ShipperAddresses.Any())
			{
				Logger.LogInfo($"{nameof(DecryptShipperAddressesAsync)} - Decrypted the data", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var shipperAddress in responseShipperAddresses.ShipperAddresses)
				{
					shipperAddress.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptShipperAddressesAsync)} - Set the item state to update the records back to entity", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeShipperAddressRequest
				{
					ShipperAddresses = responseShipperAddresses.ShipperAddresses.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeShipperAddressesAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptShipperAddressesAsync)} while saving data", nameof(Shipper), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptShipperAddressesAsync)} - Successfully processed data for decryption", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing shipperAddresses data for decryption in {nameof(DecryptShipperAddressesAsync)}", nameof(Shipper), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Shipper), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptShipperAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptShipperAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetShipperAddressRequest request)
	{
		var cacheKey = $"Domain.ShipperAddress:{request.ShipperAddressUId}:{request.ShipperUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeShipperAddressRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.ShipperAddresses != null)
		{	
			var cacheKey = $"Domain.ShipperAddress:{Guid.Empty}:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var shipperAddress in request.ShipperAddresses)
			{
				cacheKey = $"Domain.ShipperAddress:{shipperAddress.ShipperAddressUId}:{Guid.Empty}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

				cacheKey = $"Domain.ShipperAddress:{Guid.Empty}:{shipperAddress.ShipperUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
