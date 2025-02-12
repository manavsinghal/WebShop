#region Copyright (c) 2024 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2024 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion

namespace Accenture.WebShop.MessageHub.Enumerators;

#region Namespace References

#endregion

/// <summary>
/// Represents Operation types
/// </summary>
/// <remarks>
/// Represents Operation types.
/// </remarks>
public enum Operation
{
	#region Fields

	/// <summary>
	/// Begin Operation
	/// </summary>
	[Description("Begin")]
	Begin,

	/// <summary>
	/// End Operation
	/// </summary>
	[Description("End")]
	End,

	/// <summary>
	/// Processing Operation
	/// </summary>
	[Description("Processing")]
	Processing,

	/// <summary>
	/// Processed Operation
	/// </summary>
	[Description("Processed")]
	Processed,

	/// <summary>
	/// Unknown Operation
	/// </summary>
	[Description("Unknown")]
	Unknown

	#endregion
}


