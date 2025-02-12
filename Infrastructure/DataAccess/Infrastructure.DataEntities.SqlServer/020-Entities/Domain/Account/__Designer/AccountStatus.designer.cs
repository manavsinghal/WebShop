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
/// Represents AccountStatus Infrastructure DataEntities SqlServer (DOTNET090000).
/// </summary>
/// <remarks>
/// The AccountStatus Infrastructure DataEntities SqlServer (DOTNET090000).
/// </remarks>
public partial class AccountStatus : INFRADATAENTITYMSSQL.Entity<COREDOMAINDATAMODELSDOMAIN.AccountStatus>, COREAPPDENTINTERFACESDOMAIN.IAccountStatus
{	
    #region Fields

	private readonly COREAPPDENTINTERFACESDOMAIN.IAccount _account; 

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
    /// Initializes a new instance of the AccountStatusController class
    /// </summary>
	/// <param name="account">account</param>
	/// <param name="dbContext">dbContext</param>
	/// <param name="logger">logger</param>	
	/// <param name="messageHub"></param>
	public AccountStatus(COREAPPDENTINTERFACESDOMAIN.IAccount account
					, DomainDbContext dbContext
					, DomainQueryDbContext queryDbContext 
					, MSLOGGING.ILogger<AccountStatus> logger
					, MESSAGEHUBINTERFACES.IMessageHub messageHub
					) : base(dbContext, queryDbContext, logger, messageHub)
	{
		this._account = account;
	}

	#endregion

	#region Methods
 
	/// <summary>
    /// Get AccountStatuses
    /// </summary>
	/// <param name="request">request</param>
	public async Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.AccountStatus>> GetAccountStatusesAsync(COREAPPDATAREPOMODELSDOMAIN.GetAccountStatusRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetAccountStatusesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(AccountStatus), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var query = this.AsIQuerable();
		try
		{
			if (request.MatchExpression is not null)
			{
				query = query.Where(request.MatchExpression);
			}
			else
			{
				Logger.LogWarning($"{nameof(GetAccountStatusesAsync)} - MatchExpression is null", nameof(AccountStatus), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataAccess);
			}
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetAccountStatusesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetAccountStatusesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetAccountStatusesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetAccountStatusesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetAccountStatusesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(AccountStatus), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return query;
	}
 
	/// <summary>
    /// Merge AccountStatuses
    /// </summary>
	/// <param name="request">request</param>
	public virtual async Task<COREDOMAINDATAMODELS.MergeResult> MergeAccountStatusesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeAccountStatusRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeAccountStatusesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(AccountStatus), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var mergeResult = new COREDOMAINDATAMODELS.MergeResult();
		try
		{
			foreach (var accountStatus in request.AccountStatuses!)
			{
				Logger.LogDiagnosticsInfo($"Item State:{accountStatus.ItemState}", nameof(AccountStatus));

				var result = await this.MergeInternalAsync(accountStatus).ConfigureAwait(false);
				mergeResult += result;
			}

			Logger.LogDiagnosticsInfo($"InsertCount Count:{mergeResult.InsertCount}, UpdateCount Count:{mergeResult.UpdateCount}, DeleteCount Count:{mergeResult.DeleteCount}", nameof(AccountStatus));
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeAccountStatusesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeAccountStatusesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeAccountStatusesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeAccountStatusesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeAccountStatusesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(AccountStatus), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return mergeResult;
	}
	/// <summary>
    /// Merges the internal asynchronous.
    /// </summary>
	/// <param name="accountStatus">The accountStatus.</param>
    /// <returns></returns>
	internal async Task<COREDOMAINDATAMODELS.MergeResult> MergeInternalAsync(COREDOMAINDATAMODELSDOMAIN.AccountStatus accountStatus)
	{
		var mergeResult = await this.MergeAsync(accountStatus);
		this.MergeChildEntities(accountStatus);
		return mergeResult;
	}

	/// <summary>
    /// Merges the Child entities.
    /// </summary>
	/// <param name="accountStatus">The accountStatus.</param>
    /// <returns></returns>
	internal void MergeChildEntities(COREDOMAINDATAMODELSDOMAIN.AccountStatus accountStatus)
	{
		if (accountStatus != null)
		{
			if (accountStatus.Accounts != null && accountStatus.Accounts.Any())
			{
				var mergeAccountRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeAccountRequest()
				{
					Accounts = accountStatus.Accounts
				};
				this._account.MergeAccountsAsync(mergeAccountRequest);
			}

		}
	}
	

	#endregion
}
