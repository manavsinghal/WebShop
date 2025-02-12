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
/// Represents ShoppingCart class.
/// </summary>
/// <remarks>
/// The ShoppingCart class.
/// </remarks>
public partial interface IShoppingCart 
{
	#region Methods

	/// <summary>
	/// Get ShoppingCarts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetShoppingCartResponse> GetShoppingCartsAsync(COREAPPDATAMODELSDOMAIN.GetShoppingCartRequest request);

	/// <summary>
	/// Merge ShoppingCarts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeShoppingCartResponse> MergeShoppingCartsAsync(COREAPPDATAMODELSDOMAIN.MergeShoppingCartRequest request);

	/// <summary>
	/// Decrypts ShoppingCarts data
	/// </summary>
	/// <param name="shoppingCartUId">shoppingCart id</param>
	/// <returns></returns>
	Task DecryptShoppingCartsAsync(Guid shoppingCartUId);

	#endregion
}
