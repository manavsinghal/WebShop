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
/// Represents MasterListItem class.
/// </summary>
/// <remarks>
/// The MasterListItem class.
/// </remarks>
public partial interface IMaster 
{
	#region Methods

	/// <summary>
	/// Get MasterListItems
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetMasterListItemResponse> GetMasterListItemsAsync(COREAPPDATAMODELSDOMAIN.GetMasterListItemRequest request);

	/// <summary>
	/// Merge MasterListItems
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeMasterListItemResponse> MergeMasterListItemsAsync(COREAPPDATAMODELSDOMAIN.MergeMasterListItemRequest request);

	/// <summary>
	/// Decrypts MasterListItems data
	/// </summary>
	/// <param name="masterListItemUId">masterListItem id</param>
	/// <returns></returns>
	Task DecryptMasterListItemsAsync(Guid masterListItemUId);

	#endregion
}
