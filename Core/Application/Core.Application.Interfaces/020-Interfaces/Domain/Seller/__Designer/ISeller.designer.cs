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
/// Represents Seller class.
/// </summary>
/// <remarks>
/// The Seller class.
/// </remarks>
public partial interface ISeller 
{
	#region Methods

	/// <summary>
	/// Get Sellers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetSellerResponse> GetSellersAsync(COREAPPDATAMODELSDOMAIN.GetSellerRequest request);

	/// <summary>
	/// Merge Sellers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeSellerResponse> MergeSellersAsync(COREAPPDATAMODELSDOMAIN.MergeSellerRequest request);

	/// <summary>
	/// Decrypts Sellers data
	/// </summary>
	/// <param name="sellerUId">seller id</param>
	/// <returns></returns>
	Task DecryptSellersAsync(Guid sellerUId);

	#endregion
}
