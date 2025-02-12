#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion  

namespace Accenture.WebShop.Infrastructure.DataRepositories.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents ProductCategory Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The ProductCategory Infrastructure DataRepositories (DOTNET090000).
/// </remarks>
public partial class ProductRepository : INFRADATAREPO.DataRepositoryBase, COREAPPDREPOINTERFACESDOMAIN.IProductRepository
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	#endregion

	#region Methods

	/// <summary>
	/// Get ProductCategories
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetProductCategoryResponse> GetProductCategoriesAsync(COREAPPDATAREPOMODELSDOMAIN.GetProductCategoryRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetProductCategoryResponse response = new();

		try
		{
			var productCategories = await this._productCategory.GetProductCategoriesAsync(request).ConfigureAwait(false);
			response.ProductCategories = productCategories;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetProductCategoriesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge ProductCategories
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeProductCategoryResponse> MergeProductCategoriesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeProductCategoryRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeProductCategoryResponse response = new();

		try
		{
			var mergeResult = await this._productCategory.MergeProductCategoriesAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeProductCategoriesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves ProductRepository.
	/// </summary>
	public async Task SaveProductCategoryAsync()
	{
		Logger.LogInfo($"{nameof(SaveProductCategoryAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _productCategory.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveProductCategoryAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveProductCategoryAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		}
	}


	#endregion

}
