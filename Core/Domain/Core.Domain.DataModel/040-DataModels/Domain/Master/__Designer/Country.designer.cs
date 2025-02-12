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
/// Represents Country DataModel.
/// </summary>
/// <remarks>
/// The Country DataModel.
/// </remarks>
[Table("Country", Schema = "dbo")]
public partial class Country : COREDOMAINDATAMODELS.DataModel<Country>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the CountryUId of Country.
    /// </summary>
    /// <value>
    /// The CountryUId.
    /// </value>
    [Display(Name = nameof(CountryUId))]
    [Column(nameof(CountryUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CountryUId  is required.")]
    public virtual Guid CountryUId { get; set; }

    /// <summary>
    /// Gets or Sets the CountryId of Country.
    /// </summary>
    /// <value>
    /// The CountryId.
    /// </value>
    [Display(Name = nameof(CountryId))]
    [Column(nameof(CountryId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CountryId  is required.")]
    public virtual Int64 CountryId { get; set; }

    /// <summary>
    /// Gets or Sets the Name of Country.
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
    /// Gets or Sets the Code of Country.
    /// </summary>
    /// <value>
    /// The Code.
    /// </value>
    [Display(Name = nameof(Code))]
    [Column("Code", Order = 4)] 
    [MaxLength(5, ErrorMessage = "Code value cannot be more then 5 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Code  is required.")]
    public virtual String Code { get; set; }

    /// <summary>
    /// Gets or Sets the IsEmbargoed of Country.
    /// </summary>
    /// <value>
    /// The IsEmbargoed.
    /// </value>
    [Display(Name = nameof(IsEmbargoed))]
    [Column(nameof(IsEmbargoed), Order = 5)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "IsEmbargoed  is required.")]
    public virtual Boolean? IsEmbargoed { get; set; }

    /// <summary>
    /// Gets or Sets the Description of Country.
    /// </summary>
    /// <value>
    /// The Description.
    /// </value>
    [Display(Name = nameof(Description))]
    [Column("Description", Order = 6)] 
    [MaxLength(4000, ErrorMessage = "Description value cannot be more then 4000 characters.")]
    public virtual String? Description { get; set; }

    /// <summary>
    /// Gets or Sets the DisplayOrder of Country.
    /// </summary>
    /// <value>
    /// The DisplayOrder.
    /// </value>
    [Display(Name = nameof(DisplayOrder))]
    [Column(nameof(DisplayOrder), Order = 7)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "DisplayOrder  is required.")]
    public virtual Int64 DisplayOrder { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.CountryLanguage referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.CountryLanguage referenced entity.
    /// </value>
    [InverseProperty("Country")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.CountryLanguage> CountryLanguages { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.CustomerPhone referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.CustomerPhone referenced entity.
    /// </value>
    [InverseProperty("Country")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.CustomerPhone> CustomerPhones { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ShipperPhone referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ShipperPhone referenced entity.
    /// </value>
    [InverseProperty("Country")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ShipperPhone> ShipperPhones { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.SellerPhone referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.SellerPhone referenced entity.
    /// </value>
    [InverseProperty("Country")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.SellerPhone> SellerPhones { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ShipperAddress referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ShipperAddress referenced entity.
    /// </value>
    [InverseProperty("Country")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ShipperAddress> ShipperAddresses { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.SellerAddress referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.SellerAddress referenced entity.
    /// </value>
    [InverseProperty("Country")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.SellerAddress> SellerAddresses { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.CustomerAddress referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.CustomerAddress referenced entity.
    /// </value>
    [InverseProperty("Country")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.CustomerAddress> CustomerAddresses { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///  Initializes a new instance of the Country class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  Country()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
