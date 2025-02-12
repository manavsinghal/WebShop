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
/// Represents CustomerPhone Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The CustomerPhone Infrastructure DataRepositories (DOTNET090000).
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
	/// Get CustomerPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetCustomerPhoneResponse> GetCustomerPhonesAsync(COREAPPDATAREPOMODELSDOMAIN.GetCustomerPhoneRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetCustomerPhoneResponse response = new();

		try
		{
			var customerPhones = await this._customerPhone.GetCustomerPhonesAsync(request).ConfigureAwait(false);
			response.CustomerPhones = customerPhones;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetCustomerPhonesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge CustomerPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeCustomerPhoneResponse> MergeCustomerPhonesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeCustomerPhoneRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeCustomerPhoneResponse response = new();

		try
		{
			var mergeResult = await this._customerPhone.MergeCustomerPhonesAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeCustomerPhonesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomerPhonesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves CustomerRepository.
	/// </summary>
	public async Task SaveCustomerPhoneAsync()
	{
		Logger.LogInfo($"{nameof(SaveCustomerPhoneAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomerRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _customerPhone.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveCustomerPhoneAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveCustomerPhoneAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomerRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		}
	}


	#endregion

}
