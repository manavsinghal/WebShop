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
/// Represents LogLevel types
/// </summary>
/// <remarks>
/// Represents LogLevel types.
/// </remarks>
public enum LogLevel
{
	#region Fields

	/// <summary>
	/// Information Message
	/// </summary>
	[Description("Developer")]
	Information,

	/// <summary>
	/// User Messages
	/// </summary>
	[Description("User")]
	Warning,

	/// <summary>
	/// All Messages
	/// </summary>
	[Description("Error")]
	Error

	#endregion
}


