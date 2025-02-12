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
/// EmailCommunicationClient
/// </summary>
/// <seealso cref="Accenture.ServiceComposerCore.StorageProvider.IStorageProvider" />
public class StorageProvider : IStorageProvider
{
	#region Fields

	/// <summary>
	/// Storage Provider Settings
	/// </summary>
	private StorageProviderSettings? _storageProviderSettings;

	/// <summary>
	/// StorageProvider
	/// </summary>
	private IStorageProvider? _storageProvider;

	#endregion

	#region Properties        
	#endregion

	#region Constructors

	#endregion

	#region Public Methods

	/// <summary>
	/// GetStorageProvider
	/// </summary>
	/// <param name="storageProviderSettings"></param>
	/// <returns>IStorageProvider</returns>
	public IStorageProvider GetStorageProvider(StorageProviderSettings storageProviderSettings)
	{
		this._storageProviderSettings = storageProviderSettings;

		switch (this._storageProviderSettings.StorageProviderType)
		{
			case StorageProviderType.Azure:
				this._storageProvider = new AzureBlobStorage();
				break;
			case StorageProviderType.FileShare:
				this._storageProvider = new AzureFileShare();
				break;
			case StorageProviderType.Local:
				this._storageProvider = new LocalFile();
				break;
			default:
				break;
		}

		return this._storageProvider!;
	}

	/// <summary>
	/// SendEmailAsync
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	public Task<StorageProviderResponse> UploadFile(StorageProviderRequest request)
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// DownloadFile
	/// </summary>
	/// <param name="request"></param>
	/// <returns>GetFileUploadResponse</returns>
	public Task<StorageProviderResponse> DownloadFile(StorageProviderRequest request)
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// DeleteFile
	/// </summary>
	/// <param name="request"></param>
	/// <returns>GetFileUploadResponse</returns>
	public Task<StorageProviderResponse> DeleteFile(StorageProviderRequest request)
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// ApplyZipPassword
	/// </summary>
	/// <param name="streamContent"></param>
	/// <param name="zipPassword"></param>
	/// <returns></returns>
	public static Stream ApplyZipPassword(Stream streamContent, String zipPassword)
	{
		using var memoryStream = new MemoryStream();
		streamContent.Seek(0, SeekOrigin.Begin);
		streamContent.CopyTo(memoryStream);
		memoryStream.Seek(0, SeekOrigin.Begin);

		var isZipFile = IsZipFile(memoryStream);

		if (isZipFile)
		{
			// Prepare a new ZIP output stream with password protection
			using var zipStream = new MemoryStream();
			using var zipOutputStream = new ZipOutputStream(zipStream);

			zipOutputStream.Password = zipPassword;
			zipOutputStream.SetLevel(9);

			memoryStream.Seek(0, SeekOrigin.Begin);
			// Rewind and extract each file from the original ZIP stream
			using (var zipInputStream = new ZipInputStream(memoryStream))
			{
				ZipEntry entry;

				while ((entry = zipInputStream.GetNextEntry()) != null)
				{
					// Create a memory stream to hold the file content
					using var entryStream = new MemoryStream();
					zipInputStream.CopyTo(entryStream);
					// Create a new zip entry in the new password-protected archive
					entryStream.Seek(0, SeekOrigin.Begin);
					var newEntry = new ZipEntry(entry.Name);
					zipOutputStream.PutNextEntry(newEntry);
					// Write the entry content into the new zip
					entryStream.CopyTo(zipOutputStream);
					zipOutputStream.CloseEntry();
				}
			}

			zipOutputStream.Finish();
			//zipOutputStream.Close();

			zipStream.Seek(0, SeekOrigin.Begin);
			return new MemoryStream(zipStream.ToArray());
		}

		streamContent.Seek(0, SeekOrigin.Begin);
		return streamContent;

	}

	#endregion

	#region Private Methods

	private static Boolean IsZipFile(Stream stream)
	{
		try
		{
			var zip = new ZipFile(stream);
			//If it's able to read the stream then it's a Zip File.
			return true;
		}

		catch(Exception)
		{
			//Any exception thrown it's not a Zip File, return false.
			return false;
		}
	}
	#endregion
}

