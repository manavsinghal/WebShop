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
/// Represents CustomerPhone class.
/// </summary>
/// <remarks>
/// The CustomerPhone class.
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
	/// Get CustomerPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetCustomerPhonePreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetCustomerPhoneRequest request);

	/// <summary>
	/// Get CustomerPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetCustomerPhonePostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetCustomerPhoneRequest request);

	/// <summary>
	/// Merge CustomerPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeCustomerPhonePostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeCustomerPhoneRequest request);

	/// <summary>
	/// Merge CustomerPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeCustomerPhonePreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeCustomerPhoneRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get CustomerPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetCustomerPhoneResponse> GetCustomerPhonesAsync(COREAPPDATAMODELSDOMAIN.GetCustomerPhoneRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Customer), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetCustomerPhoneResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetCustomerPhonePreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetCustomerPhonesAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.CustomerPhones = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.CustomerPhone>>();
			}
			else
			{
				response = await FetchAndCacheCustomerPhonesAsync(request, cacheKey, EnableCaching("CustomerPhone", nameof(GetCustomerPhonesAsync)));
			}

			var postProcessorResponse = await GetCustomerPhonePostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetCustomerPhonesAsync)}", nameof(Customer), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Customer), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetCustomerPhoneResponse> FetchAndCacheCustomerPhonesAsync(COREAPPDATAMODELSDOMAIN.GetCustomerPhoneRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetCustomerPhoneResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._customerRepository.GetCustomerPhonesAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.CustomerPhones = await repositoryResponse.CustomerPhones.ToListAsync();

			//Decryption
			var decryptedResult = response.CustomerPhones.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.CustomerPhones = decryptedResult;
			}

			await SetCachedData(cacheKey, response.CustomerPhones.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetCustomerPhoneRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetCustomerPhoneRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetCustomerPhoneRequest();

		if (request.CustomerPhoneUId == Guid.Empty && request.CustomerUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = null;
		}
		else if (request.CustomerPhoneUId == Guid.Empty && request.CustomerUId != Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.CustomerUId == request.CustomerUId;
		}
		else if (request.CustomerPhoneUId != Guid.Empty && request.CustomerUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.CustomerPhoneUId == request.CustomerPhoneUId;
		}
		else
		{
			repositoryRequest.MatchExpression = (entity) => entity.CustomerPhoneUId == request.CustomerPhoneUId && entity.CustomerUId == request.CustomerUId;
		}

		return repositoryRequest;
	}


	/// <summary>
	/// Merge CustomerPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeCustomerPhoneResponse> MergeCustomerPhonesAsync(COREAPPDATAMODELSDOMAIN.MergeCustomerPhoneRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Customer), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeCustomerPhoneResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// CustomerPhone.custom.cs MergeCustomerPhonePreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeCustomerPhonePreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("CustomerPhone");

				if (isEncryptionRequired)
				{
					//Encryption
					request.CustomerPhones = request.CustomerPhones!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeCustomerPhoneRequest
			{
				CustomerPhones = request.CustomerPhones,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.CustomerPhones);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._customerRepository.MergeCustomerPhonesAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _customerRepository.SaveCustomerPhoneAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("CustomerPhone", nameof(MergeCustomerPhonesAsync));

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
			//// CustomerPhone.custom.cs MergeCustomerPhonePostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeCustomerPhonePostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeCustomerPhonesAsync)}", nameof(Customer), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Customer), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt CustomerPhones data
	/// </summary>
	/// <param name="customerPhoneUId"></param>
	/// <returns></returns>
	public async Task DecryptCustomerPhonesAsync(Guid customerPhoneUId)
	{
		Logger.LogInfo($"{nameof(DecryptCustomerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Customer), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptCustomerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetCustomerPhoneRequest
			{
				CustomerPhoneUId = customerPhoneUId
			};

			Logger.LogInfo($"{nameof(DecryptCustomerPhonesAsync)} - Processing data for decryption", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseCustomerPhones = await this.GetCustomerPhonesAsync(request).ConfigureAwait(false);

			if (responseCustomerPhones.CustomerPhones != null && responseCustomerPhones.CustomerPhones.Any())
			{
				Logger.LogInfo($"{nameof(DecryptCustomerPhonesAsync)} - Decrypted the data", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var customerPhone in responseCustomerPhones.CustomerPhones)
				{
					customerPhone.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptCustomerPhonesAsync)} - Set the item state to update the records back to entity", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeCustomerPhoneRequest
				{
					CustomerPhones = responseCustomerPhones.CustomerPhones.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeCustomerPhonesAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptCustomerPhonesAsync)} while saving data", nameof(Customer), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptCustomerPhonesAsync)} - Successfully processed data for decryption", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing customerPhones data for decryption in {nameof(DecryptCustomerPhonesAsync)}", nameof(Customer), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Customer), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptCustomerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptCustomerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetCustomerPhoneRequest request)
	{
		var cacheKey = $"Domain.CustomerPhone:{request.CustomerPhoneUId}:{request.CustomerUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeCustomerPhoneRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.CustomerPhones != null)
		{	
			var cacheKey = $"Domain.CustomerPhone:{Guid.Empty}:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var customerPhone in request.CustomerPhones)
			{
				cacheKey = $"Domain.CustomerPhone:{customerPhone.CustomerPhoneUId}:{Guid.Empty}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

				cacheKey = $"Domain.CustomerPhone:{Guid.Empty}:{customerPhone.CustomerUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
