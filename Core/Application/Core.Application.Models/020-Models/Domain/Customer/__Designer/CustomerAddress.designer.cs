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
/// Represents CustomerAddress class.
/// </summary>
/// <remarks>
/// The CustomerAddress class.
/// </remarks>
public partial class Customer : COREAPPMODELS.DomainModel<COREDOMAINDATAMODELSDOMAIN.Customer>, COREAPPINTERFACESDOMAIN.ICustomer
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	#endregion
	
	#region Partial Methods	

	/// <summary>
	/// Get CustomerAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetCustomerAddressPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetCustomerAddressRequest request);

	/// <summary>
	/// Get CustomerAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetCustomerAddressPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetCustomerAddressRequest request);

	/// <summary>
	/// Merge CustomerAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeCustomerAddressPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeCustomerAddressRequest request);

	/// <summary>
	/// Merge CustomerAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeCustomerAddressPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeCustomerAddressRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get CustomerAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetCustomerAddressResponse> GetCustomerAddressesAsync(COREAPPDATAMODELSDOMAIN.GetCustomerAddressRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Customer), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetCustomerAddressResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetCustomerAddressPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetCustomerAddressesAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.CustomerAddresses = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.CustomerAddress>>();
			}
			else
			{
				response = await FetchAndCacheCustomerAddressesAsync(request, cacheKey, EnableCaching("CustomerAddress", nameof(GetCustomerAddressesAsync)));
			}

			var postProcessorResponse = await GetCustomerAddressPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetCustomerAddressesAsync)}", nameof(Customer), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Customer), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetCustomerAddressResponse> FetchAndCacheCustomerAddressesAsync(COREAPPDATAMODELSDOMAIN.GetCustomerAddressRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetCustomerAddressResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._customerRepository.GetCustomerAddressesAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.CustomerAddresses = await repositoryResponse.CustomerAddresses.ToListAsync();

			//Decryption
			var decryptedResult = response.CustomerAddresses.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.CustomerAddresses = decryptedResult;
			}

			await SetCachedData(cacheKey, response.CustomerAddresses.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetCustomerAddressRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetCustomerAddressRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetCustomerAddressRequest();

		if (request.CustomerAddressUId == Guid.Empty && request.CustomerUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = null;
		}
		else if (request.CustomerAddressUId == Guid.Empty && request.CustomerUId != Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.CustomerUId == request.CustomerUId;
		}
		else if (request.CustomerAddressUId != Guid.Empty && request.CustomerUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.CustomerAddressUId == request.CustomerAddressUId;
		}
		else
		{
			repositoryRequest.MatchExpression = (entity) => entity.CustomerAddressUId == request.CustomerAddressUId && entity.CustomerUId == request.CustomerUId;
		}

		return repositoryRequest;
	}


	/// <summary>
	/// Merge CustomerAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeCustomerAddressResponse> MergeCustomerAddressesAsync(COREAPPDATAMODELSDOMAIN.MergeCustomerAddressRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Customer), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeCustomerAddressResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// CustomerAddress.custom.cs MergeCustomerAddressPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeCustomerAddressPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("CustomerAddress");

				if (isEncryptionRequired)
				{
					//Encryption
					request.CustomerAddresses = request.CustomerAddresses!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeCustomerAddressRequest
			{
				CustomerAddresses = request.CustomerAddresses,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.CustomerAddresses);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._customerRepository.MergeCustomerAddressesAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _customerRepository.SaveCustomerAddressAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("CustomerAddress", nameof(MergeCustomerAddressesAsync));

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
			//// CustomerAddress.custom.cs MergeCustomerAddressPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeCustomerAddressPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeCustomerAddressesAsync)}", nameof(Customer), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Customer), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt CustomerAddresses data
	/// </summary>
	/// <param name="customerAddressUId"></param>
	/// <returns></returns>
	public async Task DecryptCustomerAddressesAsync(Guid customerAddressUId)
	{
		Logger.LogInfo($"{nameof(DecryptCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Customer), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetCustomerAddressRequest
			{
				CustomerAddressUId = customerAddressUId
			};

			Logger.LogInfo($"{nameof(DecryptCustomerAddressesAsync)} - Processing data for decryption", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseCustomerAddresses = await this.GetCustomerAddressesAsync(request).ConfigureAwait(false);

			if (responseCustomerAddresses.CustomerAddresses != null && responseCustomerAddresses.CustomerAddresses.Any())
			{
				Logger.LogInfo($"{nameof(DecryptCustomerAddressesAsync)} - Decrypted the data", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var customerAddress in responseCustomerAddresses.CustomerAddresses)
				{
					customerAddress.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptCustomerAddressesAsync)} - Set the item state to update the records back to entity", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeCustomerAddressRequest
				{
					CustomerAddresses = responseCustomerAddresses.CustomerAddresses.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeCustomerAddressesAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptCustomerAddressesAsync)} while saving data", nameof(Customer), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptCustomerAddressesAsync)} - Successfully processed data for decryption", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing customerAddresses data for decryption in {nameof(DecryptCustomerAddressesAsync)}", nameof(Customer), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Customer), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetCustomerAddressRequest request)
	{
		var cacheKey = $"Domain.CustomerAddress:{request.CustomerAddressUId}:{request.CustomerUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeCustomerAddressRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.CustomerAddresses != null)
		{	
			var cacheKey = $"Domain.CustomerAddress:{Guid.Empty}:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var customerAddress in request.CustomerAddresses)
			{
				cacheKey = $"Domain.CustomerAddress:{customerAddress.CustomerAddressUId}:{Guid.Empty}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

				cacheKey = $"Domain.CustomerAddress:{Guid.Empty}:{customerAddress.CustomerUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
