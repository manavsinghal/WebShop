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
/// Represents OrderItemShipment Infrastructure DataEntities SqlServer (DOTNET090000).
/// </summary>
/// <remarks>
/// The OrderItemShipment Infrastructure DataEntities SqlServer (DOTNET090000).
/// </remarks>
public partial class OrderItemShipment : INFRADATAENTITYMSSQL.Entity<COREDOMAINDATAMODELSDOMAIN.OrderItemShipment>, COREAPPDENTINTERFACESDOMAIN.IOrderItemShipment
{	
    #region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
    /// Initializes a new instance of the OrderItemShipmentController class
    /// </summary>
	/// <param name="dbContext">dbContext</param>
	/// <param name="logger">logger</param>	
	/// <param name="messageHub"></param>
	public OrderItemShipment(DomainDbContext dbContext
					, DomainQueryDbContext queryDbContext 
					, MSLOGGING.ILogger<OrderItemShipment> logger
					, MESSAGEHUBINTERFACES.IMessageHub messageHub
					) : base(dbContext, queryDbContext, logger, messageHub)
	{
	}

	#endregion

	#region Methods
 
	/// <summary>
    /// Get OrderItemShipments
    /// </summary>
	/// <param name="request">request</param>
	public async Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.OrderItemShipment>> GetOrderItemShipmentsAsync(COREAPPDATAREPOMODELSDOMAIN.GetOrderItemShipmentRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(OrderItemShipment), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var query = this.AsIQuerable();
		try
		{
			if (request.MatchExpression is not null)
			{
				query = query.Where(request.MatchExpression);
			}
			else
			{
				Logger.LogWarning($"{nameof(GetOrderItemShipmentsAsync)} - MatchExpression is null", nameof(OrderItemShipment), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataAccess);
			}
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetOrderItemShipmentsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetOrderItemShipmentsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetOrderItemShipmentsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetOrderItemShipmentsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(OrderItemShipment), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return query;
	}
 
	/// <summary>
    /// Merge OrderItemShipments
    /// </summary>
	/// <param name="request">request</param>
	public virtual async Task<COREDOMAINDATAMODELS.MergeResult> MergeOrderItemShipmentsAsync(COREAPPDATAREPOMODELSDOMAIN.MergeOrderItemShipmentRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(OrderItemShipment), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var mergeResult = new COREDOMAINDATAMODELS.MergeResult();
		try
		{
			foreach (var orderItemShipment in request.OrderItemShipments!)
			{
				Logger.LogDiagnosticsInfo($"Item State:{orderItemShipment.ItemState}", nameof(OrderItemShipment));

				var result = await this.MergeInternalAsync(orderItemShipment).ConfigureAwait(false);
				mergeResult += result;
			}

			Logger.LogDiagnosticsInfo($"InsertCount Count:{mergeResult.InsertCount}, UpdateCount Count:{mergeResult.UpdateCount}, DeleteCount Count:{mergeResult.DeleteCount}", nameof(OrderItemShipment));
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeOrderItemShipmentsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeOrderItemShipmentsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeOrderItemShipmentsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeOrderItemShipmentsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeOrderItemShipmentsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(OrderItemShipment), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return mergeResult;
	}
	/// <summary>
    /// Merges the internal asynchronous.
    /// </summary>
	/// <param name="orderItemShipment">The orderItemShipment.</param>
    /// <returns></returns>
	internal async Task<COREDOMAINDATAMODELS.MergeResult> MergeInternalAsync(COREDOMAINDATAMODELSDOMAIN.OrderItemShipment orderItemShipment)
	{
		var mergeResult = await this.MergeAsync(orderItemShipment);
		return mergeResult;
	}

	

	#endregion
}
