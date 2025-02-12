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

public class AzureBlobStorage : IStorageProvider
{
	#region Fields

	readonly Char _urlSeparator = '/';

	#endregion

	#region Properties

	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// Uploads the file in BLOB.
	/// </summary>
	/// <param name="request">StorageProviderRequest</param>
	/// <returns>GetFileUploadResponse</returns>
	public async Task<StorageProviderResponse> UploadFile(StorageProviderRequest request)
	{
		var blobResponse = new StorageProviderResponse();

		try
		{
			var blobContainerClient = GetBlobContainerClient(request.ContainerName!, request.IsManagedIdentityEnabled);

			if (request.File != null)
			{
				foreach (var file in request.File!)
				{
					if (request.AssetVersionArtifactDictionary != null)
					{
						var value = request.AssetVersionArtifactDictionary.Where(x => x.Key == file.FileName).Select(y => y.Value).Distinct().FirstOrDefault();
						var pos = request.SubFolderName!.IndexOf('/');
						if (value != Guid.Empty && pos > 1)
						{
							var res = request.SubFolderName.Remove(pos);

							request.SubFolderName = res + _urlSeparator + value;
						}
						else
						{
							request.SubFolderName = request.SubFolderName + _urlSeparator + value;
						}
					}

					var blobPath = !String.IsNullOrEmpty(request.FilePath)
						? request.FilePath + _urlSeparator + file.FileName
						: request.FolderName + _urlSeparator + request.SubFolderName + _urlSeparator + file.FileName;

					var blobClient = blobContainerClient.GetBlobClient(blobPath);

					_ = await blobClient.UploadAsync(file.OpenReadStream(), true);

					blobResponse.IsSuccess = true;
				}
			}
			else
			{
				var blobPath = !String.IsNullOrEmpty(request.FilePath)
						? request.FilePath + _urlSeparator + request.FileName
						: request.FolderName + _urlSeparator + request.SubFolderName + _urlSeparator + request.FileName;

				var blobClient = blobContainerClient.GetBlobClient(blobPath);
				request.Content!.Position = 0;
				_ = await blobClient.UploadAsync(request.Content, true);

				blobResponse.IsSuccess = true;
			}

			blobResponse.FilePath = Convert.ToString(blobContainerClient.Uri)! + _urlSeparator + request.FolderName + _urlSeparator + Convert.ToString(request.SubFolderName)!;

			blobResponse.FilePath1 = Convert.ToString(blobContainerClient.Uri)! + _urlSeparator + request.FolderName;

		}
		catch (Exception ex)
		{
			#region Handle Exception

			blobResponse.IsSuccess = false;
			blobResponse.Error = ex.Message;

			#endregion
		}
		finally
		{
			#region Cleanup

			#endregion
		}

		return blobResponse;
	}

	/// <summary>
	/// Download the file from BLOB.
	/// </summary>
	/// <param name="request">StorageProviderRequest</param>
	/// <returns>GetFileUploadResponse</returns>
	public async Task<StorageProviderResponse> DownloadFile(StorageProviderRequest request)
	{
		var blobResponse = new StorageProviderResponse();

		try
		{
			var bloContainerClient = GetBlobContainerClient(request.ContainerName!, request.IsManagedIdentityEnabled);

			if (request.FilePath != null)
			{
				var blobPath = String.Empty;

				if (request.FilePath!.Contains(request.ContainerName!) && !String.IsNullOrEmpty(request.FileName))
				{
					var splitedfilePath = request.FilePath!.Split(request.ContainerName)[1]!;

					blobPath = String.IsNullOrEmpty(splitedfilePath) ? _urlSeparator.ToString() : splitedfilePath + _urlSeparator + request.FileName!;

				}
				else
				{
					blobPath = !String.IsNullOrEmpty(request!.FileName)
						? request.FilePath + _urlSeparator + request!.FileName
						: await GetLatestBlobName(bloContainerClient, request.FilePath).ConfigureAwait(false);
				}

				if (!String.IsNullOrEmpty(blobPath))
				{
					var docBlob = bloContainerClient.GetBlobClient(blobPath);

					if (await docBlob.ExistsAsync())
					{
						var blobContent = await docBlob.OpenReadAsync().ConfigureAwait(false);

						blobResponse.FileContent = request.IsZipPasswordEnabled ? StorageProvider.ApplyZipPassword(blobContent, request.ZipPassword.ToString()) : blobContent;
						blobResponse.FileName = blobPath;
						blobResponse.IsSuccess = true;
					}
				}
				else
				{
					blobResponse.IsSuccess = false;
					blobResponse.Error = "FilePath does not exist";
				}
			}
			else
			{
				blobResponse.IsSuccess = false;
				blobResponse.Error = "FilePath does not exist";
			}

		}
		catch (Exception ex)
		{
			#region Handle Exception

			blobResponse.IsSuccess = false;
			blobResponse.Error = ex.Message;

			#endregion
		}
		finally
		{
			#region Cleanup

			#endregion
		}

		return blobResponse;
	}

