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
/// Represents Product class.
/// </summary>
/// <remarks>
/// The Product class.
/// </remarks>
public partial interface IProduct 
{
	#region Methods

	/// <summary>
	/// Get Products
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetProductResponse> GetProductsAsync(COREAPPDATAMODELSDOMAIN.GetProductRequest request);

	/// <summary>
	/// Merge Products
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeProductResponse> MergeProductsAsync(COREAPPDATAMODELSDOMAIN.MergeProductRequest request);

	/// <summary>
	/// Decrypts Products data
	/// </summary>
	/// <param name="productUId">product id</param>
	/// <returns></returns>
	Task DecryptProductsAsync(Guid productUId);

	#endregion
}
