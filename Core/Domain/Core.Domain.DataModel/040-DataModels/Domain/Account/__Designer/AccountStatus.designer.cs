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
/// Represents AccountStatus DataModel.
/// </summary>
/// <remarks>
/// The AccountStatus DataModel.
/// </remarks>
[Table("AccountStatus", Schema = "dbo")]
public partial class AccountStatus : COREDOMAINDATAMODELS.DataModel<AccountStatus>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the AccountStatusUId of AccountStatus.
    /// </summary>
    /// <value>
    /// The AccountStatusUId.
    /// </value>
    [Display(Name = nameof(AccountStatusUId))]
    [Column(nameof(AccountStatusUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "AccountStatusUId  is required.")]
    public virtual Guid AccountStatusUId { get; set; }

    /// <summary>
    /// Gets or Sets the AccountStatusId of AccountStatus.
    /// </summary>
    /// <value>
    /// The AccountStatusId.
    /// </value>
    [Display(Name = nameof(AccountStatusId))]
    [Column(nameof(AccountStatusId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "AccountStatusId  is required.")]
    public virtual Int64 AccountStatusId { get; set; }

    /// <summary>
    /// Gets or Sets the Name of AccountStatus.
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
    /// Gets or Sets the Code of AccountStatus.
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
    /// Gets or Sets the Description of AccountStatus.
    /// </summary>
    /// <value>
    /// The Description.
    /// </value>
    [Display(Name = nameof(Description))]
    [Column("Description", Order = 5)] 
    [MaxLength(4000, ErrorMessage = "Description value cannot be more then 4000 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Description  is required.")]
    public virtual String Description { get; set; }

    /// <summary>
    /// Gets or Sets the DisplayOrder of AccountStatus.
    /// </summary>
    /// <value>
    /// The DisplayOrder.
    /// </value>
    [Display(Name = nameof(DisplayOrder))]
    [Column(nameof(DisplayOrder), Order = 6)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "DisplayOrder  is required.")]
    public virtual Int64 DisplayOrder { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.Account referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.Account referenced entity.
    /// </value>
    [InverseProperty("AccountStatus")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.Account> Accounts { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///  Initializes a new instance of the AccountStatus class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  AccountStatus()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
