#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion  

namespace Accenture.WebShop.Infrastructure.DataEntities.SqlServer.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents Order Infrastructure DataEntities SqlServer (DOTNET090000).
/// </summary>
/// <remarks>
/// The Order Infrastructure DataEntities SqlServer (DOTNET090000).
/// </remarks>
public partial class Order : INFRADATAENTITYMSSQL.Entity<COREDOMAINDATAMODELSDOMAIN.Order>, COREAPPDENTINTERFACESDOMAIN.IOrder
{	
    #region Fields

	private readonly COREAPPDENTINTERFACESDOMAIN.IOrderItem _orderItem; 

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
    /// Initializes a new instance of the OrderController class
    /// </summary>
	/// <param name="orderItem">orderItem</param>
	/// <param name="dbContext">dbContext</param>
	/// <param name="logger">logger</param>	
	/// <param name="messageHub"></param>
	public Order(COREAPPDENTINTERFACESDOMAIN.IOrderItem orderItem
					, DomainDbContext dbContext
					, DomainQueryDbContext queryDbContext 
					, MSLOGGING.ILogger<Order> logger
					, MESSAGEHUBINTERFACES.IMessageHub messageHub
					) : base(dbContext, queryDbContext, logger, messageHub)
	{
		this._orderItem = orderItem;
	}

	#endregion

	#region Methods
 
	/// <summary>
    /// Get Orders
    /// </summary>
	/// <param name="request">request</param>
	public async Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.Order>> GetOrdersAsync(COREAPPDATAREPOMODELSDOMAIN.GetOrderRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrdersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Order), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var query = this.AsIQuerable();
		try
		{
			if (request.MatchExpression is not null)
			{
				query = query.Where(request.MatchExpression);
			}
			else
			{
				Logger.LogWarning($"{nameof(GetOrdersAsync)} - MatchExpression is null", nameof(Order), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataAccess);
			}
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetOrdersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetOrdersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetOrdersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetOrdersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrdersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Order), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return query;
	}
 
	/// <summary>
    /// Merge Orders
    /// </summary>
	/// <param name="request">request</param>
	public virtual async Task<COREDOMAINDATAMODELS.MergeResult> MergeOrdersAsync(COREAPPDATAREPOMODELSDOMAIN.MergeOrderRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrdersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Order), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var mergeResult = new COREDOMAINDATAMODELS.MergeResult();
		try
		{
			foreach (var order in request.Orders!)
			{
				Logger.LogDiagnosticsInfo($"Item State:{order.ItemState}", nameof(Order));

				var result = await this.MergeInternalAsync(order).ConfigureAwait(false);
				mergeResult += result;
			}

			Logger.LogDiagnosticsInfo($"InsertCount Count:{mergeResult.InsertCount}, UpdateCount Count:{mergeResult.UpdateCount}, DeleteCount Count:{mergeResult.DeleteCount}", nameof(Order));
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeOrdersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeOrdersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeOrdersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeOrdersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrdersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Order), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return mergeResult;
	}
	/// <summary>
    /// Merges the internal asynchronous.
    /// </summary>
	/// <param name="order">The order.</param>
    /// <returns></returns>
	internal async Task<COREDOMAINDATAMODELS.MergeResult> MergeInternalAsync(COREDOMAINDATAMODELSDOMAIN.Order order)
	{
		var mergeResult = await this.MergeAsync(order);
		this.MergeChildEntities(order);
		return mergeResult;
	}

	/// <summary>
    /// Merges the Child entities.
    /// </summary>
	/// <param name="order">The order.</param>
    /// <returns></returns>
	internal void MergeChildEntities(COREDOMAINDATAMODELSDOMAIN.Order order)
	{
		if (order != null)
		{
			if (order.OrderItems != null && order.OrderItems.Any())
			{
				var mergeOrderItemRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeOrderItemRequest()
				{
					OrderItems = order.OrderItems
				};
				this._orderItem.MergeOrderItemsAsync(mergeOrderItemRequest);
			}

		}
	}
	

	#endregion
}
