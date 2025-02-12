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
/// Common Service App Settings from DB
/// </summary>
public class CommonSettings
{
	#region Field

	#endregion

	#region Properties

	/// <summary>
	/// Environment
	/// </summary>
	public String Environment { get; set; } = default!;

	/// <summary>
	/// AppName
	/// </summary>
	public String AppName { get; set; } = default!;

	/// <summary>
	/// AppCode
	/// </summary>
	public String AppCode { get; set; } = default!;

	/// <summary>
	/// SupportTeamEmailId
	/// </summary>
	public String SupportTeamEmailId { get; set; } = default!;

	/// <summary>
	/// SupportTeamName
	/// </summary>
	public String SupportTeamName { get; set; } = default!;

	/// <summary>
	/// AssetVersionEditFormUrl
	/// </summary>
	public String AssetVersionEditFormUrl { get; set; } = default!;

	/// <summary>
	/// EnvironmentEditFormUrl
	/// </summary>
	public String EnvironmentEditFormUrl { get; set; } = default!;

	/// <summary>
	/// Subject Suffix
	/// </summary>
	public String ProvisionServiceDecommissionMailTemplateSubjectSuffix { get; set; } = default!;

	/// <summary>
	/// EnvironmentAssetVersionProvisionStatusPageUrl
	/// </summary>
	public String EnvironmentAssetVersionProvisionStatusPageUrl { get; set; } = default!;

	/// <summary>
	/// AssetVersionAssessmentStatusPageUrl
	/// </summary>
	public String AssetVersionAssessmentStatusPageUrl { get; set; } = default!;

	/// <summary>
	/// AssetEditPageUrl
	/// </summary>
	public String AssetEditPageUrl { get; set; } = default!;

	/// <summary>
	/// ProvisionServiceEditFormUrl
	/// </summary>
	public String ProvisionServiceEditFormUrl { get; set; } = default!;

	/// <summary>
	/// AppReadOnlyModeStartOn
	/// </summary>
	public String? AppReadOnlyModeStartOn { get; set; } = default!;

	/// <summary>
	/// AppReadOnlyModeEndOn
	/// </summary>
	public String? AppReadOnlyModeEndOn { get; set; } = default!;

	/// <summary>
	/// AppReadOnlyModeExceptionForUser
	/// </summary>
	public String? AppReadOnlyModeExceptionForUser { get; set; } = default!;

	/// <summary>
	/// IsManagedIdentityEnabled
	/// </summary>
	public Boolean? IsManagedIdentityEnabledForStorageAccount { get; set; }

	/// <summary>
	/// IsManagedIdentityEnabledForServiceBus
	/// </summary>
	public Boolean? IsManagedIdentityEnabledForServiceBus { get; set; }

	/// <summary>
	/// EntityEventConfigData
	/// </summary>
	public String? EntityEventConfigData { get; set; } = default!;

	/// <summary>
	/// ARCECBackendScript
	/// </summary>
	public Guid ARCECBackendScript { get; set; }

	/// <summary>
	/// CacheObjectMappingConfigData
	/// </summary>
	public String? CacheObjectMappingConfigData { get; set; } = default!;

	#endregion

	#region Constructors

	#endregion

	#region Methods

	#endregion
}

