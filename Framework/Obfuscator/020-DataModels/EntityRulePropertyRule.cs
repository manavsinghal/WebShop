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
/// Represents JSON EntityMaskRulePropertyRule
/// </summary>
public partial class EntityRulePropertyRule
{
	#region Fields
	#endregion

	#region Properties    

	/// <summary>
	/// MaskPermissionLevel
	/// </summary>
	public PermissionLevel? PermissionLevel { get; set; }

	/// <summary>
	/// MaskPermissionLevel Values
	/// </summary>
	public IEnumerable<String>? Value { get; set; }

	/// <summary>
	/// MaskRuleType
	/// </summary>
	public ObfuscatorRuleType ObfuscatorRuleType { get; set; }

	/// <summary>
	/// DataEncryptionProperties
	/// </summary>
	public DataEncryptionRule? DataEncryptionRule { get; set; }

	/// <summary>
	/// DataMaskingProperties
	/// </summary>
	public DataMaskingRule? DataMaskingRule { get; set; }

	/// <summary>
	/// SecretKeyName
	/// </summary>
	public String? SecretKeyName { get; set; }

	/// <summary>
	/// EncryptionKey
	/// </summary>
	public String? EncryptionKey { get; set; }

	#endregion

	#region Constructors
	#endregion

	#region Methods
	#endregion
}

