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
/// Represents MasterListItem Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The MasterListItem Infrastructure DataRepositories (DOTNET090000).
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
	/// Get MasterListItems
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetMasterListItemResponse> GetMasterListItemsAsync(COREAPPDATAREPOMODELSDOMAIN.GetMasterListItemRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetMasterListItemsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MasterRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetMasterListItemResponse response = new();

		try
		{
			var masterListItems = await this._masterListItem.GetMasterListItemsAsync(request).ConfigureAwait(false);
			response.MasterListItems = masterListItems;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetMasterListItemsAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetMasterListItemsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MasterRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge MasterListItems
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeMasterListItemResponse> MergeMasterListItemsAsync(COREAPPDATAREPOMODELSDOMAIN.MergeMasterListItemRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeMasterListItemsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MasterRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeMasterListItemResponse response = new();

		try
		{
			var mergeResult = await this._masterListItem.MergeMasterListItemsAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeMasterListItemsAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeMasterListItemsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MasterRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves MasterRepository.
	/// </summary>
	public async Task SaveMasterListItemAsync()
	{
		Logger.LogInfo($"{nameof(SaveMasterListItemAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MasterRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _masterListItem.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveMasterListItemAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveMasterListItemAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MasterRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		}
	}


	#endregion

}
