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
/// Represents RowStatus Infrastructure DataEntities SqlServer (DOTNET090000).
/// </summary>
/// <remarks>
/// The RowStatus Infrastructure DataEntities SqlServer (DOTNET090000).
/// </remarks>
public partial class RowStatus : INFRADATAENTITYMSSQL.EntityWithAudit<COREDOMAINDATAMODELSDOMAIN.RowStatus>, COREAPPDENTINTERFACESDOMAIN.IRowStatus
{	
    #region Fields

	private readonly COREAPPDENTINTERFACESDOMAIN.IAppSetting _appSetting; 

	private readonly COREAPPDENTINTERFACESDOMAIN.ICountry _country; 

	private readonly COREAPPDENTINTERFACESDOMAIN.ICountryLanguage _countryLanguage; 

	private readonly COREAPPDENTINTERFACESDOMAIN.IEntity _entity; 

	private readonly COREAPPDENTINTERFACESDOMAIN.ILanguage _language; 

	private readonly COREAPPDENTINTERFACESDOMAIN.IMasterList _masterList; 

	private readonly COREAPPDENTINTERFACESDOMAIN.IMasterListItem _masterListItem; 

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
    /// Initializes a new instance of the RowStatusController class
    /// </summary>
	/// <param name="appSetting">appSetting</param>
	/// <param name="country">country</param>
	/// <param name="countryLanguage">countryLanguage</param>
	/// <param name="entity">entity</param>
	/// <param name="language">language</param>
	/// <param name="masterList">masterList</param>
	/// <param name="masterListItem">masterListItem</param>
	/// <param name="dbContext">dbContext</param>
	/// <param name="logger">logger</param>	
	/// <param name="messageHub"></param>
	public RowStatus(COREAPPDENTINTERFACESDOMAIN.IAppSetting appSetting
					, COREAPPDENTINTERFACESDOMAIN.ICountry country
					, COREAPPDENTINTERFACESDOMAIN.ICountryLanguage countryLanguage
					, COREAPPDENTINTERFACESDOMAIN.IEntity entity
					, COREAPPDENTINTERFACESDOMAIN.ILanguage language
					, COREAPPDENTINTERFACESDOMAIN.IMasterListItem masterListItem
					, COREAPPDENTINTERFACESDOMAIN.IMasterList masterList
					, DomainDbContext dbContext
					, DomainQueryDbContext queryDbContext 
					, MSLOGGING.ILogger<RowStatus> logger
					, MESSAGEHUBINTERFACES.IMessageHub messageHub
					) : base(dbContext, queryDbContext, logger, messageHub)
	{
		this._appSetting = appSetting;
		this._country = country;
		this._countryLanguage = countryLanguage;
		this._entity = entity;
		this._language = language;
		this._masterList = masterList;
		this._masterListItem = masterListItem;
	}

	#endregion

	#region Methods
 
