#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Holds the authenticated user principal
/// for the request along with the
/// access token they used.
/// </summary>
public class JwtPrincipalFeature
{
	#region Properties

	public ClaimsPrincipal Principal { get; }

	public string AccessToken { get; }

	#endregion
	
	#region Public methods

	/// <summary>
	/// JwtPrincipalFeature
	/// </summary>
	/// <param name="principal"></param>
	/// <param name="accessToken"></param>
	public JwtPrincipalFeature(ClaimsPrincipal principal, string accessToken)
	{
		Principal = principal;
		AccessToken = accessToken;
	}

	#endregion
}


