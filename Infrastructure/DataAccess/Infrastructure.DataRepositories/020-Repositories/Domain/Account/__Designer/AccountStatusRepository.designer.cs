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
/// Represents AccountStatus Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The AccountStatus Infrastructure DataRepositories (DOTNET090000).
/// </remarks>
public partial class AccountRepository : INFRADATAREPO.DataRepositoryBase, COREAPPDREPOINTERFACESDOMAIN.IAccountRepository
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	#endregion

	#region Methods

	/// <summary>
	/// Get AccountStatuses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetAccountStatusResponse> GetAccountStatusesAsync(COREAPPDATAREPOMODELSDOMAIN.GetAccountStatusRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetAccountStatusesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(AccountRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetAccountStatusResponse response = new();

		try
		{
			var accountStatuses = await this._accountStatus.GetAccountStatusesAsync(request).ConfigureAwait(false);
			response.AccountStatuses = accountStatuses;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetAccountStatusesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetAccountStatusesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(AccountRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge AccountStatuses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeAccountStatusResponse> MergeAccountStatusesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeAccountStatusRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeAccountStatusesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(AccountRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeAccountStatusResponse response = new();

		try
		{
			var mergeResult = await this._accountStatus.MergeAccountStatusesAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeAccountStatusesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeAccountStatusesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(AccountRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves AccountRepository.
	/// </summary>
	public async Task SaveAccountStatusAsync()
	{
		Logger.LogInfo($"{nameof(SaveAccountStatusAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(AccountRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _accountStatus.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveAccountStatusAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveAccountStatusAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(AccountRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		}
	}


	#endregion

}
