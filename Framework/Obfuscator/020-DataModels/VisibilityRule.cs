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
/// Represents JSON VisibilityRule
/// </summary>
public partial class VisibilityRule
{
	#region Fields
	#endregion

	#region Properties    

	/// <summary>
	/// PermissionLevel
	/// </summary>
	public PermissionLevel? PermissionLevel { get; set; }

	/// <summary>
	/// PermissionLevel Values
	/// </summary>
	public IEnumerable<String>? Value { get; set; }

	/// <summary>
	/// RuleType
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

	#endregion

	#region Constructors
	#endregion

	#region Methods
	#endregion
}

