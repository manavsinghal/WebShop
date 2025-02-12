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
/// DataEncryptionRule
/// </summary>
public class DataEncryptionRule
{
	#region Fields
	#endregion

	#region Properties      
	
	/// <summary>
	/// RUle file path
	/// </summary>
	public String? FilePath { get; set; }

	/// <summary>
	/// INline Rules
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
