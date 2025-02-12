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
/// Represents Language DataModel.
/// </summary>
/// <remarks>
/// The Language DataModel.
/// </remarks>
[Table("Language", Schema = "dbo")]
public partial class Language : COREDOMAINDATAMODELS.DataModel<Language>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the LanguageUId of Language.
    /// </summary>
    /// <value>
    /// The LanguageUId.
    /// </value>
    [Display(Name = nameof(LanguageUId))]
    [Column(nameof(LanguageUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "LanguageUId  is required.")]
    public virtual Guid LanguageUId { get; set; }

    /// <summary>
    /// Gets or Sets the LanguageId of Language.
    /// </summary>
    /// <value>
    /// The LanguageId.
    /// </value>
    [Display(Name = nameof(LanguageId))]
    [Column(nameof(LanguageId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "LanguageId  is required.")]
    public virtual Int64 LanguageId { get; set; }

    /// <summary>
    /// Gets or Sets the Name of Language.
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
    /// Gets or Sets the LocalizedName of Language.
    /// </summary>
    /// <value>
    /// The LocalizedName.
    /// </value>
    [Display(Name = nameof(LocalizedName))]
    [Column("LocalizedName", Order = 4)] 
    [MaxLength(150, ErrorMessage = "LocalizedName value cannot be more then 150 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "LocalizedName  is required.")]
    public virtual String LocalizedName { get; set; }

    /// <summary>
    /// Gets or Sets the DisplayName of Language.
    /// </summary>
    /// <value>
    /// The DisplayName.
    /// </value>
    [Display(Name = nameof(DisplayName))]
    [Column("DisplayName", Order = 5)] 
    [MaxLength(150, ErrorMessage = "DisplayName value cannot be more then 150 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "DisplayName  is required.")]
    public virtual String DisplayName { get; set; }

    /// <summary>
    /// Gets or Sets the Culture of Language.
    /// </summary>
    /// <value>
    /// The Culture.
    /// </value>
    [Display(Name = nameof(Culture))]
    [Column("Culture", Order = 6)] 
    [MaxLength(10, ErrorMessage = "Culture value cannot be more then 10 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Culture  is required.")]
    public virtual String Culture { get; set; }

    /// <summary>
    /// Gets or Sets the AzureCulture of Language.
    /// </summary>
    /// <value>
    /// The AzureCulture.
    /// </value>
    [Display(Name = nameof(AzureCulture))]
    [Column("AzureCulture", Order = 7)] 
    [MaxLength(10, ErrorMessage = "AzureCulture value cannot be more then 10 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "AzureCulture  is required.")]
    public virtual String AzureCulture { get; set; }

    /// <summary>
    /// Gets or Sets the ImageUrl of Language.
    /// </summary>
    /// <value>
    /// The ImageUrl.
    /// </value>
    [Display(Name = nameof(ImageUrl))]
    [Column("ImageUrl", Order = 8)] 
    [MaxLength(1500, ErrorMessage = "ImageUrl value cannot be more then 1500 characters.")]
    public virtual String? ImageUrl { get; set; }

    /// <summary>
    /// Gets or Sets the ImageBinary of Language.
    /// </summary>
    /// <value>
    /// The ImageBinary.
    /// </value>
    [Display(Name = nameof(ImageBinary))]
    [Column(nameof(ImageBinary), Order = 9)]
    public virtual Byte[]? ImageBinary { get; set; }

    /// <summary>
    /// Gets or Sets the Description of Language.
    /// </summary>
    /// <value>
    /// The Description.
    /// </value>
    [Display(Name = nameof(Description))]
    [Column("Description", Order = 10)] 
    [MaxLength(500, ErrorMessage = "Description value cannot be more then 500 characters.")]
    public virtual String? Description { get; set; }

    /// <summary>
    /// Gets or Sets the IsRTL of Language.
    /// </summary>
    /// <value>
    /// The IsRTL.
    /// </value>
    [Display(Name = nameof(IsRTL))]
    [Column(nameof(IsRTL), Order = 11)]
    public virtual Boolean? IsRTL { get; set; }

    /// <summary>
    /// Gets or Sets the DisplayOrder of Language.
    /// </summary>
    /// <value>
    /// The DisplayOrder.
    /// </value>
    [Display(Name = nameof(DisplayOrder))]
    [Column(nameof(DisplayOrder), Order = 12)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "DisplayOrder  is required.")]
    public virtual Int64 DisplayOrder { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.CountryLanguage referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.CountryLanguage referenced entity.
    /// </value>
    [InverseProperty("Language")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.CountryLanguage> CountryLanguages { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ProductCategoryLanguage referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ProductCategoryLanguage referenced entity.
    /// </value>
    [InverseProperty("Language")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ProductCategoryLanguage> ProductCategoryLanguages { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ProductLanguage referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ProductLanguage referenced entity.
    /// </value>
    [InverseProperty("Language")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ProductLanguage> ProductLanguages { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///  Initializes a new instance of the Language class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  Language()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
