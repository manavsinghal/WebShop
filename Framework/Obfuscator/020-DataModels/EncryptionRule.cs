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
/// EncryptionRule
/// </summary>
public class EncryptionRule
{
	#region Fields
	#endregion

	#region Properties

	/// <summary>
	/// Secret Manager Name
	/// </summary>
	public String? SecretManagerName { get; set; }

	/// <summary>
	/// Secret Key Name
	/// </summary>
	public String? SecretKeyName { get; set; }

	/// <summary>
	/// Secret Key
	/// </summary>
	public String? SecretKey { get; set; }

	/// <summary>
	/// Rule file path
	/// </summary>
	public String? RuleFilePath { get; set; }

	/// <summary>
	/// Inline Rules
	/// </summary>
	public IEnumerable<RegExRule>? Rules { get; set; }

	#endregion

	#region Constructors
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion
}
