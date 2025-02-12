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
/// Represents ShipperAddress class.
/// </summary>
/// <remarks>
/// The ShipperAddress class.
/// </remarks>
public partial interface IShipper 
{
	#region Methods

	/// <summary>
	/// Get ShipperAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetShipperAddressResponse> GetShipperAddressesAsync(COREAPPDATAMODELSDOMAIN.GetShipperAddressRequest request);

	/// <summary>
	/// Merge ShipperAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeShipperAddressResponse> MergeShipperAddressesAsync(COREAPPDATAMODELSDOMAIN.MergeShipperAddressRequest request);

	/// <summary>
	/// Decrypts ShipperAddresses data
	/// </summary>
	/// <param name="shipperAddressUId">shipperAddress id</param>
	/// <returns></returns>
	Task DecryptShipperAddressesAsync(Guid shipperAddressUId);

	#endregion
}
