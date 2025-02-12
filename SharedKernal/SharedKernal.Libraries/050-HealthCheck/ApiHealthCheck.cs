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
public class ApiHealthCheck : IHealthCheck
{
	#region Fields
	
	//private const String url = AppSettings.TestApiHealthURL;//"http://localhost:5076/v1/Domain/MasterLists/RowStatuses";  //  /v1/Domain/MasterLists/RowStatuses";

	private readonly HttpClient _httpClient;
	#endregion

	#region Properties        
	#endregion

	#region Constructors
	public ApiHealthCheck(HttpClient httpClient)
	{
		_httpClient=httpClient;
	}
	#endregion

	#region Methods
	public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
	{
		try
		{
			var response = await _httpClient.GetAsync(AppSettings.TestApiHealthURL);

			if (response != null && response.IsSuccessStatusCode)
			{
				return await Task.FromResult(new HealthCheckResult(status: HealthStatus.Healthy, description: "The Api is up & running")).ConfigureAwait(false);
			}
			else
			{
				return await Task.FromResult(new HealthCheckResult(status: HealthStatus.Unhealthy, description: "The Api is down")).ConfigureAwait(false);
			}
		}
		catch(Exception ex)
		{
			return await Task.FromResult(new HealthCheckResult(status: HealthStatus.Unhealthy, description: "The Api is down or not working", exception: ex)).ConfigureAwait(false); 
		}
	}

	#endregion

}
