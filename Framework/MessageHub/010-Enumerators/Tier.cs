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
/// Represents Macro types
/// </summary>
/// <remarks>
/// Represents Macro types.
/// </remarks>
public enum Tier
{
	#region Fields

	/// <summary>
	/// DAL Tier
	/// </summary>
	[Description("DAL")]
	DAL,

	/// <summary>
	/// DALREPO Tier
	/// </summary>
	[Description("DALREPO")]
	DALREPO,

	/// <summary>
	/// BDL Tier
	/// </summary>
	[Description("BDL")]
	BDL,

	/// <summary>
	/// SERVICE Tier
	/// </summary>
	[Description("SERVICE")]
	SERVICE,

	/// <summary>
	/// VIEWMODEL Tier
	/// </summary>
	[Description("VIEWMODEL")]
	VIEWMODEL,

	/// <summary>
	/// WEBUI Tier
	/// </summary>
	[Description("WEBUI")]
	WEBUI,

	/// <summary>
	/// CONSOLEUI Tier
	/// </summary>
	[Description("CONSOLEUI")]
	CONSOLEUI,

	/// <summary>
	/// Custom Tier
	/// </summary>
	[Description("Custom")]
	Custom,

	#endregion
}


