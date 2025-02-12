#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Core.Application.DataEntities.Interfaces;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents IEntity Interface.
/// </summary>
/// <remarks>
/// Represents  IEntity Interface.
/// </remarks>
public interface IEntity<T> where T : class
{
	#region Properties

	#endregion

	#region Methods

	/// <summary>
	/// Adds the asynchronous.
	/// </summary>
	/// <param name="entity">The entity.</param>
	/// <returns></returns>
	Task AddAsync(T entity);

	/// <summary>
	/// Updates the asynchronous.
	/// </summary>
	/// <param name="entity">The entity.</param>
	/// <returns></returns>
	Task UpdateAsync(T entity);

	/// <summary>
	/// Gets Entity.
	/// </summary>
	/// <returns></returns>
	IQueryable<T> AsIQuerable();

	/// <summary>
	/// Merges the asynchronous.
	/// </summary>
	/// <param name="entity">The entity.</param>
	/// <returns></returns>
	Task<COREDOMAINDATAMODELS.MergeResult> MergeAsync(T entity);

	/// <summary>
	/// Saves the asynchronous.
	/// </summary>
	/// <returns></returns>
	Task SaveAsync();

	#endregion
}

