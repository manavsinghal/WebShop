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
/// Represents CustomerCard DataModel.
/// </summary>
/// <remarks>
/// The CustomerCard DataModel.
/// </remarks>
[Table("CustomerCard", Schema = "dbo")]
public partial class CustomerCard : COREDOMAINDATAMODELS.DataModel<CustomerCard>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the CustomerCardUId of CustomerCard.
    /// </summary>
    /// <value>
    /// The CustomerCardUId.
    /// </value>
    [Display(Name = nameof(CustomerCardUId))]
    [Column(nameof(CustomerCardUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CustomerCardUId  is required.")]
    public virtual Guid CustomerCardUId { get; set; }

    /// <summary>
    /// Gets or Sets the CustomerCardId of CustomerCard.
    /// </summary>
    /// <value>
    /// The CustomerCardId.
    /// </value>
    [Display(Name = nameof(CustomerCardId))]
    [Column(nameof(CustomerCardId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CustomerCardId  is required.")]
    public virtual Int64 CustomerCardId { get; set; }

    /// <summary>
    /// Gets or Sets the CustomerUId of CustomerCard.
    /// </summary>
    /// <value>
    /// The CustomerUId.
    /// </value>
    [Display(Name = nameof(CustomerUId))]
    [Column(nameof(CustomerUId), Order = 3)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CustomerUId  is required.")]
    public virtual Guid CustomerUId { get; set; }

    /// <summary>
    /// Gets or Sets the CardTypeUId of CustomerCard.
    /// </summary>
    /// <value>
    /// The CardTypeUId.
    /// </value>
    [Display(Name = nameof(CardTypeUId))]
    [Column(nameof(CardTypeUId), Order = 4)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CardTypeUId  is required.")]
    public virtual Guid CardTypeUId { get; set; }

    /// <summary>
    /// Gets or Sets the NameOnCard of CustomerCard.
    /// </summary>
    /// <value>
    /// The NameOnCard.
    /// </value>
    [Display(Name = nameof(NameOnCard))]
    [Column("NameOnCard", Order = 5)] 
    [MaxLength(150, ErrorMessage = "NameOnCard value cannot be more then 150 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "NameOnCard  is required.")]
    public virtual String NameOnCard { get; set; }

    /// <summary>
    /// Gets or Sets the Number of CustomerCard.
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
    /// Gets or Sets the ExpirationDate of CustomerCard.
    /// </summary>
    /// <value>
    /// The ExpirationDate.
    /// </value>
    [Display(Name = nameof(ExpirationDate))]
    [Column("ExpirationDate", Order = 7)] 
    [MaxLength(7, ErrorMessage = "ExpirationDate value cannot be more then 7 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ExpirationDate  is required.")]
    public virtual String ExpirationDate { get; set; }

    /// <summary>
    /// Gets or Sets the SecurityCode of CustomerCard.
    /// </summary>
    /// <value>
    /// The SecurityCode.
    /// </value>
    [Display(Name = nameof(SecurityCode))]
    [Column("SecurityCode", Order = 8)] 
    [MaxLength(5, ErrorMessage = "SecurityCode value cannot be more then 5 characters.")]
    public virtual String? SecurityCode { get; set; }

    /// <summary>
    /// Gets or Sets the SortOrder of CustomerCard.
    /// </summary>
    /// <value>
    /// The SortOrder.
    /// </value>
    [Display(Name = nameof(SortOrder))]
    [Column(nameof(SortOrder), Order = 9)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "SortOrder  is required.")]
    public virtual Int64 SortOrder { get; set; }

    /// <summary>
    /// Represents Customer referenced entity.
    /// </summary>
    /// <value>
    /// The Customer referenced entity.
    /// </value>
    [ForeignKey(nameof(CustomerUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.Customer Customer { get; set; }

    /// <summary>
    /// Represents MasterListItem referenced entity.
    /// </summary>
    /// <value>
    /// The MasterListItem referenced entity.
    /// </value>
    [ForeignKey(nameof(CardTypeUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.MasterListItem CardType { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///  Initializes a new instance of the CustomerCard class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  CustomerCard()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
