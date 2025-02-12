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
/// IStorageProvider interface
/// </summary>
public interface IStorageProvider
{
	/// <summary>
	/// GetStorageProvider
	/// </summary>
	/// <param name="request"></param>
	/// <param name="request"></param>
	/// <returns></returns>
	IStorageProvider GetStorageProvider(StorageProviderSettings request);

	/// <summary>
	/// FileUpload
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	Task<StorageProviderResponse> UploadFile(StorageProviderRequest request);

	/// <summary>
	/// FileDownload
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	Task<StorageProviderResponse> DownloadFile(StorageProviderRequest request);

	/// <summary>
	/// FileDelete
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	Task<StorageProviderResponse> DeleteFile(StorageProviderRequest request);
}

