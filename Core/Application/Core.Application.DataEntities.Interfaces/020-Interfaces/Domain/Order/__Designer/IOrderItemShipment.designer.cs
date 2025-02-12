#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
 
#endregion

namespace Accenture.WebShop.Core.Application.DataEntities.Interfaces.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents OrderItemShipment Core Application DataEntities Interfaces (DOTNET090000).
/// </summary>
/// <remarks>
/// The OrderItemShipment Core Application DataEntities Interfaces (DOTNET090000).
/// </remarks>
public partial interface IOrderItemShipment : COREAPPDENTINTERFACES.IEntity<COREDOMAINDATAMODELSDOMAIN.OrderItemShipment>
{
    #region Properties

    #endregion

    #region Methods

    /// <summary>
    /// Get OrderItemShipments
    /// </summary>
	/// <param name="request">request</param>
    Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.OrderItemShipment>> GetOrderItemShipmentsAsync(COREAPPDATAREPOMODELSDOMAIN.GetOrderItemShipmentRequest request);

    /// <summary>
    /// Merge OrderItemShipments
    /// </summary>
	/// <param name="request">request</param>
    Task<COREDOMAINDATAMODELS.MergeResult> MergeOrderItemShipmentsAsync(COREAPPDATAREPOMODELSDOMAIN.MergeOrderItemShipmentRequest request);

    #endregion

}
