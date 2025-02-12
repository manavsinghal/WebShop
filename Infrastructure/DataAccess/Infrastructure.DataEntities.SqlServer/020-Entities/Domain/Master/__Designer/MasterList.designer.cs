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
/// Represents MasterList Infrastructure DataEntities SqlServer (DOTNET090000).
/// </summary>
/// <remarks>
/// The MasterList Infrastructure DataEntities SqlServer (DOTNET090000).
/// </remarks>
public partial class MasterList : INFRADATAENTITYMSSQL.Entity<COREDOMAINDATAMODELSDOMAIN.MasterList>, COREAPPDENTINTERFACESDOMAIN.IMasterList
{	
    #region Fields

	private readonly COREAPPDENTINTERFACESDOMAIN.IMasterListItem _masterListItem; 

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
    /// Initializes a new instance of the MasterListController class
    /// </summary>
	/// <param name="masterListItem">masterListItem</param>
	/// <param name="dbContext">dbContext</param>
	/// <param name="logger">logger</param>	
	/// <param name="messageHub"></param>
	public MasterList(COREAPPDENTINTERFACESDOMAIN.IMasterListItem masterListItem
					, DomainDbContext dbContext
					, DomainQueryDbContext queryDbContext 
					, MSLOGGING.ILogger<MasterList> logger
					, MESSAGEHUBINTERFACES.IMessageHub messageHub
					) : base(dbContext, queryDbContext, logger, messageHub)
	{
		this._masterListItem = masterListItem;
	}

	#endregion

	#region Methods
 
	/// <summary>
    /// Get MasterLists
    /// </summary>
	/// <param name="request">request</param>
	public async Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.MasterList>> GetMasterListsAsync(COREAPPDATAREPOMODELSDOMAIN.GetMasterListRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MasterList), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var query = this.AsIQuerable();
		try
		{
			if (request.MatchExpression is not null)
			{
				query = query.Where(request.MatchExpression);
			}
			else
			{
				Logger.LogWarning($"{nameof(GetMasterListsAsync)} - MatchExpression is null", nameof(MasterList), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataAccess);
			}
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetMasterListsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetMasterListsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetMasterListsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetMasterListsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MasterList), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return query;
	}
 
	/// <summary>
    /// Merge MasterLists
    /// </summary>
	/// <param name="request">request</param>
	public virtual async Task<COREDOMAINDATAMODELS.MergeResult> MergeMasterListsAsync(COREAPPDATAREPOMODELSDOMAIN.MergeMasterListRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MasterList), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var mergeResult = new COREDOMAINDATAMODELS.MergeResult();
		try
		{
			foreach (var masterList in request.MasterLists!)
			{
				Logger.LogDiagnosticsInfo($"Item State:{masterList.ItemState}", nameof(MasterList));

				var result = await this.MergeInternalAsync(masterList).ConfigureAwait(false);
				mergeResult += result;
			}

			Logger.LogDiagnosticsInfo($"InsertCount Count:{mergeResult.InsertCount}, UpdateCount Count:{mergeResult.UpdateCount}, DeleteCount Count:{mergeResult.DeleteCount}", nameof(MasterList));
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeMasterListsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeMasterListsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeMasterListsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeMasterListsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeMasterListsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MasterList), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return mergeResult;
	}
	/// <summary>
    /// Merges the internal asynchronous.
    /// </summary>
	/// <param name="masterList">The masterList.</param>
    /// <returns></returns>
	internal async Task<COREDOMAINDATAMODELS.MergeResult> MergeInternalAsync(COREDOMAINDATAMODELSDOMAIN.MasterList masterList)
	{
		var mergeResult = await this.MergeAsync(masterList);
		this.MergeChildEntities(masterList);
		return mergeResult;
	}

	/// <summary>
    /// Merges the Child entities.
    /// </summary>
	/// <param name="masterList">The masterList.</param>
    /// <returns></returns>
	internal void MergeChildEntities(COREDOMAINDATAMODELSDOMAIN.MasterList masterList)
	{
		if (masterList != null)
		{
			if (masterList.MasterListItems != null && masterList.MasterListItems.Any())
			{
				var mergeMasterListItemRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeMasterListItemRequest()
				{
					MasterListItems = masterList.MasterListItems
				};
				this._masterListItem.MergeMasterListItemsAsync(mergeMasterListItemRequest);
			}

		}
	}
	

	#endregion
}
