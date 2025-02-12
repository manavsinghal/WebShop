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
/// Represents Order Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The Order Infrastructure DataRepositories (DOTNET090000).
/// </remarks>
public partial class OrderRepository : INFRADATAREPO.DataRepositoryBase, COREAPPDREPOINTERFACESDOMAIN.IOrderRepository
{
	#region Fields

	private Boolean disposedValue;

	private readonly COREAPPDENTINTERFACESDOMAIN.IOrder _order;

	private readonly COREAPPDENTINTERFACESDOMAIN.IOrderItem _orderItem;

	private readonly COREAPPDENTINTERFACESDOMAIN.IOrderItemShipment _orderItemShipment;

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the OrderController class
	/// </summary>	 
	/// <param name="order">The order.</param>	 
	/// <param name="orderItem">The orderItem.</param>	 
	/// <param name="orderItemShipment">The orderItemShipment.</param>	 
	/// <param name="logger">logger</param>
	/// <param name="messageHub">messageHub.</param>
	public OrderRepository(COREAPPDENTINTERFACESDOMAIN.IOrder order, 
							COREAPPDENTINTERFACESDOMAIN.IOrderItem orderItem, 
							COREAPPDENTINTERFACESDOMAIN.IOrderItemShipment orderItemShipment, 
							MSLOGGING.ILogger<OrderRepository> logger ,MESSAGEHUBINTERFACES.IMessageHub messageHub
							) : base(logger, messageHub)
	{
		this._order = order;
		this._orderItem = orderItem;
		this._orderItemShipment = orderItemShipment;
	}

	#endregion

	#region Methods

	/// <summary>
	/// Get Orders
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetOrderResponse> GetOrdersAsync(COREAPPDATAREPOMODELSDOMAIN.GetOrderRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrdersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(OrderRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetOrderResponse response = new();

		try
		{
			var orders = await this._order.GetOrdersAsync(request).ConfigureAwait(false);
			response.Orders = orders;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetOrdersAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrdersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(OrderRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge Orders
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeOrderResponse> MergeOrdersAsync(COREAPPDATAREPOMODELSDOMAIN.MergeOrderRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrdersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(OrderRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeOrderResponse response = new();

		try
		{
			var mergeResult = await this._order.MergeOrdersAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeOrdersAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrdersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(OrderRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves OrderRepository.
	/// </summary>
	public async Task SaveOrderAsync()
	{
		Logger.LogInfo($"{nameof(SaveOrderAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(OrderRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _order.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveOrderAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveOrderAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(OrderRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		}
	}

	/// <summary>
	/// Represents Dispose Method.
	/// </summary>
	public void Dispose()
	{
		if (!disposedValue)
		{
			disposedValue = true;
		}
	}

	#endregion

}
