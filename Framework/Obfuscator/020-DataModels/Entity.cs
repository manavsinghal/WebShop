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
/// Representation of JSON configuration
/// </summary>
public partial class Entity
{
	#region Fields
	#endregion

	#region Properties      

	/// <summary>
	/// Name of entity
	/// </summary>
	public String? Name { get; set; }

	/// <summary>
	/// ClassName of entity
	/// </summary>
	public String? ClassName { get; set; }

	/// <summary>
	/// ParentClassName of entity
	/// </summary>
	public String? ParentClassName { get; set; }

	/// <summary>
	/// List of entity Properties 
	/// </summary>
	public List<EntityProperty>? Properties { get; set; }

	#endregion

	#region Constructors
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion
}

