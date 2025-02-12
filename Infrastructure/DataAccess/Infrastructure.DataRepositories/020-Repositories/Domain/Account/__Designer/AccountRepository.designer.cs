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
/// Represents Account Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The Account Infrastructure DataRepositories (DOTNET090000).
/// </remarks>
public partial class AccountRepository : INFRADATAREPO.DataRepositoryBase, COREAPPDREPOINTERFACESDOMAIN.IAccountRepository
{
	#region Fields

	private Boolean disposedValue;

	private readonly COREAPPDENTINTERFACESDOMAIN.IAccount _account;

	private readonly COREAPPDENTINTERFACESDOMAIN.IAccountStatus _accountStatus;

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the AccountController class
	/// </summary>	 
	/// <param name="account">The account.</param>	 
	/// <param name="accountStatus">The accountStatus.</param>	 
	/// <param name="logger">logger</param>
	/// <param name="messageHub">messageHub.</param>
	public AccountRepository(COREAPPDENTINTERFACESDOMAIN.IAccount account, 
							COREAPPDENTINTERFACESDOMAIN.IAccountStatus accountStatus, 
							MSLOGGING.ILogger<AccountRepository> logger ,MESSAGEHUBINTERFACES.IMessageHub messageHub
							) : base(logger, messageHub)
	{
		this._account = account;
		this._accountStatus = accountStatus;
	}

	#endregion

	#region Methods

	/// <summary>
	/// Get Accounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetAccountResponse> GetAccountsAsync(COREAPPDATAREPOMODELSDOMAIN.GetAccountRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(AccountRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetAccountResponse response = new();

		try
		{
			var accounts = await this._account.GetAccountsAsync(request).ConfigureAwait(false);
			response.Accounts = accounts;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetAccountsAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(AccountRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge Accounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeAccountResponse> MergeAccountsAsync(COREAPPDATAREPOMODELSDOMAIN.MergeAccountRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(AccountRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeAccountResponse response = new();

		try
		{
			var mergeResult = await this._account.MergeAccountsAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeAccountsAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(AccountRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves AccountRepository.
	/// </summary>
	public async Task SaveAccountAsync()
	{
		Logger.LogInfo($"{nameof(SaveAccountAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(AccountRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _account.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveAccountAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveAccountAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(AccountRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
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