	/// <summary>
    /// Get RowStatuses
    /// </summary>
	/// <param name="request">request</param>
	public async Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.RowStatus>> GetRowStatusesAsync(COREAPPDATAREPOMODELSDOMAIN.GetRowStatusRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetRowStatusesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(RowStatus), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var query = this.AsIQuerable();
		try
		{
			if (request.MatchExpression is not null)
			{
				query = query.Where(request.MatchExpression);
			}
			else
			{
				Logger.LogWarning($"{nameof(GetRowStatusesAsync)} - MatchExpression is null", nameof(RowStatus), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataAccess);
			}
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetRowStatusesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetRowStatusesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetRowStatusesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetRowStatusesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetRowStatusesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(RowStatus), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return query;
	}
 
	/// <summary>
    /// Merge RowStatuses
    /// </summary>
	/// <param name="request">request</param>
	public virtual async Task<COREDOMAINDATAMODELS.MergeResult> MergeRowStatusesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeRowStatusRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeRowStatusesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(RowStatus), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var mergeResult = new COREDOMAINDATAMODELS.MergeResult();
		try
		{
			foreach (var rowStatus in request.RowStatuses!)
			{
				Logger.LogDiagnosticsInfo($"Item State:{rowStatus.ItemState}", nameof(RowStatus));

				var result = await this.MergeInternalAsync(rowStatus).ConfigureAwait(false);
				mergeResult += result;
			}

			Logger.LogDiagnosticsInfo($"InsertCount Count:{mergeResult.InsertCount}, UpdateCount Count:{mergeResult.UpdateCount}, DeleteCount Count:{mergeResult.DeleteCount}", nameof(RowStatus));
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeRowStatusesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeRowStatusesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeRowStatusesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeRowStatusesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeRowStatusesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(RowStatus), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return mergeResult;
	}
	/// <summary>
    /// Merges the internal asynchronous.
    /// </summary>
	/// <param name="rowStatus">The rowStatus.</param>
    /// <returns></returns>
	internal async Task<COREDOMAINDATAMODELS.MergeResult> MergeInternalAsync(COREDOMAINDATAMODELSDOMAIN.RowStatus rowStatus)
	{
		var mergeResult = await this.MergeAsync(rowStatus);
		this.MergeChildEntities(rowStatus);
		return mergeResult;
	}

	/// <summary>
    /// Merges the Child entities.
    /// </summary>
	/// <param name="rowStatus">The rowStatus.</param>
    /// <returns></returns>
	internal void MergeChildEntities(COREDOMAINDATAMODELSDOMAIN.RowStatus rowStatus)
	{
		if (rowStatus != null)
		{
			if (rowStatus.AppSettings != null && rowStatus.AppSettings.Any())
			{
				var mergeAppSettingRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeAppSettingRequest()
				{
					AppSettings = rowStatus.AppSettings
				};
				this._appSetting.MergeAppSettingsAsync(mergeAppSettingRequest);
			}

			if (rowStatus.Countries != null && rowStatus.Countries.Any())
			{
				var mergeCountryRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeCountryRequest()
				{
					Countries = rowStatus.Countries
				};
				this._country.MergeCountriesAsync(mergeCountryRequest);
			}

			if (rowStatus.CountryLanguages != null && rowStatus.CountryLanguages.Any())
			{
				var mergeCountryLanguageRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeCountryLanguageRequest()
				{
					CountryLanguages = rowStatus.CountryLanguages
				};
				this._countryLanguage.MergeCountryLanguagesAsync(mergeCountryLanguageRequest);
			}

			if (rowStatus.Entities != null && rowStatus.Entities.Any())
			{
				var mergeEntityRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeEntityRequest()
				{
					Entities = rowStatus.Entities
				};
				this._entity.MergeEntitiesAsync(mergeEntityRequest);
			}

			if (rowStatus.Languages != null && rowStatus.Languages.Any())
			{
				var mergeLanguageRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeLanguageRequest()
				{
					Languages = rowStatus.Languages
				};
				this._language.MergeLanguagesAsync(mergeLanguageRequest);
			}

			if (rowStatus.MasterListItems != null && rowStatus.MasterListItems.Any())
			{
				var mergeMasterListItemRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeMasterListItemRequest()
				{
					MasterListItems = rowStatus.MasterListItems
				};
				this._masterListItem.MergeMasterListItemsAsync(mergeMasterListItemRequest);
			}

			if (rowStatus.MasterLists != null && rowStatus.MasterLists.Any())
			{
				var mergeMasterListRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeMasterListRequest()
				{
					MasterLists = rowStatus.MasterLists
				};
				this._masterList.MergeMasterListsAsync(mergeMasterListRequest);
			}

		}
	}
	

	/// <summary>
	/// Merges the entity
	/// </summary>
	/// <param name="entity"></param>
	/// <returns></returns>
	public override async Task<COREDOMAINDATAMODELS.MergeResult> MergeAsync(COREDOMAINDATAMODELSDOMAIN.RowStatus entity)
	{
		var mergeResult = new COREDOMAINDATAMODELS.MergeResult() { RequestCount = 1 };

		switch (entity.ItemState)
		{
			case COREDOMAINDATAMODELSENUM.ItemState.Added:
				await this.AddAsync(entity).ConfigureAwait(false);
				mergeResult.InsertCount = 1;
				break;
			case COREDOMAINDATAMODELSENUM.ItemState.Modified:
			case COREDOMAINDATAMODELSENUM.ItemState.Deleted:
				await UpdateAsync(entity).ConfigureAwait(false);
				mergeResult.UpdateCount = 1;
				break;
			case COREDOMAINDATAMODELSENUM.ItemState.HardDeleted:
				DbContext.Remove(entity);
				mergeResult.DeleteCount = 1;
				break;
			case COREDOMAINDATAMODELSENUM.ItemState.Unchanged:
				break;
			case COREDOMAINDATAMODELSENUM.ItemState.AddOrUpdate:
				break;
			default:
				break;
		}

		return mergeResult;
	}

	#endregion
}
