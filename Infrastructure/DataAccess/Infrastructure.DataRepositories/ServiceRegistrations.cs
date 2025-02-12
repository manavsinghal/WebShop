#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Infrastructure.DataRepositories;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents Service Registrations class.
/// </summary>
public static class ServiceRegistrations
{
	/// <summary>
	/// Registers the data services.
	/// </summary>
	/// <param name="services">The services.</param>
	public static void RegisterDataServices(this IServiceCollection services)
    {		
		_ = services.AddTransient<COREAPPDREPOINTERFACESDOMAIN.IAccountRepository, INFRADATAREPODOMAIN.AccountRepository>()
					.AddTransient<COREAPPDREPOINTERFACESDOMAIN.ICustomerRepository, INFRADATAREPODOMAIN.CustomerRepository>()
					.AddTransient<COREAPPDREPOINTERFACESDOMAIN.IMasterRepository, INFRADATAREPODOMAIN.MasterRepository>()
					.AddTransient<COREAPPDREPOINTERFACESDOMAIN.IOrderRepository, INFRADATAREPODOMAIN.OrderRepository>()
					.AddTransient<COREAPPDREPOINTERFACESDOMAIN.IProductRepository, INFRADATAREPODOMAIN.ProductRepository>()
					.AddTransient<COREAPPDREPOINTERFACESDOMAIN.ISellerRepository, INFRADATAREPODOMAIN.SellerRepository>()
					.AddTransient<COREAPPDREPOINTERFACESDOMAIN.IShipperRepository, INFRADATAREPODOMAIN.ShipperRepository>()
					.AddTransient<COREAPPDREPOINTERFACESDOMAIN.IShoppingCartRepository, INFRADATAREPODOMAIN.ShoppingCartRepository>()
		
					.AddScoped(typeof(MESSAGEHUBINTERFACES.IMessageHub), typeof(MessageHub.MessageHub));
	}
}	
