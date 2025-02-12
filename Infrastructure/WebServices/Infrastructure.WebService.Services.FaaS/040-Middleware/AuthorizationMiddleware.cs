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
/// AuthorizationMiddleware
/// </summary>
public class AuthorizationMiddleware : IFunctionsWorkerMiddleware
{
	#region Fields

	private const string ScopeClaimType = "http://schemas.microsoft.com/identity/claims/scope";

	#endregion

	#region Constructors

	/// <summary>
	/// Invoke
	/// </summary>
	/// <param name="context"></param>
	/// <param name="next"></param>
	/// <returns></returns>
	public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
	{
		var principalFeature = context.Features.Get<JwtPrincipalFeature>();

		if (principalFeature != null)
		{
			if (!AuthorizePrincipal(context, principalFeature.Principal))
			{
				context.SetHttpResponseStatusCode(HttpStatusCode.Forbidden);
				return;
			}
		}
		await next(context);
	}

	#endregion

	#region Fields

	/// <summary>
	/// AuthorizePrincipal
	/// </summary>
	/// <param name="context"></param>
	/// <param name="principal"></param>
	/// <returns></returns>
	private static bool AuthorizePrincipal(FunctionContext context, ClaimsPrincipal principal)
	{
		// This authorization implementation was made
		// for Azure AD. Your identity provider might differ.

		if (principal.HasClaim(c => c.Type == ScopeClaimType))
		{
			// Request made with delegated permissions, check scopes and user roles
			return AuthorizeDelegatedPermissions(context, principal);
		}

		// Request made with application permissions, check app roles
		return AuthorizeApplicationPermissions(context, principal);
	}

	/// <summary>
	/// AuthorizeDelegatedPermissions
	/// </summary>
	/// <param name="context"></param>
	/// <param name="principal"></param>
	/// <returns></returns>
	private static bool AuthorizeDelegatedPermissions(FunctionContext context, ClaimsPrincipal principal)
	{
		var targetMethod = context.GetTargetFunctionMethod();

		var (acceptedScopes, acceptedUserRoles) = GetAcceptedScopesAndUserRoles(targetMethod);

		var userRoles = principal.FindAll(ClaimTypes.Role);
		var userHasAcceptedRole = userRoles.Any(ur => acceptedUserRoles.Contains(ur.Value));

		// Scopes are stored in a single claim, space-separated
		var callerScopes = (principal.FindFirst(ScopeClaimType)?.Value ?? "")
							.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		var callerHasAcceptedScope = callerScopes.Any(cs => acceptedScopes.Contains(cs));

		// This app requires both a scope and user role
		// when called with scopes, so we check both
		return userHasAcceptedRole && callerHasAcceptedScope;
	}

	/// <summary>
	/// AuthorizeApplicationPermissions
	/// </summary>
	/// <param name="context"></param>
	/// <param name="principal"></param>
	/// <returns></returns>
	private static bool AuthorizeApplicationPermissions(FunctionContext context, ClaimsPrincipal principal)
	{
		var targetMethod = context.GetTargetFunctionMethod();

		var acceptedAppRoles = GetAcceptedAppRoles(targetMethod);
		var appRoles = principal.FindAll(ClaimTypes.Role);
		var appHasAcceptedRole = appRoles.Any(ur => acceptedAppRoles.Contains(ur.Value));

		return appHasAcceptedRole;
	}

	/// <summary>
	/// GetAcceptedScopesAndUserRoles
	/// </summary>
	/// <param name="targetMethod"></param>
	/// <returns></returns>
	private static (List<string> scopes, List<string> userRoles) GetAcceptedScopesAndUserRoles(MethodInfo targetMethod)
	{
		var attributes = GetCustomAttributesOnClassAndMethod<AuthorizeAttribute>(targetMethod);

		// If scopes A and B are allowed at class level,
		// and scope A is allowed at method level,
		// then only scope A can be allowed.
		// This finds those common scopes and
		// user roles on the attributes.
		var scopes = attributes.Skip(1).Select(a => a.Scopes).Aggregate(attributes.FirstOrDefault()?.Scopes ?? Enumerable.Empty<string>(), (result, acceptedScopes) =>
			{
				return result.Intersect(acceptedScopes);
			}).ToList();

		var userRoles = attributes.Skip(1).Select(a => a.UserRoles).Aggregate(attributes.FirstOrDefault()?.UserRoles ?? Enumerable.Empty<string>(), (result, acceptedRoles) =>
			{
				return result.Intersect(acceptedRoles);
			}).ToList();

		return (scopes, userRoles);
	}

	/// <summary>
	/// GetAcceptedAppRoles
	/// </summary>
	/// <param name="targetMethod"></param>
	/// <returns></returns>
	private static List<string> GetAcceptedAppRoles(MethodInfo targetMethod)
	{
		var attributes = GetCustomAttributesOnClassAndMethod<AuthorizeAttribute>(targetMethod);

		// Same as above for scopes and user roles,
		// only allow app roles that are common in
		// class and method level attributes.
		return attributes.Skip(1).Select(a => a.AppRoles).Aggregate(attributes.FirstOrDefault()?.UserRoles ?? Enumerable.Empty<string>(), (result, acceptedRoles) =>
				{
					return result.Intersect(acceptedRoles);
				}).ToList();
	}

	/// <summary>
	/// GetCustomAttributesOnClassAndMethod
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="targetMethod"></param>
	/// <returns></returns>
	private static List<T> GetCustomAttributesOnClassAndMethod<T>(MethodInfo targetMethod) where T : Attribute
	{
		var methodAttributes = targetMethod.GetCustomAttributes<T>();
		var classAttributes = targetMethod.DeclaringType?.GetCustomAttributes<T>() ?? Enumerable.Empty<T>();

		return methodAttributes.Concat(classAttributes).ToList();
	}

	#endregion
}

