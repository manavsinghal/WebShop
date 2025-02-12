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
/// Represents CountryLanguage DataModel.
/// </summary>
/// <remarks>
/// The CountryLanguage DataModel.
/// </remarks>
[Table("CountryLanguage", Schema = "dbo")]
public partial class CountryLanguage : COREDOMAINDATAMODELS.DataModel<CountryLanguage>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the CountryLanguageUId of CountryLanguage.
    /// </summary>
    /// <value>
    /// The CountryLanguageUId.
    /// </value>
    [Display(Name = nameof(CountryLanguageUId))]
    [Column(nameof(CountryLanguageUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CountryLanguageUId  is required.")]
    public virtual Guid CountryLanguageUId { get; set; }

    /// <summary>
    /// Gets or Sets the CountryLanguageId of CountryLanguage.
    /// </summary>
    /// <value>
    /// The CountryLanguageId.
    /// </value>
    [Display(Name = nameof(CountryLanguageId))]
    [Column(nameof(CountryLanguageId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CountryLanguageId  is required.")]
    public virtual Int64 CountryLanguageId { get; set; }

    /// <summary>
    /// Gets or Sets the CountryUId of CountryLanguage.
    /// </summary>
    /// <value>
    /// The CountryUId.
    /// </value>
    [Display(Name = nameof(CountryUId))]
    [Column(nameof(CountryUId), Order = 3)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CountryUId  is required.")]
    public virtual Guid CountryUId { get; set; }

    /// <summary>
    /// Gets or Sets the LanguageUId of CountryLanguage.
    /// </summary>
    /// <value>
    /// The LanguageUId.
    /// </value>
    [Display(Name = nameof(LanguageUId))]
    [Column(nameof(LanguageUId), Order = 4)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "LanguageUId  is required.")]
    public virtual Guid LanguageUId { get; set; }

    /// <summary>
    /// Gets or Sets the Name of CountryLanguage.
    /// </summary>
    /// <value>
    /// The Name.
    /// </value>
    [Display(Name = nameof(Name))]
    [Column("Name", Order = 5)] 
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name  is required.")]
    public virtual String Name { get; set; }

    /// <summary>
    /// Represents Country referenced entity.
    /// </summary>
    /// <value>
    /// The Country referenced entity.
    /// </value>
    [ForeignKey(nameof(CountryUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.Country Country { get; set; }

    /// <summary>
    /// Represents Language referenced entity.
    /// </summary>
    /// <value>
    /// The Language referenced entity.
    /// </value>
    [ForeignKey(nameof(LanguageUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.Language Language { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///  Initializes a new instance of the CountryLanguage class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  CountryLanguage()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
