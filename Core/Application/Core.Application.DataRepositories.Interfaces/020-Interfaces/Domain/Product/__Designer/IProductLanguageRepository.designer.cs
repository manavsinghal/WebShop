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
/// Represents IProductRepository Core Application DataRepositories Interfaces (DOTNET090000).
/// </summary>
/// <remarks>
/// The IProductRepository Core Application DataRepositories Interfaces (DOTNET090000).
/// </remarks>
public partial interface IProductRepository : COREAPPDREPOINTERFACES.IRepository
{
	#region Properties

	#endregion

	#region Methods

	/// <summary>
	/// Get ProductLanguages
	/// </summary>
	/// <param name="request">request</param>
	/// <returns></returns>
	Task<COREAPPDATAREPOMODELSDOMAIN.GetProductLanguageResponse> GetProductLanguagesAsync(COREAPPDATAREPOMODELSDOMAIN.GetProductLanguageRequest request);

	/// <summary>
	/// Merge ProductLanguages
	/// </summary>
	/// <param name="request">request</param>
	/// <returns></returns>
	Task<COREAPPDATAREPOMODELSDOMAIN.MergeProductLanguageResponse> MergeProductLanguagesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeProductLanguageRequest request);

	/// <summary>
	/// Save ProductLanguages
	/// </summary>
	/// <returns></returns>
	Task SaveProductLanguageAsync();

	#endregion

}
