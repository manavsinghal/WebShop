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
/// Represents Customer Infrastructure DataEntities SqlServer (DOTNET090000).
/// </summary>
/// <remarks>
/// The Customer Infrastructure DataEntities SqlServer (DOTNET090000).
/// </remarks>
public partial class Customer : INFRADATAENTITYMSSQL.Entity<COREDOMAINDATAMODELSDOMAIN.Customer>, COREAPPDENTINTERFACESDOMAIN.ICustomer
{	
    #region Fields

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
	/// <param name="customerAddress">customerAddress</param>
	/// <param name="customerCard">customerCard</param>
	/// <param name="customerPhone">customerPhone</param>
	/// <param name="dbContext">dbContext</param>
	/// <param name="logger">logger</param>	
	/// <param name="messageHub"></param>
	public Customer(COREAPPDENTINTERFACESDOMAIN.ICustomerAddress customerAddress
					, COREAPPDENTINTERFACESDOMAIN.ICustomerCard customerCard
					, COREAPPDENTINTERFACESDOMAIN.ICustomerPhone customerPhone
					, DomainDbContext dbContext
					, DomainQueryDbContext queryDbContext 
					, MSLOGGING.ILogger<Customer> logger
					, MESSAGEHUBINTERFACES.IMessageHub messageHub
					) : base(dbContext, queryDbContext, logger, messageHub)
	{
		this._customerAddress = customerAddress;
		this._customerCard = customerCard;
		this._customerPhone = customerPhone;
	}

	#endregion

	#region Methods
 
	/// <summary>
    /// Get Customers
    /// </summary>
	/// <param name="request">request</param>
	public async Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.Customer>> GetCustomersAsync(COREAPPDATAREPOMODELSDOMAIN.GetCustomerRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Customer), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var query = this.AsIQuerable();
		try
		{
			if (request.MatchExpression is not null)
			{
				query = query.Where(request.MatchExpression);
			}
			else
			{
				Logger.LogWarning($"{nameof(GetCustomersAsync)} - MatchExpression is null", nameof(Customer), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataAccess);
			}
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetCustomersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetCustomersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetCustomersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(GetCustomersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetCustomersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Customer), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return query;
	}
 
	/// <summary>
    /// Merge Customers
    /// </summary>
	/// <param name="request">request</param>
	public virtual async Task<COREDOMAINDATAMODELS.MergeResult> MergeCustomersAsync(COREAPPDATAREPOMODELSDOMAIN.MergeCustomerRequest request)
	{
		Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(Customer), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));
 
		var mergeResult = new COREDOMAINDATAMODELS.MergeResult();
		try
		{
			foreach (var customer in request.Customers!)
			{
				Logger.LogDiagnosticsInfo($"Item State:{customer.ItemState}", nameof(Customer));

				var result = await this.MergeInternalAsync(customer).ConfigureAwait(false);
				mergeResult += result;
			}

			Logger.LogDiagnosticsInfo($"InsertCount Count:{mergeResult.InsertCount}, UpdateCount Count:{mergeResult.UpdateCount}, DeleteCount Count:{mergeResult.DeleteCount}", nameof(Customer));
		}
		catch (SqlException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeCustomersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DataException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeCustomersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (DBConcurrencyException ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeCustomersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		catch (Exception ex)
		{
			#region Handle Exception

			throw await this.HandleDBException(nameof(MergeCustomersAsync), ex).ConfigureAwait(false);

			#endregion
		}
		finally
		{
			#region Cleanup
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeCustomersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(Customer), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataAccess));

			#endregion
		}

		return mergeResult;
	}
	/// <summary>
    /// Merges the internal asynchronous.
    /// </summary>
	/// <param name="customer">The customer.</param>
    /// <returns></returns>
	internal async Task<COREDOMAINDATAMODELS.MergeResult> MergeInternalAsync(COREDOMAINDATAMODELSDOMAIN.Customer customer)
	{
		var mergeResult = await this.MergeAsync(customer);
		this.MergeChildEntities(customer);
		return mergeResult;
	}

	/// <summary>
    /// Merges the Child entities.
    /// </summary>
	/// <param name="customer">The customer.</param>
    /// <returns></returns>
	internal void MergeChildEntities(COREDOMAINDATAMODELSDOMAIN.Customer customer)
	{
		if (customer != null)
		{
			if (customer.CustomerAddresses != null && customer.CustomerAddresses.Any())
			{
				var mergeCustomerAddressRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeCustomerAddressRequest()
				{
					CustomerAddresses = customer.CustomerAddresses
				};
				this._customerAddress.MergeCustomerAddressesAsync(mergeCustomerAddressRequest);
			}

			if (customer.CustomerCards != null && customer.CustomerCards.Any())
			{
				var mergeCustomerCardRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeCustomerCardRequest()
				{
					CustomerCards = customer.CustomerCards
				};
				this._customerCard.MergeCustomerCardsAsync(mergeCustomerCardRequest);
			}

			if (customer.CustomerPhones != null && customer.CustomerPhones.Any())
			{
				var mergeCustomerPhoneRequest = new COREAPPDATAREPOMODELSDOMAIN.MergeCustomerPhoneRequest()
				{
					CustomerPhones = customer.CustomerPhones
				};
				this._customerPhone.MergeCustomerPhonesAsync(mergeCustomerPhoneRequest);
			}

		}
	}
	

	#endregion
}
