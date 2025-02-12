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
/// MyKeyVaultSecretManager
/// </summary>
public class MyKeyVaultSecretManager : KeyVaultSecretManager
{
	/// <summary>
	/// Load
	/// </summary>
	/// <param name="secret"></param>
	/// <returns></returns>
	public override Boolean Load(SecretProperties secret)
	{
		return true;
	}

	/// <summary>
	/// GetKey
	/// </summary>
	/// <param name="secret"></param>
	/// <returns></returns>
	public override String GetKey(KeyVaultSecret secret)
	{
		return secret.Name.Replace("--", ConfigurationPath.KeyDelimiter);
	}
}

