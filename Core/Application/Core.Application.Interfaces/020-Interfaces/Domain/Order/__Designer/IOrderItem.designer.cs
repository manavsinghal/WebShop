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
/// Represents OrderItem class.
/// </summary>
/// <remarks>
/// The OrderItem class.
/// </remarks>
public partial interface IOrder 
{
	#region Methods

	/// <summary>
	/// Get OrderItems
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetOrderItemResponse> GetOrderItemsAsync(COREAPPDATAMODELSDOMAIN.GetOrderItemRequest request);

	/// <summary>
	/// Merge OrderItems
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeOrderItemResponse> MergeOrderItemsAsync(COREAPPDATAMODELSDOMAIN.MergeOrderItemRequest request);

	/// <summary>
	/// Decrypts OrderItems data
	/// </summary>
	/// <param name="orderItemUId">orderItem id</param>
	/// <returns></returns>
	Task DecryptOrderItemsAsync(Guid orderItemUId);

	#endregion
}
