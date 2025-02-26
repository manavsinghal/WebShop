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
/// Represents customer status static class/enumerator.
/// </summary>
/// <remarks>
/// This class is generated from the data in the MasterListItem database table/view.
/// </remarks>
public static class CustomerStatus
{
    /// <summary>
    /// Represents Active customer status.
    /// </summary>
    public static Guid Active { get; private set; } = System.Guid.Parse("F579EBDD-6046-4CDC-B470-9E5BF29849DB");

    /// <summary>
    /// Represents Inactive customer status.
    /// </summary>
    public static Guid Inactive { get; private set; } = System.Guid.Parse("38B2FA95-9CA6-4005-B2DB-D6AEFEC14B95");
}
