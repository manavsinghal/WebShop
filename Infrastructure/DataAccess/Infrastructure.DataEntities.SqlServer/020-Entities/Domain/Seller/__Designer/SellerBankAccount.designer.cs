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
/// Represents SellerBankAccount Infrastructure DataEntities SqlServer (DOTNET090000).
/// </summary>
/// <remarks>
/// The SellerBankAccount Infrastructure DataEntities SqlServer (DOTNET090000).
/// </remarks>
public partial class SellerBankAccount : INFRADATAENTITYMSSQL.Entity<COREDOMAINDATAMODELSDOMAIN.SellerBankAccount>, COREAPPDENTINTERFACESDOMAIN.ISellerBankAccount
{	
    #region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
    /// Initializes a new instance of the SellerBankAccountController class
    /// </summary>
	/// <param name="dbContext">dbContext</param>
	/// <param name="logger">logger</param>	
	/// <param name="messageHub"></param>
	public SellerBankAccount(DomainDbContext dbContext
					, DomainQueryDbContext queryDbContext 
					, MSLOGGING.ILogger<SellerBankAccount> logger
					, MESSAGEHUBINTERFACES.IMessageHub messageHub
					) : base(dbContext, queryDbContext, logger, messageHub)
	{
	}

	#endregion

	#region Methods
 
	/// <summary>
    /// Get SellerBankAccounts
    /// </summary>
	/// <param name="request">request</param>
	public async Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.SellerBankAccount>> GetSellerBankAccountsAsync(COREAPPDATAREPOMODELSDOMAIN.GetSellerBankAccountRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellerBankAccount), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var query = this.AsIQuerable();
		try
		{
			if (request.MatchExpression is not null)
			{
				query = query.Where(request.MatchExpression);
			}
			else
			{
				Logger.LogWarning($"{nameof(GetSellerBankAccountsAsync)} - MatchExpression is null", nameof(SellerBankAccount), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataAccess);
			}
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetSellerBankAccountsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetSellerBankAccountsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetSellerBankAccountsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetSellerBankAccountsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellerBankAccount), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return query;
	}
 
	/// <summary>
    /// Merge SellerBankAccounts
    /// </summary>
	/// <param name="request">request</param>
	public virtual async Task<COREDOMAINDATAMODELS.MergeResult> MergeSellerBankAccountsAsync(COREAPPDATAREPOMODELSDOMAIN.MergeSellerBankAccountRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellerBankAccount), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var mergeResult = new COREDOMAINDATAMODELS.MergeResult();
		try
		{
			foreach (var sellerBankAccount in request.SellerBankAccounts!)
			{
				Logger.LogDiagnosticsInfo($"Item State:{sellerBankAccount.ItemState}", nameof(SellerBankAccount));

				var result = await this.MergeInternalAsync(sellerBankAccount).ConfigureAwait(false);
				mergeResult += result;
			}

			Logger.LogDiagnosticsInfo($"InsertCount Count:{mergeResult.InsertCount}, UpdateCount Count:{mergeResult.UpdateCount}, DeleteCount Count:{mergeResult.DeleteCount}", nameof(SellerBankAccount));
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeSellerBankAccountsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeSellerBankAccountsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeSellerBankAccountsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeSellerBankAccountsAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellerBankAccount), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return mergeResult;
	}
	/// <summary>
    /// Merges the internal asynchronous.
    /// </summary>
	/// <param name="sellerBankAccount">The sellerBankAccount.</param>
    /// <returns></returns>
	internal async Task<COREDOMAINDATAMODELS.MergeResult> MergeInternalAsync(COREDOMAINDATAMODELSDOMAIN.SellerBankAccount sellerBankAccount)
	{
		var mergeResult = await this.MergeAsync(sellerBankAccount);
		return mergeResult;
	}

	

	#endregion
}
