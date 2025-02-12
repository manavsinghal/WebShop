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
/// Represents DataModel Data Model.
/// </summary>
/// <remarks>
/// Represents DataModel Data Model.
/// </remarks>
[Table("DataModel")]
public partial class DataModel<T> : DataModelWithAudit<T> where T : class
{
	#region Fields

	#endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the RowStatusUId of DataModel.
    /// </summary>
	/// <value>
	/// The RowStatusUId.
	/// </value>
    [Display(Name = nameof(RowStatusUId))]
    [Column(nameof(RowStatusUId), Order = 16)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "RowStatusUId  is required.")]
    public virtual Guid RowStatusUId { get; set; } 

    /// <summary>
    /// Gets or Sets the RowStatus of DataModel.
    /// </summary>
	/// <value>
	/// The RowStatus.
	/// </value>
    [ForeignKey(nameof(RowStatusUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.RowStatus RowStatus { get; set; } 

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="DataModel{T}"/> class.
	/// </summary>
	[ExcludeFromCodeCoverage]
    public DataModel() : base()
    {
		this.RowStatusUId = COREDOMAINDATAMODELSDOMAINENUM.RowStatus.Active;
	}

	#endregion

	#region Methods

	/// <summary>
	/// Set default values for this entity.
	/// </summary>
	public override void SetDefaultAuditFields()
	{
		this.RowStatusUId = COREDOMAINDATAMODELSDOMAINENUM.RowStatus.Active;
		base.SetDefaultAuditFields(user:SHAREDKERNALLIB.AppSettings.CurrentUserEmail
			                      ,app: SHAREDKERNALLIB.AppSettings.CurrentApp
								  ,datetime: SHAREDKERNALLIB.AppSettings.CurrentDateTime
								  ,correlationUId: SHAREDKERNALLIB.AppSettings.CurrentCorrelationUId
								  ,itemState: COREDOMAINDATAMODELSENUM.ItemState.Added);
	}

	#endregion
}
