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
/// Represents ShoppingCart Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The ShoppingCart Infrastructure DataRepositories (DOTNET090000).
/// </remarks>
public partial class ShoppingCartRepository : INFRADATAREPO.DataRepositoryBase, COREAPPDREPOINTERFACESDOMAIN.IShoppingCartRepository
{
	#region Fields

	private Boolean disposedValue;

	private readonly COREAPPDENTINTERFACESDOMAIN.IShoppingCart _shoppingCart;

	private readonly COREAPPDENTINTERFACESDOMAIN.IShoppingCartWishList _shoppingCartWishList;

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the ShoppingCartController class
	/// </summary>	 
	/// <param name="shoppingCart">The shoppingCart.</param>	 
	/// <param name="shoppingCartWishList">The shoppingCartWishList.</param>	 
	/// <param name="logger">logger</param>
	/// <param name="messageHub">messageHub.</param>
	public ShoppingCartRepository(COREAPPDENTINTERFACESDOMAIN.IShoppingCart shoppingCart, 
							COREAPPDENTINTERFACESDOMAIN.IShoppingCartWishList shoppingCartWishList, 
							MSLOGGING.ILogger<ShoppingCartRepository> logger ,MESSAGEHUBINTERFACES.IMessageHub messageHub
							) : base(logger, messageHub)
	{
		this._shoppingCart = shoppingCart;
		this._shoppingCartWishList = shoppingCartWishList;
	}

	#endregion

	#region Methods

	/// <summary>
	/// Get ShoppingCarts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetShoppingCartResponse> GetShoppingCartsAsync(COREAPPDATAREPOMODELSDOMAIN.GetShoppingCartRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetShoppingCartsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShoppingCartRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetShoppingCartResponse response = new();

		try
		{
			var shoppingCarts = await this._shoppingCart.GetShoppingCartsAsync(request).ConfigureAwait(false);
			response.ShoppingCarts = shoppingCarts;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetShoppingCartsAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShoppingCartsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShoppingCartRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge ShoppingCarts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeShoppingCartResponse> MergeShoppingCartsAsync(COREAPPDATAREPOMODELSDOMAIN.MergeShoppingCartRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShoppingCartsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShoppingCartRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeShoppingCartResponse response = new();

		try
		{
			var mergeResult = await this._shoppingCart.MergeShoppingCartsAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeShoppingCartsAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShoppingCartsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShoppingCartRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves ShoppingCartRepository.
	/// </summary>
	public async Task SaveShoppingCartAsync()
	{
		Logger.LogInfo($"{nameof(SaveShoppingCartAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShoppingCartRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _shoppingCart.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveShoppingCartAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveShoppingCartAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShoppingCartRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
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
