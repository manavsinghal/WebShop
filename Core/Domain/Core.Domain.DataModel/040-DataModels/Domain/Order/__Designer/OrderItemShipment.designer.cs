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
/// Represents OrderItemShipment DataModel.
/// </summary>
/// <remarks>
/// The OrderItemShipment DataModel.
/// </remarks>
[Table("OrderItemShipment", Schema = "dbo")]
public partial class OrderItemShipment : COREDOMAINDATAMODELS.DataModel<OrderItemShipment>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the OrderItemShipmentUId of OrderItemShipment.
    /// </summary>
    /// <value>
    /// The OrderItemShipmentUId.
    /// </value>
    [Display(Name = nameof(OrderItemShipmentUId))]
    [Column(nameof(OrderItemShipmentUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "OrderItemShipmentUId  is required.")]
    public virtual Guid OrderItemShipmentUId { get; set; }

    /// <summary>
    /// Gets or Sets the OrderItemShipmentId of OrderItemShipment.
    /// </summary>
    /// <value>
    /// The OrderItemShipmentId.
    /// </value>
    [Display(Name = nameof(OrderItemShipmentId))]
    [Column(nameof(OrderItemShipmentId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "OrderItemShipmentId  is required.")]
    public virtual Int64 OrderItemShipmentId { get; set; }

    /// <summary>
    /// Gets or Sets the OrderItemUId of OrderItemShipment.
    /// </summary>
    /// <value>
    /// The OrderItemUId.
    /// </value>
    [Display(Name = nameof(OrderItemUId))]
    [Column(nameof(OrderItemUId), Order = 3)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "OrderItemUId  is required.")]
    public virtual Guid OrderItemUId { get; set; }

    /// <summary>
    /// Represents OrderItem referenced entity.
    /// </summary>
    /// <value>
    /// The OrderItem referenced entity.
    /// </value>
    [ForeignKey(nameof(OrderItemUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.OrderItem OrderItem { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///  Initializes a new instance of the OrderItemShipment class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  OrderItemShipment()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
