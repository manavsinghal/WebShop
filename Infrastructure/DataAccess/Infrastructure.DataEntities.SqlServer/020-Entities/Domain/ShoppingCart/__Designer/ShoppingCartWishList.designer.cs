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
/// Represents ShoppingCartWishList Infrastructure DataEntities SqlServer (DOTNET090000).
/// </summary>
/// <remarks>
/// The ShoppingCartWishList Infrastructure DataEntities SqlServer (DOTNET090000).
/// </remarks>
public partial class ShoppingCartWishList : INFRADATAENTITYMSSQL.Entity<COREDOMAINDATAMODELSDOMAIN.ShoppingCartWishList>, COREAPPDENTINTERFACESDOMAIN.IShoppingCartWishList
{	
    #region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
    /// Initializes a new instance of the ShoppingCartWishListController class
    /// </summary>
	/// <param name="dbContext">dbContext</param>
	/// <param name="logger">logger</param>	
	/// <param name="messageHub"></param>
	public ShoppingCartWishList(DomainDbContext dbContext
					, DomainQueryDbContext queryDbContext 
					, MSLOGGING.ILogger<ShoppingCartWishList> logger
					, MESSAGEHUBINTERFACES.IMessageHub messageHub
					) : base(dbContext, queryDbContext, logger, messageHub)
	{
	}

	#endregion

	#region Methods
 
	/// <summary>
    /// Get ShoppingCartWishLists
    /// </summary>
	/// <param name="request">request</param>
	public async Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.ShoppingCartWishList>> GetShoppingCartWishListsAsync(COREAPPDATAREPOMODELSDOMAIN.GetShoppingCartWishListRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShoppingCartWishList), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var query = this.AsIQuerable();
		try
		{
			if (request.MatchExpression is not null)
			{
				query = query.Where(request.MatchExpression);
			}
			else
			{
				Logger.LogWarning($"{nameof(GetShoppingCartWishListsAsync)} - MatchExpression is null", nameof(ShoppingCartWishList), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataAccess);
			}
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetShoppingCartWishListsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetShoppingCartWishListsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetShoppingCartWishListsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetShoppingCartWishListsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShoppingCartWishList), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return query;
	}
 
	/// <summary>
    /// Merge ShoppingCartWishLists
    /// </summary>
	/// <param name="request">request</param>
	public virtual async Task<COREDOMAINDATAMODELS.MergeResult> MergeShoppingCartWishListsAsync(COREAPPDATAREPOMODELSDOMAIN.MergeShoppingCartWishListRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShoppingCartWishList), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var mergeResult = new COREDOMAINDATAMODELS.MergeResult();
		try
		{
			foreach (var shoppingCartWishList in request.ShoppingCartWishLists!)
			{
				Logger.LogDiagnosticsInfo($"Item State:{shoppingCartWishList.ItemState}", nameof(ShoppingCartWishList));

				var result = await this.MergeInternalAsync(shoppingCartWishList).ConfigureAwait(false);
				mergeResult += result;
			}

			Logger.LogDiagnosticsInfo($"InsertCount Count:{mergeResult.InsertCount}, UpdateCount Count:{mergeResult.UpdateCount}, DeleteCount Count:{mergeResult.DeleteCount}", nameof(ShoppingCartWishList));
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeShoppingCartWishListsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeShoppingCartWishListsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeShoppingCartWishListsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeShoppingCartWishListsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShoppingCartWishList), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return mergeResult;
	}
	/// <summary>
    /// Merges the internal asynchronous.
    /// </summary>
	/// <param name="shoppingCartWishList">The shoppingCartWishList.</param>
    /// <returns></returns>
	internal async Task<COREDOMAINDATAMODELS.MergeResult> MergeInternalAsync(COREDOMAINDATAMODELSDOMAIN.ShoppingCartWishList shoppingCartWishList)
	{
		var mergeResult = await this.MergeAsync(shoppingCartWishList);
		return mergeResult;
	}

	

	#endregion
}
