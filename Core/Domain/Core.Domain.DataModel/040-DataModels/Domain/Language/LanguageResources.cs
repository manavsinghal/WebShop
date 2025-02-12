#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Core.Domain.DataModels;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents LanguageResources DataModel.
/// </summary>
/// <remarks>
/// The LanguageResources DataModel.
/// </remarks>
public partial class LanguageResources
{
	#region Fields
	#endregion

	#region Properties

	/// <summary>
	/// Gets or sets the culture.
	/// </summary>
	/// <value>
	/// The culture.
	/// </value>
	public String? Culture { get; set; }

	/// <summary>
	/// Gets or sets the literals.
	/// </summary>
	/// <value>
	/// The literals.
	/// </value>
	public Dictionary<String, String>? Literals { get; set; }

	/// <summary>
	/// Message
	/// </summary>
	public String? Message { get; set; }

	#endregion

	#region Constructors
	#endregion

	#region Methods
	#endregion
}

