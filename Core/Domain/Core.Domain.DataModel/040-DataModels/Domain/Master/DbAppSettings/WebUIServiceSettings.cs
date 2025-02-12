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
/// WebUIServiceSettings
/// </summary>
public class WebUIServiceSettings
{
	#region Field

	#endregion

	#region Properties

	/// <summary>
	/// Represents MaintenanceEndDate property.
	/// </summary>
	/// <value>
	/// The MaintenanceEndDate.
	/// </value>
	public DateTime? MaintenanceEndDate { get; set; }

	/// <summary>
	/// Represents MaintenanceExceptionForUser property.
	/// </summary>
	/// <value>
	/// The MaintenanceExceptionForUser.
	/// </value>
	public String? MaintenanceExceptionForUser { get; set; }

	/// <summary>
	/// Represents IsAssetVersionAssessmentFeatureEnabled property.
	/// </summary>
	/// <value>
	/// The IsAssetVersionAssessmentFeatureEnabled.
	/// </value>
	public Boolean? IsAssetVersionAssessmentFeatureEnabled { get; set; }

	/// <summary>
	/// Represents IsAssetVersionQuestionnaireFeatureEnabled property.
	/// </summary>
	/// <value>
	/// The IsAssetVersionQuestionnaireFeatureEnabled.
	/// </value>
	public Boolean? IsAssetVersionQuestionnaireFeatureEnabled { get; set; }

	/// <summary>
	/// Represents IsAssetVersionAssessmentAutoLoadFeatureEnabled property.
	/// </summary>
	/// <value>
	/// The IsAssetVersionAssessmentAutoLoadFeatureEnabled.
	/// </value>
	public Boolean? IsAssetVersionAssessmentAutoLoadFeatureEnabled { get; set; }

	/// <summary>
	/// Represents IsAutoAssignQuestionnaireOnLoadFeatureEnabled property.
	/// </summary>
	/// <value>
	/// The IsAutoAssignQuestionnaireOnLoadFeatureEnabled.
	/// </value>
	public Boolean? IsAutoAssignQuestionnaireOnLoadFeatureEnabled { get; set; }

	/// <summary>
	/// Represents IsAssetVersionComponentDependencyFeatureEnabled property.
	/// </summary>
	/// <value>
	/// The IsAssetVersionComponentDependencyFeatureEnabled.
	/// </value>
	public Boolean? IsAssetVersionComponentDependencyFeatureEnabled { get; set; }

	/// <summary>
	/// Represents IsMultipleTabLogoutFeatureEnabled property.
	/// </summary>
	/// <value>
	/// The IsMultipleTabLogoutFeatureEnabled.
	/// </value>
	public Boolean? IsMultipleTabLogoutFeatureEnabled { get; set; }

	/// <summary>
	/// Represents IsAllowReprocessingOfEnvironmentAssetVersion property.
	/// </summary>
	/// <value>
	/// The IsAllowReprocessingOfEnvironmentAssetVersion.
	/// </value>
	public Boolean? IsAllowReprocessingOfEnvironmentAssetVersion { get; set; }

	#endregion

	#region Constructors

	#endregion

	#region Methods

	#endregion
}

