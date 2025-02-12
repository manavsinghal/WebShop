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
/// Represents MasterListItem DataModel.
/// </summary>
/// <remarks>
/// The MasterListItem DataModel.
/// </remarks>
[Table("MasterListItem", Schema = "dbo")]
public partial class MasterListItem : COREDOMAINDATAMODELS.DataModel<MasterListItem>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the MasterListItemUId of MasterListItem.
    /// </summary>
    /// <value>
    /// The MasterListItemUId.
    /// </value>
    [Display(Name = nameof(MasterListItemUId))]
    [Column(nameof(MasterListItemUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "MasterListItemUId  is required.")]
    public virtual Guid MasterListItemUId { get; set; }

    /// <summary>
    /// Gets or Sets the MasterListItemId of MasterListItem.
    /// </summary>
    /// <value>
    /// The MasterListItemId.
    /// </value>
    [Display(Name = nameof(MasterListItemId))]
    [Column(nameof(MasterListItemId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "MasterListItemId  is required.")]
    public virtual Int64 MasterListItemId { get; set; }

    /// <summary>
    /// Gets or Sets the Name of MasterListItem.
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
    /// Gets or Sets the Code of MasterListItem.
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
    /// Gets or Sets the Description of MasterListItem.
    /// </summary>
    /// <value>
    /// The Description.
    /// </value>
    [Display(Name = nameof(Description))]
    [Column("Description", Order = 5)] 
    [MaxLength(500, ErrorMessage = "Description value cannot be more then 500 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Description  is required.")]
    public virtual String Description { get; set; }

    /// <summary>
    /// Gets or Sets the MasterListUId of MasterListItem.
    /// </summary>
    /// <value>
    /// The MasterListUId.
    /// </value>
    [Display(Name = nameof(MasterListUId))]
    [Column(nameof(MasterListUId), Order = 6)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "MasterListUId  is required.")]
    public virtual Guid MasterListUId { get; set; }

    /// <summary>
    /// Gets or Sets the DisplayOrder of MasterListItem.
    /// </summary>
    /// <value>
    /// The DisplayOrder.
    /// </value>
    [Display(Name = nameof(DisplayOrder))]
    [Column(nameof(DisplayOrder), Order = 7)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "DisplayOrder  is required.")]
    public virtual Int64 DisplayOrder { get; set; }

    /// <summary>
    /// Represents MasterList referenced entity.
    /// </summary>
    /// <value>
    /// The MasterList referenced entity.
    /// </value>
    [ForeignKey(nameof(MasterListUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.MasterList MasterList { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.CustomerPhone referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.CustomerPhone referenced entity.
    /// </value>
    [InverseProperty("PhoneType")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.CustomerPhone> CustomerPhonePhoneTypes { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.CustomerAddress referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.CustomerAddress referenced entity.
    /// </value>
    [InverseProperty("AddressType")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.CustomerAddress> CustomerAddressAddressTypes { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.CustomerCard referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.CustomerCard referenced entity.
    /// </value>
    [InverseProperty("CardType")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.CustomerCard> CustomerCardCardTypes { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.SellerAddress referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.SellerAddress referenced entity.
    /// </value>
    [InverseProperty("AddressType")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.SellerAddress> SellerAddressAddressTypes { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.SellerBankAccount referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.SellerBankAccount referenced entity.
    /// </value>
    [InverseProperty("BankAccountType")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.SellerBankAccount> SellerBankAccountBankAccountTypes { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.SellerPhone referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.SellerPhone referenced entity.
    /// </value>
    [InverseProperty("PhoneType")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.SellerPhone> SellerPhonePhoneTypes { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ShipperAddress referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ShipperAddress referenced entity.
    /// </value>
    [InverseProperty("AddressType")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ShipperAddress> ShipperAddressAddressTypes { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ShipperBankAccount referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ShipperBankAccount referenced entity.
    /// </value>
    [InverseProperty("BankAccountType")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ShipperBankAccount> ShipperBankAccountBankAccountTypes { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ShipperPhone referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ShipperPhone referenced entity.
    /// </value>
    [InverseProperty("PhoneType")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ShipperPhone> ShipperPhonePhoneTypes { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.Shipper referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.Shipper referenced entity.
    /// </value>
    [InverseProperty("ShipperStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.Shipper> ShipperShipperStatuses { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.Seller referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.Seller referenced entity.
    /// </value>
    [InverseProperty("SellerStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.Seller> SellerSellerStatuses { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.Customer referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.Customer referenced entity.
    /// </value>
    [InverseProperty("CustomerStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.Customer> CustomerCustomerStatuses { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.Order referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.Order referenced entity.
    /// </value>
    [InverseProperty("OrderStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.Order> OrderOrderStatuses { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///  Initializes a new instance of the MasterListItem class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  MasterListItem()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
