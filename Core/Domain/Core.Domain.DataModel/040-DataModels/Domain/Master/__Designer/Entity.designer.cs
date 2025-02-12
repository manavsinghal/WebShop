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
/// Represents Entity DataModel.
/// </summary>
/// <remarks>
/// The Entity DataModel.
/// </remarks>
[Table("Entity", Schema = "dbo")]
public partial class Entity : COREDOMAINDATAMODELS.DataModel<Entity>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the EntityUId of Entity.
    /// </summary>
    /// <value>
    /// The EntityUId.
    /// </value>
    [Display(Name = nameof(EntityUId))]
    [Column(nameof(EntityUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "EntityUId  is required.")]
    public virtual Guid EntityUId { get; set; }

    /// <summary>
    /// Gets or Sets the EntityId of Entity.
    /// </summary>
    /// <value>
    /// The EntityId.
    /// </value>
    [Display(Name = nameof(EntityId))]
    [Column(nameof(EntityId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "EntityId  is required.")]
    public virtual Int64 EntityId { get; set; }

    /// <summary>
    /// Gets or Sets the EntityParentUId of Entity.
    /// </summary>
    /// <value>
    /// The EntityParentUId.
    /// </value>
    [Display(Name = nameof(EntityParentUId))]
    [Column(nameof(EntityParentUId), Order = 3)]
    public virtual Guid? EntityParentUId { get; set; }

    /// <summary>
    /// Gets or Sets the Name of Entity.
    /// </summary>
    /// <value>
    /// The Name.
    /// </value>
    [Display(Name = nameof(Name))]
    [Column("Name", Order = 4)] 
    [MaxLength(150, ErrorMessage = "Name value cannot be more then 150 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name  is required.")]
    public virtual String Name { get; set; }

    /// <summary>
    /// Gets or Sets the Code of Entity.
    /// </summary>
    /// <value>
    /// The Code.
    /// </value>
    [Display(Name = nameof(Code))]
    [Column("Code", Order = 5)] 
    [MaxLength(50, ErrorMessage = "Code value cannot be more then 50 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Code  is required.")]
    public virtual String Code { get; set; }

    /// <summary>
    /// Gets or Sets the Description of Entity.
    /// </summary>
    /// <value>
    /// The Description.
    /// </value>
    [Display(Name = nameof(Description))]
    [Column("Description", Order = 6)] 
    [MaxLength(4000, ErrorMessage = "Description value cannot be more then 4000 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Description  is required.")]
    public virtual String Description { get; set; }

    /// <summary>
    /// Gets or Sets the DisplayOrder of Entity.
    /// </summary>
    /// <value>
    /// The DisplayOrder.
    /// </value>
    [Display(Name = nameof(DisplayOrder))]
    [Column(nameof(DisplayOrder), Order = 7)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "DisplayOrder  is required.")]
    public virtual Int64 DisplayOrder { get; set; }

    /// <summary>
    /// Represents Entity referenced entity.
    /// </summary>
    /// <value>
    /// The Entity referenced entity.
    /// </value>
    [ForeignKey(nameof(EntityParentUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.Entity EntityParent { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///  Initializes a new instance of the Entity class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  Entity()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
