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
/// Represents SellerAddress Infrastructure DataEntities SqlServer (DOTNET090000).
/// </summary>
/// <remarks>
/// The SellerAddress Infrastructure DataEntities SqlServer (DOTNET090000).
/// </remarks>
public partial class SellerAddress : INFRADATAENTITYMSSQL.Entity<COREDOMAINDATAMODELSDOMAIN.SellerAddress>, COREAPPDENTINTERFACESDOMAIN.ISellerAddress
{	
    #region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
    /// Initializes a new instance of the SellerAddressController class
    /// </summary>
	/// <param name="dbContext">dbContext</param>
	/// <param name="logger">logger</param>	
	/// <param name="messageHub"></param>
	public SellerAddress(DomainDbContext dbContext
					, DomainQueryDbContext queryDbContext 
					, MSLOGGING.ILogger<SellerAddress> logger
					, MESSAGEHUBINTERFACES.IMessageHub messageHub
					) : base(dbContext, queryDbContext, logger, messageHub)
	{
	}

	#endregion

	#region Methods
 
	/// <summary>
    /// Get SellerAddresses
    /// </summary>
	/// <param name="request">request</param>
	public async Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.SellerAddress>> GetSellerAddressesAsync(COREAPPDATAREPOMODELSDOMAIN.GetSellerAddressRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellerAddress), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var query = this.AsIQuerable();
		try
		{
			if (request.MatchExpression is not null)
			{
				query = query.Where(request.MatchExpression);
			}
			else
			{
				Logger.LogWarning($"{nameof(GetSellerAddressesAsync)} - MatchExpression is null", nameof(SellerAddress), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataAccess);
			}
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetSellerAddressesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetSellerAddressesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetSellerAddressesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetSellerAddressesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellerAddress), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return query;
	}
 
	/// <summary>
    /// Merge SellerAddresses
    /// </summary>
	/// <param name="request">request</param>
	public virtual async Task<COREDOMAINDATAMODELS.MergeResult> MergeSellerAddressesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeSellerAddressRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellerAddress), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var mergeResult = new COREDOMAINDATAMODELS.MergeResult();
		try
		{
			foreach (var sellerAddress in request.SellerAddresses!)
			{
				Logger.LogDiagnosticsInfo($"Item State:{sellerAddress.ItemState}", nameof(SellerAddress));

				var result = await this.MergeInternalAsync(sellerAddress).ConfigureAwait(false);
				mergeResult += result;
			}

			Logger.LogDiagnosticsInfo($"InsertCount Count:{mergeResult.InsertCount}, UpdateCount Count:{mergeResult.UpdateCount}, DeleteCount Count:{mergeResult.DeleteCount}", nameof(SellerAddress));
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeSellerAddressesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeSellerAddressesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeSellerAddressesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeSellerAddressesAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellerAddress), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return mergeResult;
	}
	/// <summary>
    /// Merges the internal asynchronous.
    /// </summary>
	/// <param name="sellerAddress">The sellerAddress.</param>
    /// <returns></returns>
	internal async Task<COREDOMAINDATAMODELS.MergeResult> MergeInternalAsync(COREDOMAINDATAMODELSDOMAIN.SellerAddress sellerAddress)
	{
		var mergeResult = await this.MergeAsync(sellerAddress);
		return mergeResult;
	}

	

	#endregion
}
