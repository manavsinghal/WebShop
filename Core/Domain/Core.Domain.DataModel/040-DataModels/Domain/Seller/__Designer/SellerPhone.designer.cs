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
/// Represents SellerPhone DataModel.
/// </summary>
/// <remarks>
/// The SellerPhone DataModel.
/// </remarks>
[Table("SellerPhone", Schema = "dbo")]
public partial class SellerPhone : COREDOMAINDATAMODELS.DataModel<SellerPhone>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the SellerPhoneUId of SellerPhone.
    /// </summary>
    /// <value>
    /// The SellerPhoneUId.
    /// </value>
    [Display(Name = nameof(SellerPhoneUId))]
    [Column(nameof(SellerPhoneUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "SellerPhoneUId  is required.")]
    public virtual Guid SellerPhoneUId { get; set; }

    /// <summary>
    /// Gets or Sets the SellerPhoneId of SellerPhone.
    /// </summary>
    /// <value>
    /// The SellerPhoneId.
    /// </value>
    [Display(Name = nameof(SellerPhoneId))]
    [Column(nameof(SellerPhoneId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "SellerPhoneId  is required.")]
    public virtual Int64 SellerPhoneId { get; set; }

    /// <summary>
    /// Gets or Sets the SellerUId of SellerPhone.
    /// </summary>
    /// <value>
    /// The SellerUId.
    /// </value>
    [Display(Name = nameof(SellerUId))]
    [Column(nameof(SellerUId), Order = 3)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "SellerUId  is required.")]
    public virtual Guid SellerUId { get; set; }

    /// <summary>
    /// Gets or Sets the PhoneTypeUId of SellerPhone.
    /// </summary>
    /// <value>
    /// The PhoneTypeUId.
    /// </value>
    [Display(Name = nameof(PhoneTypeUId))]
    [Column(nameof(PhoneTypeUId), Order = 4)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "PhoneTypeUId  is required.")]
    public virtual Guid PhoneTypeUId { get; set; }

    /// <summary>
    /// Gets or Sets the CountryUId of SellerPhone.
    /// </summary>
    /// <value>
    /// The CountryUId.
    /// </value>
    [Display(Name = nameof(CountryUId))]
    [Column(nameof(CountryUId), Order = 5)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CountryUId  is required.")]
    public virtual Guid CountryUId { get; set; }

    /// <summary>
    /// Gets or Sets the PhoneNumber of SellerPhone.
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
    /// Gets or Sets the IsPreferred of SellerPhone.
    /// </summary>
    /// <value>
    /// The IsPreferred.
    /// </value>
    [Display(Name = nameof(IsPreferred))]
    [Column(nameof(IsPreferred), Order = 7)]
    public virtual Boolean? IsPreferred { get; set; }

    /// <summary>
    /// Gets or Sets the SortOrder of SellerPhone.
    /// </summary>
    /// <value>
    /// The SortOrder.
    /// </value>
    [Display(Name = nameof(SortOrder))]
    [Column(nameof(SortOrder), Order = 8)]
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
    ///  Initializes a new instance of the SellerPhone class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  SellerPhone()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
