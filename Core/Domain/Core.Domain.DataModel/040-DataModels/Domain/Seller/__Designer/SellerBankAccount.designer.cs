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
/// Represents SellerBankAccount DataModel.
/// </summary>
/// <remarks>
/// The SellerBankAccount DataModel.
/// </remarks>
[Table("SellerBankAccount", Schema = "dbo")]
public partial class SellerBankAccount : COREDOMAINDATAMODELS.DataModel<SellerBankAccount>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the SellerBankAccountUId of SellerBankAccount.
    /// </summary>
    /// <value>
    /// The SellerBankAccountUId.
    /// </value>
    [Display(Name = nameof(SellerBankAccountUId))]
    [Column(nameof(SellerBankAccountUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "SellerBankAccountUId  is required.")]
    public virtual Guid SellerBankAccountUId { get; set; }

    /// <summary>
    /// Gets or Sets the SellerBankAccountId of SellerBankAccount.
    /// </summary>
    /// <value>
    /// The SellerBankAccountId.
    /// </value>
    [Display(Name = nameof(SellerBankAccountId))]
    [Column(nameof(SellerBankAccountId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "SellerBankAccountId  is required.")]
    public virtual Int64 SellerBankAccountId { get; set; }

    /// <summary>
    /// Gets or Sets the SellerUId of SellerBankAccount.
    /// </summary>
    /// <value>
    /// The SellerUId.
    /// </value>
    [Display(Name = nameof(SellerUId))]
    [Column(nameof(SellerUId), Order = 3)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "SellerUId  is required.")]
    public virtual Guid SellerUId { get; set; }

    /// <summary>
    /// Gets or Sets the BankAccountTypeUId of SellerBankAccount.
    /// </summary>
    /// <value>
    /// The BankAccountTypeUId.
    /// </value>
    [Display(Name = nameof(BankAccountTypeUId))]
    [Column(nameof(BankAccountTypeUId), Order = 4)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "BankAccountTypeUId  is required.")]
    public virtual Guid BankAccountTypeUId { get; set; }

    /// <summary>
    /// Gets or Sets the NameOnAccount of SellerBankAccount.
    /// </summary>
    /// <value>
    /// The NameOnAccount.
    /// </value>
    [Display(Name = nameof(NameOnAccount))]
    [Column("NameOnAccount", Order = 5)] 
    [MaxLength(150, ErrorMessage = "NameOnAccount value cannot be more then 150 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "NameOnAccount  is required.")]
    public virtual String NameOnAccount { get; set; }

    /// <summary>
    /// Gets or Sets the Number of SellerBankAccount.
    /// </summary>
    /// <value>
    /// The Number.
    /// </value>
    [Display(Name = nameof(Number))]
    [Column("Number", Order = 6)] 
    [MaxLength(15, ErrorMessage = "Number value cannot be more then 15 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Number  is required.")]
    public virtual String Number { get; set; }

    /// <summary>
    /// Gets or Sets the RoutingNumber of SellerBankAccount.
    /// </summary>
    /// <value>
    /// The RoutingNumber.
    /// </value>
    [Display(Name = nameof(RoutingNumber))]
    [Column("RoutingNumber", Order = 7)] 
    [MaxLength(7, ErrorMessage = "RoutingNumber value cannot be more then 7 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "RoutingNumber  is required.")]
    public virtual String RoutingNumber { get; set; }

    /// <summary>
    /// Gets or Sets the IsPreferred of SellerBankAccount.
    /// </summary>
    /// <value>
    /// The IsPreferred.
    /// </value>
    [Display(Name = nameof(IsPreferred))]
    [Column(nameof(IsPreferred), Order = 8)]
    public virtual Boolean? IsPreferred { get; set; }

    /// <summary>
    /// Gets or Sets the SortOrder of SellerBankAccount.
    /// </summary>
    /// <value>
    /// The SortOrder.
    /// </value>
    [Display(Name = nameof(SortOrder))]
    [Column(nameof(SortOrder), Order = 9)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "SortOrder  is required.")]
    public virtual Int64 SortOrder { get; set; }

    /// <summary>
    /// Represents Seller referenced entity.
    /// </summary>
    /// <value>
    /// The Seller referenced entity.
    /// </value>
    [ForeignKey(nameof(SellerUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.Seller Seller { get; set; }

    /// <summary>
    /// Represents MasterListItem referenced entity.
    /// </summary>
    /// <value>
    /// The MasterListItem referenced entity.
    /// </value>
    [ForeignKey(nameof(BankAccountTypeUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.MasterListItem BankAccountType { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///  Initializes a new instance of the SellerBankAccount class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  SellerBankAccount()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
