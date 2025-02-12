#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion  

namespace Accenture.WebShop.Infrastructure.DataRepositories.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents OrderItemShipment Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The OrderItemShipment Infrastructure DataRepositories (DOTNET090000).
/// </remarks>
public partial class OrderRepository : INFRADATAREPO.DataRepositoryBase, COREAPPDREPOINTERFACESDOMAIN.IOrderRepository
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	#endregion

	#region Methods

	/// <summary>
	/// Get OrderItemShipments
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetOrderItemShipmentResponse> GetOrderItemShipmentsAsync(COREAPPDATAREPOMODELSDOMAIN.GetOrderItemShipmentRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(OrderRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetOrderItemShipmentResponse response = new();

		try
		{
			var orderItemShipments = await this._orderItemShipment.GetOrderItemShipmentsAsync(request).ConfigureAwait(false);
			response.OrderItemShipments = orderItemShipments;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetOrderItemShipmentsAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(OrderRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge OrderItemShipments
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeOrderItemShipmentResponse> MergeOrderItemShipmentsAsync(COREAPPDATAREPOMODELSDOMAIN.MergeOrderItemShipmentRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(OrderRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeOrderItemShipmentResponse response = new();

		try
		{
			var mergeResult = await this._orderItemShipment.MergeOrderItemShipmentsAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeOrderItemShipmentsAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(OrderRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves OrderRepository.
	/// </summary>
	public async Task SaveOrderItemShipmentAsync()
	{
		Logger.LogInfo($"{nameof(SaveOrderItemShipmentAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(OrderRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _orderItemShipment.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveOrderItemShipmentAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveOrderItemShipmentAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(OrderRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		}
	}


	#endregion

}
