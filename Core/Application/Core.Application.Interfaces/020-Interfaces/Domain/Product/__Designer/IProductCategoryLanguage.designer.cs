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
/// Represents ProductCategoryLanguage class.
/// </summary>
/// <remarks>
/// The ProductCategoryLanguage class.
/// </remarks>
public partial interface IProduct 
{
	#region Methods

	/// <summary>
	/// Get ProductCategoryLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetProductCategoryLanguageResponse> GetProductCategoryLanguagesAsync(COREAPPDATAMODELSDOMAIN.GetProductCategoryLanguageRequest request);

	/// <summary>
	/// Merge ProductCategoryLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeProductCategoryLanguageResponse> MergeProductCategoryLanguagesAsync(COREAPPDATAMODELSDOMAIN.MergeProductCategoryLanguageRequest request);

	/// <summary>
	/// Decrypts ProductCategoryLanguages data
	/// </summary>
	/// <param name="productCategoryLanguageUId">productCategoryLanguage id</param>
	/// <returns></returns>
	Task DecryptProductCategoryLanguagesAsync(Guid productCategoryLanguageUId);

	#endregion
}
