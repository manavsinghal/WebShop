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
/// Represents country static class/enumerator.
/// </summary>
/// <remarks>
/// This class is generated from the data in the Country database table/view.
/// </remarks>
public static class Country
{
    /// <summary>
    /// Represents USA country.
    /// </summary>
    public static Guid USA { get; private set; } = System.Guid.Parse("DA1FB10B-6896-4268-8F9A-79005722F3A7");

    /// <summary>
    /// Represents India country.
    /// </summary>
    public static Guid India { get; private set; } = System.Guid.Parse("7D613650-80A2-4A78-A37E-BB80C5A56D9D");

    /// <summary>
    /// Represents Canada country.
    /// </summary>
    public static Guid Canada { get; private set; } = System.Guid.Parse("CB9D64A6-262A-430F-8F87-9CE93C2F78E7");

    /// <summary>
    /// Represents Japan country.
    /// </summary>
    public static Guid Japan { get; private set; } = System.Guid.Parse("2EF43096-7344-4536-B4EF-6E16883FE2BF");
}
