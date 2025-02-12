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
/// Represents Account DataModel.
/// </summary>
/// <remarks>
/// The Account DataModel.
/// </remarks>
[Table("Account", Schema = "dbo")]
public partial class Account : COREDOMAINDATAMODELS.DataModel<Account>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the AccountUId of Account.
    /// </summary>
    /// <value>
    /// The AccountUId.
    /// </value>
    [Display(Name = nameof(AccountUId))]
    [Column(nameof(AccountUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "AccountUId  is required.")]
    public virtual Guid AccountUId { get; set; }

    /// <summary>
    /// Gets or Sets the AccountId of Account.
    /// </summary>
    /// <value>
    /// The AccountId.
    /// </value>
    [Display(Name = nameof(AccountId))]
    [Column(nameof(AccountId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "AccountId  is required.")]
    public virtual Int64 AccountId { get; set; }

    /// <summary>
    /// Gets or Sets the EmailId of Account.
    /// </summary>
    /// <value>
    /// The EmailId.
    /// </value>
    [Display(Name = nameof(EmailId))]
    [Column("EmailId", Order = 3)] 
    [MaxLength(150, ErrorMessage = "EmailId value cannot be more then 150 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "EmailId  is required.")]
    public virtual String EmailId { get; set; }

    /// <summary>
    /// Gets or Sets the AccountStatusUId of Account.
    /// </summary>
    /// <value>
    /// The AccountStatusUId.
    /// </value>
    [Display(Name = nameof(AccountStatusUId))]
    [Column(nameof(AccountStatusUId), Order = 4)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "AccountStatusUId  is required.")]
    public virtual Guid AccountStatusUId { get; set; }

    /// <summary>
    /// Gets or Sets the FirstName of Account.
    /// </summary>
    /// <value>
    /// The FirstName.
    /// </value>
    [Display(Name = nameof(FirstName))]
    [Column("FirstName", Order = 5)] 
    [MaxLength(50, ErrorMessage = "FirstName value cannot be more then 50 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "FirstName  is required.")]
    public virtual String FirstName { get; set; }

    /// <summary>
    /// Gets or Sets the LastName of Account.
    /// </summary>
    /// <value>
    /// The LastName.
    /// </value>
    [Display(Name = nameof(LastName))]
    [Column("LastName", Order = 6)] 
    [MaxLength(50, ErrorMessage = "LastName value cannot be more then 50 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "LastName  is required.")]
    public virtual String LastName { get; set; }

    /// <summary>
    /// Gets or Sets the ImportSource of Account.
    /// </summary>
    /// <value>
    /// The ImportSource.
    /// </value>
    [Display(Name = nameof(ImportSource))]
    [Column("ImportSource", Order = 7)] 
    [MaxLength(50, ErrorMessage = "ImportSource value cannot be more then 50 characters.")]
    public virtual String? ImportSource { get; set; }

    /// <summary>
    /// Gets or Sets the SortOrder of Account.
    /// </summary>
    /// <value>
    /// The SortOrder.
    /// </value>
    [Display(Name = nameof(SortOrder))]
    [Column(nameof(SortOrder), Order = 8)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "SortOrder  is required.")]
    public virtual Int64 SortOrder { get; set; }

    /// <summary>
    /// Represents AccountStatus referenced entity.
    /// </summary>
    /// <value>
    /// The AccountStatus referenced entity.
    /// </value>
    [ForeignKey(nameof(AccountStatusUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.AccountStatus AccountStatus { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///  Initializes a new instance of the Account class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  Account()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
