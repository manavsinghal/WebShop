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
/// Represents ProductLanguage class.
/// </summary>
/// <remarks>
/// The ProductLanguage class.
/// </remarks>
public partial interface IProduct 
{
	#region Methods

	/// <summary>
	/// Get ProductLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetProductLanguageResponse> GetProductLanguagesAsync(COREAPPDATAMODELSDOMAIN.GetProductLanguageRequest request);

	/// <summary>
	/// Merge ProductLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeProductLanguageResponse> MergeProductLanguagesAsync(COREAPPDATAMODELSDOMAIN.MergeProductLanguageRequest request);

	/// <summary>
	/// Decrypts ProductLanguages data
	/// </summary>
	/// <param name="productLanguageUId">productLanguage id</param>
	/// <returns></returns>
	Task DecryptProductLanguagesAsync(Guid productLanguageUId);

	#endregion
}
