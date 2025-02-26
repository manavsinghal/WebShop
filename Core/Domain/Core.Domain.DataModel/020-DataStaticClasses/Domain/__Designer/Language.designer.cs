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
/// Represents language static class/enumerator.
/// </summary>
/// <remarks>
/// This class is generated from the data in the Language database table/view.
/// </remarks>
public static class Language
{
    /// <summary>
    /// Represents Japanese language.
    /// </summary>
    public static Guid Japanese { get; private set; } = System.Guid.Parse("00300000-0000-0000-0000-000000000000");

    /// <summary>
    /// Represents Arabic language.
    /// </summary>
    public static Guid Arabic { get; private set; } = System.Guid.Parse("01600000-0000-0000-0000-000000000000");

    /// <summary>
    /// Represents Hindi language.
    /// </summary>
    public static Guid Hindi { get; private set; } = System.Guid.Parse("08100000-0000-0000-0000-000000000000");

    /// <summary>
    /// Represents English (US) language.
    /// </summary>
    public static Guid EnglishUS { get; private set; } = System.Guid.Parse("00100000-0000-0000-0000-000000000000");
}
