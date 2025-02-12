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
/// Represents Product Infrastructure DataEntities SqlServer (DOTNET090000).
/// </summary>
/// <remarks>
/// The Product Infrastructure DataEntities SqlServer (DOTNET090000).
/// </remarks>
public partial class Product : INFRADATAENTITYMSSQL.Entity<COREDOMAINDATAMODELSDOMAIN.Product>, COREAPPDENTINTERFACESDOMAIN.IProduct
{	
    #region Fields

	private readonly COREAPPDENTINTERFACESDOMAIN.IProductLanguage _productLanguage; 

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
    /// Initializes a new instance of the ProductController class
    /// </summary>
	/// <param name="productLanguage">productLanguage</param>
	/// <param name="dbContext">dbContext</param>
	/// <param name="logger">logger</param>	
	/// <param name="messageHub"></param>
	public Product(COREAPPDENTINTERFACESDOMAIN.IProductLanguage productLanguage
					, DomainDbContext dbContext
					, DomainQueryDbContext queryDbContext 
					, MSLOGGING.ILogger<Product> logger
					, MESSAGEHUBINTERFACES.IMessageHub messageHub
					) : base(dbContext, queryDbContext, logger, messageHub)
	{
		this._productLanguage = productLanguage;
	}

	#endregion

	#region Methods
 
	/// <summary>
    /// Get Products
    /// </summary>
	/// <param name="request">request</param>
	public async Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.Product>> GetProductsAsync(COREAPPDATAREPOMODELSDOMAIN.GetProductRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Product), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var query = this.AsIQuerable();
		try
		{
			if (request.MatchExpression is not null)
			{
				query = query.Where(request.MatchExpression);
			}
			else
			{
				Logger.LogWarning($"{nameof(GetProductsAsync)} - MatchExpression is null", nameof(Product), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataAccess);
			}
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetProductsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetProductsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetProductsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetProductsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Product), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return query;
	}
 
	/// <summary>
    /// Merge Products
    /// </summary>
	/// <param name="request">request</param>
	public virtual async Task<COREDOMAINDATAMODELS.MergeResult> MergeProductsAsync(COREAPPDATAREPOMODELSDOMAIN.MergeProductRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Product), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var mergeResult = new COREDOMAINDATAMODELS.MergeResult();
		try
		{
			foreach (var product in request.Products!)
			{
				Logger.LogDiagnosticsInfo($"Item State:{product.ItemState}", nameof(Product));

				var result = await this.MergeInternalAsync(product).ConfigureAwait(false);
				mergeResult += result;
			}

			Logger.LogDiagnosticsInfo($"InsertCount Count:{mergeResult.InsertCount}, UpdateCount Count:{mergeResult.UpdateCount}, DeleteCount Count:{mergeResult.DeleteCount}", nameof(Product));
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeProductsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeProductsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeProductsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeProductsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Product), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return mergeResult;
	}
	/// <summary>
    /// Merges the internal asynchronous.
    /// </summary>
	/// <param name="product">The product.</param>
    /// <returns></returns>
	internal async Task<COREDOMAINDATAMODELS.MergeResult> MergeInternalAsync(COREDOMAINDATAMODELSDOMAIN.Product product)
	{
		var mergeResult = await this.MergeAsync(product);
		this.MergeChildEntities(product);
		return mergeResult;
	}

	/// <summary>
    /// Merges the Child entities.
    /// </summary>
	/// <param name="product">The product.</param>
    /// <returns></returns>
	internal void MergeChildEntities(COREDOMAINDATAMODELSDOMAIN.Product product)
	{
		if (product != null)
		{
			if (product.ProductLanguages != null && product.ProductLanguages.Any())
			{
				var mergeProductLanguageRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeProductLanguageRequest()
				{
					ProductLanguages = product.ProductLanguages
				};
				this._productLanguage.MergeProductLanguagesAsync(mergeProductLanguageRequest);
			}

		}
	}
	

	#endregion
}
