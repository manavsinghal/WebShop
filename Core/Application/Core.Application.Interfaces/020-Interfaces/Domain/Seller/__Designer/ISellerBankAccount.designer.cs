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
/// Represents SellerBankAccount class.
/// </summary>
/// <remarks>
/// The SellerBankAccount class.
/// </remarks>
public partial interface ISeller 
{
	#region Methods

	/// <summary>
	/// Get SellerBankAccounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetSellerBankAccountResponse> GetSellerBankAccountsAsync(COREAPPDATAMODELSDOMAIN.GetSellerBankAccountRequest request);

	/// <summary>
	/// Merge SellerBankAccounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeSellerBankAccountResponse> MergeSellerBankAccountsAsync(COREAPPDATAMODELSDOMAIN.MergeSellerBankAccountRequest request);

	/// <summary>
	/// Decrypts SellerBankAccounts data
	/// </summary>
	/// <param name="sellerBankAccountUId">sellerBankAccount id</param>
	/// <returns></returns>
	Task DecryptSellerBankAccountsAsync(Guid sellerBankAccountUId);

	#endregion
}
