#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion  

namespace Accenture.WebShop.Core.Application.DataRepositories.Interfaces.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents IOrderRepository Core Application DataRepositories Interfaces (DOTNET090000).
/// </summary>
/// <remarks>
/// The IOrderRepository Core Application DataRepositories Interfaces (DOTNET090000).
/// </remarks>
public partial interface IOrderRepository : COREAPPDREPOINTERFACES.IRepository
{
	#region Properties

	#endregion

	#region Methods

	/// <summary>
	/// Get Orders
	/// </summary>
	/// <param name="request">request</param>
	/// <returns></returns>
	Task<COREAPPDATAREPOMODELSDOMAIN.GetOrderResponse> GetOrdersAsync(COREAPPDATAREPOMODELSDOMAIN.GetOrderRequest request);

	/// <summary>
	/// Merge Orders
	/// </summary>
	/// <param name="request">request</param>
	/// <returns></returns>
	Task<COREAPPDATAREPOMODELSDOMAIN.MergeOrderResponse> MergeOrdersAsync(COREAPPDATAREPOMODELSDOMAIN.MergeOrderRequest request);

	/// <summary>
	/// Save Orders
	/// </summary>
	/// <returns></returns>
	Task SaveOrderAsync();

	#endregion

}
