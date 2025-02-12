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
/// Represents ProductLanguage Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The ProductLanguage Infrastructure DataRepositories (DOTNET090000).
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
	/// Get ProductLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetProductLanguageResponse> GetProductLanguagesAsync(COREAPPDATAREPOMODELSDOMAIN.GetProductLanguageRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetProductLanguageResponse response = new();

		try
		{
			var productLanguages = await this._productLanguage.GetProductLanguagesAsync(request).ConfigureAwait(false);
			response.ProductLanguages = productLanguages;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetProductLanguagesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge ProductLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeProductLanguageResponse> MergeProductLanguagesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeProductLanguageRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeProductLanguageResponse response = new();

		try
		{
			var mergeResult = await this._productLanguage.MergeProductLanguagesAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeProductLanguagesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves ProductRepository.
	/// </summary>
	public async Task SaveProductLanguageAsync()
	{
		Logger.LogInfo($"{nameof(SaveProductLanguageAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _productLanguage.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveProductLanguageAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveProductLanguageAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		}
	}


	#endregion

}
