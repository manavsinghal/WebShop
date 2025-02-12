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
/// Represents Order DataModel.
/// </summary>
/// <remarks>
/// The Order DataModel.
/// </remarks>
[Table("Order", Schema = "dbo")]
public partial class Order : COREDOMAINDATAMODELS.DataModel<Order>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the OrderUId of Order.
    /// </summary>
    /// <value>
    /// The OrderUId.
    /// </value>
    [Display(Name = nameof(OrderUId))]
    [Column(nameof(OrderUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "OrderUId  is required.")]
    public virtual Guid OrderUId { get; set; }

    /// <summary>
    /// Gets or Sets the OrderId of Order.
    /// </summary>
    /// <value>
    /// The OrderId.
    /// </value>
    [Display(Name = nameof(OrderId))]
    [Column(nameof(OrderId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "OrderId  is required.")]
    public virtual Int64 OrderId { get; set; }

    /// <summary>
    /// Gets or Sets the CustomerUId of Order.
    /// </summary>
    /// <value>
    /// The CustomerUId.
    /// </value>
    [Display(Name = nameof(CustomerUId))]
    [Column(nameof(CustomerUId), Order = 3)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CustomerUId  is required.")]
    public virtual Guid CustomerUId { get; set; }

    /// <summary>
    /// Gets or Sets the Name of Order.
    /// </summary>
    /// <value>
    /// The Name.
    /// </value>
    [Display(Name = nameof(Name))]
    [Column("Name", Order = 4)] 
    [MaxLength(100, ErrorMessage = "Name value cannot be more then 100 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name  is required.")]
    public virtual String Name { get; set; }

    /// <summary>
    /// Gets or Sets the Date of Order.
    /// </summary>
    /// <value>
    /// The Date.
    /// </value>
    [Display(Name = nameof(Date))]
    [Column(nameof(Date), Order = 5)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Date  is required.")]
    public virtual DateTime Date { get; set; }

    /// <summary>
    /// Gets or Sets the OrderStatusUId of Order.
    /// </summary>
    /// <value>
    /// The OrderStatusUId.
    /// </value>
    [Display(Name = nameof(OrderStatusUId))]
    [Column(nameof(OrderStatusUId), Order = 6)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "OrderStatusUId  is required.")]
    public virtual Guid OrderStatusUId { get; set; }

    /// <summary>
    /// Represents Customer referenced entity.
    /// </summary>
    /// <value>
    /// The Customer referenced entity.
    /// </value>
    [ForeignKey(nameof(CustomerUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.Customer Customer { get; set; }

    /// <summary>
    /// Represents MasterListItem referenced entity.
    /// </summary>
    /// <value>
    /// The MasterListItem referenced entity.
    /// </value>
    [ForeignKey(nameof(OrderStatusUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.MasterListItem OrderStatus { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.OrderItem referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.OrderItem referenced entity.
    /// </value>
    [InverseProperty("Order")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.OrderItem> OrderItems { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///  Initializes a new instance of the Order class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  Order()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
