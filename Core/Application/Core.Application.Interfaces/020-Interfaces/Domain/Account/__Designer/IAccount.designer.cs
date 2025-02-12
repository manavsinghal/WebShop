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
/// Represents Account class.
/// </summary>
/// <remarks>
/// The Account class.
/// </remarks>
public partial interface IAccount 
{
	#region Methods

	/// <summary>
	/// Get Accounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetAccountResponse> GetAccountsAsync(COREAPPDATAMODELSDOMAIN.GetAccountRequest request);

	/// <summary>
	/// Merge Accounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeAccountResponse> MergeAccountsAsync(COREAPPDATAMODELSDOMAIN.MergeAccountRequest request);

	/// <summary>
	/// Get current Account
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetAccountResponse> GetCurrentAccountAsync(COREAPPDATAMODELSDOMAIN.GetAccountRequest request);

	/// <summary>
	/// Decrypts Accounts data
	/// </summary>
	/// <param name="accountUId">account id</param>
	/// <returns></returns>
	Task DecryptAccountsAsync(Guid accountUId);

	#endregion
}
