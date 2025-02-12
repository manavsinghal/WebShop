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
/// Represents SellerBankAccount Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The SellerBankAccount Infrastructure DataRepositories (DOTNET090000).
/// </remarks>
public partial class SellerRepository : INFRADATAREPO.DataRepositoryBase, COREAPPDREPOINTERFACESDOMAIN.ISellerRepository
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	#endregion

	#region Methods

	/// <summary>
	/// Get SellerBankAccounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetSellerBankAccountResponse> GetSellerBankAccountsAsync(COREAPPDATAREPOMODELSDOMAIN.GetSellerBankAccountRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetSellerBankAccountResponse response = new();

		try
		{
			var sellerBankAccounts = await this._sellerBankAccount.GetSellerBankAccountsAsync(request).ConfigureAwait(false);
			response.SellerBankAccounts = sellerBankAccounts;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetSellerBankAccountsAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge SellerBankAccounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeSellerBankAccountResponse> MergeSellerBankAccountsAsync(COREAPPDATAREPOMODELSDOMAIN.MergeSellerBankAccountRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeSellerBankAccountResponse response = new();

		try
		{
			var mergeResult = await this._sellerBankAccount.MergeSellerBankAccountsAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeSellerBankAccountsAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves SellerRepository.
	/// </summary>
	public async Task SaveSellerBankAccountAsync()
	{
		Logger.LogInfo($"{nameof(SaveSellerBankAccountAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellerRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _sellerBankAccount.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveSellerBankAccountAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveSellerBankAccountAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellerRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		}
	}


	#endregion

}
