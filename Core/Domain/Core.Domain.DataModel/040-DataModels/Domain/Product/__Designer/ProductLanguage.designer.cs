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
/// Represents ProductLanguage DataModel.
/// </summary>
/// <remarks>
/// The ProductLanguage DataModel.
/// </remarks>
[Table("ProductLanguage", Schema = "dbo")]
public partial class ProductLanguage : COREDOMAINDATAMODELS.DataModel<ProductLanguage>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the ProductLanguageUId of ProductLanguage.
    /// </summary>
    /// <value>
    /// The ProductLanguageUId.
    /// </value>
    [Display(Name = nameof(ProductLanguageUId))]
    [Column(nameof(ProductLanguageUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ProductLanguageUId  is required.")]
    public virtual Guid ProductLanguageUId { get; set; }

    /// <summary>
    /// Gets or Sets the ProductLanguageId of ProductLanguage.
    /// </summary>
    /// <value>
    /// The ProductLanguageId.
    /// </value>
    [Display(Name = nameof(ProductLanguageId))]
    [Column(nameof(ProductLanguageId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ProductLanguageId  is required.")]
    public virtual Int64 ProductLanguageId { get; set; }

    /// <summary>
    /// Gets or Sets the ProductUId of ProductLanguage.
    /// </summary>
    /// <value>
    /// The ProductUId.
    /// </value>
    [Display(Name = nameof(ProductUId))]
    [Column(nameof(ProductUId), Order = 3)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ProductUId  is required.")]
    public virtual Guid ProductUId { get; set; }

    /// <summary>
    /// Gets or Sets the LanguageUId of ProductLanguage.
    /// </summary>
    /// <value>
    /// The LanguageUId.
    /// </value>
    [Display(Name = nameof(LanguageUId))]
    [Column(nameof(LanguageUId), Order = 4)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "LanguageUId  is required.")]
    public virtual Guid LanguageUId { get; set; }

    /// <summary>
    /// Gets or Sets the Name of ProductLanguage.
    /// </summary>
    /// <value>
    /// The Name.
    /// </value>
    [Display(Name = nameof(Name))]
    [Column("Name", Order = 5)] 
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name  is required.")]
    public virtual String Name { get; set; }

    /// <summary>
    /// Represents Product referenced entity.
    /// </summary>
    /// <value>
    /// The Product referenced entity.
    /// </value>
    [ForeignKey(nameof(ProductUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.Product Product { get; set; }

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
    ///  Initializes a new instance of the ProductLanguage class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  ProductLanguage()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
