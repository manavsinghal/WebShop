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
/// Represents OrderItemShipment class.
/// </summary>
/// <remarks>
/// The OrderItemShipment class.
/// </remarks>
public partial interface IOrder 
{
	#region Methods

	/// <summary>
	/// Get OrderItemShipments
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetOrderItemShipmentResponse> GetOrderItemShipmentsAsync(COREAPPDATAMODELSDOMAIN.GetOrderItemShipmentRequest request);

	/// <summary>
	/// Merge OrderItemShipments
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeOrderItemShipmentResponse> MergeOrderItemShipmentsAsync(COREAPPDATAMODELSDOMAIN.MergeOrderItemShipmentRequest request);

	/// <summary>
	/// Decrypts OrderItemShipments data
	/// </summary>
	/// <param name="orderItemShipmentUId">orderItemShipment id</param>
	/// <returns></returns>
	Task DecryptOrderItemShipmentsAsync(Guid orderItemShipmentUId);

	#endregion
}
