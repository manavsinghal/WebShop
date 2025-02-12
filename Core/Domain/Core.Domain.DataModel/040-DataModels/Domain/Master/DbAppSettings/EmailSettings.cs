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
/// Email App Settings from DB
/// </summary>
public class EmailSettings
{
	#region Field

	#endregion

	#region Properties

	/// <summary>
	/// ProviderType
	/// </summary>
	public String EmailProviderType { get; set; } = default!;

	/// <summary>
	/// From
	/// </summary>
	public String From { get; set; } = default!;

	/// <summary>
	/// DebugProviderFolderPath
	/// </summary>
	public String DebugProviderFolderPath { get; set; } = default!;

	/// <summary>
	/// OneAssetProvider.Endpoint
	/// </summary>
	public String OneAssetProviderEndpoint { get; set; } = default!;

	/// <summary>
	/// OneAssetProvider.ClientId
	/// </summary>
	public String OneAssetProviderClientId { get; set; } = default!;

	/// <summary>
	/// OneAssetProvider.ClientSecret
	/// </summary>
	public String OneAssetProviderClientSecret { get; set; } = default!;

	/// <summary>
	/// OneAssetProvider.TenantId
	/// </summary>
	public String OneAssetProviderTenantId { get; set; } = default!;

	/// <summary>
	/// OneAssetProvider.Scope
	/// </summary>
	public String OneAssetProviderScope { get; set; } = default!;

	/// <summary>
	/// OneAssetProvider.EmailActionUId
	/// </summary>
	public String OneAssetProviderEmailActionUId { get; set; } = default!;

	/// <summary>
	/// OneAssetProviderCreatedByApp
	/// </summary>
	public String OneAssetProviderCreatedByApp { get; set; } = default!;

	/// <summary>
	/// OneAssetProvider.EncodeBody
	/// </summary>
	public Boolean? OneAssetProviderEncodeBody { get; set; }

	/// <summary>
	/// AllowedEmailDomainsCsv
	/// </summary>
	public String AllowedEmailDomainsCsv { get; set; } = default!;

	/// <summary>
	/// ReplacementEnvironmentCsv
	/// </summary>
	public String ReplacementEnvironmentCsv { get; set; } = default!;

	/// <summary>
	/// ReplacementToEmailId
	/// </summary>
	public String ReplacementToEmailId { get; set; } = default!;

	/// <summary>
	/// ReplacementCcEmailId
	/// </summary>
	public String ReplacementCcEmailId { get; set; } = default!;

	/// <summary>
	/// ReplacementBccEmailId
	/// </summary>
	public String ReplacementBccEmailId { get; set; } = default!;

	/// <summary>
	/// IsExtendedFailedLoggingEnabled
	/// </summary>
	public Boolean? IsExtendedFailedLoggingEnabled { get; set; }

	#endregion

	#region Constructors

	#endregion

	#region Methods

	#endregion
}

