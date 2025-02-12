#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
 
#endregion

namespace Accenture.WebShop.Core.Application.DataEntities.Interfaces.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents ProductCategory Core Application DataEntities Interfaces (DOTNET090000).
/// </summary>
/// <remarks>
/// The ProductCategory Core Application DataEntities Interfaces (DOTNET090000).
/// </remarks>
public partial interface IProductCategory : COREAPPDENTINTERFACES.IEntity<COREDOMAINDATAMODELSDOMAIN.ProductCategory>
{
    #region Properties

    #endregion

    #region Methods

    /// <summary>
    /// Get ProductCategories
    /// </summary>
	/// <param name="request">request</param>
    Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.ProductCategory>> GetProductCategoriesAsync(COREAPPDATAREPOMODELSDOMAIN.GetProductCategoryRequest request);

    /// <summary>
    /// Merge ProductCategories
    /// </summary>
	/// <param name="request">request</param>
    Task<COREDOMAINDATAMODELS.MergeResult> MergeProductCategoriesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeProductCategoryRequest request);

    #endregion

}
