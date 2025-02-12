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
/// Represents ShipperBankAccount class.
/// </summary>
/// <remarks>
/// The ShipperBankAccount class.
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
	/// Merge ShipperBankAccounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeShipperBankAccountPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeShipperBankAccountRequest request);

	/// <summary>
	/// Get ShipperBankAccounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetShipperBankAccountPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetShipperBankAccountRequest request);

	/// <summary>
	/// Get ShipperBankAccounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetShipperBankAccountPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetShipperBankAccountRequest request);

	/// <summary>
	/// Merge ShipperBankAccounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeShipperBankAccountPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeShipperBankAccountRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get ShipperBankAccounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetShipperBankAccountResponse> GetShipperBankAccountsAsync(COREAPPDATAMODELSDOMAIN.GetShipperBankAccountRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetShipperBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Shipper), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetShipperBankAccountResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetShipperBankAccountPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetShipperBankAccountsAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.ShipperBankAccounts = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.ShipperBankAccount>>();
			}
			else
			{
				response = await FetchAndCacheShipperBankAccountsAsync(request, cacheKey, EnableCaching("ShipperBankAccount", nameof(GetShipperBankAccountsAsync)));
			}

			var postProcessorResponse = await GetShipperBankAccountPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetShipperBankAccountsAsync)}", nameof(Shipper), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShipperBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Shipper), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetShipperBankAccountResponse> FetchAndCacheShipperBankAccountsAsync(COREAPPDATAMODELSDOMAIN.GetShipperBankAccountRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetShipperBankAccountResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._shipperRepository.GetShipperBankAccountsAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.ShipperBankAccounts = await repositoryResponse.ShipperBankAccounts.ToListAsync();

			//Decryption
			var decryptedResult = response.ShipperBankAccounts.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.ShipperBankAccounts = decryptedResult;
			}

			await SetCachedData(cacheKey, response.ShipperBankAccounts.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetShipperBankAccountRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetShipperBankAccountRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetShipperBankAccountRequest();

		if (request.ShipperBankAccountUId == Guid.Empty && request.ShipperUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = null;
		}
		else if (request.ShipperBankAccountUId == Guid.Empty && request.ShipperUId != Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.ShipperUId == request.ShipperUId;
		}
		else if (request.ShipperBankAccountUId != Guid.Empty && request.ShipperUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.ShipperBankAccountUId == request.ShipperBankAccountUId;
		}
		else
		{
			repositoryRequest.MatchExpression = (entity) => entity.ShipperBankAccountUId == request.ShipperBankAccountUId && entity.ShipperUId == request.ShipperUId;
		}

		return repositoryRequest;
	}


	/// <summary>
	/// Merge ShipperBankAccounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeShipperBankAccountResponse> MergeShipperBankAccountsAsync(COREAPPDATAMODELSDOMAIN.MergeShipperBankAccountRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShipperBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Shipper), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeShipperBankAccountResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// ShipperBankAccount.custom.cs MergeShipperBankAccountPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeShipperBankAccountPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("ShipperBankAccount");

				if (isEncryptionRequired)
				{
					//Encryption
					request.ShipperBankAccounts = request.ShipperBankAccounts!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeShipperBankAccountRequest
			{
				ShipperBankAccounts = request.ShipperBankAccounts,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.ShipperBankAccounts);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._shipperRepository.MergeShipperBankAccountsAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _shipperRepository.SaveShipperBankAccountAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("ShipperBankAccount", nameof(MergeShipperBankAccountsAsync));

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
			//// ShipperBankAccount.custom.cs MergeShipperBankAccountPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeShipperBankAccountPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeShipperBankAccountsAsync)}", nameof(Shipper), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShipperBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Shipper), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt ShipperBankAccounts data
	/// </summary>
	/// <param name="shipperBankAccountUId"></param>
	/// <returns></returns>
	public async Task DecryptShipperBankAccountsAsync(Guid shipperBankAccountUId)
	{
		Logger.LogInfo($"{nameof(DecryptShipperBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Shipper), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptShipperBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetShipperBankAccountRequest
			{
				ShipperBankAccountUId = shipperBankAccountUId
			};

			Logger.LogInfo($"{nameof(DecryptShipperBankAccountsAsync)} - Processing data for decryption", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseShipperBankAccounts = await this.GetShipperBankAccountsAsync(request).ConfigureAwait(false);

			if (responseShipperBankAccounts.ShipperBankAccounts != null && responseShipperBankAccounts.ShipperBankAccounts.Any())
			{
				Logger.LogInfo($"{nameof(DecryptShipperBankAccountsAsync)} - Decrypted the data", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var shipperBankAccount in responseShipperBankAccounts.ShipperBankAccounts)
				{
					shipperBankAccount.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptShipperBankAccountsAsync)} - Set the item state to update the records back to entity", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeShipperBankAccountRequest
				{
					ShipperBankAccounts = responseShipperBankAccounts.ShipperBankAccounts.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeShipperBankAccountsAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptShipperBankAccountsAsync)} while saving data", nameof(Shipper), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptShipperBankAccountsAsync)} - Successfully processed data for decryption", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing shipperBankAccounts data for decryption in {nameof(DecryptShipperBankAccountsAsync)}", nameof(Shipper), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Shipper), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptShipperBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptShipperBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetShipperBankAccountRequest request)
	{
		var cacheKey = $"Domain.ShipperBankAccount:{request.ShipperBankAccountUId}:{request.ShipperUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeShipperBankAccountRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.ShipperBankAccounts != null)
		{	
			var cacheKey = $"Domain.ShipperBankAccount:{Guid.Empty}:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var shipperBankAccount in request.ShipperBankAccounts)
			{
				cacheKey = $"Domain.ShipperBankAccount:{shipperBankAccount.ShipperBankAccountUId}:{Guid.Empty}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

				cacheKey = $"Domain.ShipperBankAccount:{Guid.Empty}:{shipperBankAccount.ShipperUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
