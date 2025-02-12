#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion

namespace Accenture.WebShop.Core.Application.Interfaces.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents ShoppingCartWishList class.
/// </summary>
/// <remarks>
/// The ShoppingCartWishList class.
/// </remarks>
public partial interface IShoppingCart 
{
	#region Methods

	/// <summary>
	/// Get ShoppingCartWishLists
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetShoppingCartWishListResponse> GetShoppingCartWishListsAsync(COREAPPDATAMODELSDOMAIN.GetShoppingCartWishListRequest request);

	/// <summary>
	/// Merge ShoppingCartWishLists
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeShoppingCartWishListResponse> MergeShoppingCartWishListsAsync(COREAPPDATAMODELSDOMAIN.MergeShoppingCartWishListRequest request);

	/// <summary>
	/// Decrypts ShoppingCartWishLists data
	/// </summary>
	/// <param name="shoppingCartWishListUId">shoppingCartWishList id</param>
	/// <returns></returns>
	Task DecryptShoppingCartWishListsAsync(Guid shoppingCartWishListUId);

	#endregion
}
