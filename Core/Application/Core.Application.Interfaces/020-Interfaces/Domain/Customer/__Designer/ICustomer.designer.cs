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
/// Represents Customer class.
/// </summary>
/// <remarks>
/// The Customer class.
/// </remarks>
public partial interface ICustomer 
{
	#region Methods

	/// <summary>
	/// Get Customers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetCustomerResponse> GetCustomersAsync(COREAPPDATAMODELSDOMAIN.GetCustomerRequest request);

	/// <summary>
	/// Merge Customers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeCustomerResponse> MergeCustomersAsync(COREAPPDATAMODELSDOMAIN.MergeCustomerRequest request);

	/// <summary>
	/// Decrypts Customers data
	/// </summary>
	/// <param name="customerUId">customer id</param>
	/// <returns></returns>
	Task DecryptCustomersAsync(Guid customerUId);

	#endregion
}
