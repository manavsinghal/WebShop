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
/// Represents OrderItemShipment class.
/// </summary>
/// <remarks>
/// The OrderItemShipment class.
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
	/// Get OrderItemShipments
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetOrderItemShipmentPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetOrderItemShipmentRequest request);

	/// <summary>
	/// Get OrderItemShipments
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetOrderItemShipmentPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetOrderItemShipmentRequest request);

	/// <summary>
	/// Merge OrderItemShipments
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeOrderItemShipmentPreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeOrderItemShipmentRequest request);

	/// <summary>
	/// Merge OrderItemShipments
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeOrderItemShipmentPostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeOrderItemShipmentRequest request);

	#endregion

	#region Public Methods

	/// <summary>
	/// Get OrderItemShipments
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.GetOrderItemShipmentResponse> GetOrderItemShipmentsAsync(COREAPPDATAMODELSDOMAIN.GetOrderItemShipmentRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Order), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		var response = new COREAPPDATAMODELSDOMAIN.GetOrderItemShipmentResponse();
		var faults = new FaultCollection();

		try
		{
			var preProcessorResponse = await GetOrderItemShipmentPreProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePreProcessorFaults(preProcessorResponse, response)) 
			{
				return response;
			}

			var cacheKey = await GetCacheKey(request).ConfigureAwait(false);
            var cachedData = await GetCachedDataIfEnabled(request, cacheKey, nameof(GetOrderItemShipmentsAsync)).ConfigureAwait(false);

			if (cachedData != null)
			{
				response.OrderItemShipments = cachedData.ToObject<IEnumerable<COREDOMAINDATAMODELSDOMAIN.OrderItemShipment>>();
			}
			else
			{
				response = await FetchAndCacheOrderItemShipmentsAsync(request, cacheKey, EnableCaching("OrderItemShipment", nameof(GetOrderItemShipmentsAsync)));
			}

			var postProcessorResponse = await GetOrderItemShipmentPostProcessorResponseAsync(request).ConfigureAwait(false);
			if (HandlePostProcessorFaults(postProcessorResponse, response)) 
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));	

			await HandleError($"Error occurred in {nameof(GetOrderItemShipmentsAsync)}", nameof(Order), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Order), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	private async Task<COREAPPDATAMODELSDOMAIN.GetOrderItemShipmentResponse> FetchAndCacheOrderItemShipmentsAsync(COREAPPDATAMODELSDOMAIN.GetOrderItemShipmentRequest request, String cacheKey, Boolean isEnabledEntityCaching)
    {
        var response = new COREAPPDATAMODELSDOMAIN.GetOrderItemShipmentResponse();
        var repositoryRequest = CreateRepositoryRequest(request);
        var repositoryResponse = await this._orderRepository.GetOrderItemShipmentsAsync(repositoryRequest).ConfigureAwait(false);

        if (repositoryResponse.Faults!.Any())
        {
            response.Faults = repositoryResponse.Faults;
        }
        else
        {
            response.MergeResult = repositoryResponse.MergeResult;

			response.OrderItemShipments = await repositoryResponse.OrderItemShipments.ToListAsync();

			//Decryption
			var decryptedResult = response.OrderItemShipments.DeObfuscate(new { AccessRole = new String[] { "All" } });

			if (decryptedResult != null)
			{
				response.OrderItemShipments = decryptedResult;
			}

			await SetCachedData(cacheKey, response.OrderItemShipments.ToJson(), isEnabledEntityCaching).ConfigureAwait(false);
        }

        return response;
    }

    private COREAPPDATAREPOMODELSDOMAIN.GetOrderItemShipmentRequest CreateRepositoryRequest(COREAPPDATAMODELSDOMAIN.GetOrderItemShipmentRequest request)
    {
		var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.GetOrderItemShipmentRequest
		{
			MatchExpression = request.OrderItemShipmentUId != Guid.Empty ? (entity) => entity.OrderItemShipmentUId == request.OrderItemShipmentUId : null,
			CorrelationUId = request.CorrelationUId
		};

		return repositoryRequest;
	}


	/// <summary>
	/// Merge OrderItemShipments
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAMODELSDOMAIN.MergeOrderItemShipmentResponse> MergeOrderItemShipmentsAsync(COREAPPDATAMODELSDOMAIN.MergeOrderItemShipmentRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Order), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		COREAPPDATAMODELSDOMAIN.MergeOrderItemShipmentResponse response = new();

		var faults = new FaultCollection();

		try
		{
			if (await HandleReadOnlyModeAsync(response, faults).ConfigureAwait(false))
			{
				return response;
			}

			//// If there are any customization required, Add custom code in the
			//// OrderItemShipment.custom.cs MergeOrderItemShipmentPreProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var preprocessorResponse = await MergeOrderItemShipmentPreProcessorResponseAsync(request).ConfigureAwait(false);

			if (preprocessorResponse != null && preprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(preprocessorResponse.Faults);
				return response;
			}

			if (request.IsEncryptionEnabled)
			{
				var isEncryptionRequired = GetEncryptionStatus("OrderItemShipment");

				if (isEncryptionRequired)
				{
					//Encryption
					request.OrderItemShipments = request.OrderItemShipments!.Obfuscate();
				}
			}
			
			var repositoryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeOrderItemShipmentRequest
			{
				OrderItemShipments = request.OrderItemShipments,
				CorrelationUId = request.CorrelationUId,
			};

			faults = ValidateDataModel(request.OrderItemShipments);
			
			if (!faults.Any())
			{
				var repositoryResponse = await this._orderRepository.MergeOrderItemShipmentsAsync(repositoryRequest).ConfigureAwait(false);

				faults = repositoryResponse.Faults;

				await _orderRepository.SaveOrderItemShipmentAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						faults.Add(CreateExceptionFault(t.Exception));
					}
					return Task.CompletedTask;

				}).ConfigureAwait(false);	

				if (!faults.Any())
				{	
					var isEnabledEntityCaching = this.EnableCaching("OrderItemShipment", nameof(MergeOrderItemShipmentsAsync));

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
			//// OrderItemShipment.custom.cs MergeOrderItemShipmentPostProcessorResponseAsync partial method.
			//// Do not add any custom code in this file
			var postprocessorResponse = await MergeOrderItemShipmentPostProcessorResponseAsync(request).ConfigureAwait(false);

			if (postprocessorResponse != null && postprocessorResponse.Faults.Any())
			{
				response.Faults!.AddRange(postprocessorResponse.Faults);
			}
		}
		catch (Exception ex)
		{
			faults.Add(CreateExceptionFault(ex));

			await HandleError($"Error occurred in {nameof(MergeOrderItemShipmentsAsync)}", nameof(Order), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Order), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.BusinessDomain));
		}

		response.Faults = faults;

		return response;
	}

	/// <summary>
	/// Decrypt OrderItemShipments data
	/// </summary>
	/// <param name="orderItemShipmentUId"></param>
	/// <returns></returns>
	public async Task DecryptOrderItemShipmentsAsync(Guid orderItemShipmentUId)
	{
		Logger.LogInfo($"{nameof(DecryptOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Order), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Order), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}").ConfigureAwait(false);

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetOrderItemShipmentRequest
			{
				OrderItemShipmentUId = orderItemShipmentUId
			};

			Logger.LogInfo($"{nameof(DecryptOrderItemShipmentsAsync)} - Processing data for decryption", nameof(Order), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

			var responseOrderItemShipments = await this.GetOrderItemShipmentsAsync(request).ConfigureAwait(false);

			if (responseOrderItemShipments.OrderItemShipments != null && responseOrderItemShipments.OrderItemShipments.Any())
			{
				Logger.LogInfo($"{nameof(DecryptOrderItemShipmentsAsync)} - Decrypted the data", nameof(Order), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				foreach (var orderItemShipment in responseOrderItemShipments.OrderItemShipments)
				{
					orderItemShipment.ItemState = COREDOMAINDATAMODELSENUM.ItemState.Modified;
				}

				Logger.LogInfo($"{nameof(DecryptOrderItemShipmentsAsync)} - Set the item state to update the records back to entity", nameof(Order), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);

				var mergeRequest = new COREAPPDATAMODELSDOMAIN.MergeOrderItemShipmentRequest
				{
					OrderItemShipments = responseOrderItemShipments.OrderItemShipments.ToList(),
					IsEncryptionEnabled = false
				};

				var mergeResponse = await this.MergeOrderItemShipmentsAsync(mergeRequest).ConfigureAwait(false);

				if (mergeResponse.Faults != null && mergeResponse.Faults.Any())
				{
					Logger.LogFault($"Error occurred in {nameof(DecryptOrderItemShipmentsAsync)} while saving data", nameof(Order), mergeResponse.Faults, SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
				}

				Logger.LogInfo($"{nameof(DecryptOrderItemShipmentsAsync)} - Successfully processed data for decryption", nameof(Order), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
			}
		}
		catch (Exception ex)
		{
			await HandleError($"Error occurred while processing orderItemShipments data for decryption in {nameof(DecryptOrderItemShipmentsAsync)}", nameof(Order), ex).ConfigureAwait(false);
		}
		finally
		{
			await this.SendInfoAsync(MESSAGEHUBENUMS.Topic.DeveloperAndUser, nameof(Order), SHAREDKERNALRESX.WebShop.CodeComposer, $"{nameof(DecryptOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.End}").ConfigureAwait(false);
			Logger.LogInfo($"{nameof(DecryptOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Order), this.GetType(), SHAREDKERNALLIB.ApplicationTier.BusinessDomain);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Gets the cache key.
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private static async Task<String> GetCacheKey(COREAPPDATAMODELSDOMAIN.GetOrderItemShipmentRequest request)
	{
		var cacheKey = $"Domain.OrderItemShipment:{request.OrderItemShipmentUId}";

		return await Task.FromResult(cacheKey).ConfigureAwait(false);
	}

	/// <summary>
    /// Resets the cache data.
    /// </summary>
    /// <param name="request">The request.</param>
	private async Task ResetCacheData(COREAPPDATAMODELSDOMAIN.MergeOrderItemShipmentRequest request, Boolean isEnabledEntityCaching)
	{
		if (request.OrderItemShipments != null)
		{	
			var cacheKey = $"Domain.OrderItemShipment:{Guid.Empty}";

			await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);

			foreach (var orderItemShipment in request.OrderItemShipments)
			{
				cacheKey = $"Domain.OrderItemShipment:{orderItemShipment.OrderItemShipmentUId}";

				await this.ClearCachedData(cacheKey, isEnabledEntityCaching).ConfigureAwait(false);
				await this.ClearCachedData("AllAccounts", isEnabledEntityCaching).ConfigureAwait(false);
			}
		}
	}

	#endregion
}
