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
/// Represents OrderItem Infrastructure DataEntities SqlServer (DOTNET090000).
/// </summary>
/// <remarks>
/// The OrderItem Infrastructure DataEntities SqlServer (DOTNET090000).
/// </remarks>
public partial class OrderItem : INFRADATAENTITYMSSQL.Entity<COREDOMAINDATAMODELSDOMAIN.OrderItem>, COREAPPDENTINTERFACESDOMAIN.IOrderItem
{	
    #region Fields

	private readonly COREAPPDENTINTERFACESDOMAIN.IOrderItemShipment _orderItemShipment; 

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
    /// Initializes a new instance of the OrderItemController class
    /// </summary>
	/// <param name="orderItemShipment">orderItemShipment</param>
	/// <param name="dbContext">dbContext</param>
	/// <param name="logger">logger</param>	
	/// <param name="messageHub"></param>
	public OrderItem(COREAPPDENTINTERFACESDOMAIN.IOrderItemShipment orderItemShipment
					, DomainDbContext dbContext
					, DomainQueryDbContext queryDbContext 
					, MSLOGGING.ILogger<OrderItem> logger
					, MESSAGEHUBINTERFACES.IMessageHub messageHub
					) : base(dbContext, queryDbContext, logger, messageHub)
	{
		this._orderItemShipment = orderItemShipment;
	}

	#endregion

	#region Methods
 
	/// <summary>
    /// Get OrderItems
    /// </summary>
	/// <param name="request">request</param>
	public async Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.OrderItem>> GetOrderItemsAsync(COREAPPDATAREPOMODELSDOMAIN.GetOrderItemRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrderItemsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(OrderItem), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var query = this.AsIQuerable();
		try
		{
			if (request.MatchExpression is not null)
			{
				query = query.Where(request.MatchExpression);
			}
			else
			{
				Logger.LogWarning($"{nameof(GetOrderItemsAsync)} - MatchExpression is null", nameof(OrderItem), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataAccess);
			}
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetOrderItemsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetOrderItemsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetOrderItemsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetOrderItemsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrderItemsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(OrderItem), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return query;
	}
 
	/// <summary>
    /// Merge OrderItems
    /// </summary>
	/// <param name="request">request</param>
	public virtual async Task<COREDOMAINDATAMODELS.MergeResult> MergeOrderItemsAsync(COREAPPDATAREPOMODELSDOMAIN.MergeOrderItemRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrderItemsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(OrderItem), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var mergeResult = new COREDOMAINDATAMODELS.MergeResult();
		try
		{
			foreach (var orderItem in request.OrderItems!)
			{
				Logger.LogDiagnosticsInfo($"Item State:{orderItem.ItemState}", nameof(OrderItem));

				var result = await this.MergeInternalAsync(orderItem).ConfigureAwait(false);
				mergeResult += result;
			}

			Logger.LogDiagnosticsInfo($"InsertCount Count:{mergeResult.InsertCount}, UpdateCount Count:{mergeResult.UpdateCount}, DeleteCount Count:{mergeResult.DeleteCount}", nameof(OrderItem));
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeOrderItemsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeOrderItemsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeOrderItemsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeOrderItemsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrderItemsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(OrderItem), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return mergeResult;
	}
	/// <summary>
    /// Merges the internal asynchronous.
    /// </summary>
	/// <param name="orderItem">The orderItem.</param>
    /// <returns></returns>
	internal async Task<COREDOMAINDATAMODELS.MergeResult> MergeInternalAsync(COREDOMAINDATAMODELSDOMAIN.OrderItem orderItem)
	{
		var mergeResult = await this.MergeAsync(orderItem);
		this.MergeChildEntities(orderItem);
		return mergeResult;
	}

	/// <summary>
    /// Merges the Child entities.
    /// </summary>
	/// <param name="orderItem">The orderItem.</param>
    /// <returns></returns>
	internal void MergeChildEntities(COREDOMAINDATAMODELSDOMAIN.OrderItem orderItem)
	{
		if (orderItem != null)
		{
			if (orderItem.OrderItemShipments != null && orderItem.OrderItemShipments.Any())
			{
				var mergeOrderItemShipmentRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeOrderItemShipmentRequest()
				{
					OrderItemShipments = orderItem.OrderItemShipments
				};
				this._orderItemShipment.MergeOrderItemShipmentsAsync(mergeOrderItemShipmentRequest);
			}

		}
	}
	

	#endregion
}
