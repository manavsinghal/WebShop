#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion  

namespace Accenture.WebShop.Infrastructure.DataEntities.SqlServer.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents ProductCategory Infrastructure DataEntities SqlServer (DOTNET090000).
/// </summary>
/// <remarks>
/// The ProductCategory Infrastructure DataEntities SqlServer (DOTNET090000).
/// </remarks>
public partial class ProductCategory : INFRADATAENTITYMSSQL.Entity<COREDOMAINDATAMODELSDOMAIN.ProductCategory>, COREAPPDENTINTERFACESDOMAIN.IProductCategory
{	
    #region Fields

	private readonly COREAPPDENTINTERFACESDOMAIN.IProductCategoryLanguage _productCategoryLanguage; 

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
    /// Initializes a new instance of the ProductCategoryController class
    /// </summary>
	/// <param name="productCategoryLanguage">productCategoryLanguage</param>
	/// <param name="dbContext">dbContext</param>
	/// <param name="logger">logger</param>	
	/// <param name="messageHub"></param>
	public ProductCategory(COREAPPDENTINTERFACESDOMAIN.IProductCategoryLanguage productCategoryLanguage
					, DomainDbContext dbContext
					, DomainQueryDbContext queryDbContext 
					, MSLOGGING.ILogger<ProductCategory> logger
					, MESSAGEHUBINTERFACES.IMessageHub messageHub
					) : base(dbContext, queryDbContext, logger, messageHub)
	{
		this._productCategoryLanguage = productCategoryLanguage;
	}

	#endregion

	#region Methods
 
	/// <summary>
    /// Get ProductCategories
    /// </summary>
	/// <param name="request">request</param>
	public async Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.ProductCategory>> GetProductCategoriesAsync(COREAPPDATAREPOMODELSDOMAIN.GetProductCategoryRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductCategory), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var query = this.AsIQuerable();
		try
		{
			if (request.MatchExpression is not null)
			{
				query = query.Where(request.MatchExpression);
			}
			else
			{
				Logger.LogWarning($"{nameof(GetProductCategoriesAsync)} - MatchExpression is null", nameof(ProductCategory), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataAccess);
			}
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetProductCategoriesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetProductCategoriesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetProductCategoriesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetProductCategoriesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductCategory), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return query;
	}
 
	/// <summary>
    /// Merge ProductCategories
    /// </summary>
	/// <param name="request">request</param>
	public virtual async Task<COREDOMAINDATAMODELS.MergeResult> MergeProductCategoriesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeProductCategoryRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductCategory), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var mergeResult = new COREDOMAINDATAMODELS.MergeResult();
		try
		{
			foreach (var productCategory in request.ProductCategories!)
			{
				Logger.LogDiagnosticsInfo($"Item State:{productCategory.ItemState}", nameof(ProductCategory));

				var result = await this.MergeInternalAsync(productCategory).ConfigureAwait(false);
				mergeResult += result;
			}

			Logger.LogDiagnosticsInfo($"InsertCount Count:{mergeResult.InsertCount}, UpdateCount Count:{mergeResult.UpdateCount}, DeleteCount Count:{mergeResult.DeleteCount}", nameof(ProductCategory));
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeProductCategoriesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeProductCategoriesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeProductCategoriesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeProductCategoriesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductCategoriesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductCategory), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return mergeResult;
	}
	/// <summary>
    /// Merges the internal asynchronous.
    /// </summary>
	/// <param name="productCategory">The productCategory.</param>
    /// <returns></returns>
	internal async Task<COREDOMAINDATAMODELS.MergeResult> MergeInternalAsync(COREDOMAINDATAMODELSDOMAIN.ProductCategory productCategory)
	{
		var mergeResult = await this.MergeAsync(productCategory);
		this.MergeChildEntities(productCategory);
		return mergeResult;
	}

	/// <summary>
    /// Merges the Child entities.
    /// </summary>
	/// <param name="productCategory">The productCategory.</param>
    /// <returns></returns>
	internal void MergeChildEntities(COREDOMAINDATAMODELSDOMAIN.ProductCategory productCategory)
	{
		if (productCategory != null)
		{
			if (productCategory.ProductCategoryLanguages != null && productCategory.ProductCategoryLanguages.Any())
			{
				var mergeProductCategoryLanguageRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeProductCategoryLanguageRequest()
				{
					ProductCategoryLanguages = productCategory.ProductCategoryLanguages
				};
				this._productCategoryLanguage.MergeProductCategoryLanguagesAsync(mergeProductCategoryLanguageRequest);
			}

		}
	}
	

	#endregion
}
