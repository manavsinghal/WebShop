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
/// Represents WebShopException method.
/// </summary>
public class WebShopException : Exception
{
	/// <summary>
    /// Initializes a new instance of the <see cref="WebShopException"/> class.
    /// </summary>
	public WebShopException()
	{

	}

	/// <summary>
    /// Initializes a new instance of the <see cref="WebShopException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
	public WebShopException(String message)
		: this(message, null)
	{

	}

	/// <summary>
    /// Initializes a new instance of the <see cref="WebShopException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="exception">The exception.</param>
	public WebShopException(String message, Exception? exception)
		: base(message, exception)
	{

	}
}
