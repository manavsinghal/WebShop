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
/// Represents a RegEx rule used for encryption/masking
/// </summary>
public class RegExRule
{
	#region Fields
	#endregion

	#region Properties      

	/// <summary>
	/// RuleId
	/// </summary>
	public String? RuleUId { get; set; }

	/// <summary>
	/// Rule Name
	/// </summary>
	public String? Name { get; set; }

	/// <summary>
	/// RegEx Pattern
	/// </summary>
	public String? Pattern { get; set; }

	/// <summary>
	/// ReplaceWith
	/// </summary>
	public String? ReplaceWith { get; set; }

	#endregion

	#region Constructors
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion
}
