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
/// Represents account status static class/enumerator.
/// </summary>
/// <remarks>
/// This class is generated from the data in the AccountStatus database table/view.
/// </remarks>
public static class AccountStatus
{
    /// <summary>
    /// Represents Inactive account status.
    /// </summary>
    public static Guid Inactive { get; private set; } = System.Guid.Parse("00200000-0000-0000-0000-000000000000");

    /// <summary>
    /// Represents Active account status.
    /// </summary>
    public static Guid Active { get; private set; } = System.Guid.Parse("00100000-0000-0000-0000-000000000000");
}
