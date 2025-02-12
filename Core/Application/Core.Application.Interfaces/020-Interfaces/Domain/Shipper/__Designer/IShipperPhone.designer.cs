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
/// Represents ShipperPhone class.
/// </summary>
/// <remarks>
/// The ShipperPhone class.
/// </remarks>
public partial interface IShipper 
{
	#region Methods

	/// <summary>
	/// Get ShipperPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetShipperPhoneResponse> GetShipperPhonesAsync(COREAPPDATAMODELSDOMAIN.GetShipperPhoneRequest request);

	/// <summary>
	/// Merge ShipperPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeShipperPhoneResponse> MergeShipperPhonesAsync(COREAPPDATAMODELSDOMAIN.MergeShipperPhoneRequest request);

	/// <summary>
	/// Decrypts ShipperPhones data
	/// </summary>
	/// <param name="shipperPhoneUId">shipperPhone id</param>
	/// <returns></returns>
	Task DecryptShipperPhonesAsync(Guid shipperPhoneUId);

	#endregion
}
