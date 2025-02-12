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
/// Represents ProductCategoryLanguage Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The ProductCategoryLanguage Infrastructure DataRepositories (DOTNET090000).
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
	/// Get ProductCategoryLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetProductCategoryLanguageResponse> GetProductCategoryLanguagesAsync(COREAPPDATAREPOMODELSDOMAIN.GetProductCategoryLanguageRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetProductCategoryLanguageResponse response = new();

		try
		{
			var productCategoryLanguages = await this._productCategoryLanguage.GetProductCategoryLanguagesAsync(request).ConfigureAwait(false);
			response.ProductCategoryLanguages = productCategoryLanguages;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetProductCategoryLanguagesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge ProductCategoryLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeProductCategoryLanguageResponse> MergeProductCategoryLanguagesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeProductCategoryLanguageRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeProductCategoryLanguageResponse response = new();

		try
		{
			var mergeResult = await this._productCategoryLanguage.MergeProductCategoryLanguagesAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeProductCategoryLanguagesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductCategoryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves ProductRepository.
	/// </summary>
	public async Task SaveProductCategoryLanguageAsync()
	{
		Logger.LogInfo($"{nameof(SaveProductCategoryLanguageAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _productCategoryLanguage.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveProductCategoryLanguageAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveProductCategoryLanguageAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		}
	}


	#endregion

}
