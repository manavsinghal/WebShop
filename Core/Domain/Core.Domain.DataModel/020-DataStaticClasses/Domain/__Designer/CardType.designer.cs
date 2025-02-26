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
/// Represents card type static class/enumerator.
/// </summary>
/// <remarks>
/// This class is generated from the data in the MasterListItem database table/view.
/// </remarks>
public static class CardType
{
    /// <summary>
    /// Represents Credit Card card type.
    /// </summary>
    public static Guid CreditCard { get; private set; } = System.Guid.Parse("B14E8354-EFE2-48FB-8B7F-91DF44AC96EE");

    /// <summary>
    /// Represents Debit Card card type.
    /// </summary>
    public static Guid DebitCard { get; private set; } = System.Guid.Parse("99EE8B1B-9B04-4149-96DD-93620CB22850");
}
