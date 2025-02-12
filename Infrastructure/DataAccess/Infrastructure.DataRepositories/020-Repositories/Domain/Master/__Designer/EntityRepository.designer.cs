#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion  

namespace Accenture.WebShop.Infrastructure.DataRepositories.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents Entity Infrastructure DataRepositories (DOTNET090000).
/// </summary>
/// <remarks>
/// The Entity Infrastructure DataRepositories (DOTNET090000).
/// </remarks>
public partial class MasterRepository : INFRADATAREPO.DataRepositoryBase, COREAPPDREPOINTERFACESDOMAIN.IMasterRepository
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	#endregion

	#region Methods

	/// <summary>
	/// Get Entities
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.GetEntityResponse> GetEntitiesAsync(COREAPPDATAREPOMODELSDOMAIN.GetEntityRequest request)
	{
	    Logger.LogInfo(LogLevel.Information, $"{nameof(GetEntitiesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MasterRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.GetEntityResponse response = new();

		try
		{
			var entities = await this._entity.GetEntitiesAsync(request).ConfigureAwait(false);
			response.Entities = entities;		
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(GetEntitiesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetEntitiesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MasterRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}
	 
	/// <summary>
	/// Merge Entities
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	public async Task<COREAPPDATAREPOMODELSDOMAIN.MergeEntityResponse> MergeEntitiesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeEntityRequest request)
	{
	 	Logger.LogInfo(LogLevel.Information, $"{nameof(MergeEntitiesAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MasterRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		COREAPPDATAREPOMODELSDOMAIN.MergeEntityResponse response = new();

		try
		{
			var mergeResult = await this._entity.MergeEntitiesAsync(request).ConfigureAwait(false);
			response.MergeResult = mergeResult;
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(MergeEntitiesAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeEntitiesAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MasterRepository), this.GetType(), request.CorrelationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.DataRepository));
		}

		return response;
	}

	/// <summary>
	/// Saves MasterRepository.
	/// </summary>
	public async Task SaveEntityAsync()
	{
		Logger.LogInfo($"{nameof(SaveEntityAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(MasterRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		try
		{
			await _entity.SaveAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw await HandleDBException(nameof(SaveEntityAsync), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo($"{nameof(SaveEntityAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(MasterRepository), this.GetType(), SHAREDKERNALLIB.ApplicationTier.DataRepository);
		}
	}


	#endregion

}
