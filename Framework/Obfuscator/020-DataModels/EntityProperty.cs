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
/// Represents Entity Property in JSON configuration
/// </summary>
public partial class EntityProperty
{
	#region Fields
	#endregion

	#region Properties

	/// <summary>
	/// Entity Property Name
	/// </summary>
	public String? Name { get; set; }

	/// <summary>
	/// Obfuscator Rule Type
	/// </summary>
	public ObfuscatorRuleType ObfuscatorRuleType { get; set; }

	/// <summary>
	/// Data Encryption Rule
	/// </summary>
	public EncryptionRule? EncryptionRule { get; set; }

	/// <summary>
	/// Data Masking Rule
	/// </summary>
	public MaskingRule? MaskingRule { get; set; }

	/// <summary>
	/// List of Visibility rules
	/// </summary>
	public List<VisibilityRule>? VisibilityRules { get; set; }

	#endregion

	#region Constructors
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion
}

