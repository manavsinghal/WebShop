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
/// Represents SqlConnectionHealthCheck
/// </summary>
/// <seealso cref="Microsoft.Extensions.Diagnostics.HealthChecks.IHealthCheck" />
public class SqlConnectionHealthCheck : IHealthCheck
{
	#region Fields

	private const String DefaultTestQuery = "Select 1";

	public String ConnectionString { get; }

	public String TestQuery { get; }

	#endregion

	#region Properties        
	#endregion

	#region Constructors

	public SqlConnectionHealthCheck(String connectionString)
		: this(connectionString, testQuery: DefaultTestQuery)
	{
	}

	public SqlConnectionHealthCheck(String connectionString, String testQuery)
	{
		ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
		TestQuery = testQuery;
	}

	#endregion

	#region Methods

	/// <summary>
	/// Runs the health check, returning the status of the component being checked.
	/// </summary>
	/// <param name="context">A context object associated with the current execution.</param>
	/// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> that can be used to cancel the health check.</param>
	/// <returns>
	/// A <see cref="T:System.Threading.Tasks.Task`1" /> that completes when the health check has finished, yielding the status of the component being checked.
	/// </returns>
	public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
	{
		using (var connection = new System.Data.SqlClient.SqlConnection(ConnectionString))
		{
			try
			{
				await connection.OpenAsync(cancellationToken);

				if (TestQuery != null)
				{
					var command = connection.CreateCommand();
					command.CommandText = TestQuery;

					_ = await command.ExecuteNonQueryAsync(cancellationToken);
				}
			}
			catch (Exception ex)
			{
				return new HealthCheckResult(status: context.Registration.FailureStatus, exception: ex);
			}
		}

		return HealthCheckResult.Healthy();
	}

	#endregion
}
