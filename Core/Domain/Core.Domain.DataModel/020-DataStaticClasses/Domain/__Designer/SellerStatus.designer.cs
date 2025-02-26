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
/// Represents seller status static class/enumerator.
/// </summary>
/// <remarks>
/// This class is generated from the data in the MasterListItem database table/view.
/// </remarks>
public static class SellerStatus
{
    /// <summary>
    /// Represents Active seller status.
    /// </summary>
    public static Guid Active { get; private set; } = System.Guid.Parse("15BBCBF5-A52B-4A08-8CCD-1097EF86A332");

    /// <summary>
    /// Represents Inactive seller status.
    /// </summary>
    public static Guid Inactive { get; private set; } = System.Guid.Parse("0AE5BF30-4BF2-4A70-A575-9E9519238C00");

    /// <summary>
    /// Represents Embargoed seller status.
    /// </summary>
    public static Guid Embargoed { get; private set; } = System.Guid.Parse("974F5D84-E59C-4F1F-85B7-56EEC54008EB");

    /// <summary>
    /// Represents Discontinued seller status.
    /// </summary>
    public static Guid Discontinued { get; private set; } = System.Guid.Parse("CF2D1970-3025-4D30-992B-E92EEE2B7F2F");
}
