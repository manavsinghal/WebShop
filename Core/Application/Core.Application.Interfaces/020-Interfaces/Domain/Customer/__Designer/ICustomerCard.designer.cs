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
/// Represents CustomerCard class.
/// </summary>
/// <remarks>
/// The CustomerCard class.
/// </remarks>
public partial interface ICustomer 
{
	#region Methods

	/// <summary>
	/// Get CustomerCards
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetCustomerCardResponse> GetCustomerCardsAsync(COREAPPDATAMODELSDOMAIN.GetCustomerCardRequest request);

	/// <summary>
	/// Merge CustomerCards
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeCustomerCardResponse> MergeCustomerCardsAsync(COREAPPDATAMODELSDOMAIN.MergeCustomerCardRequest request);

	/// <summary>
	/// Decrypts CustomerCards data
	/// </summary>
	/// <param name="customerCardUId">customerCard id</param>
	/// <returns></returns>
	Task DecryptCustomerCardsAsync(Guid customerCardUId);

	#endregion
}
