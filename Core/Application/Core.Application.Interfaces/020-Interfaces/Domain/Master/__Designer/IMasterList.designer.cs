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
/// Represents MasterList class.
/// </summary>
/// <remarks>
/// The MasterList class.
/// </remarks>
public partial interface IMaster 
{
	#region Methods

	/// <summary>
	/// Get MasterLists
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetMasterListResponse> GetMasterListsAsync(COREAPPDATAMODELSDOMAIN.GetMasterListRequest request);

	/// <summary>
	/// Merge MasterLists
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeMasterListResponse> MergeMasterListsAsync(COREAPPDATAMODELSDOMAIN.MergeMasterListRequest request);

	/// <summary>
	/// Get MasterListItemsBYCode
	/// </summary>
	/// <param name="masterListCode"></param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetMasterListItemResponse> GetMasterListItemsByCode(String masterListCode);

		/// <summary>
	/// Decrypts MasterLists data
	/// </summary>
	/// <param name="masterListUId">masterList id</param>
	/// <returns></returns>
	Task DecryptMasterListsAsync(Guid masterListUId);

	#endregion
}
