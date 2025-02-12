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
/// Represents Shipper Infrastructure DataEntities SqlServer (DOTNET090000).
/// </summary>
/// <remarks>
/// The Shipper Infrastructure DataEntities SqlServer (DOTNET090000).
/// </remarks>
public partial class Shipper : INFRADATAENTITYMSSQL.Entity<COREDOMAINDATAMODELSDOMAIN.Shipper>, COREAPPDENTINTERFACESDOMAIN.IShipper
{	
    #region Fields

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
	/// <param name="shipperAddress">shipperAddress</param>
	/// <param name="shipperBankAccount">shipperBankAccount</param>
	/// <param name="shipperPhone">shipperPhone</param>
	/// <param name="dbContext">dbContext</param>
	/// <param name="logger">logger</param>	
	/// <param name="messageHub"></param>
	public Shipper(COREAPPDENTINTERFACESDOMAIN.IShipperAddress shipperAddress
					, COREAPPDENTINTERFACESDOMAIN.IShipperBankAccount shipperBankAccount
					, COREAPPDENTINTERFACESDOMAIN.IShipperPhone shipperPhone
					, DomainDbContext dbContext
					, DomainQueryDbContext queryDbContext 
					, MSLOGGING.ILogger<Shipper> logger
					, MESSAGEHUBINTERFACES.IMessageHub messageHub
					) : base(dbContext, queryDbContext, logger, messageHub)
	{
		this._shipperAddress = shipperAddress;
		this._shipperBankAccount = shipperBankAccount;
		this._shipperPhone = shipperPhone;
	}

	#endregion

	#region Methods
 
	/// <summary>
    /// Get Shippers
    /// </summary>
	/// <param name="request">request</param>
	public async Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.Shipper>> GetShippersAsync(COREAPPDATAREPOMODELSDOMAIN.GetShipperRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetShippersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Shipper), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var query = this.AsIQuerable();
		try
		{
			if (request.MatchExpression is not null)
			{
				query = query.Where(request.MatchExpression);
			}
			else
			{
				Logger.LogWarning($"{nameof(GetShippersAsync)} - MatchExpression is null", nameof(Shipper), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataAccess);
			}
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetShippersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetShippersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetShippersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetShippersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShippersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Shipper), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return query;
	}
 
	/// <summary>
    /// Merge Shippers
    /// </summary>
	/// <param name="request">request</param>
	public virtual async Task<COREDOMAINDATAMODELS.MergeResult> MergeShippersAsync(COREAPPDATAREPOMODELSDOMAIN.MergeShipperRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShippersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Shipper), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var mergeResult = new COREDOMAINDATAMODELS.MergeResult();
		try
		{
			foreach (var shipper in request.Shippers!)
			{
				Logger.LogDiagnosticsInfo($"Item State:{shipper.ItemState}", nameof(Shipper));

				var result = await this.MergeInternalAsync(shipper).ConfigureAwait(false);
				mergeResult += result;
			}

			Logger.LogDiagnosticsInfo($"InsertCount Count:{mergeResult.InsertCount}, UpdateCount Count:{mergeResult.UpdateCount}, DeleteCount Count:{mergeResult.DeleteCount}", nameof(Shipper));
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeShippersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeShippersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeShippersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeShippersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShippersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Shipper), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return mergeResult;
	}
	/// <summary>
    /// Merges the internal asynchronous.
    /// </summary>
	/// <param name="shipper">The shipper.</param>
    /// <returns></returns>
	internal async Task<COREDOMAINDATAMODELS.MergeResult> MergeInternalAsync(COREDOMAINDATAMODELSDOMAIN.Shipper shipper)
	{
		var mergeResult = await this.MergeAsync(shipper);
		this.MergeChildEntities(shipper);
		return mergeResult;
	}

	/// <summary>
    /// Merges the Child entities.
    /// </summary>
	/// <param name="shipper">The shipper.</param>
    /// <returns></returns>
	internal void MergeChildEntities(COREDOMAINDATAMODELSDOMAIN.Shipper shipper)
	{
		if (shipper != null)
		{
			if (shipper.ShipperAddresses != null && shipper.ShipperAddresses.Any())
			{
				var mergeShipperAddressRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeShipperAddressRequest()
				{
					ShipperAddresses = shipper.ShipperAddresses
				};
				this._shipperAddress.MergeShipperAddressesAsync(mergeShipperAddressRequest);
			}

			if (shipper.ShipperBankAccounts != null && shipper.ShipperBankAccounts.Any())
			{
				var mergeShipperBankAccountRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeShipperBankAccountRequest()
				{
					ShipperBankAccounts = shipper.ShipperBankAccounts
				};
				this._shipperBankAccount.MergeShipperBankAccountsAsync(mergeShipperBankAccountRequest);
			}

			if (shipper.ShipperPhones != null && shipper.ShipperPhones.Any())
			{
				var mergeShipperPhoneRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeShipperPhoneRequest()
				{
					ShipperPhones = shipper.ShipperPhones
				};
				this._shipperPhone.MergeShipperPhonesAsync(mergeShipperPhoneRequest);
			}

		}
	}
	

	#endregion
}
