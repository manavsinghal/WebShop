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
/// Represents ShipperPhone Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The ShipperPhone Infrastructure DataRepositories (DOTNET090000).
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
	/// Get ShipperPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetShipperPhoneResponse> GetShipperPhonesAsync(COREAPPDATAREPOMODELSDOMAIN.GetShipperPhoneRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetShipperPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShipperRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetShipperPhoneResponse response = new();

		try
		{
			var shipperPhones = await this._shipperPhone.GetShipperPhonesAsync(request).ConfigureAwait(false);
			response.ShipperPhones = shipperPhones;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetShipperPhonesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShipperPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShipperRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge ShipperPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeShipperPhoneResponse> MergeShipperPhonesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeShipperPhoneRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShipperPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShipperRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeShipperPhoneResponse response = new();

		try
		{
			var mergeResult = await this._shipperPhone.MergeShipperPhonesAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeShipperPhonesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShipperPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShipperRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves ShipperRepository.
	/// </summary>
	public async Task SaveShipperPhoneAsync()
	{
		Logger.LogInfo($"{nameof(SaveShipperPhoneAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShipperRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _shipperPhone.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveShipperPhoneAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveShipperPhoneAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShipperRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		}
	}


	#endregion

}
