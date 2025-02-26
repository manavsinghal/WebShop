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
/// Represents phone type static class/enumerator.
/// </summary>
/// <remarks>
/// This class is generated from the data in the MasterListItem database table/view.
/// </remarks>
public static class PhoneType
{
    /// <summary>
    /// Represents Mobile phone type.
    /// </summary>
    public static Guid Mobile { get; private set; } = System.Guid.Parse("A174D403-AD9A-4A6A-BE2B-3622BE273982");

    /// <summary>
    /// Represents Home phone type.
    /// </summary>
    public static Guid Home { get; private set; } = System.Guid.Parse("6773A1FC-AEC4-4FC0-AC83-E237EABC6D02");

    /// <summary>
    /// Represents Office phone type.
    /// </summary>
    public static Guid Office { get; private set; } = System.Guid.Parse("887547B0-0C28-4FF5-90C0-A999D653C942");
}
