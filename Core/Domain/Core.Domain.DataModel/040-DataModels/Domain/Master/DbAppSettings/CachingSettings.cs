#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Core.Domain.DataModels.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead 

#endregion

/// <summary>
/// Caching Service App Settings from DB
/// </summary>
public class CachingSettings
{
	#region Field

	#endregion

	#region Properties

	/// <summary>
	/// Gets or sets the cache absolute expiration in minutes.
	/// </summary>
	/// <value>
	/// The cache absolute expiration in minutes.
	/// </value>
	public Int32 AbsoluteExpirationInMinutes { get; set; } = default!;

	/// <summary>
	/// Gets or sets the cache sliding expiration in minutes.
	/// </summary>
	/// <value>
	/// The cache sliding expiration in minutes.
	/// </value>
	public Int32 SlidingExpirationInMinutes { get; set; } = default!;

	/// <summary>
	/// Gets or sets a value indicating whether this instance is enabled.
	/// </summary>
	/// <value>
	///   <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
	/// </value>
	public Boolean IsEnabled { get; set; } = default!;

	#endregion

	#region Constructors

	#endregion

	#region Methods

	#endregion

}

