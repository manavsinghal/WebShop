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
public class CpuHealthCheck : IHealthCheck
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
		try
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				var processorTime = new PerformanceCounter("Processor", "% Processor Time", "_Total");
				_ = processorTime.NextValue();

				if (processorTime.NextValue() > 0)
				{
					return Task.FromResult(HealthCheckResult.Degraded($"CPU usage : {processorTime.NextValue()}"));
				}
				else
				{
					return Task.FromResult(HealthCheckResult.Unhealthy($"CPU usage : {processorTime.NextValue()}"));
				}
			}
		}
		catch (Exception ex)
		{
			return Task.FromResult(new HealthCheckResult(status: HealthStatus.Unhealthy, description: "there is some issue to check cpu health", exception: ex));
		}

		return Task.FromResult(HealthCheckResult.Healthy());
	}

	#endregion

}
