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
/// Represents MasterListItem Infrastructure DataEntities SqlServer (DOTNET090000).
/// </summary>
/// <remarks>
/// The MasterListItem Infrastructure DataEntities SqlServer (DOTNET090000).
/// </remarks>
public partial class MasterListItem : INFRADATAENTITYMSSQL.Entity<COREDOMAINDATAMODELSDOMAIN.MasterListItem>, COREAPPDENTINTERFACESDOMAIN.IMasterListItem
{	
    #region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
    /// Initializes a new instance of the MasterListItemController class
    /// </summary>
	/// <param name="dbContext">dbContext</param>
	/// <param name="logger">logger</param>	
	/// <param name="messageHub"></param>
	public MasterListItem(DomainDbContext dbContext
					, DomainQueryDbContext queryDbContext 
					, MSLOGGING.ILogger<MasterListItem> logger
					, MESSAGEHUBINTERFACES.IMessageHub messageHub
					) : base(dbContext, queryDbContext, logger, messageHub)
	{
	}

	#endregion

	#region Methods
 
	/// <summary>
    /// Get MasterListItems
    /// </summary>
	/// <param name="request">request</param>
	public async Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.MasterListItem>> GetMasterListItemsAsync(COREAPPDATAREPOMODELSDOMAIN.GetMasterListItemRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetMasterListItemsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MasterListItem), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var query = this.AsIQuerable();
		try
		{
			if (request.MatchExpression is not null)
			{
				query = query.Where(request.MatchExpression);
			}
			else
			{
				Logger.LogWarning($"{nameof(GetMasterListItemsAsync)} - MatchExpression is null", nameof(MasterListItem), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataAccess);
			}
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetMasterListItemsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetMasterListItemsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetMasterListItemsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetMasterListItemsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetMasterListItemsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MasterListItem), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return query;
	}
 
	/// <summary>
    /// Merge MasterListItems
    /// </summary>
	/// <param name="request">request</param>
	public virtual async Task<COREDOMAINDATAMODELS.MergeResult> MergeMasterListItemsAsync(COREAPPDATAREPOMODELSDOMAIN.MergeMasterListItemRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeMasterListItemsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MasterListItem), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var mergeResult = new COREDOMAINDATAMODELS.MergeResult();
		try
		{
			foreach (var masterListItem in request.MasterListItems!)
			{
				Logger.LogDiagnosticsInfo($"Item State:{masterListItem.ItemState}", nameof(MasterListItem));

				var result = await this.MergeInternalAsync(masterListItem).ConfigureAwait(false);
				mergeResult += result;
			}

			Logger.LogDiagnosticsInfo($"InsertCount Count:{mergeResult.InsertCount}, UpdateCount Count:{mergeResult.UpdateCount}, DeleteCount Count:{mergeResult.DeleteCount}", nameof(MasterListItem));
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeMasterListItemsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeMasterListItemsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeMasterListItemsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeMasterListItemsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeMasterListItemsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MasterListItem), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return mergeResult;
	}
	/// <summary>
    /// Merges the internal asynchronous.
    /// </summary>
	/// <param name="masterListItem">The masterListItem.</param>
    /// <returns></returns>
	internal async Task<COREDOMAINDATAMODELS.MergeResult> MergeInternalAsync(COREDOMAINDATAMODELSDOMAIN.MasterListItem masterListItem)
	{
		var mergeResult = await this.MergeAsync(masterListItem);
		return mergeResult;
	}

	

	#endregion
}
