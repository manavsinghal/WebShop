#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion  

namespace Accenture.WebShop.Core.Domain.DataModels;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents DataModelWithAudit Data Model.
/// </summary>
/// <remarks>
/// Represents DataModelWithAudit Data Model.
/// </remarks>
[Table("DataModelWithAudit")]
public partial class DataModelWithAudit<T> : DataModelBase<T> where T : class
{
	#region Fields

	#endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the CreatedByAccountUId of DataModelWithAudit.
    /// </summary>
    /// <value>
    /// The CreatedByAccountUId.
    /// </value>
    [Display(Name = nameof(CreatedByAccountUId))]
    [Column(nameof(CreatedByAccountUId), Order = 242)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CreatedByAccountUId  is required.")]
    public virtual Guid CreatedByAccountUId { get; set; }

    /// <summary>
    /// Gets or Sets the CreatedByApp of DataModelWithAudit.
    /// </summary>
    /// <value>
    /// The CreatedByApp.
    /// </value>
    [Display(Name = nameof(CreatedByApp))]
    [Column(nameof(CreatedByApp), Order = 243)] 
    [StringLength(150)]
    [MinLength(5, ErrorMessage = "CreatedByApp value cannot be less then 5 characters.")]
    [MaxLength(150, ErrorMessage = "CreatedByApp value cannot be more then 150 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CreatedByApp  is required.")]
    public virtual String CreatedByApp { get; set; }

    /// <summary>
    /// Gets or Sets the CreatedOn of DataModelWithAudit.
    /// </summary>
    /// <value>
    /// The CreatedOn.
    /// </value>
    [Display(Name = nameof(CreatedOn))]
    [Column(nameof(CreatedOn), Order = 244, TypeName = "DateTime2")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CreatedOn  is required.")]
    public virtual DateTime CreatedOn { get; set; }

    /// <summary>
    /// Gets or Sets the ModifiedByAccountUId of DataModelWithAudit.
    /// </summary>
    /// <value>
    /// The ModifiedByAccountUId.
    /// </value>
    [Display(Name = nameof(ModifiedByAccountUId))]
    [Column(nameof(ModifiedByAccountUId), Order = 245)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ModifiedByAccountUId  is required.")]
    public virtual Guid ModifiedByAccountUId { get; set; }

    /// <summary>
    /// Gets or Sets the ModifiedByApp of DataModelWithAudit.
    /// </summary>
    /// <value>
    /// The ModifiedByApp.
    /// </value>
    [Display(Name = nameof(ModifiedByApp))]
    [Column(nameof(ModifiedByApp), Order = 246)] 
    [StringLength(150)]
    [MinLength(5, ErrorMessage = "ModifiedByApp value cannot be less then 5 characters.")]
    [MaxLength(150, ErrorMessage = "ModifiedByApp value cannot be more then 150 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ModifiedByApp  is required.")]
    public virtual String ModifiedByApp { get; set; }

    /// <summary>
    /// Gets or Sets the ModifiedOn of DataModelWithAudit.
    /// </summary>
    /// <value>
    /// The ModifiedOn.
    /// </value>
    [Display(Name = nameof(ModifiedOn))]
    [Column(nameof(ModifiedOn), Order = 247, TypeName = "DateTime2")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ModifiedOn  is required.")]
    public virtual DateTime ModifiedOn { get; set; }

    /// <summary>
    /// Gets or Sets the CorrelationUId of DataModelWithAudit.
    /// </summary>
    /// <value>
    /// The CorrelationUId.
    /// </value>
    [Display(Name = nameof(CorrelationUId))]
    [Column(nameof(CorrelationUId), Order = 248)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CorrelationUId  is required.")]
    public virtual Guid CorrelationUId { get; set; }

    /// <summary>
    /// Gets or Sets the EffectiveFrom of DataModelWithAudit.
    /// </summary>
    /// <value>
    /// The EffectiveFrom.
    /// </value>
    [Display(Name = nameof(EffectiveFrom))]
    [Column(nameof(EffectiveFrom), Order = 249, TypeName = "DateTime2")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "EffectiveFrom  is required.")]
    public virtual DateTime EffectiveFrom { get; set; }

    /// <summary>
    /// Gets or Sets the EffectiveTo of DataModelWithAudit.
    /// </summary>
    /// <value>
    /// The EffectiveTo.
    /// </value>
    [Display(Name = nameof(EffectiveTo))]
    [Column(nameof(EffectiveTo), Order = 250, TypeName = "DateTime2")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "EffectiveTo  is required.")]
    public virtual DateTime EffectiveTo { get; set; }

    /// <summary>
    /// Gets or Sets the RowVersion of DataModelWithAudit.
    /// </summary>
    /// <value>
    /// The RowVersion.
    /// </value>
    [Display(Name = nameof(RowVersion))]
    [Column(nameof(RowVersion), Order = 251)]
	[Timestamp]
	[ConcurrencyCheck]
    public virtual Byte[] RowVersion { get; set; }

    /// <summary>
    /// Gets or Sets the ItemState of DataModelWithAudit.
    /// </summary>
    /// <value>
    /// The ItemState.
    /// </value>
    [Display(Name = nameof(ItemState))]
    [Column(nameof(ItemState), Order = 250)]
	[NotMapped]
    public virtual COREDOMAINDATAMODELSENUM.ItemState ItemState { get; set; }

    /// <summary>
    /// Gets or Sets the CreatedByAccountUIdEmailId of DataModelWithAudit.
    /// </summary>
    /// <value>
    /// The CreatedByAccountUIdEmailId.
    /// </value>
    [Display(Name = nameof(CreatedByAccountUIdEmailId))]
    [Column(nameof(CreatedByAccountUIdEmailId), Order = 253)] 
    [StringLength(150)]
    [MinLength(5, ErrorMessage = "CreatedByAccountUIdEmailId value cannot be less then 5 characters.")]
    [MaxLength(150, ErrorMessage = "CreatedByAccountUIdEmailId value cannot be more then 150 characters.")]
	[NotMapped]
    public virtual String CreatedByAccountUIdEmailId { get; set; }

    /// <summary>
    /// Gets or Sets the ModifiedByAccountUIdEmailId of DataModelWithAudit.
    /// </summary>
    /// <value>
    /// The ModifiedByAccountUIdEmailId.
    /// </value>
    [Display(Name = nameof(ModifiedByAccountUIdEmailId))]
    [Column(nameof(ModifiedByAccountUIdEmailId), Order = 254)] 
    [StringLength(150)]
    [MinLength(5, ErrorMessage = "ModifiedByAccountUIdEmailId value cannot be less then 5 characters.")]
    [MaxLength(150, ErrorMessage = "ModifiedByAccountUIdEmailId value cannot be more then 150 characters.")]
	[NotMapped]
    public virtual String ModifiedByAccountUIdEmailId { get; set; }

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="DataModelWithAudit{T}"/> class.
	/// </summary>
	[ExcludeFromCodeCoverage]
    public DataModelWithAudit() : base()
    {
		ItemState = COREDOMAINDATAMODELSENUM.ItemState.Unchanged;
	}

	#endregion

	#region Methods

	/// <summary>
	/// SetDefaultAuditFields
	/// </summary>
	public virtual void SetDefaultAuditFields()
	{
	}


	/// <summary>
	/// Set default values for this entity.
	/// </summary>
	public virtual void SetDefaultAuditFields(Guid user, String app, DateTime datetime, Guid correlationUId, COREDOMAINDATAMODELSENUM.ItemState itemState)
	{
		this.CreatedByAccountUId = user;
		this.CreatedByApp = app;
		this.CreatedOn = datetime;
		this.ModifiedByAccountUId = user;
		this.ModifiedByApp = app;
		this.ModifiedOn = datetime;
		this.CorrelationUId = correlationUId;
		this.EffectiveFrom = datetime;
		this.EffectiveTo = datetime;
		this.ItemState = itemState;
	}

	#endregion
}
