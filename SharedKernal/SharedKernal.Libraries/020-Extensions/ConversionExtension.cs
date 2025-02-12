#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.SharedKernal.Libraries;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Extensions to flatten list of properties
/// </summary>
public static class ConversionExtensions
{
	#region Field

	#endregion

	#region Properties

	#endregion

	#region Methods

	/// <summary>
	///  Converts Datetime to Long 
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public static Int64 ToInt64(this DateTime value)
	{
		var toInt64 = Convert.ToInt64(value.ToString("yyyyMMddHHmm"));

		return toInt64;
	}
	#endregion
}

