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
/// Represents Country Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The Country Infrastructure DataRepositories (DOTNET090000).
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
	/// Get Countries
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetCountryResponse> GetCountriesAsync(COREAPPDATAREPOMODELSDOMAIN.GetCountryRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetCountriesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MasterRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetCountryResponse response = new();

		try
		{
			var countries = await this._country.GetCountriesAsync(request).ConfigureAwait(false);
			response.Countries = countries;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetCountriesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCountriesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MasterRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge Countries
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeCountryResponse> MergeCountriesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeCountryRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCountriesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MasterRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeCountryResponse response = new();

		try
		{
			var mergeResult = await this._country.MergeCountriesAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeCountriesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCountriesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MasterRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves MasterRepository.
	/// </summary>
	public async Task SaveCountryAsync()
	{
		Logger.LogInfo($"{nameof(SaveCountryAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MasterRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _country.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveCountryAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveCountryAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MasterRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		}
	}


	#endregion

}
