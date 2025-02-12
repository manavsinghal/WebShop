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
/// Represents Shipper Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The Shipper Infrastructure DataRepositories (DOTNET090000).
/// </remarks>
public partial class ShipperRepository : INFRADATAREPO.DataRepositoryBase, COREAPPDREPOINTERFACESDOMAIN.IShipperRepository
{
	#region Fields

	private Boolean disposedValue;

	private readonly COREAPPDENTINTERFACESDOMAIN.IShipper _shipper;

	private readonly COREAPPDENTINTERFACESDOMAIN.IShipperAddress _shipperAddress;

	private readonly COREAPPDENTINTERFACESDOMAIN.IShipperBankAccount _shipperBankAccount;

	private readonly COREAPPDENTINTERFACESDOMAIN.IShipperPhone _shipperPhone;

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the ShipperController class
	/// </summary>	 
	/// <param name="shipper">The shipper.</param>	 
	/// <param name="shipperAddress">The shipperAddress.</param>	 
	/// <param name="shipperBankAccount">The shipperBankAccount.</param>	 
	/// <param name="shipperPhone">The shipperPhone.</param>	 
	/// <param name="logger">logger</param>
	/// <param name="messageHub">messageHub.</param>
	public ShipperRepository(COREAPPDENTINTERFACESDOMAIN.IShipper shipper, 
							COREAPPDENTINTERFACESDOMAIN.IShipperAddress shipperAddress, 
							COREAPPDENTINTERFACESDOMAIN.IShipperBankAccount shipperBankAccount, 
							COREAPPDENTINTERFACESDOMAIN.IShipperPhone shipperPhone, 
							MSLOGGING.ILogger<ShipperRepository> logger ,MESSAGEHUBINTERFACES.IMessageHub messageHub
							) : base(logger, messageHub)
	{
		this._shipper = shipper;
		this._shipperAddress = shipperAddress;
		this._shipperBankAccount = shipperBankAccount;
		this._shipperPhone = shipperPhone;
	}

	#endregion

	#region Methods

	/// <summary>
	/// Get Shippers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetShipperResponse> GetShippersAsync(COREAPPDATAREPOMODELSDOMAIN.GetShipperRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetShippersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShipperRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetShipperResponse response = new();

		try
		{
			var shippers = await this._shipper.GetShippersAsync(request).ConfigureAwait(false);
			response.Shippers = shippers;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetShippersAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShippersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShipperRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge Shippers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeShipperResponse> MergeShippersAsync(COREAPPDATAREPOMODELSDOMAIN.MergeShipperRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShippersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShipperRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeShipperResponse response = new();

		try
		{
			var mergeResult = await this._shipper.MergeShippersAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeShippersAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShippersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShipperRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves ShipperRepository.
	/// </summary>
	public async Task SaveShipperAsync()
	{
		Logger.LogInfo($"{nameof(SaveShipperAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShipperRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _shipper.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveShipperAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveShipperAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShipperRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
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
