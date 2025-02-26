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
/// Represents shipper status static class/enumerator.
/// </summary>
/// <remarks>
/// This class is generated from the data in the MasterListItem database table/view.
/// </remarks>
public static class ShipperStatus
{
    /// <summary>
    /// Represents Active shipper status.
    /// </summary>
    public static Guid Active { get; private set; } = System.Guid.Parse("390DF8A7-7006-44B2-BB87-4735E9B9BB3B");

    /// <summary>
    /// Represents Inactive shipper status.
    /// </summary>
    public static Guid Inactive { get; private set; } = System.Guid.Parse("ED60A06C-CCAB-4236-B7A2-03915DE59D6F");

    /// <summary>
    /// Represents Embargoed shipper status.
    /// </summary>
    public static Guid Embargoed { get; private set; } = System.Guid.Parse("5B269B91-A035-4622-95B2-9D7C106160C0");

    /// <summary>
    /// Represents Discontinued shipper status.
    /// </summary>
    public static Guid Discontinued { get; private set; } = System.Guid.Parse("12EEB0EB-4D3D-4AEF-AAC2-FA408F599B02");
}
