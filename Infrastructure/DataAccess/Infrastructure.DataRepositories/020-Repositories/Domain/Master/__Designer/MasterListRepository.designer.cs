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
/// Represents MasterList Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The MasterList Infrastructure DataRepositories (DOTNET090000).
/// </remarks>
public partial class MasterRepository : INFRADATAREPO.DataRepositoryBase, COREAPPDREPOINTERFACESDOMAIN.IMasterRepository
{
	#region Fields

	private Boolean disposedValue;

	private readonly COREAPPDENTINTERFACESDOMAIN.IAppSetting _appSetting;

	private readonly COREAPPDENTINTERFACESDOMAIN.ICountry _country;

	private readonly COREAPPDENTINTERFACESDOMAIN.ICountryLanguage _countryLanguage;

	private readonly COREAPPDENTINTERFACESDOMAIN.IEntity _entity;

	private readonly COREAPPDENTINTERFACESDOMAIN.ILanguage _language;

	private readonly COREAPPDENTINTERFACESDOMAIN.IMasterList _masterList;

	private readonly COREAPPDENTINTERFACESDOMAIN.IMasterListItem _masterListItem;

	private readonly COREAPPDENTINTERFACESDOMAIN.IRowStatus _rowStatus;

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the MasterListController class
	/// </summary>	 
	/// <param name="appSetting">The appSetting.</param>	 
	/// <param name="country">The country.</param>	 
	/// <param name="countryLanguage">The countryLanguage.</param>	 
	/// <param name="entity">The entity.</param>	 
	/// <param name="language">The language.</param>	 
	/// <param name="masterList">The masterList.</param>	 
	/// <param name="masterListItem">The masterListItem.</param>	 
	/// <param name="rowStatus">The rowStatus.</param>	 
	/// <param name="logger">logger</param>
	/// <param name="messageHub">messageHub.</param>
	public MasterRepository(COREAPPDENTINTERFACESDOMAIN.IAppSetting appSetting, 
							COREAPPDENTINTERFACESDOMAIN.ICountry country, 
							COREAPPDENTINTERFACESDOMAIN.ICountryLanguage countryLanguage, 
							COREAPPDENTINTERFACESDOMAIN.IEntity entity, 
							COREAPPDENTINTERFACESDOMAIN.ILanguage language, 
							COREAPPDENTINTERFACESDOMAIN.IMasterList masterList, 
							COREAPPDENTINTERFACESDOMAIN.IMasterListItem masterListItem, 
							COREAPPDENTINTERFACESDOMAIN.IRowStatus rowStatus, 
							MSLOGGING.ILogger<MasterRepository> logger ,MESSAGEHUBINTERFACES.IMessageHub messageHub
							) : base(logger, messageHub)
	{
		this._appSetting = appSetting;
		this._country = country;
		this._countryLanguage = countryLanguage;
		this._entity = entity;
		this._language = language;
		this._masterList = masterList;
		this._masterListItem = masterListItem;
		this._rowStatus = rowStatus;
	}

	#endregion

	#region Methods

	/// <summary>
	/// Get MasterLists
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetMasterListResponse> GetMasterListsAsync(COREAPPDATAREPOMODELSDOMAIN.GetMasterListRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MasterRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetMasterListResponse response = new();

		try
		{
			var masterLists = await this._masterList.GetMasterListsAsync(request).ConfigureAwait(false);
			response.MasterLists = masterLists;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetMasterListsAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MasterRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge MasterLists
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeMasterListResponse> MergeMasterListsAsync(COREAPPDATAREPOMODELSDOMAIN.MergeMasterListRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MasterRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeMasterListResponse response = new();

		try
		{
			var mergeResult = await this._masterList.MergeMasterListsAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeMasterListsAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MasterRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves MasterRepository.
	/// </summary>
	public async Task SaveMasterListAsync()
	{
		Logger.LogInfo($"{nameof(SaveMasterListAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MasterRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _masterList.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveMasterListAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveMasterListAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MasterRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		}
	}

	/// <summary>
	/// Represents Dispose Method.
	/// </summary>
	public void Dispose()
	{
		if (!disposedValue)
		{
			disposedValue = true;
		}
	}

	#endregion

}
