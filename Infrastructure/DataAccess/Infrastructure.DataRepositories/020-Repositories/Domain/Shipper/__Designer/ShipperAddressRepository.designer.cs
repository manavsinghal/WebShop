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
/// Represents ShipperAddress Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The ShipperAddress Infrastructure DataRepositories (DOTNET090000).
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
	/// Get ShipperAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetShipperAddressResponse> GetShipperAddressesAsync(COREAPPDATAREPOMODELSDOMAIN.GetShipperAddressRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetShipperAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShipperRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetShipperAddressResponse response = new();

		try
		{
			var shipperAddresses = await this._shipperAddress.GetShipperAddressesAsync(request).ConfigureAwait(false);
			response.ShipperAddresses = shipperAddresses;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetShipperAddressesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShipperAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShipperRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge ShipperAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeShipperAddressResponse> MergeShipperAddressesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeShipperAddressRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShipperAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShipperRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeShipperAddressResponse response = new();

		try
		{
			var mergeResult = await this._shipperAddress.MergeShipperAddressesAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeShipperAddressesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShipperAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShipperRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves ShipperRepository.
	/// </summary>
	public async Task SaveShipperAddressAsync()
	{
		Logger.LogInfo($"{nameof(SaveShipperAddressAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShipperRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _shipperAddress.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveShipperAddressAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveShipperAddressAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShipperRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		}
	}


	#endregion

}
