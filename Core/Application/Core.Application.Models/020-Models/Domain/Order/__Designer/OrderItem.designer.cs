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
/// Represents OrderItem class.
/// </summary>
/// <remarks>
/// The OrderItem class.
/// </remarks>
public partial class Order : COREAPPMODELS.DomainModel<COREDOMAINDATAMODELSDOMAIN.Order>, COREAPPINTERFACESDOMAIN.IOrder
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	#endregion
	
	#region Partial Methods	

	/// <summary>
	/// Merge OrderItems
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeOrderItemPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeOrderItemRequest request);

	/// <summary>
	/// Get OrderItems
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetOrderItemPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetOrderItemRequest request);

	/// <summary>
	/// Get OrderItems
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetOrderItemPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetOrderItemRequest request);

	/// <summary>
	/// Merge OrderItems
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeOrderItemPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeOrderItemRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get OrderItems
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetOrderItemResponse> GetOrderItemsAsync(COREAPPDATAMODELSDOMAIN.GetOrderItemRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrderItemsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Order), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetOrderItemResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetOrderItemPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetOrderItemsAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.OrderItems = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.OrderItem>>();
			}
			else
			{
				response = await FetchAndCacheOrderItemsAsync(request, cacheKey, EnableCaching("OrderItem", nameof(GetOrderItemsAsync)));
			}

			var postProcessorResponse = await GetOrderItemPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetOrderItemsAsync)}", nameof(Order), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrderItemsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Order), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetOrderItemResponse> FetchAndCacheOrderItemsAsync(COREAPPDATAMODELSDOMAIN.GetOrderItemRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetOrderItemResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._orderRepository.GetOrderItemsAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.OrderItems = await repositoryResponse.OrderItems.ToListAsync();

			//Decryption
			var decryptedResult = response.OrderItems.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.OrderItems = decryptedResult;
			}

			await SetCachedData(cacheKey, response.OrderItems.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetOrderItemRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetOrderItemRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetOrderItemRequest();

		if (request.OrderItemUId == Guid.Empty && request.OrderUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = null;
		}
		else if (request.OrderItemUId == Guid.Empty && request.OrderUId != Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.OrderUId == request.OrderUId;
		}
		else if (request.OrderItemUId != Guid.Empty && request.OrderUId == Guid.Empty)
		{
			repositoryRequest.MatchExpression = (entity) => entity.OrderItemUId == request.OrderItemUId;
		}
		else
		{
			repositoryRequest.MatchExpression = (entity) => entity.OrderItemUId == request.OrderItemUId && entity.OrderUId == request.OrderUId;
		}

		return repositoryRequest;
	}


	/// <summary>
	/// Merge OrderItems
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeOrderItemResponse> MergeOrderItemsAsync(COREAPPDATAMODELSDOMAIN.MergeOrderItemRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrderItemsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Order), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeOrderItemResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// OrderItem.custom.cs MergeOrderItemPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeOrderItemPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("OrderItem");

				if (isEncryptionRequired)
				{
					//Encryption
					request.OrderItems = request.OrderItems!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeOrderItemRequest
			{
				OrderItems = request.OrderItems,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.OrderItems);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._orderRepository.MergeOrderItemsAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _orderRepository.SaveOrderItemAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("OrderItem", nameof(MergeOrderItemsAsync));

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
			//// OrderItem.custom.cs MergeOrderItemPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeOrderItemPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeOrderItemsAsync)}", nameof(Order), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrderItemsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Order), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt OrderItems data
	/// </summary>
	/// <param name="orderItemUId"></param>
	/// <returns></returns>
	public async Task DecryptOrderItemsAsync(Guid orderItemUId)
	{
		Logger.LogInfo($"{nameof(DecryptOrderItemsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Order), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Order), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptOrderItemsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetOrderItemRequest
			{
				OrderItemUId = orderItemUId
			};

			Logger.LogInfo($"{nameof(DecryptOrderItemsAsync)} - Processing data for decryption", nameof(Order), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseOrderItems = await this.GetOrderItemsAsync(request).ConfigureAwait(false);

			if (responseOrderItems.OrderItems != null && responseOrderItems.OrderItems.Any())
			{
				Logger.LogInfo($"{nameof(DecryptOrderItemsAsync)} - Decrypted the data", nameof(Order), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var orderItem in responseOrderItems.OrderItems)
				{
					orderItem.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptOrderItemsAsync)} - Set the item state to update the records back to entity", nameof(Order), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeOrderItemRequest
				{
					OrderItems = responseOrderItems.OrderItems.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeOrderItemsAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptOrderItemsAsync)} while saving data", nameof(Order), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptOrderItemsAsync)} - Successfully processed data for decryption", nameof(Order), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing orderItems data for decryption in {nameof(DecryptOrderItemsAsync)}", nameof(Order), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Order), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptOrderItemsAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptOrderItemsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Order), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetOrderItemRequest request)
	{
		var cacheKey = $"Domain.OrderItem:{request.OrderItemUId}:{request.OrderUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeOrderItemRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.OrderItems != null)
		{	
			var cacheKey = $"Domain.OrderItem:{Guid.Empty}:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var orderItem in request.OrderItems)
			{
				cacheKey = $"Domain.OrderItem:{orderItem.OrderItemUId}:{Guid.Empty}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

				cacheKey = $"Domain.OrderItem:{Guid.Empty}:{orderItem.OrderUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
