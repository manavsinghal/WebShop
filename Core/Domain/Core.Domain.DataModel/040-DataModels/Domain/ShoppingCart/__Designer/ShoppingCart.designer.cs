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
/// Represents ShoppingCart DataModel.
/// </summary>
/// <remarks>
/// The ShoppingCart DataModel.
/// </remarks>
[Table("ShoppingCart", Schema = "dbo")]
public partial class ShoppingCart : COREDOMAINDATAMODELS.DataModel<ShoppingCart>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the ShoppingCartUId of ShoppingCart.
    /// </summary>
    /// <value>
    /// The ShoppingCartUId.
    /// </value>
    [Display(Name = nameof(ShoppingCartUId))]
    [Column(nameof(ShoppingCartUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ShoppingCartUId  is required.")]
    public virtual Guid ShoppingCartUId { get; set; }

    /// <summary>
    /// Gets or Sets the ShoppingCartId of ShoppingCart.
    /// </summary>
    /// <value>
    /// The ShoppingCartId.
    /// </value>
    [Display(Name = nameof(ShoppingCartId))]
    [Column(nameof(ShoppingCartId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ShoppingCartId  is required.")]
    public virtual Int64 ShoppingCartId { get; set; }

    /// <summary>
    /// Gets or Sets the CustomerUId of ShoppingCart.
    /// </summary>
    /// <value>
    /// The CustomerUId.
    /// </value>
    [Display(Name = nameof(CustomerUId))]
    [Column(nameof(CustomerUId), Order = 3)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CustomerUId  is required.")]
    public virtual Guid CustomerUId { get; set; }

    /// <summary>
    /// Gets or Sets the ProductUId of ShoppingCart.
    /// </summary>
    /// <value>
    /// The ProductUId.
    /// </value>
    [Display(Name = nameof(ProductUId))]
    [Column(nameof(ProductUId), Order = 4)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ProductUId  is required.")]
    public virtual Guid ProductUId { get; set; }

    /// <summary>
    /// Gets or Sets the Quantity of ShoppingCart.
    /// </summary>
    /// <value>
    /// The Quantity.
    /// </value>
    [Display(Name = nameof(Quantity))]
    [Column(nameof(Quantity), Order = 5)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Quantity  is required.")]
    public virtual Int32 Quantity { get; set; }

    /// <summary>
    /// Gets or Sets the Rate of ShoppingCart.
    /// </summary>
    /// <value>
    /// The Rate.
    /// </value>
    [Display(Name = nameof(Rate))]
    [Column(nameof(Rate), Order = 6)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Rate  is required.")]
    public virtual Decimal Rate { get; set; }

    /// <summary>
    /// Gets or Sets the SortOrder of ShoppingCart.
    /// </summary>
    /// <value>
    /// The SortOrder.
    /// </value>
    [Display(Name = nameof(SortOrder))]
    [Column(nameof(SortOrder), Order = 7)]
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
    ///  Initializes a new instance of the ShoppingCart class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  ShoppingCart()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
