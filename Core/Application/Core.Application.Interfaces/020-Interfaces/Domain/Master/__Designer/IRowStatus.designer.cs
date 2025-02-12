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
/// Represents RowStatus class.
/// </summary>
/// <remarks>
/// The RowStatus class.
/// </remarks>
public partial interface IMaster 
{
	#region Methods

	/// <summary>
	/// Get RowStatuses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetRowStatusResponse> GetRowStatusesAsync(COREAPPDATAMODELSDOMAIN.GetRowStatusRequest request);

	/// <summary>
	/// Merge RowStatuses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeRowStatusResponse> MergeRowStatusesAsync(COREAPPDATAMODELSDOMAIN.MergeRowStatusRequest request);

	/// <summary>
	/// Decrypts RowStatuses data
	/// </summary>
	/// <param name="rowStatusUId">rowStatus id</param>
	/// <returns></returns>
	Task DecryptRowStatusesAsync(Guid rowStatusUId);

	#endregion
}
