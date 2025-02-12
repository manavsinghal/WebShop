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
/// Represents ShoppingCartWishList Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The ShoppingCartWishList Infrastructure DataRepositories (DOTNET090000).
/// </remarks>
public partial class ShoppingCartRepository : INFRADATAREPO.DataRepositoryBase, COREAPPDREPOINTERFACESDOMAIN.IShoppingCartRepository
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	#endregion

	#region Methods

	/// <summary>
	/// Get ShoppingCartWishLists
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetShoppingCartWishListResponse> GetShoppingCartWishListsAsync(COREAPPDATAREPOMODELSDOMAIN.GetShoppingCartWishListRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShoppingCartRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetShoppingCartWishListResponse response = new();

		try
		{
			var shoppingCartWishLists = await this._shoppingCartWishList.GetShoppingCartWishListsAsync(request).ConfigureAwait(false);
			response.ShoppingCartWishLists = shoppingCartWishLists;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetShoppingCartWishListsAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShoppingCartRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge ShoppingCartWishLists
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeShoppingCartWishListResponse> MergeShoppingCartWishListsAsync(COREAPPDATAREPOMODELSDOMAIN.MergeShoppingCartWishListRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShoppingCartRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeShoppingCartWishListResponse response = new();

		try
		{
			var mergeResult = await this._shoppingCartWishList.MergeShoppingCartWishListsAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeShoppingCartWishListsAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShoppingCartWishListsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShoppingCartRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves ShoppingCartRepository.
	/// </summary>
	public async Task SaveShoppingCartWishListAsync()
	{
		Logger.LogInfo($"{nameof(SaveShoppingCartWishListAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShoppingCartRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _shoppingCartWishList.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveShoppingCartWishListAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveShoppingCartWishListAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShoppingCartRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		}
	}


	#endregion

}
