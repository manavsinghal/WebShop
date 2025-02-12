#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion

namespace Accenture.WebShop.Core.Application.DataModels;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents OperationOptions
/// </summary>
public class OperationOptions
{
	#region Properties

	/// <summary>
	/// Datamodel ContinueOnError
	/// </summary>
	public Boolean? ContinueOnError { get; set; }

	#endregion
}

/// <summary>
/// Represents Request class.
/// </summary>
/// <remarks>
/// Represents Request class.
/// </remarks>
public class Request<T> where T : COREDOMAINDATAMODELS.DataModel<T>
{
	#region Properties

	/// <summary>
	/// Gets or sets the email identifier.
	/// </summary>
	/// <value>
	/// The email identifier.
	/// </value>
	public String? EmailId { get; set; }

	/// <summary>
	/// Gets or sets the enabling encryption.
	/// </summary>
	/// <value>
	/// Default set to false.
	/// </value>
	public Boolean IsEncryptionEnabled { get; set; } = SHAREDKERNALLIB.AppSettings.ObfuscatorIsEnabled;

	/// <summary>
	/// Gets or sets the enabling decryption
	/// </summary>
	/// /// <value>
	/// Default set to true
	/// </value>
	public Boolean? IsDecryptionEnabled { get; set; } = false;

	/// <summary>
	/// Gets or sets the CorrelationUId.
	/// </summary>
	/// <value>
	/// The CorrelationUId.
	/// </value>
	public Guid CorrelationUId { get; set; }

	/// <summary>
	/// Gets or sets the RequestedByAppUId.
	/// </summary>
	/// <value>
	/// The RequestedByAppUId.
	/// </value>
	public Guid RequestedByAppUId { get; set; }

	/// <summary>
	/// Gets or sets the RequestedByAppName.
	/// </summary>
	/// <value>
	/// The RequestedByAppName.
	/// </value>
	public String RequestedByAppName { get; set; }

	#endregion

	#region Constructors

	/// <summary>
    /// Initializes a new instance of the <see cref="Request{T}"/> class.
    /// </summary>
	public Request()
	{

	}

	#endregion
}

