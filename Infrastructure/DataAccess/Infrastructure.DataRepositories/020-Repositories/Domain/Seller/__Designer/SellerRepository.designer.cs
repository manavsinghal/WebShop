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
/// Represents Seller Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The Seller Infrastructure DataRepositories (DOTNET090000).
/// </remarks>
public partial class SellerRepository : INFRADATAREPO.DataRepositoryBase, COREAPPDREPOINTERFACESDOMAIN.ISellerRepository
{
	#region Fields

	private Boolean disposedValue;

	private readonly COREAPPDENTINTERFACESDOMAIN.ISeller _seller;

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
	/// <param name="seller">The seller.</param>	 
	/// <param name="sellerAddress">The sellerAddress.</param>	 
	/// <param name="sellerBankAccount">The sellerBankAccount.</param>	 
	/// <param name="sellerPhone">The sellerPhone.</param>	 
	/// <param name="logger">logger</param>
	/// <param name="messageHub">messageHub.</param>
	public SellerRepository(COREAPPDENTINTERFACESDOMAIN.ISeller seller, 
							COREAPPDENTINTERFACESDOMAIN.ISellerAddress sellerAddress, 
							COREAPPDENTINTERFACESDOMAIN.ISellerBankAccount sellerBankAccount, 
							COREAPPDENTINTERFACESDOMAIN.ISellerPhone sellerPhone, 
							MSLOGGING.ILogger<SellerRepository> logger ,MESSAGEHUBINTERFACES.IMessageHub messageHub
							) : base(logger, messageHub)
	{
		this._seller = seller;
		this._sellerAddress = sellerAddress;
		this._sellerBankAccount = sellerBankAccount;
		this._sellerPhone = sellerPhone;
	}

	#endregion

	#region Methods

	/// <summary>
	/// Get Sellers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetSellerResponse> GetSellersAsync(COREAPPDATAREPOMODELSDOMAIN.GetSellerRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetSellerResponse response = new();

		try
		{
			var sellers = await this._seller.GetSellersAsync(request).ConfigureAwait(false);
			response.Sellers = sellers;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetSellersAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge Sellers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeSellerResponse> MergeSellersAsync(COREAPPDATAREPOMODELSDOMAIN.MergeSellerRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeSellerResponse response = new();

		try
		{
			var mergeResult = await this._seller.MergeSellersAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeSellersAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves SellerRepository.
	/// </summary>
	public async Task SaveSellerAsync()
	{
		Logger.LogInfo($"{nameof(SaveSellerAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellerRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _seller.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveSellerAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveSellerAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellerRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
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
