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
/// Represents  DomainQueryDbContext Model.
/// </summary>
/// <remarks>
///  Represents  DomainQueryDbContext Model.
/// </remarks>
public partial class DomainQueryDbContext : DbContext
{
	#region Fields


	#endregion
	
	#region Properties

    /// <summary>
    /// Gets or sets the AccountUId
    /// </summary>
    /// <value>
    /// The AccountUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.Account> Accounts { get; set; }  

    /// <summary>
    /// Gets or sets the AccountStatusUId
    /// </summary>
    /// <value>
    /// The AccountStatusUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.AccountStatus> AccountStatuses { get; set; }  

    /// <summary>
    /// Gets or sets the AppSettingUId
    /// </summary>
    /// <value>
    /// The AppSettingUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.AppSetting> AppSettings { get; set; }  

    /// <summary>
    /// Gets or sets the CountryUId
    /// </summary>
    /// <value>
    /// The CountryUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.Country> Countries { get; set; }  

    /// <summary>
    /// Gets or sets the CountryLanguageUId
    /// </summary>
    /// <value>
    /// The CountryLanguageUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.CountryLanguage> CountryLanguages { get; set; }  

    /// <summary>
    /// Gets or sets the CustomerUId
    /// </summary>
    /// <value>
    /// The CustomerUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.Customer> Customers { get; set; }  

    /// <summary>
    /// Gets or sets the CustomerAddressUId
    /// </summary>
    /// <value>
    /// The CustomerAddressUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.CustomerAddress> CustomerAddresses { get; set; }  

    /// <summary>
    /// Gets or sets the CustomerCardUId
    /// </summary>
    /// <value>
    /// The CustomerCardUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.CustomerCard> CustomerCards { get; set; }  

    /// <summary>
    /// Gets or sets the CustomerPhoneUId
    /// </summary>
    /// <value>
    /// The CustomerPhoneUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.CustomerPhone> CustomerPhones { get; set; }  

    /// <summary>
    /// Gets or sets the EntityUId
    /// </summary>
    /// <value>
    /// The EntityUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.Entity> Entities { get; set; }  

    /// <summary>
    /// Gets or sets the LanguageUId
    /// </summary>
    /// <value>
    /// The LanguageUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.Language> Languages { get; set; }  

    /// <summary>
    /// Gets or sets the MasterListUId
    /// </summary>
    /// <value>
    /// The MasterListUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.MasterList> MasterLists { get; set; }  

    /// <summary>
    /// Gets or sets the MasterListItemUId
    /// </summary>
    /// <value>
    /// The MasterListItemUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.MasterListItem> MasterListItems { get; set; }  

    /// <summary>
    /// Gets or sets the OrderUId
    /// </summary>
    /// <value>
    /// The OrderUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.Order> Orders { get; set; }  

    /// <summary>
    /// Gets or sets the OrderItemUId
    /// </summary>
    /// <value>
    /// The OrderItemUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.OrderItem> OrderItems { get; set; }  

    /// <summary>
    /// Gets or sets the OrderItemShipmentUId
    /// </summary>
    /// <value>
    /// The OrderItemShipmentUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.OrderItemShipment> OrderItemShipments { get; set; }  

    /// <summary>
    /// Gets or sets the ProductUId
    /// </summary>
    /// <value>
    /// The ProductUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.Product> Products { get; set; }  

    /// <summary>
    /// Gets or sets the ProductCategoryUId
    /// </summary>
    /// <value>
    /// The ProductCategoryUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.ProductCategory> ProductCategories { get; set; }  

    /// <summary>
    /// Gets or sets the ProductCategoryLanguageUId
    /// </summary>
    /// <value>
    /// The ProductCategoryLanguageUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.ProductCategoryLanguage> ProductCategoryLanguages { get; set; }  

    /// <summary>
    /// Gets or sets the ProductLanguageUId
    /// </summary>
    /// <value>
    /// The ProductLanguageUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.ProductLanguage> ProductLanguages { get; set; }  

    /// <summary>
    /// Gets or sets the RowStatusUId
    /// </summary>
    /// <value>
    /// The RowStatusUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.RowStatus> RowStatuses { get; set; }  

    /// <summary>
    /// Gets or sets the SellerUId
    /// </summary>
    /// <value>
    /// The SellerUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.Seller> Sellers { get; set; }  

    /// <summary>
    /// Gets or sets the SellerAddressUId
    /// </summary>
    /// <value>
    /// The SellerAddressUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.SellerAddress> SellerAddresses { get; set; }  

    /// <summary>
    /// Gets or sets the SellerBankAccountUId
    /// </summary>
    /// <value>
    /// The SellerBankAccountUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.SellerBankAccount> SellerBankAccounts { get; set; }  

    /// <summary>
    /// Gets or sets the SellerPhoneUId
    /// </summary>
    /// <value>
    /// The SellerPhoneUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.SellerPhone> SellerPhones { get; set; }  

    /// <summary>
    /// Gets or sets the ShipperUId
    /// </summary>
    /// <value>
    /// The ShipperUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.Shipper> Shippers { get; set; }  

    /// <summary>
    /// Gets or sets the ShipperAddressUId
    /// </summary>
    /// <value>
    /// The ShipperAddressUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.ShipperAddress> ShipperAddresses { get; set; }  

    /// <summary>
    /// Gets or sets the ShipperBankAccountUId
    /// </summary>
    /// <value>
    /// The ShipperBankAccountUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.ShipperBankAccount> ShipperBankAccounts { get; set; }  

    /// <summary>
    /// Gets or sets the ShipperPhoneUId
    /// </summary>
    /// <value>
    /// The ShipperPhoneUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.ShipperPhone> ShipperPhones { get; set; }  

    /// <summary>
    /// Gets or sets the ShoppingCartUId
    /// </summary>
    /// <value>
    /// The ShoppingCartUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.ShoppingCart> ShoppingCarts { get; set; }  

    /// <summary>
    /// Gets or sets the ShoppingCartWishListUId
    /// </summary>
    /// <value>
    /// The ShoppingCartWishListUId
    /// </value>
	public virtual DbSet<COREDOMAINDATAMODELSDOMAIN.ShoppingCartWishList> ShoppingCartWishLists { get; set; }  

   #endregion
 
 	#region Constructors

	#endregion

	#region Methods
	
	/// <summary>
	/// <para>
	/// Override this method to configure the database (and other options) to be used for this context.
	/// This method is called for each instance of the context that is created.
	/// The base implementation does nothing.
	/// </para>
	/// <para>
	/// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
	/// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
	/// the options have already been set, and skip some or all of the logic in
	/// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
	/// </para>
	/// </summary>
	/// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
	/// typically define extension methods on this object that allow you to configure the context.</param>
	/// <remarks>
	/// See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see>
	/// for more information.
	/// </remarks>
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var connectionString = SHAREDKERNALLIB.AppSettings.GetQueryConnectionString();
		optionsBuilder.UseSqlServer(connectionString, options =>
		{
			options.EnableRetryOnFailure(
				maxRetryCount: 5,
				maxRetryDelay: TimeSpan.FromSeconds(30),
				errorNumbersToAdd: null);
		});

		base.OnConfiguring(optionsBuilder);
	}

	#endregion
}
