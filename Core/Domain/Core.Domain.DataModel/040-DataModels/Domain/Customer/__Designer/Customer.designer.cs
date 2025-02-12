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
/// Represents Customer DataModel.
/// </summary>
/// <remarks>
/// The Customer DataModel.
/// </remarks>
[Table("Customer", Schema = "dbo")]
public partial class Customer : COREDOMAINDATAMODELS.DataModel<Customer>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the CustomerUId of Customer.
    /// </summary>
    /// <value>
    /// The CustomerUId.
    /// </value>
    [Display(Name = nameof(CustomerUId))]
    [Column(nameof(CustomerUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CustomerUId  is required.")]
    public virtual Guid CustomerUId { get; set; }

    /// <summary>
    /// Gets or Sets the CustomerId of Customer.
    /// </summary>
    /// <value>
    /// The CustomerId.
    /// </value>
    [Display(Name = nameof(CustomerId))]
    [Column(nameof(CustomerId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CustomerId  is required.")]
    public virtual Int64 CustomerId { get; set; }

    /// <summary>
    /// Gets or Sets the Name of Customer.
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
    /// Gets or Sets the Rating of Customer.
    /// </summary>
    /// <value>
    /// The Rating.
    /// </value>
    [Display(Name = nameof(Rating))]
    [Column(nameof(Rating), Order = 4)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Rating  is required.")]
    public virtual Decimal Rating { get; set; }

    /// <summary>
    /// Gets or Sets the CustomerStatusUId of Customer.
    /// </summary>
    /// <value>
    /// The CustomerStatusUId.
    /// </value>
    [Display(Name = nameof(CustomerStatusUId))]
    [Column(nameof(CustomerStatusUId), Order = 5)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "CustomerStatusUId  is required.")]
    public virtual Guid CustomerStatusUId { get; set; }

    /// <summary>
    /// Gets or Sets the SortOrder of Customer.
    /// </summary>
    /// <value>
    /// The SortOrder.
    /// </value>
    [Display(Name = nameof(SortOrder))]
    [Column(nameof(SortOrder), Order = 6)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "SortOrder  is required.")]
    public virtual Int64 SortOrder { get; set; }

    /// <summary>
    /// Represents MasterListItem referenced entity.
    /// </summary>
    /// <value>
    /// The MasterListItem referenced entity.
    /// </value>
    [ForeignKey(nameof(CustomerStatusUId))]
    public virtual COREDOMAINDATAMODELSDOMAIN.MasterListItem CustomerStatus { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.Order referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.Order referenced entity.
    /// </value>
    [InverseProperty("Customer")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.Order> Orders { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.CustomerPhone referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.CustomerPhone referenced entity.
    /// </value>
    [InverseProperty("Customer")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.CustomerPhone> CustomerPhones { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.CustomerAddress referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.CustomerAddress referenced entity.
    /// </value>
    [InverseProperty("Customer")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.CustomerAddress> CustomerAddresses { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.CustomerCard referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.CustomerCard referenced entity.
    /// </value>
    [InverseProperty("Customer")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.CustomerCard> CustomerCards { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ShoppingCart referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ShoppingCart referenced entity.
    /// </value>
    [InverseProperty("Customer")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ShoppingCart> ShoppingCarts { get; set; }

    /// <summary>
    /// Represents COREDOMAINDATAMODELSDOMAIN.ShoppingCartWishList referenced entity.
    /// </summary>
    /// <value>
    /// The COREDOMAINDATAMODELSDOMAIN.ShoppingCartWishList referenced entity.
    /// </value>
    [InverseProperty("Customer")]
    public virtual ICollection<COREDOMAINDATAMODELSDOMAIN.ShoppingCartWishList> ShoppingCartWishLists { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///  Initializes a new instance of the Customer class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  Customer()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
