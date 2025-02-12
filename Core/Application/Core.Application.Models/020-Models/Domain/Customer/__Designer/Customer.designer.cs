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
/// Represents Customer class.
/// </summary>
/// <remarks>
/// The Customer class.
/// </remarks>
public partial class Customer : COREAPPMODELS.DomainModel<COREDOMAINDATAMODELSDOMAIN.Customer>, COREAPPINTERFACESDOMAIN.ICustomer
{
	#region Fields

	private readonly COREAPPDREPOINTERFACESDOMAIN.ICustomerRepository _customerRepository;

	private readonly IServiceProvider _serviceProvider;

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the Customer class
	/// </summary>
	/// <param name="customerRepository">The customerRepository.</param>
	/// <param name="logger">The logger.</param>
	/// <param name="cache">The cache.</param>
	/// <param name="messageHub"></param>
	/// <param name="dbAppSettings"></param>
	/// <param name="serviceProvider"></param>
	public Customer(COREAPPDREPOINTERFACESDOMAIN.ICustomerRepository customerRepository	
							, ILogger<COREDOMAINDATAMODELSDOMAIN.Customer> logger
							, IDistributedCache cache
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							, COREAPPINTERFACESDBAPPSETTINGS.IDbAppSettings dbAppSettings
							, IServiceProvider serviceProvider
							): base(logger, cache, messageHub, dbAppSettings, serviceProvider)
	{
		this._customerRepository = customerRepository;
		this._serviceProvider = serviceProvider;
	}

	#endregion
	
	#region Partial Methods	

	/// <summary>
	/// Merge Customers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeCustomerPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeCustomerRequest request);

	/// <summary>
	/// Get Customers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetCustomerPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetCustomerRequest request);

	/// <summary>
	/// Merge Customers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeCustomerPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeCustomerRequest request);

	/// <summary>
	/// Get Customers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetCustomerPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetCustomerRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get Customers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetCustomerResponse> GetCustomersAsync(COREAPPDATAMODELSDOMAIN.GetCustomerRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Customer), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetCustomerResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetCustomerPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetCustomersAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.Customers = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.Customer>>();
			}
			else
			{
				response = await FetchAndCacheCustomersAsync(request, cacheKey, EnableCaching("Customer", nameof(GetCustomersAsync)));
			}

			var postProcessorResponse = await GetCustomerPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetCustomersAsync)}", nameof(Customer), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Customer), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetCustomerResponse> FetchAndCacheCustomersAsync(COREAPPDATAMODELSDOMAIN.GetCustomerRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetCustomerResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._customerRepository.GetCustomersAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.Customers = await repositoryResponse.Customers.ToListAsync();

			//Decryption
			var decryptedResult = response.Customers.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.Customers = decryptedResult;
			}

			await SetCachedData(cacheKey, response.Customers.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetCustomerRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetCustomerRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetCustomerRequest
		{
			MatchExpression = request.CustomerUId != Guid.Empty ? (entity) => entity.CustomerUId == request.CustomerUId : null,
			CorrelationUId = request.CorrelationUId
		};

		return repositoryRequest;
	}


	/// <summary>
	/// Merge Customers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeCustomerResponse> MergeCustomersAsync(COREAPPDATAMODELSDOMAIN.MergeCustomerRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Customer), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeCustomerResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// Customer.custom.cs MergeCustomerPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeCustomerPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("Customer");

				if (isEncryptionRequired)
				{
					//Encryption
					request.Customers = request.Customers!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeCustomerRequest
			{
				Customers = request.Customers,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.Customers);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._customerRepository.MergeCustomersAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _customerRepository.SaveCustomerAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("Customer", nameof(MergeCustomersAsync));

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
			//// Customer.custom.cs MergeCustomerPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeCustomerPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeCustomersAsync)}", nameof(Customer), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Customer), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt Customers data
	/// </summary>
	/// <param name="customerUId"></param>
	/// <returns></returns>
	public async Task DecryptCustomersAsync(Guid customerUId)
	{
		Logger.LogInfo($"{nameof(DecryptCustomersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Customer), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptCustomersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetCustomerRequest
			{
				CustomerUId = customerUId
			};

			Logger.LogInfo($"{nameof(DecryptCustomersAsync)} - Processing data for decryption", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseCustomers = await this.GetCustomersAsync(request).ConfigureAwait(false);

			if (responseCustomers.Customers != null && responseCustomers.Customers.Any())
			{
				Logger.LogInfo($"{nameof(DecryptCustomersAsync)} - Decrypted the data", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var customer in responseCustomers.Customers)
				{
					customer.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptCustomersAsync)} - Set the item state to update the records back to entity", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeCustomerRequest
				{
					Customers = responseCustomers.Customers.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeCustomersAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptCustomersAsync)} while saving data", nameof(Customer), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptCustomersAsync)} - Successfully processed data for decryption", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing customers data for decryption in {nameof(DecryptCustomersAsync)}", nameof(Customer), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Customer), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptCustomersAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptCustomersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetCustomerRequest request)
	{
		var cacheKey = $"Domain.Customer:{request.CustomerUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeCustomerRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.Customers != null)
		{	
			var cacheKey = $"Domain.Customer:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var customer in request.Customers)
			{
				cacheKey = $"Domain.Customer:{customer.CustomerUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
