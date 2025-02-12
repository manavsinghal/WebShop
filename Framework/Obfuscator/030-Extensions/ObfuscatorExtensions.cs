#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Obfuscator;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// ObfuscatorExtensions
/// </summary>
public static class ObfuscatorExtensions
{
	#region Fields

	/// <summary>
	/// Obfuscator
	/// </summary>
	static readonly Obfuscator? obfuscator;

	#endregion

	#region Properties        
	#endregion

	#region Constructors

	/// <summary>
	/// ctor
	/// </summary>
	static ObfuscatorExtensions()
	{
		var obfuscatorConfig = AppSettings.ObfuscatorSection;

		var isObfuscatorEnabled = obfuscatorConfig.GetValue<Boolean>("IsEnabled");

		if (isObfuscatorEnabled)
		{
			var appSettingsSecretProvider = new AppSettingsSecretProvider();

			var obfuscatorAppSettingSection = obfuscatorConfig.GetSection("AppSettingsSecretProvider");

			var childSettings = obfuscatorAppSettingSection.GetChildren();

			var secretKeys = new Dictionary<String, String>();

			foreach (var childSetting in childSettings)
			{
				if (childSetting.Key == "Name")
				{
					appSettingsSecretProvider.Name = childSetting.Value;
				}				 
				else if (childSetting.Key == "Url")
				{
					appSettingsSecretProvider.Url = childSetting.Value;
				}
				else if (childSetting.Key == "UserAssignedManagedIdentity")
				{
					appSettingsSecretProvider.UserAssignedManagedIdentity = childSetting.Value;
				}
				else
				{
					secretKeys.Add(childSetting.Key, childSetting.Value!);
				}
			}

			if (secretKeys.Any())
			{
				appSettingsSecretProvider.ObfuscatorEncryptionKeys = secretKeys;
			}

			//var azureKeyVaultSecretProvider = new AzureKeyVaultSecretProvider();

			//var obfuscatorAzureKeyVaultSection = obfuscatorConfig.GetSection("AzureKeyVaultSecretProvider");

			//azureKeyVaultSecretProvider.Name = obfuscatorAzureKeyVaultSection.GetValue<String>("Name");
			//azureKeyVaultSecretProvider.Url = obfuscatorAzureKeyVaultSection.GetValue<String>("Url");
			//azureKeyVaultSecretProvider.UserAssignedManagedIdentity = obfuscatorAzureKeyVaultSection.GetValue<String>("UserAssignedManagedIdentity");

			var configFilePath = obfuscatorConfig.GetValue<String>("ConfigFilePath");

			obfuscator = new Obfuscator(configFilePath!,appSettingsSecretProvider);
		}
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Obfuscate Collection of Objects
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="collection"></param>
	/// <returns></returns>
	public static ICollection<T> Obfuscate<T>(this ICollection<T> collection)
	{
		return obfuscator != null ? obfuscator.Obfuscate(collection) : collection;
	}

	/// <summary>
	/// Obfuscate Enumerable of Objects
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="collection"></param>
	/// <returns></returns>
	public static IEnumerable<T> Obfuscate<T>(this IEnumerable<T> collection)
	{
		return obfuscator != null ? obfuscator!.Obfuscate(collection) : collection;
	}

	/// <summary>
	/// Obfuscate Object
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="entity"></param>
	/// <returns></returns>
	public static T Obfuscate<T>(this T entity)
	{
		return obfuscator != null ? obfuscator!.Obfuscate(entity) : entity;
	}

	/// <summary>
	/// DeObfuscate Collection of Objects
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="collection"></param>
	/// <param name="userContext"></param>
	/// <returns></returns>
	public static ICollection<T> DeObfuscate<T>(this ICollection<T> collection, Object userContext)
	{
		return obfuscator != null ? (obfuscator!.DeObfuscate(collection, userContext) ?? collection) : collection;
	}

	/// <summary>
	/// DeObfuscate Enumerable of Objects
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="collection"></param>
	/// <param name="userContext"></param>
	/// <returns></returns>
	public static IEnumerable<T> DeObfuscate<T>(this IEnumerable<T> collection, Object userContext)
	{
		return obfuscator != null ? (obfuscator!.DeObfuscate(collection, userContext) ?? collection) : collection;
	}

	/// <summary>
	/// DeObfuscate Object
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="entity"></param>
	/// <param name="userContext"></param>
	/// <returns></returns>
	public static T DeObfuscate<T>(this T entity, Object userContext)
	{
		return obfuscator != null ? (obfuscator!.DeObfuscate(entity, userContext) ?? entity) : entity;
	}

	#endregion
}
