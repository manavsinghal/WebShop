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
/// Represents c3 product status static class/enumerator.
/// </summary>
/// <remarks>
/// This class is generated from the data in the MasterListItem database table/view.
/// </remarks>
public static class C3ProductStatus
{
    /// <summary>
    /// Represents Available product status.
    /// </summary>
    public static Guid Available { get; private set; } = System.Guid.Parse("B75DA47A-A1C0-4AA9-91D5-98851BF6FFED");

    /// <summary>
    /// Represents Not Available product status.
    /// </summary>
    public static Guid NotAvailable { get; private set; } = System.Guid.Parse("607CF990-4A44-4076-80ED-33F3078A8DDF");
}
