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
/// Represents Order class.
/// </summary>
/// <remarks>
/// The Order class.
/// </remarks>
public partial class Order : COREAPPMODELS.DomainModel<COREDOMAINDATAMODELSDOMAIN.Order>, COREAPPINTERFACESDOMAIN.IOrder
{
	#region Fields

	private readonly COREAPPDREPOINTERFACESDOMAIN.IOrderRepository _orderRepository;

	private readonly IServiceProvider _serviceProvider;

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the Order class
	/// </summary>
	/// <param name="orderRepository">The orderRepository.</param>
	/// <param name="logger">The logger.</param>
	/// <param name="cache">The cache.</param>
	/// <param name="messageHub"></param>
	/// <param name="dbAppSettings"></param>
	/// <param name="serviceProvider"></param>
	public Order(COREAPPDREPOINTERFACESDOMAIN.IOrderRepository orderRepository	
							, ILogger<COREDOMAINDATAMODELSDOMAIN.Order> logger
							, IDistributedCache cache
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							, COREAPPINTERFACESDBAPPSETTINGS.IDbAppSettings dbAppSettings
							, IServiceProvider serviceProvider
							): base(logger, cache, messageHub, dbAppSettings, serviceProvider)
	{
		this._orderRepository = orderRepository;
		this._serviceProvider = serviceProvider;
	}

	#endregion
	
	#region Partial Methods	

	/// <summary>
	/// Merge Orders
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeOrderPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeOrderRequest request);

	/// <summary>
	/// Merge Orders
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeOrderPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeOrderRequest request);

	/// <summary>
	/// Get Orders
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetOrderPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetOrderRequest request);

	/// <summary>
	/// Get Orders
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetOrderPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetOrderRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get Orders
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetOrderResponse> GetOrdersAsync(COREAPPDATAMODELSDOMAIN.GetOrderRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrdersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Order), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetOrderResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetOrderPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetOrdersAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.Orders = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.Order>>();
			}
			else
			{
				response = await FetchAndCacheOrdersAsync(request, cacheKey, EnableCaching("Order", nameof(GetOrdersAsync)));
			}

			var postProcessorResponse = await GetOrderPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetOrdersAsync)}", nameof(Order), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrdersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Order), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetOrderResponse> FetchAndCacheOrdersAsync(COREAPPDATAMODELSDOMAIN.GetOrderRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetOrderResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._orderRepository.GetOrdersAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.Orders = await repositoryResponse.Orders.ToListAsync();

			//Decryption
			var decryptedResult = response.Orders.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.Orders = decryptedResult;
			}

			await SetCachedData(cacheKey, response.Orders.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetOrderRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetOrderRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetOrderRequest
		{
			MatchExpression = request.OrderUId != Guid.Empty ? (entity) => entity.OrderUId == request.OrderUId : null,
			CorrelationUId = request.CorrelationUId
		};

		return repositoryRequest;
	}


	/// <summary>
	/// Merge Orders
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeOrderResponse> MergeOrdersAsync(COREAPPDATAMODELSDOMAIN.MergeOrderRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrdersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Order), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeOrderResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// Order.custom.cs MergeOrderPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeOrderPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("Order");

				if (isEncryptionRequired)
				{
					//Encryption
					request.Orders = request.Orders!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeOrderRequest
			{
				Orders = request.Orders,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.Orders);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._orderRepository.MergeOrdersAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _orderRepository.SaveOrderAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("Order", nameof(MergeOrdersAsync));

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
			//// Order.custom.cs MergeOrderPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeOrderPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeOrdersAsync)}", nameof(Order), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrdersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Order), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt Orders data
	/// </summary>
	/// <param name="orderUId"></param>
	/// <returns></returns>
	public async Task DecryptOrdersAsync(Guid orderUId)
	{
		Logger.LogInfo($"{nameof(DecryptOrdersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Order), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Order), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptOrdersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetOrderRequest
			{
				OrderUId = orderUId
			};

			Logger.LogInfo($"{nameof(DecryptOrdersAsync)} - Processing data for decryption", nameof(Order), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseOrders = await this.GetOrdersAsync(request).ConfigureAwait(false);

			if (responseOrders.Orders != null && responseOrders.Orders.Any())
			{
				Logger.LogInfo($"{nameof(DecryptOrdersAsync)} - Decrypted the data", nameof(Order), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var order in responseOrders.Orders)
				{
					order.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptOrdersAsync)} - Set the item state to update the records back to entity", nameof(Order), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeOrderRequest
				{
					Orders = responseOrders.Orders.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeOrdersAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptOrdersAsync)} while saving data", nameof(Order), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptOrdersAsync)} - Successfully processed data for decryption", nameof(Order), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing orders data for decryption in {nameof(DecryptOrdersAsync)}", nameof(Order), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Order), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptOrdersAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptOrdersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Order), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetOrderRequest request)
	{
		var cacheKey = $"Domain.Order:{request.OrderUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeOrderRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.Orders != null)
		{	
			var cacheKey = $"Domain.Order:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var order in request.Orders)
			{
				cacheKey = $"Domain.Order:{order.OrderUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
