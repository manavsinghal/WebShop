#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion

namespace Accenture.WebShop.Core.Application.Interfaces.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents Order class.
/// </summary>
/// <remarks>
/// The Order class.
/// </remarks>
public partial interface IOrder 
{
	#region Methods

	/// <summary>
	/// Get Orders
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetOrderResponse> GetOrdersAsync(COREAPPDATAMODELSDOMAIN.GetOrderRequest request);

	/// <summary>
	/// Merge Orders
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeOrderResponse> MergeOrdersAsync(COREAPPDATAMODELSDOMAIN.MergeOrderRequest request);

	/// <summary>
	/// Decrypts Orders data
	/// </summary>
	/// <param name="orderUId">order id</param>
	/// <returns></returns>
	Task DecryptOrdersAsync(Guid orderUId);

	#endregion
}
