#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
 
#endregion

namespace Accenture.WebShop.Core.Application.DataEntities.Interfaces.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents CustomerCard Core Application DataEntities Interfaces (DOTNET090000).
/// </summary>
/// <remarks>
/// The CustomerCard Core Application DataEntities Interfaces (DOTNET090000).
/// </remarks>
public partial interface ICustomerCard : COREAPPDENTINTERFACES.IEntity<COREDOMAINDATAMODELSDOMAIN.CustomerCard>
{
    #region Properties

    #endregion

    #region Methods

    /// <summary>
    /// Get CustomerCards
    /// </summary>
	/// <param name="request">request</param>
    Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.CustomerCard>> GetCustomerCardsAsync(COREAPPDATAREPOMODELSDOMAIN.GetCustomerCardRequest request);

    /// <summary>
    /// Merge CustomerCards
    /// </summary>
	/// <param name="request">request</param>
    Task<COREDOMAINDATAMODELS.MergeResult> MergeCustomerCardsAsync(COREAPPDATAREPOMODELSDOMAIN.MergeCustomerCardRequest request);

    #endregion

}
