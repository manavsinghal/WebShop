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
/// Represents Language Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The Language Infrastructure DataRepositories (DOTNET090000).
/// </remarks>
public partial class MasterRepository : INFRADATAREPO.DataRepositoryBase, COREAPPDREPOINTERFACESDOMAIN.IMasterRepository
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	#endregion

	#region Methods

	/// <summary>
	/// Get Languages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetLanguageResponse> GetLanguagesAsync(COREAPPDATAREPOMODELSDOMAIN.GetLanguageRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MasterRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetLanguageResponse response = new();

		try
		{
			var languages = await this._language.GetLanguagesAsync(request).ConfigureAwait(false);
			response.Languages = languages;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetLanguagesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MasterRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge Languages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeLanguageResponse> MergeLanguagesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeLanguageRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MasterRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeLanguageResponse response = new();

		try
		{
			var mergeResult = await this._language.MergeLanguagesAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeLanguagesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MasterRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves MasterRepository.
	/// </summary>
	public async Task SaveLanguageAsync()
	{
		Logger.LogInfo($"{nameof(SaveLanguageAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MasterRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _language.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveLanguageAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveLanguageAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MasterRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		}
	}


	#endregion

}
