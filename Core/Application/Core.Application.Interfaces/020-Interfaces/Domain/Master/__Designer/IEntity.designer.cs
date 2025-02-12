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
/// Represents Entity class.
/// </summary>
/// <remarks>
/// The Entity class.
/// </remarks>
public partial interface IMaster 
{
	#region Methods

	/// <summary>
	/// Get Entities
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetEntityResponse> GetEntitiesAsync(COREAPPDATAMODELSDOMAIN.GetEntityRequest request);

	/// <summary>
	/// Merge Entities
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeEntityResponse> MergeEntitiesAsync(COREAPPDATAMODELSDOMAIN.MergeEntityRequest request);

	/// <summary>
	/// Decrypts Entities data
	/// </summary>
	/// <param name="entityUId">entity id</param>
	/// <returns></returns>
	Task DecryptEntitiesAsync(Guid entityUId);

	#endregion
}
