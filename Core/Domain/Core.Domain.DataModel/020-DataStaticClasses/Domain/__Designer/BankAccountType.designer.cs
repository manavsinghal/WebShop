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
/// Represents bank account type static class/enumerator.
/// </summary>
/// <remarks>
/// This class is generated from the data in the MasterListItem database table/view.
/// </remarks>
public static class BankAccountType
{
    /// <summary>
    /// Represents Checking bank account type.
    /// </summary>
    public static Guid Checking { get; private set; } = System.Guid.Parse("85B205B0-8E0C-4A64-A48A-91462C4E7520");

    /// <summary>
    /// Represents Saving bank account type.
    /// </summary>
    public static Guid Saving { get; private set; } = System.Guid.Parse("A2BFD634-36BA-4C11-97D8-AF62E4FC649F");

    /// <summary>
    /// Represents Business bank account type.
    /// </summary>
    public static Guid Business { get; private set; } = System.Guid.Parse("BA951165-CA7E-4D38-B7C8-7A6C643E29B8");
}
