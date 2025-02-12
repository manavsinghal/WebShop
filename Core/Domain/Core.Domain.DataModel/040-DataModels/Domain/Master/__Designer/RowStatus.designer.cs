#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion  

namespace Accenture.WebShop.Core.Domain.DataModels.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents RowStatus DataModel.
/// </summary>
/// <remarks>
/// The RowStatus DataModel.
/// </remarks>
[Table("RowStatus", Schema = "dbo")]
public partial class RowStatus : COREDOMAINDATAMODELS.DataModel<RowStatus>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the RowStatusUId of RowStatus.
    /// </summary>
    /// <value>
    /// The RowStatusUId.
    /// </value>
    [Display(Name = nameof(RowStatusUId))]
    [Column(nameof(RowStatusUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "RowStatusUId  is required.")]
    public virtual Guid RowStatusUId { get; set; }

    /// <summary>
    /// Gets or Sets the RowStatusId of RowStatus.
    /// </summary>
    /// <value>
    /// The RowStatusId.
    /// </value>
    [Display(Name = nameof(RowStatusId))]
    [Column(nameof(RowStatusId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "RowStatusId  is required.")]
    public virtual Int64 RowStatusId { get; set; }

    /// <summary>
    /// Gets or Sets the Name of RowStatus.
    /// </summary>
    /// <value>
    /// The Name.
    /// </value>
    [Display(Name = nameof(Name))]
    [Column("Name", Order = 3)] 
    [MaxLength(150, ErrorMessage = "Name value cannot be more then 150 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name  is required.")]
    public virtual String Name { get; set; }

    /// <summary>
    /// Gets or Sets the Code of RowStatus.
    /// </summary>
    /// <value>
    /// The Code.
    /// </value>
    [Display(Name = nameof(Code))]
    [Column("Code", Order = 4)] 
    [MaxLength(50, ErrorMessage = "Code value cannot be more then 50 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Code  is required.")]
    public virtual String Code { get; set; }

    /// <summary>
    /// Gets or Sets the Description of RowStatus.
    /// </summary>
    /// <value>
    /// The Description.
    /// </value>
    [Display(Name = nameof(Description))]
    [Column("Description", Order = 5)] 
    [MaxLength(4000, ErrorMessage = "Description value cannot be more then 4000 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Description  is required.")]
    public virtual String Description { get; set; }

    /// <summary>
    /// Gets or Sets the DisplayOrder of RowStatus.
    /// </summary>
    /// <value>
    /// The DisplayOrder.
    /// </value>
    [Display(Name = nameof(DisplayOrder))]
    [Column(nameof(DisplayOrder), Order = 6)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "DisplayOrder  is required.")]
    public virtual Int64 DisplayOrder { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.OrderItemShipment referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.OrderItemShipment referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.OrderItemShipment> OrderItemShipments { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ProductLanguage referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ProductLanguage referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ProductLanguage> ProductLanguages { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.CountryLanguage referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.CountryLanguage referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.CountryLanguage> CountryLanguages { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ShoppingCartWishList referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ShoppingCartWishList referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ShoppingCartWishList> ShoppingCartWishLists { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.Shipper referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.Shipper referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.Shipper> Shippers { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.AccountStatus referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.AccountStatus referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.AccountStatus> AccountStatuses { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.Customer referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.Customer referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.Customer> Customers { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.Order referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.Order referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.Order> Orders { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ProductCategory referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ProductCategory referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ProductCategory> ProductCategories { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ProductCategoryLanguage referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ProductCategoryLanguage referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ProductCategoryLanguage> ProductCategoryLanguages { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.Seller referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.Seller referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.Seller> Sellers { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.MasterListItem referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.MasterListItem referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.MasterListItem> MasterListItems { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.Entity referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.Entity referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.Entity> Entities { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.Country referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.Country referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.Country> Countries { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ShoppingCart referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ShoppingCart referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ShoppingCart> ShoppingCarts { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ShipperPhone referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ShipperPhone referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ShipperPhone> ShipperPhones { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.AppSetting referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.AppSetting referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.AppSetting> AppSettings { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.Account referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.Account referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.Account> Accounts { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.MasterList referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.MasterList referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.MasterList> MasterLists { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.CustomerPhone referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.CustomerPhone referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.CustomerPhone> CustomerPhones { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.SellerPhone referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.SellerPhone referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.SellerPhone> SellerPhones { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.OrderItem referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.OrderItem referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.OrderItem> OrderItems { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ShipperBankAccount referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ShipperBankAccount referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ShipperBankAccount> ShipperBankAccounts { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.SellerBankAccount referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.SellerBankAccount referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.SellerBankAccount> SellerBankAccounts { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.CustomerCard referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.CustomerCard referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.CustomerCard> CustomerCards { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.Product referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.Product referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.Product> Products { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.SellerAddress referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.SellerAddress referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.SellerAddress> SellerAddresses { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ShipperAddress referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ShipperAddress referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ShipperAddress> ShipperAddresses { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.CustomerAddress referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.CustomerAddress referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.CustomerAddress> CustomerAddresses { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.Language referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.Language referenced entity.
    /// </value>
    [InverseProperty("RowStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.Language> Languages { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///  Initializes a new instance of the RowStatus class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  RowStatus()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
