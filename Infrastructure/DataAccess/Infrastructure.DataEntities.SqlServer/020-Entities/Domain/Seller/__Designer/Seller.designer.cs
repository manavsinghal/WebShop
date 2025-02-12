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
/// Represents Seller Infrastructure DataEntities SqlServer (DOTNET090000).
/// </summary>
/// <remarks>
/// The Seller Infrastructure DataEntities SqlServer (DOTNET090000).
/// </remarks>
public partial class Seller : INFRADATAENTITYMSSQL.Entity<COREDOMAINDATAMODELSDOMAIN.Seller>, COREAPPDENTINTERFACESDOMAIN.ISeller
{	
    #region Fields

	private readonly COREAPPDENTINTERFACESDOMAIN.ISellerAddress _sellerAddress; 

	private readonly COREAPPDENTINTERFACESDOMAIN.ISellerBankAccount _sellerBankAccount; 

	private readonly COREAPPDENTINTERFACESDOMAIN.ISellerPhone _sellerPhone; 

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
    /// Initializes a new instance of the SellerController class
    /// </summary>
	/// <param name="sellerAddress">sellerAddress</param>
	/// <param name="sellerBankAccount">sellerBankAccount</param>
	/// <param name="sellerPhone">sellerPhone</param>
	/// <param name="dbContext">dbContext</param>
	/// <param name="logger">logger</param>	
	/// <param name="messageHub"></param>
	public Seller(COREAPPDENTINTERFACESDOMAIN.ISellerAddress sellerAddress
					, COREAPPDENTINTERFACESDOMAIN.ISellerBankAccount sellerBankAccount
					, COREAPPDENTINTERFACESDOMAIN.ISellerPhone sellerPhone
					, DomainDbContext dbContext
					, DomainQueryDbContext queryDbContext 
					, MSLOGGING.ILogger<Seller> logger
					, MESSAGEHUBINTERFACES.IMessageHub messageHub
					) : base(dbContext, queryDbContext, logger, messageHub)
	{
		this._sellerAddress = sellerAddress;
		this._sellerBankAccount = sellerBankAccount;
		this._sellerPhone = sellerPhone;
	}

	#endregion

	#region Methods
 
	/// <summary>
    /// Get Sellers
    /// </summary>
	/// <param name="request">request</param>
	public async Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.Seller>> GetSellersAsync(COREAPPDATAREPOMODELSDOMAIN.GetSellerRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Seller), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var query = this.AsIQuerable();
		try
		{
			if (request.MatchExpression is not null)
			{
				query = query.Where(request.MatchExpression);
			}
			else
			{
				Logger.LogWarning($"{nameof(GetSellersAsync)} - MatchExpression is null", nameof(Seller), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataAccess);
			}
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetSellersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetSellersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetSellersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetSellersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Seller), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return query;
	}
 
	/// <summary>
    /// Merge Sellers
    /// </summary>
	/// <param name="request">request</param>
	public virtual async Task<COREDOMAINDATAMODELS.MergeResult> MergeSellersAsync(COREAPPDATAREPOMODELSDOMAIN.MergeSellerRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Seller), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var mergeResult = new COREDOMAINDATAMODELS.MergeResult();
		try
		{
			foreach (var seller in request.Sellers!)
			{
				Logger.LogDiagnosticsInfo($"Item State:{seller.ItemState}", nameof(Seller));

				var result = await this.MergeInternalAsync(seller).ConfigureAwait(false);
				mergeResult += result;
			}

			Logger.LogDiagnosticsInfo($"InsertCount Count:{mergeResult.InsertCount}, UpdateCount Count:{mergeResult.UpdateCount}, DeleteCount Count:{mergeResult.DeleteCount}", nameof(Seller));
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeSellersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeSellersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeSellersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeSellersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Seller), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return mergeResult;
	}
	/// <summary>
    /// Merges the internal asynchronous.
    /// </summary>
	/// <param name="seller">The seller.</param>
    /// <returns></returns>
	internal async Task<COREDOMAINDATAMODELS.MergeResult> MergeInternalAsync(COREDOMAINDATAMODELSDOMAIN.Seller seller)
	{
		var mergeResult = await this.MergeAsync(seller);
		this.MergeChildEntities(seller);
		return mergeResult;
	}

	/// <summary>
    /// Merges the Child entities.
    /// </summary>
	/// <param name="seller">The seller.</param>
    /// <returns></returns>
	internal void MergeChildEntities(COREDOMAINDATAMODELSDOMAIN.Seller seller)
	{
		if (seller != null)
		{
			if (seller.SellerAddresses != null && seller.SellerAddresses.Any())
			{
				var mergeSellerAddressRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeSellerAddressRequest()
				{
					SellerAddresses = seller.SellerAddresses
				};
				this._sellerAddress.MergeSellerAddressesAsync(mergeSellerAddressRequest);
			}

			if (seller.SellerBankAccounts != null && seller.SellerBankAccounts.Any())
			{
				var mergeSellerBankAccountRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeSellerBankAccountRequest()
				{
					SellerBankAccounts = seller.SellerBankAccounts
				};
				this._sellerBankAccount.MergeSellerBankAccountsAsync(mergeSellerBankAccountRequest);
			}

			if (seller.SellerPhones != null && seller.SellerPhones.Any())
			{
				var mergeSellerPhoneRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeSellerPhoneRequest()
				{
					SellerPhones = seller.SellerPhones
				};
				this._sellerPhone.MergeSellerPhonesAsync(mergeSellerPhoneRequest);
			}

		}
	}
	

	#endregion
}
