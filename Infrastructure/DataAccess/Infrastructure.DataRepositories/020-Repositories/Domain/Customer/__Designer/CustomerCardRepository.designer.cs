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
/// Represents CustomerCard Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The CustomerCard Infrastructure DataRepositories (DOTNET090000).
/// </remarks>
public partial class CustomerRepository : INFRADATAREPO.DataRepositoryBase, COREAPPDREPOINTERFACESDOMAIN.ICustomerRepository
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	#endregion

	#region Methods

	/// <summary>
	/// Get CustomerCards
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetCustomerCardResponse> GetCustomerCardsAsync(COREAPPDATAREPOMODELSDOMAIN.GetCustomerCardRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerCardsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetCustomerCardResponse response = new();

		try
		{
			var customerCards = await this._customerCard.GetCustomerCardsAsync(request).ConfigureAwait(false);
			response.CustomerCards = customerCards;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetCustomerCardsAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerCardsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge CustomerCards
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeCustomerCardResponse> MergeCustomerCardsAsync(COREAPPDATAREPOMODELSDOMAIN.MergeCustomerCardRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerCardsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeCustomerCardResponse response = new();

		try
		{
			var mergeResult = await this._customerCard.MergeCustomerCardsAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeCustomerCardsAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerCardsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves CustomerRepository.
	/// </summary>
	public async Task SaveCustomerCardAsync()
	{
		Logger.LogInfo($"{nameof(SaveCustomerCardAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomerRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _customerCard.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveCustomerCardAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveCustomerCardAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomerRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		}
	}


	#endregion

}
