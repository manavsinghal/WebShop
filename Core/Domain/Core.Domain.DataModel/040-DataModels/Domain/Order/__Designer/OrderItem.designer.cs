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
/// Represents OrderItem DataModel.
/// </summary>
/// <remarks>
/// The OrderItem DataModel.
/// </remarks>
[Table("OrderItem", Schema = "dbo")]
public partial class OrderItem : COREDOMAINDATAMODELS.DataModel<OrderItem>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the OrderItemUId of OrderItem.
    /// </summary>
    /// <value>
    /// The OrderItemUId.
    /// </value>
    [Display(Name = nameof(OrderItemUId))]
    [Column(nameof(OrderItemUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "OrderItemUId  is required.")]
    public virtual Guid OrderItemUId { get; set; }

    /// <summary>
    /// Gets or Sets the OrderItemId of OrderItem.
    /// </summary>
    /// <value>
    /// The OrderItemId.
    /// </value>
    [Display(Name = nameof(OrderItemId))]
    [Column(nameof(OrderItemId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "OrderItemId  is required.")]
    public virtual Int64 OrderItemId { get; set; }

    /// <summary>
    /// Gets or Sets the OrderUId of OrderItem.
    /// </summary>
    /// <value>
    /// The OrderUId.
    /// </value>
    [Display(Name = nameof(OrderUId))]
    [Column(nameof(OrderUId), Order = 3)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "OrderUId  is required.")]
    public virtual Guid OrderUId { get; set; }

    /// <summary>
    /// Gets or Sets the ProductUId of OrderItem.
    /// </summary>
    /// <value>
    /// The ProductUId.
    /// </value>
    [Display(Name = nameof(ProductUId))]
    [Column(nameof(ProductUId), Order = 4)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ProductUId  is required.")]
    public virtual Guid ProductUId { get; set; }

    /// <summary>
    /// Gets or Sets the ShipperUId of OrderItem.
    /// </summary>
    /// <value>
    /// The ShipperUId.
    /// </value>
    [Display(Name = nameof(ShipperUId))]
    [Column(nameof(ShipperUId), Order = 5)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ShipperUId  is required.")]
    public virtual Guid ShipperUId { get; set; }

    /// <summary>
    /// Gets or Sets the Quantity of OrderItem.
    /// </summary>
    /// <value>
    /// The Quantity.
    /// </value>
    [Display(Name = nameof(Quantity))]
    [Column(nameof(Quantity), Order = 6)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Quantity  is required.")]
    public virtual Int32 Quantity { get; set; }

    /// <summary>
    /// Gets or Sets the Rate of OrderItem.
    /// </summary>
    /// <value>
    /// The Rate.
    /// </value>
    [Display(Name = nameof(Rate))]
    [Column(nameof(Rate), Order = 7)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Rate  is required.")]
    public virtual Decimal Rate { get; set; }

    /// <summary>
    /// Gets or Sets the ShipperTrackingNumber of OrderItem.
    /// </summary>
    /// <value>
    /// The ShipperTrackingNumber.
    /// </value>
    [Display(Name = nameof(ShipperTrackingNumber))]
    [Column("ShipperTrackingNumber", Order = 8)] 
    [Required(AllowEmptyStrings = false, ErrorMessage = "ShipperTrackingNumber  is required.")]
    public virtual String ShipperTrackingNumber { get; set; }

    /// <summary>
    /// Represents Order referenced entity.
    /// </summary>
    /// <value>
    /// The Order referenced entity.
    /// </value>
    [ForeignKey(nameof(OrderUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.Order Order { get; set; }

    /// <summary>
    /// Represents Product referenced entity.
    /// </summary>
    /// <value>
    /// The Product referenced entity.
    /// </value>
    [ForeignKey(nameof(ProductUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.Product Product { get; set; }

    /// <summary>
    /// Represents Shipper referenced entity.
    /// </summary>
    /// <value>
    /// The Shipper referenced entity.
    /// </value>
    [ForeignKey(nameof(ShipperUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.Shipper Shipper { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.OrderItemShipment referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.OrderItemShipment referenced entity.
    /// </value>
    [InverseProperty("OrderItem")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.OrderItemShipment> OrderItemShipments { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///  Initializes a new instance of the OrderItem class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  OrderItem()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
