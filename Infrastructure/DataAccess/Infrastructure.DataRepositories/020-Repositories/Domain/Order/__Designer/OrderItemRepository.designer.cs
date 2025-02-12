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
/// Represents OrderItem Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The OrderItem Infrastructure DataRepositories (DOTNET090000).
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
	/// Get OrderItems
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetOrderItemResponse> GetOrderItemsAsync(COREAPPDATAREPOMODELSDOMAIN.GetOrderItemRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrderItemsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(OrderRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetOrderItemResponse response = new();

		try
		{
			var orderItems = await this._orderItem.GetOrderItemsAsync(request).ConfigureAwait(false);
			response.OrderItems = orderItems;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetOrderItemsAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrderItemsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(OrderRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge OrderItems
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeOrderItemResponse> MergeOrderItemsAsync(COREAPPDATAREPOMODELSDOMAIN.MergeOrderItemRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrderItemsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(OrderRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeOrderItemResponse response = new();

		try
		{
			var mergeResult = await this._orderItem.MergeOrderItemsAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeOrderItemsAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrderItemsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(OrderRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves OrderRepository.
	/// </summary>
	public async Task SaveOrderItemAsync()
	{
		Logger.LogInfo($"{nameof(SaveOrderItemAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(OrderRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _orderItem.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveOrderItemAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveOrderItemAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(OrderRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		}
	}


	#endregion

}
