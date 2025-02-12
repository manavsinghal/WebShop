#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion


namespace RateLimiting.RateLimiter;

/// <summary>
/// MyRateLimitOptions
/// </summary>
public class MyRateLimitOptions
{
	/// <summary>
	/// Gets or Sets the MyRateLimit.
	/// </summary>
	public const String MyRateLimit = "MyRateLimit";

	/// <summary>
	/// Gets or Sets the PermitLimit.
	/// </summary>	
	public Int32 PermitLimit { get; set; } = 10;

	/// <summary>
	/// Gets or Sets the SlidingPermitLimit.
	/// </summary>
	public Int32 SlidingPermitLimit { get; set; } = 100;

	/// <summary>
	/// Gets or Sets the Window.
	/// </summary>
	public Int32 Window { get; set; } = 10;

	/// <summary>
	/// Gets or Sets the ReplenishmentPeriod.
	/// </summary>
	public Int32 ReplenishmentPeriod { get; set; } = 2;

	/// <summary>
	/// Gets or Sets the QueueLimit.
	/// </summary>
	public Int32 QueueLimit { get; set; } = 100;

	/// <summary>
	/// Gets or Sets the SegmentsPerWindow.
	/// </summary>
	public Int32 SegmentsPerWindow { get; set; } = 8;

	/// <summary>
	/// Gets or Sets the TokenLimit.
	/// </summary>
	public Int32 TokenLimit { get; set; } = 10;

	/// <summary>
	/// Gets or Sets the TokenLimit2.
	/// </summary>
	public Int32 TokenLimit2 { get; set; } = 20;

	/// <summary>
	/// Gets or Sets the TokensPerPeriod.
	/// </summary>
	public Int32 TokensPerPeriod { get; set; } = 4;

	/// <summary>
	/// Gets or Sets the AutoReplenishment.
	/// </summary>
	public Boolean? AutoReplenishment { get; set; } = false;
}

