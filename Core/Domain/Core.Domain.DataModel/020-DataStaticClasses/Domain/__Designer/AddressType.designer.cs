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
/// Represents address type static class/enumerator.
/// </summary>
/// <remarks>
/// This class is generated from the data in the MasterListItem database table/view.
/// </remarks>
public static class AddressType
{
    /// <summary>
    /// Represents Home address type.
    /// </summary>
    public static Guid Home { get; private set; } = System.Guid.Parse("A0CFBADF-5476-4DAB-BE9A-3349D331715E");

    /// <summary>
    /// Represents Office address type.
    /// </summary>
    public static Guid Office { get; private set; } = System.Guid.Parse("DA986741-C6FA-4BBB-A917-F27DCDA97DBD");
}
