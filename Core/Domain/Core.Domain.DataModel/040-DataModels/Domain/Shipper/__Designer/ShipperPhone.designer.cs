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
/// Represents ShipperPhone DataModel.
/// </summary>
/// <remarks>
/// The ShipperPhone DataModel.
/// </remarks>
[Table("ShipperPhone", Schema = "dbo")]
public partial class ShipperPhone : COREDOMAINDATAMODELS.DataModel<ShipperPhone>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the ShipperPhoneUId of ShipperPhone.
    /// </summary>
    /// <value>
    /// The ShipperPhoneUId.
    /// </value>
    [Display(Name = nameof(ShipperPhoneUId))]
    [Column(nameof(ShipperPhoneUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ShipperPhoneUId  is required.")]
    public virtual Guid ShipperPhoneUId { get; set; }

    /// <summary>
    /// Gets or Sets the ShipperPhoneId of ShipperPhone.
    /// </summary>
    /// <value>
    /// The ShipperPhoneId.
    /// </value>
    [Display(Name = nameof(ShipperPhoneId))]
    [Column(nameof(ShipperPhoneId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ShipperPhoneId  is required.")]
    public virtual Int64 ShipperPhoneId { get; set; }

    /// <summary>
    /// Gets or Sets the ShipperUId of ShipperPhone.
    /// </summary>
    /// <value>
    /// The ShipperUId.
    /// </value>
    [Display(Name = nameof(ShipperUId))]
    [Column(nameof(ShipperUId), Order = 3)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ShipperUId  is required.")]
    public virtual Guid ShipperUId { get; set; }

    /// <summary>
    /// Gets or Sets the PhoneTypeUId of ShipperPhone.
    /// </summary>
    /// <value>
    /// The PhoneTypeUId.
    /// </value>
    [Display(Name = nameof(PhoneTypeUId))]
    [Column(nameof(PhoneTypeUId), Order = 4)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "PhoneTypeUId  is required.")]
    public virtual Guid PhoneTypeUId { get; set; }

    /// <summary>
    /// Gets or Sets the CountryUId of ShipperPhone.
    /// </summary>
    /// <value>
    /// The CountryUId.
    /// </value>
    [Display(Name = nameof(CountryUId))]
    [Column(nameof(CountryUId), Order = 5)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CountryUId  is required.")]
    public virtual Guid CountryUId { get; set; }

    /// <summary>
    /// Gets or Sets the PhoneNumber of ShipperPhone.
    /// </summary>
    /// <value>
    /// The PhoneNumber.
    /// </value>
    [Display(Name = nameof(PhoneNumber))]
    [Column("PhoneNumber", Order = 6)] 
    [MaxLength(12, ErrorMessage = "PhoneNumber value cannot be more then 12 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "PhoneNumber  is required.")]
    public virtual String PhoneNumber { get; set; }

    /// <summary>
    /// Gets or Sets the IsPreferred of ShipperPhone.
    /// </summary>
    /// <value>
    /// The IsPreferred.
    /// </value>
    [Display(Name = nameof(IsPreferred))]
    [Column(nameof(IsPreferred), Order = 7)]
    public virtual Boolean? IsPreferred { get; set; }

    /// <summary>
    /// Gets or Sets the SortOrder of ShipperPhone.
    /// </summary>
    /// <value>
    /// The SortOrder.
    /// </value>
    [Display(Name = nameof(SortOrder))]
    [Column(nameof(SortOrder), Order = 8)]
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
    [ForeignKey(nameof(PhoneTypeUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.MasterListItem PhoneType { get; set; }

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
    ///  Initializes a new instance of the ShipperPhone class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  ShipperPhone()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
