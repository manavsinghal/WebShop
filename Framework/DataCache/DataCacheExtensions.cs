#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.DataCache;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// DataCacheExtensions
/// </summary>
public static class DataCacheExtensions
{
	#region Fields
	#endregion

	#region Properties        
	#endregion

	#region Constructors
	#endregion

	#region Methods

	/// <summary>
	/// builder.Services.AddDataCacheServices extension method
	/// </summary>
	/// <param name="services"></param>
	public static void AddDataCacheServices(this IServiceCollection services)
	{
		switch (AppSettings.DataCacheProvider)
		{
			case "InMemoryCacheProvider":
				_ = services.AddDistributedMemoryCache();
				break;
			case "SqlServerCacheProvider":
				SqlServerCacheProvider(services);
				break;
			case "RedisCacheProvider":
				RedisCacheProvider(services);
				break;
			default:
				break;
		}
	}

	/// <summary>
	/// Redises the cache provider.
	/// </summary>
	/// <param name="services">The services.</param>
	private static void RedisCacheProvider(IServiceCollection services)
	{
		_ = services.AddStackExchangeRedisCache(options =>
		{
			options.Configuration = AppSettings.DataCacheRedisConnectionString;
			options.InstanceName = AppSettings.DataCacheRedisInstanceName;
		});
	}

	/// <summary>
	/// SQLs the server cache provider.
	/// </summary>
	/// <param name="services">The services.</param>
	private static void SqlServerCacheProvider(IServiceCollection services)
	{
		_ = services.AddDistributedSqlServerCache(options =>
		{
			options.ConnectionString = AppSettings.DataCacheSqlConnectionString;
			options.SchemaName = AppSettings.DataCacheSqlSchemaName;
			options.TableName = AppSettings.DataCacheSqlTableName;
		});
	}

	#endregion
}

