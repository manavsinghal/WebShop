#region Copyright (c) 2024 Accenture. All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2024 Accenture. All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means,
// electronic, mechanical or otherwise, is prohibited without the prior written
// consent of the copyright owner.
// </copyright>

#endregion

namespace Accenture.WebShop.Core.Domain.DataModels.Domain.Enumerators;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents order status static class/enumerator.
/// </summary>
/// <remarks>
/// This class is generated from the data in the MasterListItem database table/view.
/// </remarks>
public static class OrderStatus
{
    /// <summary>
    /// Represents Created order status.
    /// </summary>
    public static Guid Created { get; private set; } = System.Guid.Parse("499B75D5-01EE-4E17-B22D-7C28F66C6E65");

    /// <summary>
    /// Represents Payment Successful order status.
    /// </summary>
    public static Guid PaymentSuccessful { get; private set; } = System.Guid.Parse("567644A8-745D-4472-9721-FBBDA9867C45");

    /// <summary>
    /// Represents Payment Failed order status.
    /// </summary>
    public static Guid PaymentFailed { get; private set; } = System.Guid.Parse("32775A5A-60EF-4FC5-8115-21E9F5610C1E");

    /// <summary>
    /// Represents Processing order status.
    /// </summary>
    public static Guid Processing { get; private set; } = System.Guid.Parse("3ED27156-9D64-44B3-B565-7688F105E3B3");

    /// <summary>
    /// Represents Shipped order status.
    /// </summary>
    public static Guid Shipped { get; private set; } = System.Guid.Parse("D593EDFA-7246-41DE-BC7A-C9C4E1BE26F3");

    /// <summary>
    /// Represents Delivered order status.
    /// </summary>
    public static Guid Delivered { get; private set; } = System.Guid.Parse("429B581B-06BD-4FC5-B59F-56442606788D");

    /// <summary>
    /// Represents Cancelled order status.
    /// </summary>
    public static Guid Cancelled { get; private set; } = System.Guid.Parse("C6F1595D-5714-45E2-A332-F8B9BE078E2A");
}
