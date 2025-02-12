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
/// Represents topics.
/// </summary>
/// <remarks>
/// Represents topics.
/// </remarks>
public enum Topic
{
	#region Fields

	/// <summary>
	/// Developer Message
	/// </summary>
	[Description("Developer")]
	Developer,

	/// <summary>
	/// User Messages
	/// </summary>
	[Description("User")]
	User,

	/// <summary>
	/// DeveloperAndUser Messages
	/// </summary>
	[Description("DeveloperAndUser")]
	DeveloperAndUser,

	/// <summary>
	/// All Messages
	/// </summary>
	[Description("All")]
	All,

	/// <summary>
	/// ENS Messages
	/// </summary>
	[Description("ENS")]
	EventAndNotificationService

	#endregion
}


