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
/// Represents ProductCategoryLanguage DataModel.
/// </summary>
/// <remarks>
/// The ProductCategoryLanguage DataModel.
/// </remarks>
[Table("ProductCategoryLanguage", Schema = "dbo")]
public partial class ProductCategoryLanguage : COREDOMAINDATAMODELS.DataModel<ProductCategoryLanguage>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the ProductCategoryLanguageUId of ProductCategoryLanguage.
    /// </summary>
    /// <value>
    /// The ProductCategoryLanguageUId.
    /// </value>
    [Display(Name = nameof(ProductCategoryLanguageUId))]
    [Column(nameof(ProductCategoryLanguageUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ProductCategoryLanguageUId  is required.")]
    public virtual Guid ProductCategoryLanguageUId { get; set; }

    /// <summary>
    /// Gets or Sets the ProductCategoryLanguageId of ProductCategoryLanguage.
    /// </summary>
    /// <value>
    /// The ProductCategoryLanguageId.
    /// </value>
    [Display(Name = nameof(ProductCategoryLanguageId))]
    [Column(nameof(ProductCategoryLanguageId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ProductCategoryLanguageId  is required.")]
    public virtual Int64 ProductCategoryLanguageId { get; set; }

    /// <summary>
    /// Gets or Sets the ProductCategoryUId of ProductCategoryLanguage.
    /// </summary>
    /// <value>
    /// The ProductCategoryUId.
    /// </value>
    [Display(Name = nameof(ProductCategoryUId))]
    [Column(nameof(ProductCategoryUId), Order = 3)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ProductCategoryUId  is required.")]
    public virtual Guid ProductCategoryUId { get; set; }

    /// <summary>
    /// Gets or Sets the LanguageUId of ProductCategoryLanguage.
    /// </summary>
    /// <value>
    /// The LanguageUId.
    /// </value>
    [Display(Name = nameof(LanguageUId))]
    [Column(nameof(LanguageUId), Order = 4)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "LanguageUId  is required.")]
    public virtual Guid LanguageUId { get; set; }

    /// <summary>
    /// Gets or Sets the Name of ProductCategoryLanguage.
    /// </summary>
    /// <value>
    /// The Name.
    /// </value>
    [Display(Name = nameof(Name))]
    [Column("Name", Order = 5)] 
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name  is required.")]
    public virtual String Name { get; set; }

    /// <summary>
    /// Gets or Sets the Description of ProductCategoryLanguage.
    /// </summary>
    /// <value>
    /// The Description.
    /// </value>
    [Display(Name = nameof(Description))]
    [Column("Description", Order = 6)] 
    public virtual String? Description { get; set; }

    /// <summary>
    /// Represents ProductCategory referenced entity.
    /// </summary>
    /// <value>
    /// The ProductCategory referenced entity.
    /// </value>
    [ForeignKey(nameof(ProductCategoryUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.ProductCategory ProductCategory { get; set; }

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
    ///  Initializes a new instance of the ProductCategoryLanguage class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  ProductCategoryLanguage()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
