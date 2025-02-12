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
/// Represents Seller DataModel.
/// </summary>
/// <remarks>
/// The Seller DataModel.
/// </remarks>
[Table("Seller", Schema = "dbo")]
public partial class Seller : COREDOMAINDATAMODELS.DataModel<Seller>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the SellerUId of Seller.
    /// </summary>
    /// <value>
    /// The SellerUId.
    /// </value>
    [Display(Name = nameof(SellerUId))]
    [Column(nameof(SellerUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "SellerUId  is required.")]
    public virtual Guid SellerUId { get; set; }

    /// <summary>
    /// Gets or Sets the SellerId of Seller.
    /// </summary>
    /// <value>
    /// The SellerId.
    /// </value>
    [Display(Name = nameof(SellerId))]
    [Column(nameof(SellerId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "SellerId  is required.")]
    public virtual Int64 SellerId { get; set; }

    /// <summary>
    /// Gets or Sets the Name of Seller.
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
    /// Gets or Sets the Rating of Seller.
    /// </summary>
    /// <value>
    /// The Rating.
    /// </value>
    [Display(Name = nameof(Rating))]
    [Column(nameof(Rating), Order = 4)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Rating  is required.")]
    public virtual Decimal Rating { get; set; }

    /// <summary>
    /// Gets or Sets the SellerStatusUId of Seller.
    /// </summary>
    /// <value>
    /// The SellerStatusUId.
    /// </value>
    [Display(Name = nameof(SellerStatusUId))]
    [Column(nameof(SellerStatusUId), Order = 5)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "SellerStatusUId  is required.")]
    public virtual Guid SellerStatusUId { get; set; }

    /// <summary>
    /// Gets or Sets the SortOrder of Seller.
    /// </summary>
    /// <value>
    /// The SortOrder.
    /// </value>
    [Display(Name = nameof(SortOrder))]
    [Column(nameof(SortOrder), Order = 6)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "SortOrder  is required.")]
    public virtual Int64 SortOrder { get; set; }

    /// <summary>
    /// Represents MasterListItem referenced entity.
    /// </summary>
    /// <value>
    /// The MasterListItem referenced entity.
    /// </value>
    [ForeignKey(nameof(SellerStatusUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.MasterListItem SellerStatus { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.SellerPhone referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.SellerPhone referenced entity.
    /// </value>
    [InverseProperty("Seller")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.SellerPhone> SellerPhones { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.SellerAddress referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.SellerAddress referenced entity.
    /// </value>
    [InverseProperty("Seller")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.SellerAddress> SellerAddresses { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.SellerBankAccount referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.SellerBankAccount referenced entity.
    /// </value>
    [InverseProperty("Seller")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.SellerBankAccount> SellerBankAccounts { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.Product referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.Product referenced entity.
    /// </value>
    [InverseProperty("Seller")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.Product> Products { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///  Initializes a new instance of the Seller class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  Seller()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
