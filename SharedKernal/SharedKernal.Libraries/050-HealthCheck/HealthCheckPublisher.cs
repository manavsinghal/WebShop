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
public class HealthCheckPublisher : IHealthCheckPublisher
{
	#region Fields

	private readonly ILogger<HealthCheckPublisher> logger;

	#endregion

	#region Properties        
	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="HealthCheckPublisher"/> class.
	/// </summary>
	/// <param name="logger">The logger.</param>
	public HealthCheckPublisher(ILogger<HealthCheckPublisher> logger)
	{
		this.logger = logger;
	}

	#endregion

	#region Methods

	/// <summary>
	/// Publishes the provided <paramref name="report" />.
	/// </summary>
	/// <param name="report">The <see cref="T:Microsoft.Extensions.Diagnostics.HealthChecks.HealthReport" />. The result of executing a set of health checks.</param>
	/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" />.</param>
	/// <returns>
	/// A <see cref="T:System.Threading.Tasks.Task" /> which will complete when publishing is complete.
	/// </returns>
	public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
	{
		switch (report.Status)
		{
			case HealthStatus.Unhealthy:
				foreach (var entry in report.Entries)
				{
					logger.LogError("{entry.Key}: {entry.Value.Status}, {entry.Value.Duration}", entry.Key, entry.Value.Status, entry.Value.Duration);
				}
				break;
			case HealthStatus.Degraded:
				foreach (var entry in report.Entries)
				{
					logger.LogWarning("{entry.Key}: {entry.Value.Status}, {entry.Value.Duration}", entry.Key, entry.Value.Status, entry.Value.Duration);
				}
				break;
			case HealthStatus.Healthy:
				break;
			default:
				break;
		}

		return Task.FromResult(HealthCheckResult.Healthy("Service is healthy"));
	}
	
	#endregion
}
