#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.StorageProvider;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// FileUploadResponse
/// </summary>
public class StorageProviderResponse
{
	#region Fields
	#endregion

	#region Properties

	/// <summary>
	/// Gets or sets the FileContent
	/// </summary>
	/// <value>
	/// The FileContent
	/// </value>
	public Stream? FileContent { get; set; }

	/// <summary>
	/// Gets or sets the IsSuccess
	/// </summary>
	/// <value>
	/// The IsSuccess
	/// </value>
	public Boolean IsSuccess { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether this instance is file exist.
	/// </summary>
	/// <value>
	///   <c>true</c> if this instance is file exist; otherwise, <c>false</c>.
	/// </value>
	public Boolean IsFileExist { get; set; }

	/// <summary>
	/// Gets or sets a value indicating file path.
	/// </summary>
	/// <value>
	/// The FilePath.
	/// </value>
	public String? FilePath { get; set; }

	/// <summary>
	/// Gets or sets a value for custom file path
	/// </summary>
	/// <value>
	/// The FilePath.
	/// </value>
	public String? FilePath1 { get; set; }

	/// <summary>
	/// Gets or sets a value indicating file name.
	/// </summary>
	/// <value>
	/// The FileName.
	/// </value>
	public String? FileName { get; set; }

	/// <summary>
	/// Gets or sets a value indicating Error.
	/// </summary>
	/// <value>
	/// The FilePath.
	/// </value>
	public String? Error { get; set; }

	#endregion

	#region Constructors
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion
}


