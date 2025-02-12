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
/// Represents Country Infrastructure DataEntities SqlServer (DOTNET090000).
/// </summary>
/// <remarks>
/// The Country Infrastructure DataEntities SqlServer (DOTNET090000).
/// </remarks>
public partial class Country : INFRADATAENTITYMSSQL.Entity<COREDOMAINDATAMODELSDOMAIN.Country>, COREAPPDENTINTERFACESDOMAIN.ICountry
{	
    #region Fields

	private readonly COREAPPDENTINTERFACESDOMAIN.ICountryLanguage _countryLanguage; 

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
    /// Initializes a new instance of the CountryController class
    /// </summary>
	/// <param name="countryLanguage">countryLanguage</param>
	/// <param name="dbContext">dbContext</param>
	/// <param name="logger">logger</param>	
	/// <param name="messageHub"></param>
	public Country(COREAPPDENTINTERFACESDOMAIN.ICountryLanguage countryLanguage
					, DomainDbContext dbContext
					, DomainQueryDbContext queryDbContext 
					, MSLOGGING.ILogger<Country> logger
					, MESSAGEHUBINTERFACES.IMessageHub messageHub
					) : base(dbContext, queryDbContext, logger, messageHub)
	{
		this._countryLanguage = countryLanguage;
	}

	#endregion

	#region Methods
 
	/// <summary>
    /// Get Countries
    /// </summary>
	/// <param name="request">request</param>
	public async Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.Country>> GetCountriesAsync(COREAPPDATAREPOMODELSDOMAIN.GetCountryRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetCountriesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Country), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var query = this.AsIQuerable();
		try
		{
			if (request.MatchExpression is not null)
			{
				query = query.Where(request.MatchExpression);
			}
			else
			{
				Logger.LogWarning($"{nameof(GetCountriesAsync)} - MatchExpression is null", nameof(Country), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataAccess);
			}
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetCountriesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetCountriesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetCountriesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetCountriesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCountriesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Country), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return query;
	}
 
	/// <summary>
    /// Merge Countries
    /// </summary>
	/// <param name="request">request</param>
	public virtual async Task<COREDOMAINDATAMODELS.MergeResult> MergeCountriesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeCountryRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCountriesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Country), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var mergeResult = new COREDOMAINDATAMODELS.MergeResult();
		try
		{
			foreach (var country in request.Countries!)
			{
				Logger.LogDiagnosticsInfo($"Item State:{country.ItemState}", nameof(Country));

				var result = await this.MergeInternalAsync(country).ConfigureAwait(false);
				mergeResult += result;
			}

			Logger.LogDiagnosticsInfo($"InsertCount Count:{mergeResult.InsertCount}, UpdateCount Count:{mergeResult.UpdateCount}, DeleteCount Count:{mergeResult.DeleteCount}", nameof(Country));
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeCountriesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeCountriesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeCountriesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeCountriesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCountriesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Country), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return mergeResult;
	}
	/// <summary>
    /// Merges the internal asynchronous.
    /// </summary>
	/// <param name="country">The country.</param>
    /// <returns></returns>
	internal async Task<COREDOMAINDATAMODELS.MergeResult> MergeInternalAsync(COREDOMAINDATAMODELSDOMAIN.Country country)
	{
		var mergeResult = await this.MergeAsync(country);
		this.MergeChildEntities(country);
		return mergeResult;
	}

	/// <summary>
    /// Merges the Child entities.
    /// </summary>
	/// <param name="country">The country.</param>
    /// <returns></returns>
	internal void MergeChildEntities(COREDOMAINDATAMODELSDOMAIN.Country country)
	{
		if (country != null)
		{
			if (country.CountryLanguages != null && country.CountryLanguages.Any())
			{
				var mergeCountryLanguageRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeCountryLanguageRequest()
				{
					CountryLanguages = country.CountryLanguages
				};
				this._countryLanguage.MergeCountryLanguagesAsync(mergeCountryLanguageRequest);
			}

		}
	}
	

	#endregion
}
