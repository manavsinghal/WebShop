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
/// Represents SellerAddress class.
/// </summary>
/// <remarks>
/// The SellerAddress class.
/// </remarks>
public partial interface ISeller 
{
	#region Methods

	/// <summary>
	/// Get SellerAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetSellerAddressResponse> GetSellerAddressesAsync(COREAPPDATAMODELSDOMAIN.GetSellerAddressRequest request);

	/// <summary>
	/// Merge SellerAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeSellerAddressResponse> MergeSellerAddressesAsync(COREAPPDATAMODELSDOMAIN.MergeSellerAddressRequest request);

	/// <summary>
	/// Decrypts SellerAddresses data
	/// </summary>
	/// <param name="sellerAddressUId">sellerAddress id</param>
	/// <returns></returns>
	Task DecryptSellerAddressesAsync(Guid sellerAddressUId);

	#endregion
}
