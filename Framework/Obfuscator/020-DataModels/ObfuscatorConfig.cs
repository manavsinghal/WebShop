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
/// Represents JSON Obfuscator Config
/// </summary>
public class ObfuscatorConfig
{
	#region Field

	#endregion

	#region Properties

	/// <summary>
	/// Prefix added before encrypted value
	/// </summary>
	public String? TokenPrefix { get; set; }

	/// <summary>
	/// Suffix added after encrypted value
	/// </summary>
	public String? TokenSuffix { get; set; }

	/// <summary>
	/// RegEx Rules default 
	/// </summary>
	public String? RegExRuleFilePath { get; set; }

	/// <summary>
	/// Entity Rules
	/// </summary>
	public List<Entity>? Entities { get; set; }

	#endregion

	#region Constructors
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion

}
