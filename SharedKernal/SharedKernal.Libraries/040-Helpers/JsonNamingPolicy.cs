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
/// PascalCaseNamingPolicy
/// </summary>
public class PascalCaseNamingPolicy : JsonNamingPolicy
{
	/// <summary>
	/// Convert Name to PascalCase
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	public override string ConvertName(string name)
	{
		if (string.IsNullOrEmpty(name))
		{
			return name;
		}

		return char.ToUpper(name[0]) + name.Substring(1);
	}
}

