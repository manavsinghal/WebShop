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
/// Represents CustomerAddress Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The CustomerAddress Infrastructure DataRepositories (DOTNET090000).
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
	/// Get CustomerAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetCustomerAddressResponse> GetCustomerAddressesAsync(COREAPPDATAREPOMODELSDOMAIN.GetCustomerAddressRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetCustomerAddressResponse response = new();

		try
		{
			var customerAddresses = await this._customerAddress.GetCustomerAddressesAsync(request).ConfigureAwait(false);
			response.CustomerAddresses = customerAddresses;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetCustomerAddressesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge CustomerAddresses
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeCustomerAddressResponse> MergeCustomerAddressesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeCustomerAddressRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeCustomerAddressResponse response = new();

		try
		{
			var mergeResult = await this._customerAddress.MergeCustomerAddressesAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeCustomerAddressesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerAddressesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves CustomerRepository.
	/// </summary>
	public async Task SaveCustomerAddressAsync()
	{
		Logger.LogInfo($"{nameof(SaveCustomerAddressAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomerRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _customerAddress.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveCustomerAddressAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveCustomerAddressAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomerRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		}
	}


	#endregion

}
