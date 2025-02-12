#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Core.Domain.DataModels.Enumerators;

#region Namespace References

#endregion

/// <summary>
/// Represents ItemState.
/// </summary>
/// <remarks>
/// Represents ItemState.
/// </remarks>
public enum ItemState
{
	/// <summary>
	/// The added
	/// </summary>
	Added,

	/// <summary>
	/// The modified
	/// </summary>
	Modified,

	/// <summary>
	/// The deleted
	/// </summary>
	Deleted,

	/// <summary>
	/// The hard deleted
	/// </summary>
	HardDeleted,

	/// <summary>
	/// The unchanged
	/// </summary>
	Unchanged,

	/// <summary>
	/// The add or update
	/// </summary>
	AddOrUpdate,
}

