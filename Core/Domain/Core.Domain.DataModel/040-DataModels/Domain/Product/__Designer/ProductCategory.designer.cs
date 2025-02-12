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
/// Represents ProductCategory DataModel.
/// </summary>
/// <remarks>
/// The ProductCategory DataModel.
/// </remarks>
[Table("ProductCategory", Schema = "dbo")]
public partial class ProductCategory : COREDOMAINDATAMODELS.DataModel<ProductCategory>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the ProductCategoryUId of ProductCategory.
    /// </summary>
    /// <value>
    /// The ProductCategoryUId.
    /// </value>
    [Display(Name = nameof(ProductCategoryUId))]
    [Column(nameof(ProductCategoryUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ProductCategoryUId  is required.")]
    public virtual Guid ProductCategoryUId { get; set; }

    /// <summary>
    /// Gets or Sets the ProductCategoryId of ProductCategory.
    /// </summary>
    /// <value>
    /// The ProductCategoryId.
    /// </value>
    [Display(Name = nameof(ProductCategoryId))]
    [Column(nameof(ProductCategoryId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ProductCategoryId  is required.")]
    public virtual Int64 ProductCategoryId { get; set; }

    /// <summary>
    /// Gets or Sets the Name of ProductCategory.
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
    /// Gets or Sets the ProductCategoryParentUId of ProductCategory.
    /// </summary>
    /// <value>
    /// The ProductCategoryParentUId.
    /// </value>
    [Display(Name = nameof(ProductCategoryParentUId))]
    [Column(nameof(ProductCategoryParentUId), Order = 4)]
    public virtual Guid? ProductCategoryParentUId { get; set; }

    /// <summary>
    /// Gets or Sets the Description of ProductCategory.
    /// </summary>
    /// <value>
    /// The Description.
    /// </value>
    [Display(Name = nameof(Description))]
    [Column("Description", Order = 5)] 
    [MaxLength(4000, ErrorMessage = "Description value cannot be more then 4000 characters.")]
    public virtual String? Description { get; set; }

    /// <summary>
    /// Gets or Sets the SortOrder of ProductCategory.
    /// </summary>
    /// <value>
    /// The SortOrder.
    /// </value>
    [Display(Name = nameof(SortOrder))]
    [Column(nameof(SortOrder), Order = 6)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "SortOrder  is required.")]
    public virtual Int64 SortOrder { get; set; }

    /// <summary>
    /// Represents ProductCategory referenced entity.
    /// </summary>
    /// <value>
    /// The ProductCategory referenced entity.
    /// </value>
    [ForeignKey(nameof(ProductCategoryParentUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.ProductCategory ProductCategoryParent { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ProductCategoryLanguage referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ProductCategoryLanguage referenced entity.
    /// </value>
    [InverseProperty("ProductCategory")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ProductCategoryLanguage> ProductCategoryLanguages { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.Product referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.Product referenced entity.
    /// </value>
    [InverseProperty("ProductCategoryParent")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.Product> ProductProductCategoryParents { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///  Initializes a new instance of the ProductCategory class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  ProductCategory()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
