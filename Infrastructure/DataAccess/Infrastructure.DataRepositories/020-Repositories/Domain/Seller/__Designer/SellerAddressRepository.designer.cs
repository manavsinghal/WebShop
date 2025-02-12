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
/// Represents SellerAddress Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The SellerAddress Infrastructure DataRepositories (DOTNET090000).
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
	/// Get SellerAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetSellerAddressResponse> GetSellerAddressesAsync(COREAPPDATAREPOMODELSDOMAIN.GetSellerAddressRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetSellerAddressResponse response = new();

		try
		{
			var sellerAddresses = await this._sellerAddress.GetSellerAddressesAsync(request).ConfigureAwait(false);
			response.SellerAddresses = sellerAddresses;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetSellerAddressesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge SellerAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeSellerAddressResponse> MergeSellerAddressesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeSellerAddressRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeSellerAddressResponse response = new();

		try
		{
			var mergeResult = await this._sellerAddress.MergeSellerAddressesAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeSellerAddressesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves SellerRepository.
	/// </summary>
	public async Task SaveSellerAddressAsync()
	{
		Logger.LogInfo($"{nameof(SaveSellerAddressAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellerRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _sellerAddress.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveSellerAddressAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveSellerAddressAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellerRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		}
	}


	#endregion

}
