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
/// Represents ProductCategoryLanguage Infrastructure DataEntities SqlServer (DOTNET090000).
/// </summary>
/// <remarks>
/// The ProductCategoryLanguage Infrastructure DataEntities SqlServer (DOTNET090000).
/// </remarks>
public partial class ProductCategoryLanguage : INFRADATAENTITYMSSQL.Entity<COREDOMAINDATAMODELSDOMAIN.ProductCategoryLanguage>, COREAPPDENTINTERFACESDOMAIN.IProductCategoryLanguage
{	
    #region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
    /// Initializes a new instance of the ProductCategoryLanguageController class
    /// </summary>
	/// <param name="dbContext">dbContext</param>
	/// <param name="logger">logger</param>	
	/// <param name="messageHub"></param>
	public ProductCategoryLanguage(DomainDbContext dbContext
					, DomainQueryDbContext queryDbContext 
					, MSLOGGING.ILogger<ProductCategoryLanguage> logger
					, MESSAGEHUBINTERFACES.IMessageHub messageHub
					) : base(dbContext, queryDbContext, logger, messageHub)
	{
	}

	#endregion

	#region Methods
 
	/// <summary>
    /// Get ProductCategoryLanguages
    /// </summary>
	/// <param name="request">request</param>
	public async Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.ProductCategoryLanguage>> GetProductCategoryLanguagesAsync(COREAPPDATAREPOMODELSDOMAIN.GetProductCategoryLanguageRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductCategoryLanguage), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var query = this.AsIQuerable();
		try
		{
			if (request.MatchExpression is not null)
			{
				query = query.Where(request.MatchExpression);
			}
			else
			{
				Logger.LogWarning($"{nameof(GetProductCategoryLanguagesAsync)} - MatchExpression is null", nameof(ProductCategoryLanguage), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataAccess);
			}
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetProductCategoryLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetProductCategoryLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetProductCategoryLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetProductCategoryLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductCategoryLanguage), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return query;
	}
 
	/// <summary>
    /// Merge ProductCategoryLanguages
    /// </summary>
	/// <param name="request">request</param>
	public virtual async Task<COREDOMAINDATAMODELS.MergeResult> MergeProductCategoryLanguagesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeProductCategoryLanguageRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductCategoryLanguage), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var mergeResult = new COREDOMAINDATAMODELS.MergeResult();
		try
		{
			foreach (var productCategoryLanguage in request.ProductCategoryLanguages!)
			{
				Logger.LogDiagnosticsInfo($"Item State:{productCategoryLanguage.ItemState}", nameof(ProductCategoryLanguage));

				var result = await this.MergeInternalAsync(productCategoryLanguage).ConfigureAwait(false);
				mergeResult += result;
			}

			Logger.LogDiagnosticsInfo($"InsertCount Count:{mergeResult.InsertCount}, UpdateCount Count:{mergeResult.UpdateCount}, DeleteCount Count:{mergeResult.DeleteCount}", nameof(ProductCategoryLanguage));
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeProductCategoryLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeProductCategoryLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeProductCategoryLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeProductCategoryLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductCategoryLanguage), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return mergeResult;
	}
	/// <summary>
    /// Merges the internal asynchronous.
    /// </summary>
	/// <param name="productCategoryLanguage">The productCategoryLanguage.</param>
    /// <returns></returns>
	internal async Task<COREDOMAINDATAMODELS.MergeResult> MergeInternalAsync(COREDOMAINDATAMODELSDOMAIN.ProductCategoryLanguage productCategoryLanguage)
	{
		var mergeResult = await this.MergeAsync(productCategoryLanguage);
		return mergeResult;
	}

	

	#endregion
}
