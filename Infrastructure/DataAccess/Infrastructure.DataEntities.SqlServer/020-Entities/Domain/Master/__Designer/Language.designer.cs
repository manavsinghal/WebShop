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
/// Represents Language Infrastructure DataEntities SqlServer (DOTNET090000).
/// </summary>
/// <remarks>
/// The Language Infrastructure DataEntities SqlServer (DOTNET090000).
/// </remarks>
public partial class Language : INFRADATAENTITYMSSQL.Entity<COREDOMAINDATAMODELSDOMAIN.Language>, COREAPPDENTINTERFACESDOMAIN.ILanguage
{	
    #region Fields

	private readonly COREAPPDENTINTERFACESDOMAIN.ICountryLanguage _countryLanguage; 

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
    /// Initializes a new instance of the LanguageController class
    /// </summary>
	/// <param name="countryLanguage">countryLanguage</param>
	/// <param name="dbContext">dbContext</param>
	/// <param name="logger">logger</param>	
	/// <param name="messageHub"></param>
	public Language(COREAPPDENTINTERFACESDOMAIN.ICountryLanguage countryLanguage
					, DomainDbContext dbContext
					, DomainQueryDbContext queryDbContext 
					, MSLOGGING.ILogger<Language> logger
					, MESSAGEHUBINTERFACES.IMessageHub messageHub
					) : base(dbContext, queryDbContext, logger, messageHub)
	{
		this._countryLanguage = countryLanguage;
	}

	#endregion

	#region Methods
 
	/// <summary>
    /// Get Languages
    /// </summary>
	/// <param name="request">request</param>
	public async Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.Language>> GetLanguagesAsync(COREAPPDATAREPOMODELSDOMAIN.GetLanguageRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Language), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var query = this.AsIQuerable();
		try
		{
			if (request.MatchExpression is not null)
			{
				query = query.Where(request.MatchExpression);
			}
			else
			{
				Logger.LogWarning($"{nameof(GetLanguagesAsync)} - MatchExpression is null", nameof(Language), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataAccess);
			}
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Language), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return query;
	}
 
	/// <summary>
    /// Merge Languages
    /// </summary>
	/// <param name="request">request</param>
	public virtual async Task<COREDOMAINDATAMODELS.MergeResult> MergeLanguagesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeLanguageRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Language), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var mergeResult = new COREDOMAINDATAMODELS.MergeResult();
		try
		{
			foreach (var language in request.Languages!)
			{
				Logger.LogDiagnosticsInfo($"Item State:{language.ItemState}", nameof(Language));

				var result = await this.MergeInternalAsync(language).ConfigureAwait(false);
				mergeResult += result;
			}

			Logger.LogDiagnosticsInfo($"InsertCount Count:{mergeResult.InsertCount}, UpdateCount Count:{mergeResult.UpdateCount}, DeleteCount Count:{mergeResult.DeleteCount}", nameof(Language));
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeLanguagesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeLanguagesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Language), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return mergeResult;
	}
	/// <summary>
    /// Merges the internal asynchronous.
    /// </summary>
	/// <param name="language">The language.</param>
    /// <returns></returns>
	internal async Task<COREDOMAINDATAMODELS.MergeResult> MergeInternalAsync(COREDOMAINDATAMODELSDOMAIN.Language language)
	{
		var mergeResult = await this.MergeAsync(language);
		this.MergeChildEntities(language);
		return mergeResult;
	}

	/// <summary>
    /// Merges the Child entities.
    /// </summary>
	/// <param name="language">The language.</param>
    /// <returns></returns>
	internal void MergeChildEntities(COREDOMAINDATAMODELSDOMAIN.Language language)
	{
		if (language != null)
		{
			if (language.CountryLanguages != null && language.CountryLanguages.Any())
			{
				var mergeCountryLanguageRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeCountryLanguageRequest()
				{
					CountryLanguages = language.CountryLanguages
				};
				this._countryLanguage.MergeCountryLanguagesAsync(mergeCountryLanguageRequest);
			}

		}
	}
	

	#endregion
}
