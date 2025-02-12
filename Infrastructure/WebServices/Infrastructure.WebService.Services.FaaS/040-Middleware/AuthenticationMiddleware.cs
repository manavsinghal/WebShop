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

using Microsoft.IdentityModel.Protocols;

#endregion

/// <summary>
/// Represents AuthenticationMiddleware.
/// </summary>
public class AuthenticationMiddleware : IFunctionsWorkerMiddleware
{
	#region Fields

	private readonly JwtSecurityTokenHandler _tokenValidator;

	private readonly TokenValidationParameters _tokenValidationParameters;

	private readonly ConfigurationManager<OpenIdConnectConfiguration> _configurationManager;

	#endregion

	#region Constructors

	/// <summary>
	/// AuthenticationMiddleware
	/// </summary>
	/// <param name="configuration"></param>
	public AuthenticationMiddleware(IConfiguration configuration)
	{
		var authority = configuration["AzureAD:Authority"];
		var audience = configuration["AzureAD:Audience"];

		_tokenValidator = new JwtSecurityTokenHandler();

		_tokenValidationParameters = new TokenValidationParameters
		{
			ValidAudience = audience
		};

		_configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>($"{authority}/.well-known/openid-configuration", new OpenIdConnectConfigurationRetriever());
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Invoke
	/// </summary>
	/// <param name="context"></param>
	/// <param name="next"></param>
	/// <returns></returns>
	public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
	{
		if (!TryGetTokenFromHeaders(context, out var token))
		{
			// Unable to get token from headers
			context.SetHttpResponseStatusCode(HttpStatusCode.Unauthorized);
			return;
		}

		if (!_tokenValidator.CanReadToken(token))
		{
			// Token is malformed
			context.SetHttpResponseStatusCode(HttpStatusCode.Unauthorized);
			return;
		}

		// Get OpenID Connect metadata
		var validationParameters = _tokenValidationParameters.Clone();

		try
		{
			var openIdConfig = await _configurationManager.GetConfigurationAsync(default);
			validationParameters.ValidIssuer = openIdConfig.Issuer;
			validationParameters.IssuerSigningKeys = openIdConfig.SigningKeys;

			// Validate token
			var principal = _tokenValidator.ValidateToken(token, validationParameters, out _);

			if (string.IsNullOrEmpty(token))
			{
				// Handle the case where the accessToken is null or empty
				throw new ArgumentNullException(nameof(token), "Access token cannot be null or empty.");
			}
			else
			{
				// Set principal + token in Features collection
				// They can be accessed from here later in the call chain
				context.Features.Set(new JwtPrincipalFeature(principal, token));
			}

			await next(context);
		}
		catch
		{
			// Token is not valid (expired etc.)
			context.SetHttpResponseStatusCode(HttpStatusCode.Unauthorized);
			return;
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// TryGetTokenFromHeaders
	/// </summary>
	/// <param name="context"></param>
	/// <param name="token"></param>
	/// <returns></returns>
	private static bool TryGetTokenFromHeaders(FunctionContext context, out string? token)
	{
		token = null;

		// HTTP headers are in the binding context as a JSON object
		// The first checks ensure that we have the JSON string
		if (!context.BindingContext.BindingData.TryGetValue("Headers", out var headersObj))
		{
			return false;
		}

		if (headersObj is not string headersStr)
		{
			return false;
		}

		// Deserialize headers from JSON
		var headers = JsonSerializer.Deserialize<Dictionary<string, string>>(headersStr);

		if (headers == null)
		{
			return false;
		}

		var normalizedKeyHeaders = headers.ToDictionary(h => h.Key.ToLowerInvariant(), h => h.Value);

		if (!normalizedKeyHeaders.TryGetValue("authorization", out var authHeaderValue))
		{
			// No Authorization header present
			return false;
		}

		if (!authHeaderValue.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
		{
			// Scheme is not Bearer
			return false;
		}

		token = authHeaderValue.Substring("Bearer ".Length).Trim();

		return true;
	}

	#endregion
}


