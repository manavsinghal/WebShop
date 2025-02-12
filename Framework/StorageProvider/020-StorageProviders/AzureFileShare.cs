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

public class AzureFileShare : IStorageProvider
{
	/// <summary>
	/// Uploads the file in fileshare.
	/// </summary>
	/// <param name="request">StorageProviderRequest</param>
	/// <returns>GetFileUploadResponse</returns>
	public Task<StorageProviderResponse> UploadFile(StorageProviderRequest request)
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// Download the file from fileshare.
	/// </summary>
	/// <param name="request">StorageProviderRequest</param>
	/// <returns>GetFileUploadResponse</returns>
	public Task<StorageProviderResponse> DownloadFile(StorageProviderRequest request)
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// Gets Storage Provider.
	/// </summary>
	/// <param name="request">StorageProviderSettings</param>
	/// <returns>IStorageProvider</returns>
	public IStorageProvider GetStorageProvider(StorageProviderSettings request)
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// Delete the file from fileshare.
	/// </summary>
	/// <param name="request">StorageProviderRequest</param>
	/// <returns>GetFileUploadResponse</returns>
	public Task<StorageProviderResponse> DeleteFile(StorageProviderRequest request)
	{
		throw new NotImplementedException();
	}
}

