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

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents the HealthCheckPublisher
/// </summary>
/// <seealso cref="Microsoft.Extensions.Diagnostics.HealthChecks.IHealthCheckPublisher" />
public class StorageProviderHealthCheck : IHealthCheck
{
	#region Fields

	private readonly string _connectionString;
	private readonly string _containerName;
	private readonly string _blobName;

	#endregion

	#region Properties        
	#endregion

	#region Constructors

	/// <summary>
	/// StorageProviderHealthCheck
	/// </summary>
	/// <param name="connectionString"></param>
	/// <param name="containerName"></param>
	/// <param name="blobName file path including file name"></param>
	public StorageProviderHealthCheck(String connectionString, String containerName, String blobName)
	{
		_connectionString = connectionString;
		_containerName = containerName;
		_blobName = blobName;
	}
	#endregion

	#region Methods
	public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
	{
		try
		{
			var blobServiceClient = new BlobServiceClient(_connectionString);
			var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

			// Check if the container exists
			if (!await containerClient.ExistsAsync(cancellationToken))
			{
				return HealthCheckResult.Unhealthy("Blob container does not exist.");
			}

			// Try to access the specific blob to verify storage is available
			var blobClient = containerClient.GetBlobClient(_blobName);
			if (blobClient != null)
			{
				var blobExists = await blobClient.ExistsAsync(cancellationToken);

				if (!blobExists)
				{
					return HealthCheckResult.Unhealthy("Blob does not exist.");
				}
				else
				{
					return HealthCheckResult.Healthy($"{_blobName} exist.");
				}
			}
			else
			{
				return HealthCheckResult.Unhealthy("Blob file path does not exist.");
			}
		}
		catch (Exception ex)
		{
			return new HealthCheckResult(status: HealthStatus.Unhealthy, description: "Azure storage having some issue", exception: ex);
		}
	}

	#endregion

}
