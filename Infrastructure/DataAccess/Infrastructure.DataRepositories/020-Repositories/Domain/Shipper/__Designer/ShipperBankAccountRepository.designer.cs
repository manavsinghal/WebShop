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
/// Represents ShipperBankAccount Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The ShipperBankAccount Infrastructure DataRepositories (DOTNET090000).
/// </remarks>
public partial class ShipperRepository : INFRADATAREPO.DataRepositoryBase, COREAPPDREPOINTERFACESDOMAIN.IShipperRepository
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	#endregion

	#region Methods

	/// <summary>
	/// Get ShipperBankAccounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetShipperBankAccountResponse> GetShipperBankAccountsAsync(COREAPPDATAREPOMODELSDOMAIN.GetShipperBankAccountRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetShipperBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShipperRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetShipperBankAccountResponse response = new();

		try
		{
			var shipperBankAccounts = await this._shipperBankAccount.GetShipperBankAccountsAsync(request).ConfigureAwait(false);
			response.ShipperBankAccounts = shipperBankAccounts;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetShipperBankAccountsAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShipperBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShipperRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge ShipperBankAccounts
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeShipperBankAccountResponse> MergeShipperBankAccountsAsync(COREAPPDATAREPOMODELSDOMAIN.MergeShipperBankAccountRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShipperBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShipperRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeShipperBankAccountResponse response = new();

		try
		{
			var mergeResult = await this._shipperBankAccount.MergeShipperBankAccountsAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeShipperBankAccountsAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShipperBankAccountsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShipperRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves ShipperRepository.
	/// </summary>
	public async Task SaveShipperBankAccountAsync()
	{
		Logger.LogInfo($"{nameof(SaveShipperBankAccountAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShipperRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _shipperBankAccount.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveShipperBankAccountAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveShipperBankAccountAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShipperRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		}
	}


	#endregion

}
