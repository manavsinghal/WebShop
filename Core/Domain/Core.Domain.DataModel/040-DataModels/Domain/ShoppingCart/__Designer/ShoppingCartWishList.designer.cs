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
/// Represents ShoppingCartWishList DataModel.
/// </summary>
/// <remarks>
/// The ShoppingCartWishList DataModel.
/// </remarks>
[Table("ShoppingCartWishList", Schema = "dbo")]
public partial class ShoppingCartWishList : COREDOMAINDATAMODELS.DataModel<ShoppingCartWishList>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the ShoppingCartWishListUId of ShoppingCartWishList.
    /// </summary>
    /// <value>
    /// The ShoppingCartWishListUId.
    /// </value>
    [Display(Name = nameof(ShoppingCartWishListUId))]
    [Column(nameof(ShoppingCartWishListUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ShoppingCartWishListUId  is required.")]
    public virtual Guid ShoppingCartWishListUId { get; set; }

    /// <summary>
    /// Gets or Sets the ShoppingCartWishListId of ShoppingCartWishList.
    /// </summary>
    /// <value>
    /// The ShoppingCartWishListId.
    /// </value>
    [Display(Name = nameof(ShoppingCartWishListId))]
    [Column(nameof(ShoppingCartWishListId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ShoppingCartWishListId  is required.")]
    public virtual Int64 ShoppingCartWishListId { get; set; }

    /// <summary>
    /// Gets or Sets the CustomerUId of ShoppingCartWishList.
    /// </summary>
    /// <value>
    /// The CustomerUId.
    /// </value>
    [Display(Name = nameof(CustomerUId))]
    [Column(nameof(CustomerUId), Order = 3)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CustomerUId  is required.")]
    public virtual Guid CustomerUId { get; set; }

    /// <summary>
    /// Gets or Sets the ProductUId of ShoppingCartWishList.
    /// </summary>
    /// <value>
    /// The ProductUId.
    /// </value>
    [Display(Name = nameof(ProductUId))]
    [Column(nameof(ProductUId), Order = 4)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ProductUId  is required.")]
    public virtual Guid ProductUId { get; set; }

    /// <summary>
    /// Gets or Sets the SortOrder of ShoppingCartWishList.
    /// </summary>
    /// <value>
    /// The SortOrder.
    /// </value>
    [Display(Name = nameof(SortOrder))]
    [Column(nameof(SortOrder), Order = 5)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "SortOrder  is required.")]
    public virtual Int64 SortOrder { get; set; }

    /// <summary>
    /// Represents Customer referenced entity.
    /// </summary>
    /// <value>
    /// The Customer referenced entity.
    /// </value>
    [ForeignKey(nameof(CustomerUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.Customer Customer { get; set; }

    /// <summary>
    /// Represents Product referenced entity.
    /// </summary>
    /// <value>
    /// The Product referenced entity.
    /// </value>
    [ForeignKey(nameof(ProductUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.Product Product { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///  Initializes a new instance of the ShoppingCartWishList class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  ShoppingCartWishList()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
