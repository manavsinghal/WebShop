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
/// Represents CustomerAddress class.
/// </summary>
/// <remarks>
/// The CustomerAddress class.
/// </remarks>
public partial interface ICustomer 
{
	#region Methods

	/// <summary>
	/// Get CustomerAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetCustomerAddressResponse> GetCustomerAddressesAsync(COREAPPDATAMODELSDOMAIN.GetCustomerAddressRequest request);

	/// <summary>
	/// Merge CustomerAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeCustomerAddressResponse> MergeCustomerAddressesAsync(COREAPPDATAMODELSDOMAIN.MergeCustomerAddressRequest request);

	/// <summary>
	/// Decrypts CustomerAddresses data
	/// </summary>
	/// <param name="customerAddressUId">customerAddress id</param>
	/// <returns></returns>
	Task DecryptCustomerAddressesAsync(Guid customerAddressUId);

	#endregion
}
