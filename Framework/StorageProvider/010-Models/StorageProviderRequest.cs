#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.StorageProvider;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// StorageProviderRequest
/// </summary>
public class StorageProviderRequest
{
	#region Fields
	#endregion

	#region Properties
	/// <summary>
	/// File
	/// </summary>
	public IFormFileCollection? File { get; set; }

	/// <summary>
	/// FileName
	/// </summary>
	public String? FileName { get; set; }

	/// <summary>
	/// FilePath
	/// </summary>
	public String? FilePath { get; set; }

	/// <summary>
	/// FolderName
	/// </summary>
	public String? FolderName { get; set; }

	/// <summary>
	/// SubFolderName
	/// </summary>
	public String? SubFolderName { get; set; }

	/// <summary>
	/// ContainerName
	/// </summary>
	public String? ContainerName { get; set; }

	/// <summary>
	/// Content
	/// </summary>
	public Stream? Content { get; set; }

	/// <summary>
	/// IsZipPasswordEnabled
	/// </summary>
	public Boolean IsZipPasswordEnabled { get; set; }

	/// <summary>
	/// ZipPassword
	/// </summary>
	public Int64 ZipPassword { get; set; }

	/// <summary>
	/// IsManagedIdentityEnabled
	/// </summary>
	public Boolean IsManagedIdentityEnabled { get; set; }

	public Dictionary<String, Guid>? AssetVersionArtifactDictionary { get; set; }
 
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion
}

