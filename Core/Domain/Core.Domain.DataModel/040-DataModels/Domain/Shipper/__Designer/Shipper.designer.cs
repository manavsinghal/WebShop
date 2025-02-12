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
/// Represents Shipper DataModel.
/// </summary>
/// <remarks>
/// The Shipper DataModel.
/// </remarks>
[Table("Shipper", Schema = "dbo")]
public partial class Shipper : COREDOMAINDATAMODELS.DataModel<Shipper>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the ShipperUId of Shipper.
    /// </summary>
    /// <value>
    /// The ShipperUId.
    /// </value>
    [Display(Name = nameof(ShipperUId))]
    [Column(nameof(ShipperUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ShipperUId  is required.")]
    public virtual Guid ShipperUId { get; set; }

    /// <summary>
    /// Gets or Sets the ShipperId of Shipper.
    /// </summary>
    /// <value>
    /// The ShipperId.
    /// </value>
    [Display(Name = nameof(ShipperId))]
    [Column(nameof(ShipperId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ShipperId  is required.")]
    public virtual Int64 ShipperId { get; set; }

    /// <summary>
    /// Gets or Sets the Name of Shipper.
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
    /// Gets or Sets the Rating of Shipper.
    /// </summary>
    /// <value>
    /// The Rating.
    /// </value>
    [Display(Name = nameof(Rating))]
    [Column(nameof(Rating), Order = 4)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Rating  is required.")]
    public virtual Decimal Rating { get; set; }

    /// <summary>
    /// Gets or Sets the ShipperStatusUId of Shipper.
    /// </summary>
    /// <value>
    /// The ShipperStatusUId.
    /// </value>
    [Display(Name = nameof(ShipperStatusUId))]
    [Column(nameof(ShipperStatusUId), Order = 5)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ShipperStatusUId  is required.")]
    public virtual Guid ShipperStatusUId { get; set; }

    /// <summary>
    /// Gets or Sets the SortOrder of Shipper.
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
    [ForeignKey(nameof(ShipperStatusUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.MasterListItem ShipperStatus { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ShipperPhone referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ShipperPhone referenced entity.
    /// </value>
    [InverseProperty("Shipper")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ShipperPhone> ShipperPhones { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ShipperAddress referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ShipperAddress referenced entity.
    /// </value>
    [InverseProperty("Shipper")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ShipperAddress> ShipperAddresses { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ShipperBankAccount referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ShipperBankAccount referenced entity.
    /// </value>
    [InverseProperty("Shipper")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ShipperBankAccount> ShipperBankAccounts { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.OrderItem referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.OrderItem referenced entity.
    /// </value>
    [InverseProperty("Shipper")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.OrderItem> OrderItems { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///  Initializes a new instance of the Shipper class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  Shipper()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
