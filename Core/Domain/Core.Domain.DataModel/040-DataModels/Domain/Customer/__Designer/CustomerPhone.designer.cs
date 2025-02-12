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
/// Represents CustomerPhone DataModel.
/// </summary>
/// <remarks>
/// The CustomerPhone DataModel.
/// </remarks>
[Table("CustomerPhone", Schema = "dbo")]
public partial class CustomerPhone : COREDOMAINDATAMODELS.DataModel<CustomerPhone>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the CustomerPhoneUId of CustomerPhone.
    /// </summary>
    /// <value>
    /// The CustomerPhoneUId.
    /// </value>
    [Display(Name = nameof(CustomerPhoneUId))]
    [Column(nameof(CustomerPhoneUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CustomerPhoneUId  is required.")]
    public virtual Guid CustomerPhoneUId { get; set; }

    /// <summary>
    /// Gets or Sets the CustomerPhoneId of CustomerPhone.
    /// </summary>
    /// <value>
    /// The CustomerPhoneId.
    /// </value>
    [Display(Name = nameof(CustomerPhoneId))]
    [Column(nameof(CustomerPhoneId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CustomerPhoneId  is required.")]
    public virtual Int64 CustomerPhoneId { get; set; }

    /// <summary>
    /// Gets or Sets the CustomerUId of CustomerPhone.
    /// </summary>
    /// <value>
    /// The CustomerUId.
    /// </value>
    [Display(Name = nameof(CustomerUId))]
    [Column(nameof(CustomerUId), Order = 3)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CustomerUId  is required.")]
    public virtual Guid CustomerUId { get; set; }

    /// <summary>
    /// Gets or Sets the PhoneTypeUId of CustomerPhone.
    /// </summary>
    /// <value>
    /// The PhoneTypeUId.
    /// </value>
    [Display(Name = nameof(PhoneTypeUId))]
    [Column(nameof(PhoneTypeUId), Order = 4)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "PhoneTypeUId  is required.")]
    public virtual Guid PhoneTypeUId { get; set; }

    /// <summary>
    /// Gets or Sets the CountryUId of CustomerPhone.
    /// </summary>
    /// <value>
    /// The CountryUId.
    /// </value>
    [Display(Name = nameof(CountryUId))]
    [Column(nameof(CountryUId), Order = 5)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CountryUId  is required.")]
    public virtual Guid CountryUId { get; set; }

    /// <summary>
    /// Gets or Sets the PhoneNumber of CustomerPhone.
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
    /// Gets or Sets the IsPreferred of CustomerPhone.
    /// </summary>
    /// <value>
    /// The IsPreferred.
    /// </value>
    [Display(Name = nameof(IsPreferred))]
    [Column(nameof(IsPreferred), Order = 7)]
    public virtual Boolean? IsPreferred { get; set; }

    /// <summary>
    /// Gets or Sets the SortOrder of CustomerPhone.
    /// </summary>
    /// <value>
    /// The SortOrder.
    /// </value>
    [Display(Name = nameof(SortOrder))]
    [Column(nameof(SortOrder), Order = 8)]
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
    ///  Initializes a new instance of the CustomerPhone class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  CustomerPhone()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
