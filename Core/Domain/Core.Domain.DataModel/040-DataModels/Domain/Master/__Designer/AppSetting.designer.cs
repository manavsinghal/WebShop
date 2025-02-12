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
/// Represents AppSetting DataModel.
/// </summary>
/// <remarks>
/// The AppSetting DataModel.
/// </remarks>
[Table("AppSetting", Schema = "dbo")]
public partial class AppSetting : COREDOMAINDATAMODELS.DataModel<AppSetting>
{
	#region Fields

    #endregion

	#region Properties

    /// <summary>
    /// Gets or Sets the AppSettingUId of AppSetting.
    /// </summary>
    /// <value>
    /// The AppSettingUId.
    /// </value>
    [Display(Name = nameof(AppSettingUId))]
    [Column(nameof(AppSettingUId), Order = 1)]
    [Key]
    [Required(AllowEmptyStrings = false, ErrorMessage = "AppSettingUId  is required.")]
    public virtual Guid AppSettingUId { get; set; }

    /// <summary>
    /// Gets or Sets the AppSettingId of AppSetting.
    /// </summary>
    /// <value>
    /// The AppSettingId.
    /// </value>
    [Display(Name = nameof(AppSettingId))]
    [Column(nameof(AppSettingId), Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "AppSettingId  is required.")]
    public virtual Int64 AppSettingId { get; set; }

    /// <summary>
    /// Gets or Sets the Name of AppSetting.
    /// </summary>
    /// <value>
    /// The Name.
    /// </value>
    [Display(Name = nameof(Name))]
    [Column("Name", Order = 3)] 
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [MaxLength(401, ErrorMessage = "Name value cannot be more then 401 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name  is required.")]
    public virtual String Name { get; set; }

    /// <summary>
    /// Gets or Sets the ComponentName of AppSetting.
    /// </summary>
    /// <value>
    /// The ComponentName.
    /// </value>
    [Display(Name = nameof(ComponentName))]
    [Column("ComponentName", Order = 4)] 
    [MaxLength(150, ErrorMessage = "ComponentName value cannot be more then 150 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ComponentName  is required.")]
    public virtual String ComponentName { get; set; }

    /// <summary>
    /// Gets or Sets the KeyName of AppSetting.
    /// </summary>
    /// <value>
    /// The KeyName.
    /// </value>
    [Display(Name = nameof(KeyName))]
    [Column("KeyName", Order = 5)] 
    [MaxLength(250, ErrorMessage = "KeyName value cannot be more then 250 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "KeyName  is required.")]
    public virtual String KeyName { get; set; }

    /// <summary>
    /// Gets or Sets the Value of AppSetting.
    /// </summary>
    /// <value>
    /// The Value.
    /// </value>
    [Display(Name = nameof(Value))]
    [Column("Value", Order = 6)] 
    public virtual String? Value { get; set; }

    /// <summary>
    /// Gets or Sets the Description of AppSetting.
    /// </summary>
    /// <value>
    /// The Description.
    /// </value>
    [Display(Name = nameof(Description))]
    [Column("Description", Order = 7)] 
    [MaxLength(4000, ErrorMessage = "Description value cannot be more then 4000 characters.")]
    public virtual String? Description { get; set; }

    /// <summary>
    /// Gets or Sets the DisplayOrder of AppSetting.
    /// </summary>
    /// <value>
    /// The DisplayOrder.
    /// </value>
    [Display(Name = nameof(DisplayOrder))]
    [Column(nameof(DisplayOrder), Order = 8)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "DisplayOrder  is required.")]
    public virtual Int64 DisplayOrder { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///  Initializes a new instance of the AppSetting class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public  AppSetting()
    {
    } 

    #endregion

    #region Methods

    #endregion
}
