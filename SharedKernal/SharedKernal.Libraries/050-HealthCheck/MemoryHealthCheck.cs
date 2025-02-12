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
public class MemoryHealthCheck : IHealthCheck
{
	#region Fields
	private readonly IOptionsMonitor<MemoryCheckOptions> _options;
	public string Name => "memory_check";
	#endregion

	#region Properties        
	#endregion

	#region Constructors
	public MemoryHealthCheck(IOptionsMonitor<MemoryCheckOptions> options)
	{
		_options = options;
	}
	#endregion

	#region Methods
	public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
	{

		var options = _options.Get(context.Registration.Name);

		// Include GC information in the reported diagnostics.
		var allocated = GC.GetTotalMemory(forceFullCollection: false);
		var data = new Dictionary<string, object>()
		{
			{ "AllocatedBytes", allocated },
			{ "Gen0Collections", GC.CollectionCount(0) },
			{ "Gen1Collections", GC.CollectionCount(1) },
			{ "Gen2Collections", GC.CollectionCount(2) },
		};
		var status = (allocated < options.Threshold) ? HealthStatus.Healthy : HealthStatus.Unhealthy;

		return Task.FromResult(new HealthCheckResult(
			status,
			description: "Reports degraded status if allocated bytes " +
				$">= {options.Threshold} bytes.",
			exception: null,
			data: data));


		throw new NotImplementedException();
	}

	public class MemoryCheckOptions
	{
		/// <summary>
		/// Threshold
		/// </summary>
		public long Threshold { get; set; } = 1024L * 1024L * 1024L;
	}
	#endregion

}
