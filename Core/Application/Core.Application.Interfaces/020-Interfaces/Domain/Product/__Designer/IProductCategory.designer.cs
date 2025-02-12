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
/// Represents ProductCategory class.
/// </summary>
/// <remarks>
/// The ProductCategory class.
/// </remarks>
public partial interface IProduct 
{
	#region Methods

	/// <summary>
	/// Get ProductCategories
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetProductCategoryResponse> GetProductCategoriesAsync(COREAPPDATAMODELSDOMAIN.GetProductCategoryRequest request);

	/// <summary>
	/// Merge ProductCategories
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeProductCategoryResponse> MergeProductCategoriesAsync(COREAPPDATAMODELSDOMAIN.MergeProductCategoryRequest request);

	/// <summary>
	/// Decrypts ProductCategories data
	/// </summary>
	/// <param name="productCategoryUId">productCategory id</param>
	/// <returns></returns>
	Task DecryptProductCategoriesAsync(Guid productCategoryUId);

	#endregion
}
