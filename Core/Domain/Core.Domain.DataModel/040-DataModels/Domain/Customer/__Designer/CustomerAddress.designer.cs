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
/// Represents CustomerAddress DataModel.
/// </summary>
/// <remarks>
/// The CustomerAddress DataModel.
/// </remarks>
[Table("CustomerAddress", Schema = "dbo")]
public partial class CustomerAddress : COREDOMAINDATAMODELS.DataModel<CustomerAddress>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the CustomerAddressUId of CustomerAddress.
    /// </summary>
    /// <value>
    /// The CustomerAddressUId.
    /// </value>
    [Display(Name = nameof(CustomerAddressUId))]
    [Column(nameof(CustomerAddressUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CustomerAddressUId  is required.")]
    public virtual Guid CustomerAddressUId { get; set; }

    /// <summary>
    /// Gets or Sets the CustomerAddressId of CustomerAddress.
    /// </summary>
    /// <value>
    /// The CustomerAddressId.
    /// </value>
    [Display(Name = nameof(CustomerAddressId))]
    [Column(nameof(CustomerAddressId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CustomerAddressId  is required.")]
    public virtual Int64 CustomerAddressId { get; set; }

    /// <summary>
    /// Gets or Sets the CustomerUId of CustomerAddress.
    /// </summary>
    /// <value>
    /// The CustomerUId.
    /// </value>
    [Display(Name = nameof(CustomerUId))]
    [Column(nameof(CustomerUId), Order = 3)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CustomerUId  is required.")]
    public virtual Guid CustomerUId { get; set; }

    /// <summary>
    /// Gets or Sets the AddressTypeUId of CustomerAddress.
    /// </summary>
    /// <value>
    /// The AddressTypeUId.
    /// </value>
    [Display(Name = nameof(AddressTypeUId))]
    [Column(nameof(AddressTypeUId), Order = 4)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "AddressTypeUId  is required.")]
    public virtual Guid AddressTypeUId { get; set; }

    /// <summary>
    /// Gets or Sets the Line1 of CustomerAddress.
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
    /// Gets or Sets the Line2 of CustomerAddress.
    /// </summary>
    /// <value>
    /// The Line2.
    /// </value>
    [Display(Name = nameof(Line2))]
    [Column("Line2", Order = 6)] 
    [MaxLength(150, ErrorMessage = "Line2 value cannot be more then 150 characters.")]
    public virtual String? Line2 { get; set; }

    /// <summary>
    /// Gets or Sets the Line3 of CustomerAddress.
    /// </summary>
    /// <value>
    /// The Line3.
    /// </value>
    [Display(Name = nameof(Line3))]
    [Column("Line3", Order = 7)] 
    [MaxLength(150, ErrorMessage = "Line3 value cannot be more then 150 characters.")]
    public virtual String? Line3 { get; set; }

    /// <summary>
    /// Gets or Sets the CountryUId of CustomerAddress.
    /// </summary>
    /// <value>
    /// The CountryUId.
    /// </value>
    [Display(Name = nameof(CountryUId))]
    [Column(nameof(CountryUId), Order = 8)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CountryUId  is required.")]
    public virtual Guid CountryUId { get; set; }

    /// <summary>
    /// Gets or Sets the StateCode of CustomerAddress.
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
    /// Gets or Sets the ZipCode of CustomerAddress.
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
    /// Gets or Sets the IsPreferred of CustomerAddress.
    /// </summary>
    /// <value>
    /// The IsPreferred.
    /// </value>
    [Display(Name = nameof(IsPreferred))]
    [Column(nameof(IsPreferred), Order = 11)]
    public virtual Boolean? IsPreferred { get; set; }

    /// <summary>
    /// Gets or Sets the SortOrder of CustomerAddress.
    /// </summary>
    /// <value>
    /// The SortOrder.
    /// </value>
    [Display(Name = nameof(SortOrder))]
    [Column(nameof(SortOrder), Order = 12)]
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
    ///  Initializes a new instance of the CustomerAddress class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  CustomerAddress()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
