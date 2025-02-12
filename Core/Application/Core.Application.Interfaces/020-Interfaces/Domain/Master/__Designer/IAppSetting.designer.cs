#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion

namespace Accenture.WebShop.Core.Application.Interfaces.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents AppSetting class.
/// </summary>
/// <remarks>
/// The AppSetting class.
/// </remarks>
public partial interface IMaster 
{
	#region Methods

	/// <summary>
	/// Get AppSettings
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetAppSettingResponse> GetAppSettingsAsync(COREAPPDATAMODELSDOMAIN.GetAppSettingRequest request);

	/// <summary>
	/// Merge AppSettings
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeAppSettingResponse> MergeAppSettingsAsync(COREAPPDATAMODELSDOMAIN.MergeAppSettingRequest request);

	/// <summary>
	/// Decrypts AppSettings data
	/// </summary>
	/// <param name="appSettingUId">appSetting id</param>
	/// <returns></returns>
	Task DecryptAppSettingsAsync(Guid appSettingUId);

	#endregion
}
