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
/// Represents ShipperBankAccount class.
/// </summary>
/// <remarks>
/// The ShipperBankAccount class.
/// </remarks>
public partial interface IShipper 
{
	#region Methods

	/// <summary>
	/// Get ShipperBankAccounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetShipperBankAccountResponse> GetShipperBankAccountsAsync(COREAPPDATAMODELSDOMAIN.GetShipperBankAccountRequest request);

	/// <summary>
	/// Merge ShipperBankAccounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeShipperBankAccountResponse> MergeShipperBankAccountsAsync(COREAPPDATAMODELSDOMAIN.MergeShipperBankAccountRequest request);

	/// <summary>
	/// Decrypts ShipperBankAccounts data
	/// </summary>
	/// <param name="shipperBankAccountUId">shipperBankAccount id</param>
	/// <returns></returns>
	Task DecryptShipperBankAccountsAsync(Guid shipperBankAccountUId);

	#endregion
}
