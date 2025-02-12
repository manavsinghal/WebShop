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
/// Represents AccountStatus class.
/// </summary>
/// <remarks>
/// The AccountStatus class.
/// </remarks>
public partial interface IAccount 
{
	#region Methods

	/// <summary>
	/// Get AccountStatuses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetAccountStatusResponse> GetAccountStatusesAsync(COREAPPDATAMODELSDOMAIN.GetAccountStatusRequest request);

	/// <summary>
	/// Merge AccountStatuses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeAccountStatusResponse> MergeAccountStatusesAsync(COREAPPDATAMODELSDOMAIN.MergeAccountStatusRequest request);

	/// <summary>
	/// Decrypts AccountStatuses data
	/// </summary>
	/// <param name="accountStatusUId">accountStatus id</param>
	/// <returns></returns>
	Task DecryptAccountStatusesAsync(Guid accountStatusUId);

	#endregion
}
