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
/// Represents Shipper class.
/// </summary>
/// <remarks>
/// The Shipper class.
/// </remarks>
public partial class Shipper : COREAPPMODELS.DomainModel<COREDOMAINDATAMODELSDOMAIN.Shipper>, COREAPPINTERFACESDOMAIN.IShipper
{
	#region Fields

	private readonly IServiceProvider _serviceProvider;

	private readonly COREAPPDREPOINTERFACESDOMAIN.IShipperRepository _shipperRepository;

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the Shipper class
	/// </summary>
	/// <param name="shipperRepository">The shipperRepository.</param>
	/// <param name="logger">The logger.</param>
	/// <param name="cache">The cache.</param>
	/// <param name="messageHub"></param>
	/// <param name="dbAppSettings"></param>
	/// <param name="serviceProvider"></param>
	public Shipper(COREAPPDREPOINTERFACESDOMAIN.IShipperRepository shipperRepository	
							, ILogger<COREDOMAINDATAMODELSDOMAIN.Shipper> logger
							, IDistributedCache cache
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							, COREAPPINTERFACESDBAPPSETTINGS.IDbAppSettings dbAppSettings
							, IServiceProvider serviceProvider
							): base(logger, cache, messageHub, dbAppSettings, serviceProvider)
	{
		this._serviceProvider = serviceProvider;
		this._shipperRepository = shipperRepository;
	}

	#endregion
	
	#region Partial Methods	

	/// <summary>
	/// Get Shippers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetShipperPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetShipperRequest request);

	/// <summary>
	/// Merge Shippers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeShipperPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeShipperRequest request);

	/// <summary>
	/// Merge Shippers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeShipperPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeShipperRequest request);

	/// <summary>
	/// Get Shippers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetShipperPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetShipperRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get Shippers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetShipperResponse> GetShippersAsync(COREAPPDATAMODELSDOMAIN.GetShipperRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetShippersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Shipper), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetShipperResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetShipperPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetShippersAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.Shippers = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.Shipper>>();
			}
			else
			{
				response = await FetchAndCacheShippersAsync(request, cacheKey, EnableCaching("Shipper", nameof(GetShippersAsync)));
			}

			var postProcessorResponse = await GetShipperPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetShippersAsync)}", nameof(Shipper), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShippersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Shipper), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetShipperResponse> FetchAndCacheShippersAsync(COREAPPDATAMODELSDOMAIN.GetShipperRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetShipperResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._shipperRepository.GetShippersAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.Shippers = await repositoryResponse.Shippers.ToListAsync();

			//Decryption
			var decryptedResult = response.Shippers.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.Shippers = decryptedResult;
			}

			await SetCachedData(cacheKey, response.Shippers.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetShipperRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetShipperRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetShipperRequest
		{
			MatchExpression = request.ShipperUId != Guid.Empty ? (entity) => entity.ShipperUId == request.ShipperUId : null,
			CorrelationUId = request.CorrelationUId
		};

		return repositoryRequest;
	}


	/// <summary>
	/// Merge Shippers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeShipperResponse> MergeShippersAsync(COREAPPDATAMODELSDOMAIN.MergeShipperRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShippersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Shipper), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeShipperResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// Shipper.custom.cs MergeShipperPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeShipperPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("Shipper");

				if (isEncryptionRequired)
				{
					//Encryption
					request.Shippers = request.Shippers!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeShipperRequest
			{
				Shippers = request.Shippers,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.Shippers);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._shipperRepository.MergeShippersAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _shipperRepository.SaveShipperAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("Shipper", nameof(MergeShippersAsync));

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
			//// Shipper.custom.cs MergeShipperPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeShipperPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeShippersAsync)}", nameof(Shipper), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShippersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Shipper), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt Shippers data
	/// </summary>
	/// <param name="shipperUId"></param>
	/// <returns></returns>
	public async Task DecryptShippersAsync(Guid shipperUId)
	{
		Logger.LogInfo($"{nameof(DecryptShippersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Shipper), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptShippersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetShipperRequest
			{
				ShipperUId = shipperUId
			};

			Logger.LogInfo($"{nameof(DecryptShippersAsync)} - Processing data for decryption", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseShippers = await this.GetShippersAsync(request).ConfigureAwait(false);

			if (responseShippers.Shippers != null && responseShippers.Shippers.Any())
			{
				Logger.LogInfo($"{nameof(DecryptShippersAsync)} - Decrypted the data", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var shipper in responseShippers.Shippers)
				{
					shipper.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptShippersAsync)} - Set the item state to update the records back to entity", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeShipperRequest
				{
					Shippers = responseShippers.Shippers.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeShippersAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptShippersAsync)} while saving data", nameof(Shipper), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptShippersAsync)} - Successfully processed data for decryption", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing shippers data for decryption in {nameof(DecryptShippersAsync)}", nameof(Shipper), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Shipper), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptShippersAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptShippersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetShipperRequest request)
	{
		var cacheKey = $"Domain.Shipper:{request.ShipperUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeShipperRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.Shippers != null)
		{	
			var cacheKey = $"Domain.Shipper:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var shipper in request.Shippers)
			{
				cacheKey = $"Domain.Shipper:{shipper.ShipperUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
