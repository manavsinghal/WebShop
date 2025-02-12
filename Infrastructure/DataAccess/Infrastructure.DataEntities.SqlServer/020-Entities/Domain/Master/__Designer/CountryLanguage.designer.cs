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
/// Represents CountryLanguage Infrastructure DataEntities SqlServer (DOTNET090000).
/// </summary>
/// <remarks>
/// The CountryLanguage Infrastructure DataEntities SqlServer (DOTNET090000).
/// </remarks>
public partial class CountryLanguage : INFRADATAENTITYMSSQL.Entity<COREDOMAINDATAMODELSDOMAIN.CountryLanguage>, COREAPPDENTINTERFACESDOMAIN.ICountryLanguage
{	
    #region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
    /// Initializes a new instance of the CountryLanguageController class
    /// </summary>
	/// <param name="dbContext">dbContext</param>
	/// <param name="logger">logger</param>	
	/// <param name="messageHub"></param>
	public CountryLanguage(DomainDbContext dbContext
					, DomainQueryDbContext queryDbContext 
					, MSLOGGING.ILogger<CountryLanguage> logger
					, MESSAGEHUBINTERFACES.IMessageHub messageHub
					) : base(dbContext, queryDbContext, logger, messageHub)
	{
	}

	#endregion

	#region Methods
 
	/// <summary>
    /// Get CountryLanguages
    /// </summary>
	/// <param name="request">request</param>
	public async Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.CountryLanguage>> GetCountryLanguagesAsync(COREAPPDATAREPOMODELSDOMAIN.GetCountryLanguageRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetCountryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CountryLanguage), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var query = this.AsIQuerable();
		try
		{
			if (request.MatchExpression is not null)
			{
				query = query.Where(request.MatchExpression);
			}
			else
			{
				Logger.LogWarning($"{nameof(GetCountryLanguagesAsync)} - MatchExpression is null", nameof(CountryLanguage), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataAccess);
			}
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetCountryLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetCountryLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetCountryLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetCountryLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCountryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CountryLanguage), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return query;
	}
 
	/// <summary>
    /// Merge CountryLanguages
    /// </summary>
	/// <param name="request">request</param>
	public virtual async Task<COREDOMAINDATAMODELS.MergeResult> MergeCountryLanguagesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeCountryLanguageRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCountryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CountryLanguage), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var mergeResult = new COREDOMAINDATAMODELS.MergeResult();
		try
		{
			foreach (var countryLanguage in request.CountryLanguages!)
			{
				Logger.LogDiagnosticsInfo($"Item State:{countryLanguage.ItemState}", nameof(CountryLanguage));

				var result = await this.MergeInternalAsync(countryLanguage).ConfigureAwait(false);
				mergeResult += result;
			}

			Logger.LogDiagnosticsInfo($"InsertCount Count:{mergeResult.InsertCount}, UpdateCount Count:{mergeResult.UpdateCount}, DeleteCount Count:{mergeResult.DeleteCount}", nameof(CountryLanguage));
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeCountryLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeCountryLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeCountryLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeCountryLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCountryLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CountryLanguage), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return mergeResult;
	}
	/// <summary>
    /// Merges the internal asynchronous.
    /// </summary>
	/// <param name="countryLanguage">The countryLanguage.</param>
    /// <returns></returns>
	internal async Task<COREDOMAINDATAMODELS.MergeResult> MergeInternalAsync(COREDOMAINDATAMODELSDOMAIN.CountryLanguage countryLanguage)
	{
		var mergeResult = await this.MergeAsync(countryLanguage);
		return mergeResult;
	}

	

	#endregion
}
