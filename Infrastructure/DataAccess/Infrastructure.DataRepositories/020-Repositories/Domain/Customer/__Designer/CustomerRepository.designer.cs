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
/// Represents Customer Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The Customer Infrastructure DataRepositories (DOTNET090000).
/// </remarks>
public partial class CustomerRepository : INFRADATAREPO.DataRepositoryBase, COREAPPDREPOINTERFACESDOMAIN.ICustomerRepository
{
	#region Fields

	private Boolean disposedValue;

	private readonly COREAPPDENTINTERFACESDOMAIN.ICustomer _customer;

	private readonly COREAPPDENTINTERFACESDOMAIN.ICustomerAddress _customerAddress;

	private readonly COREAPPDENTINTERFACESDOMAIN.ICustomerCard _customerCard;

	private readonly COREAPPDENTINTERFACESDOMAIN.ICustomerPhone _customerPhone;

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the CustomerController class
	/// </summary>	 
	/// <param name="customer">The customer.</param>	 
	/// <param name="customerAddress">The customerAddress.</param>	 
	/// <param name="customerCard">The customerCard.</param>	 
	/// <param name="customerPhone">The customerPhone.</param>	 
	/// <param name="logger">logger</param>
	/// <param name="messageHub">messageHub.</param>
	public CustomerRepository(COREAPPDENTINTERFACESDOMAIN.ICustomer customer, 
							COREAPPDENTINTERFACESDOMAIN.ICustomerAddress customerAddress, 
							COREAPPDENTINTERFACESDOMAIN.ICustomerCard customerCard, 
							COREAPPDENTINTERFACESDOMAIN.ICustomerPhone customerPhone, 
							MSLOGGING.ILogger<CustomerRepository> logger ,MESSAGEHUBINTERFACES.IMessageHub messageHub
							) : base(logger, messageHub)
	{
		this._customer = customer;
		this._customerAddress = customerAddress;
		this._customerCard = customerCard;
		this._customerPhone = customerPhone;
	}

	#endregion

	#region Methods

	/// <summary>
	/// Get Customers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetCustomerResponse> GetCustomersAsync(COREAPPDATAREPOMODELSDOMAIN.GetCustomerRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetCustomerResponse response = new();

		try
		{
			var customers = await this._customer.GetCustomersAsync(request).ConfigureAwait(false);
			response.Customers = customers;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetCustomersAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge Customers
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeCustomerResponse> MergeCustomersAsync(COREAPPDATAREPOMODELSDOMAIN.MergeCustomerRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeCustomerResponse response = new();

		try
		{
			var mergeResult = await this._customer.MergeCustomersAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeCustomersAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomerRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves CustomerRepository.
	/// </summary>
	public async Task SaveCustomerAsync()
	{
		Logger.LogInfo($"{nameof(SaveCustomerAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(CustomerRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _customer.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveCustomerAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveCustomerAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(CustomerRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
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
