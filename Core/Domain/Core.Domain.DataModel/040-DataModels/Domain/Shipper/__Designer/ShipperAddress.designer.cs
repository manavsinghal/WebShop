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
/// Represents ShipperAddress DataModel.
/// </summary>
/// <remarks>
/// The ShipperAddress DataModel.
/// </remarks>
[Table("ShipperAddress", Schema = "dbo")]
public partial class ShipperAddress : COREDOMAINDATAMODELS.DataModel<ShipperAddress>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the ShipperAddressUId of ShipperAddress.
    /// </summary>
    /// <value>
    /// The ShipperAddressUId.
    /// </value>
    [Display(Name = nameof(ShipperAddressUId))]
    [Column(nameof(ShipperAddressUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ShipperAddressUId  is required.")]
    public virtual Guid ShipperAddressUId { get; set; }

    /// <summary>
    /// Gets or Sets the ShipperAddressId of ShipperAddress.
    /// </summary>
    /// <value>
    /// The ShipperAddressId.
    /// </value>
    [Display(Name = nameof(ShipperAddressId))]
    [Column(nameof(ShipperAddressId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ShipperAddressId  is required.")]
    public virtual Int64 ShipperAddressId { get; set; }

    /// <summary>
    /// Gets or Sets the ShipperUId of ShipperAddress.
    /// </summary>
    /// <value>
    /// The ShipperUId.
    /// </value>
    [Display(Name = nameof(ShipperUId))]
    [Column(nameof(ShipperUId), Order = 3)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ShipperUId  is required.")]
    public virtual Guid ShipperUId { get; set; }

    /// <summary>
    /// Gets or Sets the AddressTypeUId of ShipperAddress.
    /// </summary>
    /// <value>
    /// The AddressTypeUId.
    /// </value>
    [Display(Name = nameof(AddressTypeUId))]
    [Column(nameof(AddressTypeUId), Order = 4)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "AddressTypeUId  is required.")]
    public virtual Guid AddressTypeUId { get; set; }

    /// <summary>
    /// Gets or Sets the Line1 of ShipperAddress.
    /// </summary>
    /// <value>
    /// The Line1.
    /// </value>
    [Display(Name = nameof(Line1))]
    [Column("Line1", Order = 5)] 
    [MaxLength(150, ErrorMessage = "Line1 value cannot be more then 150 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Line1  is required.")]
    public virtual String Line1 { get; set; }

    /// <summary>
    /// Gets or Sets the Line2 of ShipperAddress.
    /// </summary>
    /// <value>
    /// The Line2.
    /// </value>
    [Display(Name = nameof(Line2))]
    [Column("Line2", Order = 6)] 
    [MaxLength(150, ErrorMessage = "Line2 value cannot be more then 150 characters.")]
    public virtual String? Line2 { get; set; }

    /// <summary>
    /// Gets or Sets the Line3 of ShipperAddress.
    /// </summary>
    /// <value>
    /// The Line3.
    /// </value>
    [Display(Name = nameof(Line3))]
    [Column("Line3", Order = 7)] 
    [MaxLength(150, ErrorMessage = "Line3 value cannot be more then 150 characters.")]
    public virtual String? Line3 { get; set; }

    /// <summary>
    /// Gets or Sets the CountryUId of ShipperAddress.
    /// </summary>
    /// <value>
    /// The CountryUId.
    /// </value>
    [Display(Name = nameof(CountryUId))]
    [Column(nameof(CountryUId), Order = 8)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CountryUId  is required.")]
    public virtual Guid CountryUId { get; set; }

    /// <summary>
    /// Gets or Sets the StateCode of ShipperAddress.
    /// </summary>
    /// <value>
    /// The StateCode.
    /// </value>
    [Display(Name = nameof(StateCode))]
    [Column("StateCode", Order = 9)] 
    [MaxLength(10, ErrorMessage = "StateCode value cannot be more then 10 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "StateCode  is required.")]
    public virtual String StateCode { get; set; }

    /// <summary>
    /// Gets or Sets the ZipCode of ShipperAddress.
    /// </summary>
    /// <value>
    /// The ZipCode.
    /// </value>
    [Display(Name = nameof(ZipCode))]
    [Column("ZipCode", Order = 10)] 
    [MaxLength(10, ErrorMessage = "ZipCode value cannot be more then 10 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ZipCode  is required.")]
    public virtual String ZipCode { get; set; }

    /// <summary>
    /// Gets or Sets the IsPreferred of ShipperAddress.
    /// </summary>
    /// <value>
    /// The IsPreferred.
    /// </value>
    [Display(Name = nameof(IsPreferred))]
    [Column(nameof(IsPreferred), Order = 11)]
    public virtual Boolean? IsPreferred { get; set; }

    /// <summary>
    /// Gets or Sets the SortOrder of ShipperAddress.
    /// </summary>
    /// <value>
    /// The SortOrder.
    /// </value>
    [Display(Name = nameof(SortOrder))]
    [Column(nameof(SortOrder), Order = 12)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "SortOrder  is required.")]
    public virtual Int64 SortOrder { get; set; }

    /// <summary>
    /// Represents Shipper referenced entity.
    /// </summary>
    /// <value>
    /// The Shipper referenced entity.
    /// </value>
    [ForeignKey(nameof(ShipperUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.Shipper Shipper { get; set; }

    /// <summary>
    /// Represents MasterListItem referenced entity.
    /// </summary>
    /// <value>
    /// The MasterListItem referenced entity.
    /// </value>
    [ForeignKey(nameof(AddressTypeUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.MasterListItem AddressType { get; set; }

    /// <summary>
    /// Represents Country referenced entity.
    /// </summary>
    /// <value>
    /// The Country referenced entity.
    /// </value>
    [ForeignKey(nameof(CountryUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.Country Country { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///  Initializes a new instance of the ShipperAddress class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  ShipperAddress()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
