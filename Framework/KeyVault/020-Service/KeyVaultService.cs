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
/// KeyVault
/// </summary>
public class KeyVaultService
{
	#region Fields

	/// <summary>
	/// _secretClient
	/// </summary>
	private readonly SecretClient _secretClient;

	#endregion

	#region Properties        
	#endregion

	#region Constructors

	/// <summary>
	/// KeyVaultService
	/// </summary>
	/// <param name="config"></param>
	public KeyVaultService(KeyVaultConfig config)
	{
		_secretClient = new SecretClient(new Uri(config.VaultUri!), new DefaultAzureCredential());
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// GetSecretAsync
	/// </summary>
	/// <param name="secretName"></param>
	/// <returns></returns>
	public async Task<String> GetSecretAsync(String secretName)
	{
		KeyVaultSecret secret = await _secretClient.GetSecretAsync(secretName);
		return secret.Value;
	}

	#endregion

	#region Private Methods

	#endregion
}

