#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Obfuscator;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Representation of AppSettingsSecretProvider
/// </summary>
public partial class AppSettingsSecretProvider
{
	#region Fields
	#endregion

	#region Properties      

	/// <summary>
	/// Azure SecretManager Name
	/// </summary>
	public String? Name { get; set; }

	/// <summary>
	/// Encryption Keys
	/// </summary>
	public Dictionary<String, String>? ObfuscatorEncryptionKeys { get; set; }

	/// <summary>
	/// Azure key vault Url
	/// </summary>
	public String? Url { get; set; }

	/// <summary>
	/// Azure key vault user assigned managed identity
	/// </summary>
	public String? UserAssignedManagedIdentity { get; set; }

	#endregion

	#region Constructors
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion
}

