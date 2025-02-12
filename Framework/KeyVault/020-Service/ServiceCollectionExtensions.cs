#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.KeyVault;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// ServiceCollectionExtensions
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// AddKeyVaultService
	/// </summary>
	/// <param name="services"></param>
	/// <param name="configuration"></param>
	/// <returns></returns>
	public static IServiceCollection AddKeyVaultService(this IServiceCollection services, IConfiguration configuration)
	{
		var keyVaultConfig = new KeyVaultConfig();
		
		configuration.Bind("AzureKeyVault", keyVaultConfig);
		
		services.AddSingleton(keyVaultConfig);
		services.AddSingleton<KeyVaultService>();
		
		return services;
	}
}
