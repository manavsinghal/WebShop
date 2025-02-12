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
/// WebAPIServiceSettings
/// </summary>
public class WebAPIServiceSettings
{
	#region Field

	#endregion

	#region Properties

	/// <summary>
	/// Represents BatchSize property.
	/// </summary>
	/// <value>
	/// The BatchSize.
	/// </value>
	public Int32 BatchSize { get; set; }

	/// <summary>
	/// Represents RetryMessageBusCount property.
	/// </summary>
	/// <value>
	/// The RetryMessageBusCount.
	/// </value>
	public Int32 RetryMessageBusCount { get; set; }

	/// <summary>
	/// Represents RequiredProvisionStatusForLiveEnvironment property.
	/// </summary>
	/// <value>
	/// The RequiredProvisionStatusForLiveEnvironment.
	/// </value>
	public String? RequiredProvisionStatusForLiveEnvironment { get; set; }

	/// <summary>
	/// Represents ObfuscatorConfigJsonPath property.
	/// </summary>
	/// <value>
	/// The ObfuscatorConfigJsonPath.
	/// </value>
	public String? ObfuscatorConfigJsonPath { get; set; }

	/// <summary>
	/// EmailTemplateCssFilePath
	/// </summary>
	public String? EmailTemplateCssFilePath { get; set; }

	/// <summary>
	/// IsAssetVersionLockedCheckRequired
	/// </summary>
	public Boolean? IsAssetVersionLockedCheckRequired { get; set; }

	/// <summary>
	/// AssetVersionOwnerRole
	/// </summary>
	public String? AssetVersionOwnerRole { get; set; }

	/// <summary>
	/// AssetVersionOwnerRole
	/// </summary>
	public String? AssetVersionContactRole { get; set; }

	/// <summary>
	/// AssetVersionOwnerRole
	/// </summary>
	public String? AssetVersionProvisionTeamRole { get; set; }

	/// <summary>
	/// IncludeAssetRbac
	/// </summary>
	public Boolean? IncludeAssetRbac { get; set; }

	/// <summary>
	/// IncludeEnvironmentRbac
	/// </summary>
	public Boolean? IncludeEnvironmentRbac { get; set; }

	/// <summary>
	/// RequestAssessmentMailTemplate
	/// </summary>
	public String? RequestAssessmentMailTemplate { get; set; }

	/// <summary>
	/// ContainerName
	/// </summary>
	public String? StorageProvider { get; set; }

	/// <summary>
	/// Gets or sets the name of the user guide folder.
	/// </summary>
	/// <value>
	/// The name of the user guide folder.
	/// </value>
	public String? UserGuideFolderName { get; set; }

	/// <summary>
	/// ContainerName
	/// </summary>
	public String? ContainerName { get; set; }

	/// <summary>
	/// ContainerName
	/// </summary>
	public String? CodeRepositoryFolderName { get; set; }

	/// <summary>
	/// IsZipPasswordEnabled
	/// </summary>
	public Boolean? IsZipPasswordEnabled { get; set; }

	/// <summary>
	/// ARCECDataSyncService
	/// </summary>
	public Guid ARCECDataSyncService { get; set; }

	/// <summary>
	/// ARCECApp
	/// </summary>
	public Guid ARCECApp { get; set; }

	/// <summary>
	/// IsShowCreatedByModifiedByEmailId
	/// </summary>
	public Boolean? IsShowCreatedByModifiedByEmailId { get; set; }

	/// <summary>
	/// ContainerName
	/// </summary>
	public String? AssetVersionArtifactFolderName { get; set; }

	/// <summary>
	/// ValidateHeadersForAPI
	/// </summary>
	public Boolean? ValidateHeadersForAPI { get; set; }

	/// <summary>
	/// QBExternalSystemUId
	/// </summary>
	public String? QBExternalSystemUId { get; set; }

	#endregion

	#region Constructors

	#endregion

	#region Methods

	#endregion
}

