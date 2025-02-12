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

public class LocalFile : IStorageProvider
{
	/// <summary>
	/// Uploads the file in Local.
	/// </summary>
	/// <param name="request">StorageProviderRequest</param>
	/// <returns>GetFileUploadResponse</returns>
	public async Task<StorageProviderResponse> UploadFile(StorageProviderRequest request)
	{
		var response = new StorageProviderResponse();

		try
		{
			foreach (var file in request.File!)
			{
				if (request.AssetVersionArtifactDictionary != null)
				{
					var value = request.AssetVersionArtifactDictionary.Where(x => x.Key == file.FileName!).Select(y => y.Value).Distinct().FirstOrDefault();
					var pos = request.SubFolderName!.IndexOf('/');
					if (value != Guid.Empty && pos > 1)
					{
						var res = request.SubFolderName.Remove(pos);

						request.SubFolderName = res + '/' + value;
					}
					else
					{
						request.SubFolderName = request.SubFolderName + '/' + value;
					}
				}

				var combinedPath = Path.Combine(request.FolderName!, request.SubFolderName!);

				var currentFolderName = Directory.GetCurrentDirectory();

				var pathToSave = Path.Combine(currentFolderName, combinedPath);

				var pathIndex = pathToSave.IndexOf("AssetVersionArtifacts");
				
				var pathToSave1 = "";
				if (pathIndex > 0)
				{
					pathToSave1 = pathToSave.Remove(pathIndex);

				}

				if (!Directory.Exists(pathToSave))
				{
					_ = Directory.CreateDirectory(pathToSave);
				}

				if (!String.IsNullOrEmpty(pathToSave1))
				{
					var combinedPath1 = Path.Combine(pathToSave1, request.FolderName!);
					var fullPath = Path.Combine(pathToSave, file.FileName);
					using var stream = new FileStream(fullPath, FileMode.Create);
					file.CopyTo(stream);

					response.FilePath1 = combinedPath1;
					response.IsSuccess = true;
				}
				else
				{
					var fullPath = Path.Combine(pathToSave, file.FileName);
					using var stream = new FileStream(fullPath, FileMode.Create);
					file.CopyTo(stream);

					response.FilePath = pathToSave;
					response.IsSuccess = true;
				}
			}
		}
		catch (Exception ex)
		{
			#region Handle Exception
			response.IsSuccess = false;
			response.Error = ex.Message;

			#endregion
		}
		finally
		{
			#region Cleanup

			#endregion
		}

		return await Task.FromResult(response).ConfigureAwait(false);
	}

	/// <summary>
	/// Download the file from Local.
	/// </summary>
	/// <param name="request">StorageProviderRequest</param>
	/// <returns>GetFileUploadResponse</returns>
	public async Task<StorageProviderResponse> DownloadFile(StorageProviderRequest request)
	{
		var memory = new MemoryStream();
		var response = new StorageProviderResponse();

		try
		{
			if (!String.IsNullOrEmpty(request.FilePath))
			{
				var filepath = request.FilePath.Replace("/", "\\");
				var filePathWithName = Path.Combine(filepath!, request.FileName!);
				using (var stream = new FileStream(filePathWithName, FileMode.Open))
				{
					await stream.CopyToAsync(memory);
				}

				memory.Position = 0;
			}

			response.FileContent = request.IsZipPasswordEnabled ? StorageProvider.ApplyZipPassword(memory, request.ZipPassword.ToString()) : memory;

			response.IsSuccess = true;
		}
		catch (Exception ex)
		{
			#region Handle Exception

			response.IsSuccess = false;
			response.Error = ex.Message;

			#endregion
		}
		finally
		{
			#region Cleanup

			#endregion
		}

		return await Task.FromResult(response).ConfigureAwait(false);
	}

	/// <summary>
	/// Gets Storage Provider.
	/// </summary>
	/// <param name="request">StorageProviderSettings</param>
	/// <returns></returns>
	public IStorageProvider GetStorageProvider(StorageProviderSettings request)
	{
		throw new ArgumentException();
	}

	/// <summary>
	/// Delete the file from local.
	/// </summary>
	/// <param name="request">StorageProviderRequest</param>
	/// <returns>GetFileUploadResponse</returns>
	public Task<StorageProviderResponse> DeleteFile(StorageProviderRequest request)
	{
		var response = new StorageProviderResponse();

		try
		{
			if (!String.IsNullOrEmpty(request.FilePath))
			{
				var filePathWithName = Path.Combine(request.FilePath!, request.FileName!);
				var file = new FileInfo(filePathWithName);

				if (file.Exists)//check file exsit or not  
				{
					file.Delete();
					response.IsSuccess = true;
				}
			}
		}
		catch (Exception ex)
		{
			#region Handle Exception

			response.IsSuccess = false;
			response.Error = ex.Message;

			#endregion
		}
		finally
		{
			#region Cleanup

			#endregion
		}

		return Task.FromResult(response);
	}
}

