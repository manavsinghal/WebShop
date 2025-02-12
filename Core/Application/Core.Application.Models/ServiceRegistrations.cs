#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Core.Application.Models;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents Service Registrations class.
/// </summary>
public static class ServiceRegistrations
{
	/// <summary>
	/// Registers the services.
	/// </summary>
	/// <param name="services">The services.</param>
	public static void RegisterServices(this IServiceCollection services)
    {	
		_ = services.AddTransient<COREAPPINTERFACESDOMAIN.IAccount, COREAPPMODELSDOMAIN.Account>()
					.AddTransient<COREAPPINTERFACESDOMAIN.IMaster, COREAPPMODELSDOMAIN.Master>()
					.AddTransient<COREAPPINTERFACESDOMAIN.ICustomer, COREAPPMODELSDOMAIN.Customer>()
					.AddTransient<COREAPPINTERFACESDOMAIN.ISeller, COREAPPMODELSDOMAIN.Seller>()
					.AddTransient<COREAPPINTERFACESDOMAIN.IProduct, COREAPPMODELSDOMAIN.Product>()
					.AddTransient<COREAPPINTERFACESDOMAIN.IShoppingCart, COREAPPMODELSDOMAIN.ShoppingCart>()
					.AddTransient<COREAPPINTERFACESDOMAIN.IShipper, COREAPPMODELSDOMAIN.Shipper>()
					.AddTransient<COREAPPINTERFACESDOMAIN.IOrder, COREAPPMODELSDOMAIN.Order>()
		
					.AddSingleton<COREAPPINTERFACESDBAPPSETTINGS.IDbAppSettings, COREAPPDATAMODELSDBAPPSETTINGS.DbAppSettings>()
					.AddScoped(typeof(MESSAGEHUBINTERFACES.IMessageHub), typeof(MessageHub.MessageHub));
	}
}	
