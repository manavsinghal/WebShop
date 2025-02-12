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
public class RadisHealthCheck : IHealthCheck
{
	#region Fields

	#endregion

	#region Properties
	
	#endregion

	#region Constructors

	#endregion

	#region Methods
	public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
	{
		var radisConnection = ConnectionMultiplexer.Connect(AppSettings.DataCacheRedisConnectionString);
		
		try
		{
			if (radisConnection != null)
			{
				var RadisDBConnectionString = radisConnection.GetDatabase();
				RadisDBConnectionString.StringSet("RadisCache", "health");

				var cacheValue = RadisDBConnectionString.StringGet(key: "RadisCache");

				if (String.IsNullOrEmpty(cacheValue))
				{
					return Task.FromResult(HealthCheckResult.Healthy());
				}
				else
				{
					return Task.FromResult(HealthCheckResult.Unhealthy());
				}
			}
			else
			{
				return Task.FromResult(HealthCheckResult.Unhealthy());
			}

		}
		catch (Exception ex)
		{
			return Task.FromResult(new HealthCheckResult(status: HealthStatus.Unhealthy, description: "Radis caching having some issue", exception: ex));
		}
		finally
		{
			radisConnection.Close();
		}
		throw new NotImplementedException();
	}

	#endregion

}
