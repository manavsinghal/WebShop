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
/// Represents SellerPhone Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The SellerPhone Infrastructure DataRepositories (DOTNET090000).
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
	/// Get SellerPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetSellerPhoneResponse> GetSellerPhonesAsync(COREAPPDATAREPOMODELSDOMAIN.GetSellerPhoneRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetSellerPhoneResponse response = new();

		try
		{
			var sellerPhones = await this._sellerPhone.GetSellerPhonesAsync(request).ConfigureAwait(false);
			response.SellerPhones = sellerPhones;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetSellerPhonesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge SellerPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeSellerPhoneResponse> MergeSellerPhonesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeSellerPhoneRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeSellerPhoneResponse response = new();

		try
		{
			var mergeResult = await this._sellerPhone.MergeSellerPhonesAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeSellerPhonesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves SellerRepository.
	/// </summary>
	public async Task SaveSellerPhoneAsync()
	{
		Logger.LogInfo($"{nameof(SaveSellerPhoneAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellerRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _sellerPhone.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveSellerPhoneAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveSellerPhoneAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellerRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		}
	}


	#endregion

}
