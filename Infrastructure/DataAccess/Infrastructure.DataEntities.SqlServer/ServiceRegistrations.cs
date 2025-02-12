#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Infrastructure.DataEntities.SqlServer;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// ServiceRegistrations class
/// </summary>
public static class ServiceRegistrations
{
	/// <summary>
	/// Registers the SQL services.
	/// </summary>
	/// <param name="services">The services.</param>
	public static void RegisterSqlServices(this IServiceCollection services)
    {		
		_ = services.AddTransient<COREAPPDENTINTERFACESDOMAIN.IAccount, INFRADATAENTITYMSSQLDOMAIN.Account>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.IAccountStatus, INFRADATAENTITYMSSQLDOMAIN.AccountStatus>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.IAppSetting, INFRADATAENTITYMSSQLDOMAIN.AppSetting>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.ICountry, INFRADATAENTITYMSSQLDOMAIN.Country>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.ICountryLanguage, INFRADATAENTITYMSSQLDOMAIN.CountryLanguage>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.ICustomer, INFRADATAENTITYMSSQLDOMAIN.Customer>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.ICustomerAddress, INFRADATAENTITYMSSQLDOMAIN.CustomerAddress>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.ICustomerCard, INFRADATAENTITYMSSQLDOMAIN.CustomerCard>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.ICustomerPhone, INFRADATAENTITYMSSQLDOMAIN.CustomerPhone>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.IEntity, INFRADATAENTITYMSSQLDOMAIN.Entity>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.ILanguage, INFRADATAENTITYMSSQLDOMAIN.Language>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.IMasterList, INFRADATAENTITYMSSQLDOMAIN.MasterList>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.IMasterListItem, INFRADATAENTITYMSSQLDOMAIN.MasterListItem>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.IOrder, INFRADATAENTITYMSSQLDOMAIN.Order>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.IOrderItem, INFRADATAENTITYMSSQLDOMAIN.OrderItem>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.IOrderItemShipment, INFRADATAENTITYMSSQLDOMAIN.OrderItemShipment>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.IProduct, INFRADATAENTITYMSSQLDOMAIN.Product>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.IProductCategory, INFRADATAENTITYMSSQLDOMAIN.ProductCategory>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.IProductCategoryLanguage, INFRADATAENTITYMSSQLDOMAIN.ProductCategoryLanguage>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.IProductLanguage, INFRADATAENTITYMSSQLDOMAIN.ProductLanguage>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.IRowStatus, INFRADATAENTITYMSSQLDOMAIN.RowStatus>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.ISeller, INFRADATAENTITYMSSQLDOMAIN.Seller>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.ISellerAddress, INFRADATAENTITYMSSQLDOMAIN.SellerAddress>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.ISellerBankAccount, INFRADATAENTITYMSSQLDOMAIN.SellerBankAccount>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.ISellerPhone, INFRADATAENTITYMSSQLDOMAIN.SellerPhone>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.IShipper, INFRADATAENTITYMSSQLDOMAIN.Shipper>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.IShipperAddress, INFRADATAENTITYMSSQLDOMAIN.ShipperAddress>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.IShipperBankAccount, INFRADATAENTITYMSSQLDOMAIN.ShipperBankAccount>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.IShipperPhone, INFRADATAENTITYMSSQLDOMAIN.ShipperPhone>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.IShoppingCart, INFRADATAENTITYMSSQLDOMAIN.ShoppingCart>()
					.AddTransient<COREAPPDENTINTERFACESDOMAIN.IShoppingCartWishList, INFRADATAENTITYMSSQLDOMAIN.ShoppingCartWishList>();
		_ = services.AddScoped<INFRADATAENTITYMSSQLDOMAIN.DomainDbContext>();
		_ = services.AddScoped<INFRADATAENTITYMSSQLDOMAIN.DomainQueryDbContext>()
					.AddScoped(typeof(MESSAGEHUBINTERFACES.IMessageHub), typeof(MessageHub.MessageHub));
    }
}	
