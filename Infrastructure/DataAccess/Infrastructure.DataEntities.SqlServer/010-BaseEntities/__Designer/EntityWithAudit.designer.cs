#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Infrastructure.DataEntities.SqlServer;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents EntityWithAudit
/// </summary>
/// <typeparam name="T"></typeparam>
public partial class EntityWithAudit<T> : EntityBase<T> where T : COREDOMAINDATAMODELS.DataModelWithAudit<T>
{
	#region Constructors

	/// <summary>
	/// Initializes EntityWithAudit class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	/// <param name="logger">The logger.</param>
	/// <param name="notifier"></param>
	public EntityWithAudit(DbContext dbContext, DbContext queryDbContext,
						MSLOGGING.ILogger logger,
						MESSAGEHUBINTERFACES.IMessageHub notifier
						) : base(dbContext, queryDbContext, logger, notifier)
	{
	}

	#endregion

	#region Methods

	/// <summary>
	/// Merges the entity
	/// </summary>
	/// <param name="entity"></param>
	/// <returns></returns>
	public override async Task<COREDOMAINDATAMODELS.MergeResult> MergeAsync(T entity)
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

