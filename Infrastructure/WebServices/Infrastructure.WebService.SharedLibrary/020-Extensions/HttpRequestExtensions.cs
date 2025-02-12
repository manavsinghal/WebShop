#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Infrastructure.WebService.SharedLibrary.Extensions;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Provides extension methods for HttpRequest
/// </summary>
public static class HttpRequestExtensions
{
	/// <summary>
	/// Gets the raw string stream from the HttpRequest
	/// </summary>
	/// <param name="request"></param>
	/// <param name="encoding"></param>
	/// <returns></returns>
	public static async Task<String> GetRawBodyStringAsync(this HttpRequest request, Encoding? encoding = null)
	{
		encoding ??= Encoding.UTF8;

		if (request != null)
		{
			using var reader = new StreamReader(request.Body, encoding);
			return await reader.ReadToEndAsync().ConfigureAwait(false);
		}

		return await Task.FromResult(String.Empty).ConfigureAwait(false);
	}
}

