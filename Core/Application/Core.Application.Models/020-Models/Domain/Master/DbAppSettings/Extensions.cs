#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Core.Application.Models.DbAppSettings;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead 

#endregion

/// <summary>
/// Extensions
/// </summary>
public static class Extensions
{
	#region Field

	#endregion

	#region Properties

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Type cast AppSettings value
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="appSettings"></param>
	/// <param name="key"></param>
	/// <returns></returns>
	public static T? GetValue<T>(this IEnumerable<COREDOMAINDATAMODELSDOMAIN.AppSetting> appSettings, String key)
	{
		try
		{
			var appSetting = appSettings?.FirstOrDefault(a => a.KeyName.Equals(key, StringComparison.OrdinalIgnoreCase));

			if (appSetting != null)
			{
				return (T?)Convert.ChangeType(appSetting.Value, typeof(T));
			}
		}
		catch (Exception ex)
		{
			var test = ex.Message;
		}

		return default!;
	}

	#endregion
}
