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
/// Represents Shipper class.
/// </summary>
/// <remarks>
/// The Shipper class.
/// </remarks>
public partial interface IShipper 
{
	#region Methods

	/// <summary>
	/// Get Shippers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetShipperResponse> GetShippersAsync(COREAPPDATAMODELSDOMAIN.GetShipperRequest request);

	/// <summary>
	/// Merge Shippers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeShipperResponse> MergeShippersAsync(COREAPPDATAMODELSDOMAIN.MergeShipperRequest request);

	/// <summary>
	/// Decrypts Shippers data
	/// </summary>
	/// <param name="shipperUId">shipper id</param>
	/// <returns></returns>
	Task DecryptShippersAsync(Guid shipperUId);

	#endregion
}
