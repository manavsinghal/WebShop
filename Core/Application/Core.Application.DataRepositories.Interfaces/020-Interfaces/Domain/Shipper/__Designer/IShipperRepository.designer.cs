#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion  

namespace Accenture.WebShop.Core.Application.DataRepositories.Interfaces.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents IShipperRepository Core Application DataRepositories Interfaces (DOTNET090000).
/// </summary>
/// <remarks>
/// The IShipperRepository Core Application DataRepositories Interfaces (DOTNET090000).
/// </remarks>
public partial interface IShipperRepository : COREAPPDREPOINTERFACES.IRepository
{
	#region Properties

	#endregion

	#region Methods

	/// <summary>
	/// Get Shippers
	/// </summary>
	/// <param name="request">request</param>
	/// <returns></returns>
	Task<COREAPPDATAREPOMODELSDOMAIN.GetShipperResponse> GetShippersAsync(COREAPPDATAREPOMODELSDOMAIN.GetShipperRequest request);

	/// <summary>
	/// Merge Shippers
	/// </summary>
	/// <param name="request">request</param>
	/// <returns></returns>
	Task<COREAPPDATAREPOMODELSDOMAIN.MergeShipperResponse> MergeShippersAsync(COREAPPDATAREPOMODELSDOMAIN.MergeShipperRequest request);

	/// <summary>
	/// Save Shippers
	/// </summary>
	/// <returns></returns>
	Task SaveShipperAsync();

	#endregion

}
