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
/// Represents CustomerAddress Infrastructure DataEntities SqlServer (DOTNET090000).
/// </summary>
/// <remarks>
/// The CustomerAddress Infrastructure DataEntities SqlServer (DOTNET090000).
/// </remarks>
public partial class CustomerAddress : INFRADATAENTITYMSSQL.Entity<COREDOMAINDATAMODELSDOMAIN.CustomerAddress>, COREAPPDENTINTERFACESDOMAIN.ICustomerAddress
{	
    #region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
    /// Initializes a new instance of the CustomerAddressController class
    /// </summary>
	/// <param name="dbContext">dbContext</param>
	/// <param name="logger">logger</param>	
	/// <param name="messageHub"></param>
	public CustomerAddress(DomainDbContext dbContext
					, DomainQueryDbContext queryDbContext 
					, MSLOGGING.ILogger<CustomerAddress> logger
					, MESSAGEHUBINTERFACES.IMessageHub messageHub
					) : base(dbContext, queryDbContext, logger, messageHub)
	{
	}

	#endregion

	#region Methods
 
	/// <summary>
    /// Get CustomerAddresses
    /// </summary>
	/// <param name="request">request</param>
	public async Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.CustomerAddress>> GetCustomerAddressesAsync(COREAPPDATAREPOMODELSDOMAIN.GetCustomerAddressRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomerAddress), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var query = this.AsIQuerable();
		try
		{
			if (request.MatchExpression is not null)
			{
				query = query.Where(request.MatchExpression);
			}
			else
			{
				Logger.LogWarning($"{nameof(GetCustomerAddressesAsync)} - MatchExpression is null", nameof(CustomerAddress), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataAccess);
			}
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetCustomerAddressesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetCustomerAddressesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetCustomerAddressesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetCustomerAddressesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomerAddress), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return query;
	}
 
	/// <summary>
    /// Merge CustomerAddresses
    /// </summary>
	/// <param name="request">request</param>
	public virtual async Task<COREDOMAINDATAMODELS.MergeResult> MergeCustomerAddressesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeCustomerAddressRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomerAddress), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var mergeResult = new COREDOMAINDATAMODELS.MergeResult();
		try
		{
			foreach (var customerAddress in request.CustomerAddresses!)
			{
				Logger.LogDiagnosticsInfo($"Item State:{customerAddress.ItemState}", nameof(CustomerAddress));

				var result = await this.MergeInternalAsync(customerAddress).ConfigureAwait(false);
				mergeResult += result;
			}

			Logger.LogDiagnosticsInfo($"InsertCount Count:{mergeResult.InsertCount}, UpdateCount Count:{mergeResult.UpdateCount}, DeleteCount Count:{mergeResult.DeleteCount}", nameof(CustomerAddress));
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeCustomerAddressesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeCustomerAddressesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeCustomerAddressesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeCustomerAddressesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomerAddress), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return mergeResult;
	}
	/// <summary>
    /// Merges the internal asynchronous.
    /// </summary>
	/// <param name="customerAddress">The customerAddress.</param>
    /// <returns></returns>
	internal async Task<COREDOMAINDATAMODELS.MergeResult> MergeInternalAsync(COREDOMAINDATAMODELSDOMAIN.CustomerAddress customerAddress)
	{
		var mergeResult = await this.MergeAsync(customerAddress);
		return mergeResult;
	}

	

	#endregion
}
