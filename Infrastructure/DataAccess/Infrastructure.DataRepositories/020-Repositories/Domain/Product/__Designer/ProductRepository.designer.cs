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
/// Represents Product Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The Product Infrastructure DataRepositories (DOTNET090000).
/// </remarks>
public partial class ProductRepository : INFRADATAREPO.DataRepositoryBase, COREAPPDREPOINTERFACESDOMAIN.IProductRepository
{
	#region Fields

	private Boolean disposedValue;

	private readonly COREAPPDENTINTERFACESDOMAIN.IProduct _product;

	private readonly COREAPPDENTINTERFACESDOMAIN.IProductCategory _productCategory;

	private readonly COREAPPDENTINTERFACESDOMAIN.IProductCategoryLanguage _productCategoryLanguage;

	private readonly COREAPPDENTINTERFACESDOMAIN.IProductLanguage _productLanguage;

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the ProductController class
	/// </summary>	 
	/// <param name="product">The product.</param>	 
	/// <param name="productCategory">The productCategory.</param>	 
	/// <param name="productCategoryLanguage">The productCategoryLanguage.</param>	 
	/// <param name="productLanguage">The productLanguage.</param>	 
	/// <param name="logger">logger</param>
	/// <param name="messageHub">messageHub.</param>
	public ProductRepository(COREAPPDENTINTERFACESDOMAIN.IProduct product, 
							COREAPPDENTINTERFACESDOMAIN.IProductCategory productCategory, 
							COREAPPDENTINTERFACESDOMAIN.IProductCategoryLanguage productCategoryLanguage, 
							COREAPPDENTINTERFACESDOMAIN.IProductLanguage productLanguage, 
							MSLOGGING.ILogger<ProductRepository> logger ,MESSAGEHUBINTERFACES.IMessageHub messageHub
							) : base(logger, messageHub)
	{
		this._product = product;
		this._productCategory = productCategory;
		this._productCategoryLanguage = productCategoryLanguage;
		this._productLanguage = productLanguage;
	}

	#endregion

	#region Methods

	/// <summary>
	/// Get Products
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetProductResponse> GetProductsAsync(COREAPPDATAREPOMODELSDOMAIN.GetProductRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetProductResponse response = new();

		try
		{
			var products = await this._product.GetProductsAsync(request).ConfigureAwait(false);
			response.Products = products;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetProductsAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge Products
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeProductResponse> MergeProductsAsync(COREAPPDATAREPOMODELSDOMAIN.MergeProductRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeProductResponse response = new();

		try
		{
			var mergeResult = await this._product.MergeProductsAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeProductsAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves ProductRepository.
	/// </summary>
	public async Task SaveProductAsync()
	{
		Logger.LogInfo($"{nameof(SaveProductAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _product.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveProductAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveProductAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		}
	}

	/// <summary>
	/// Represents Dispose Method.
	/// </summary>
	public void Dispose()
	{
		if (!disposedValue)
		{
			disposedValue = true;
		}
	}

	#endregion

}
