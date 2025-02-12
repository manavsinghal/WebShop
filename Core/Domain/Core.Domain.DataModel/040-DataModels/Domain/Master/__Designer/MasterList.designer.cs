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
/// Represents MasterList DataModel.
/// </summary>
/// <remarks>
/// The MasterList DataModel.
/// </remarks>
[Table("MasterList", Schema = "dbo")]
public partial class MasterList : COREDOMAINDATAMODELS.DataModel<MasterList>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the MasterListUId of MasterList.
    /// </summary>
    /// <value>
    /// The MasterListUId.
    /// </value>
    [Display(Name = nameof(MasterListUId))]
    [Column(nameof(MasterListUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "MasterListUId  is required.")]
    public virtual Guid MasterListUId { get; set; }

    /// <summary>
    /// Gets or Sets the MasterListId of MasterList.
    /// </summary>
    /// <value>
    /// The MasterListId.
    /// </value>
    [Display(Name = nameof(MasterListId))]
    [Column(nameof(MasterListId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "MasterListId  is required.")]
    public virtual Int64 MasterListId { get; set; }

    /// <summary>
    /// Gets or Sets the Name of MasterList.
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
    /// Gets or Sets the Code of MasterList.
    /// </summary>
    /// <value>
    /// The Code.
    /// </value>
    [Display(Name = nameof(Code))]
    [Column("Code", Order = 4)] 
    [MaxLength(50, ErrorMessage = "Code value cannot be more then 50 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Code  is required.")]
    public virtual String Code { get; set; }

    /// <summary>
    /// Gets or Sets the Description of MasterList.
    /// </summary>
    /// <value>
    /// The Description.
    /// </value>
    [Display(Name = nameof(Description))]
    [Column("Description", Order = 5)] 
    [MaxLength(500, ErrorMessage = "Description value cannot be more then 500 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Description  is required.")]
    public virtual String Description { get; set; }

    /// <summary>
    /// Gets or Sets the Source of MasterList.
    /// </summary>
    /// <value>
    /// The Source.
    /// </value>
    [Display(Name = nameof(Source))]
    [Column("Source", Order = 6)] 
    [MaxLength(150, ErrorMessage = "Source value cannot be more then 150 characters.")]
    public virtual String? Source { get; set; }

    /// <summary>
    /// Gets or Sets the ImageUrl of MasterList.
    /// </summary>
    /// <value>
    /// The ImageUrl.
    /// </value>
    [Display(Name = nameof(ImageUrl))]
    [Column("ImageUrl", Order = 7)] 
    [MaxLength(4000, ErrorMessage = "ImageUrl value cannot be more then 4000 characters.")]
    public virtual String? ImageUrl { get; set; }

    /// <summary>
    /// Gets or Sets the DisplayOrder of MasterList.
    /// </summary>
    /// <value>
    /// The DisplayOrder.
    /// </value>
    [Display(Name = nameof(DisplayOrder))]
    [Column(nameof(DisplayOrder), Order = 8)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "DisplayOrder  is required.")]
    public virtual Int64 DisplayOrder { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.MasterListItem referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.MasterListItem referenced entity.
    /// </value>
    [InverseProperty("MasterList")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.MasterListItem> MasterListItems { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///  Initializes a new instance of the MasterList class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  MasterList()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