	/// <summary>
	/// Delete the file from BLOB.
	/// </summary>
	/// <param name="request">StorageProviderRequest</param>
	/// <returns>GetFileUploadResponse</returns>
	public async Task<StorageProviderResponse> DeleteFile(StorageProviderRequest request)
	{
		var blobResponse = new StorageProviderResponse();

		try
		{
			var blobContainerClient = GetBlobContainerClient(request.ContainerName!, request.IsManagedIdentityEnabled);

			var blobPath = String.Empty;

			if (request.FilePath!.Contains(request.ContainerName!))
			{
				var splitedfilePath = request!.FilePath!.Split(request.ContainerName)[1]!;
				blobPath = splitedfilePath + _urlSeparator + request!.FileName;
			}
			else
			{
				blobPath = request!.FilePath + _urlSeparator + request!.FileName;
			}

			var docBlob = blobContainerClient.GetBlobClient(blobPath);

			blobResponse.IsSuccess = (Boolean)await docBlob.DeleteIfExistsAsync();
		}
		catch (Exception ex)
		{
			#region Handle Exception

			blobResponse.IsSuccess = false;
			blobResponse.Error = ex.Message;

			#endregion
		}
		finally
		{
			#region Cleanup

			#endregion
		}

		return blobResponse;
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

	#endregion

	#region Private Methods

	/// <summary>
	/// Get BlobServiceClient
	/// </summary>
	/// <returns></returns>
	private static BlobContainerClient GetBlobContainerClient(String containerName, Boolean isManagedIdentityEnabled)
	{
		if (isManagedIdentityEnabled)
		{
			var azureStorageAccount = SHAREDKERNALLIB.AppSettings.AzureStorageAccount;
			var blobEndpoint = String.Format("https://{0}.blob.core.windows.net/{1}", azureStorageAccount, containerName);

			var userAssignedClientId = SHAREDKERNALLIB.AppSettings.ManagedIdentityClientId;

			var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = userAssignedClientId });

			return new BlobContainerClient(new Uri(blobEndpoint), credential);
		}
		else
		{
			var azureStorageConnectionString = SHAREDKERNALLIB.AppSettings.BlobDistributedMutexStorageConnectionString;

			return new BlobContainerClient(azureStorageConnectionString, containerName);
		}
	}

	//private static BlobServiceClient GetBlobServiceClient(Boolean isManagedIdentityEnabled)
	//{
	//	var azureStorageAccount = SHAREDKERNALLIB.AppSettings.AzureStorageAccount;
	//	var blobEndpoint = String.Format("https://{0}.blob.core.windows.net", azureStorageAccount);

	//	BlobServiceClient blobServiceClient;

	//	if (isManagedIdentityEnabled)
	//	{
	//		var userAssignedClientId = SHAREDKERNALLIB.AppSettings.ManagedIdentityClientId;
	//		var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = userAssignedClientId });
	//		blobServiceClient = new BlobServiceClient(new Uri(blobEndpoint), credential);
	//	}
	//	else
	//	{
	//		var azureStorageConnectionString = SHAREDKERNALLIB.AppSettings.BlobDistributedMutexStorageConnectionString;
	//		blobServiceClient = new BlobServiceClient(azureStorageConnectionString);
	//	}

	//	return blobServiceClient;
	//}

	///// <summary>
	///// Create CloudStorageAccount thru WindowsAzure.Storage
	///// </summary>
	///// <returns></returns>
	//private async Task<CloudStorageAccount> GetCloudStorageAccount()
	//{
	//	var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = "<managedIdentityClientId>" });
	//	var tokenResponse = await credential.GetTokenAsync(new Azure.Core.TokenRequestContext(new String[] { "https://storage.azure.com" }));

	//	var tokenCredential = new TokenCredential(tokenResponse.Token);
	//	var storageCredentials = new StorageCredentials(tokenCredential);
	//	var cloudStorageAccount = new CloudStorageAccount(storageCredentials, "<storage-account-name>", endpointSuffix: null, useHttps: true);

	//	return cloudStorageAccount;
	//}

	private static async Task<String> GetLatestBlobName(BlobContainerClient blobContainerClient, String prefix)
	{
		try
		{
			var blobName = String.Empty;

			// Call the listing operation and return pages of the specified size.
			var resultSegment = blobContainerClient.GetBlobsByHierarchyAsync(prefix: prefix, delimiter: "/").AsPages(default, null);

			await foreach (var blobPage in resultSegment)
			{
				foreach (var blobhierarchyItem in blobPage.Values)
				{
					if (blobhierarchyItem.IsPrefix)
					{
						// Call recursively with the prefix to traverse the virtual directory.
						blobName = await GetLatestBlobName(blobContainerClient, blobhierarchyItem.Prefix).ConfigureAwait(false);
					}
					else
					{
						var blobPageValue = blobPage.Values.OrderByDescending(x => x.Blob.Properties.LastModified).FirstOrDefault();

						if (blobPageValue != null)
						{
							blobName = blobPageValue.Blob.Name;
						}
					}
				}
			}

			return blobName;
		}
		catch (RequestFailedException e)
		{
			throw new RequestFailedException($"{nameof(GetLatestBlobName)} Latest Blob Name", e);
		}
	}

	#endregion
} 
