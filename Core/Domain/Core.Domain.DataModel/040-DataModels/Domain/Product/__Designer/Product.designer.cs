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
/// Represents Product DataModel.
/// </summary>
/// <remarks>
/// The Product DataModel.
/// </remarks>
[Table("Product", Schema = "dbo")]
public partial class Product : COREDOMAINDATAMODELS.DataModel<Product>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the ProductUId of Product.
    /// </summary>
    /// <value>
    /// The ProductUId.
    /// </value>
    [Display(Name = nameof(ProductUId))]
    [Column(nameof(ProductUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ProductUId  is required.")]
    public virtual Guid ProductUId { get; set; }

    /// <summary>
    /// Gets or Sets the ProductId of Product.
    /// </summary>
    /// <value>
    /// The ProductId.
    /// </value>
    [Display(Name = nameof(ProductId))]
    [Column(nameof(ProductId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ProductId  is required.")]
    public virtual Int64 ProductId { get; set; }

    /// <summary>
    /// Gets or Sets the SellerUId of Product.
    /// </summary>
    /// <value>
    /// The SellerUId.
    /// </value>
    [Display(Name = nameof(SellerUId))]
    [Column(nameof(SellerUId), Order = 3)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "SellerUId  is required.")]
    public virtual Guid SellerUId { get; set; }

    /// <summary>
    /// Gets or Sets the Name of Product.
    /// </summary>
    /// <value>
    /// The Name.
    /// </value>
    [Display(Name = nameof(Name))]
    [Column("Name", Order = 4)] 
    [MaxLength(150, ErrorMessage = "Name value cannot be more then 150 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name  is required.")]
    public virtual String Name { get; set; }

    /// <summary>
    /// Gets or Sets the ProductCategoryParentUId of Product.
    /// </summary>
    /// <value>
    /// The ProductCategoryParentUId.
    /// </value>
    [Display(Name = nameof(ProductCategoryParentUId))]
    [Column(nameof(ProductCategoryParentUId), Order = 5)]
    public virtual Guid? ProductCategoryParentUId { get; set; }

    /// <summary>
    /// Gets or Sets the Rate of Product.
    /// </summary>
    /// <value>
    /// The Rate.
    /// </value>
    [Display(Name = nameof(Rate))]
    [Column(nameof(Rate), Order = 6)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public virtual Int64? Rate { get; set; }

    /// <summary>
    /// Gets or Sets the TotalQuantity of Product.
    /// </summary>
    /// <value>
    /// The TotalQuantity.
    /// </value>
    [Display(Name = nameof(TotalQuantity))]
    [Column(nameof(TotalQuantity), Order = 7)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "TotalQuantity  is required.")]
    public virtual Int64 TotalQuantity { get; set; }

    /// <summary>
    /// Gets or Sets the SoldQuantity of Product.
    /// </summary>
    /// <value>
    /// The SoldQuantity.
    /// </value>
    [Display(Name = nameof(SoldQuantity))]
    [Column(nameof(SoldQuantity), Order = 8)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "SoldQuantity  is required.")]
    public virtual Int64 SoldQuantity { get; set; }

    /// <summary>
    /// Gets or Sets the AvailableQuantity of Product.
    /// </summary>
    /// <value>
    /// The AvailableQuantity.
    /// </value>
    [Display(Name = nameof(AvailableQuantity))]
    [Column(nameof(AvailableQuantity), Order = 9)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public virtual Int64? AvailableQuantity { get; set; }

    /// <summary>
    /// Gets or Sets the SortOrder of Product.
    /// </summary>
    /// <value>
    /// The SortOrder.
    /// </value>
    [Display(Name = nameof(SortOrder))]
    [Column(nameof(SortOrder), Order = 10)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "SortOrder  is required.")]
    public virtual Int64 SortOrder { get; set; }

    /// <summary>
    /// Represents Seller referenced entity.
    /// </summary>
    /// <value>
    /// The Seller referenced entity.
    /// </value>
    [ForeignKey(nameof(SellerUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.Seller Seller { get; set; }

    /// <summary>
    /// Represents ProductCategory referenced entity.
    /// </summary>
    /// <value>
    /// The ProductCategory referenced entity.
    /// </value>
    [ForeignKey(nameof(ProductCategoryParentUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.ProductCategory ProductCategoryParent { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ProductLanguage referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ProductLanguage referenced entity.
    /// </value>
    [InverseProperty("Product")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ProductLanguage> ProductLanguages { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.OrderItem referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.OrderItem referenced entity.
    /// </value>
    [InverseProperty("Product")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.OrderItem> OrderItems { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ShoppingCart referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ShoppingCart referenced entity.
    /// </value>
    [InverseProperty("Product")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ShoppingCart> ShoppingCarts { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ShoppingCartWishList referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ShoppingCartWishList referenced entity.
    /// </value>
    [InverseProperty("Product")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ShoppingCartWishList> ShoppingCartWishLists { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///  Initializes a new instance of the Product class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  Product()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
