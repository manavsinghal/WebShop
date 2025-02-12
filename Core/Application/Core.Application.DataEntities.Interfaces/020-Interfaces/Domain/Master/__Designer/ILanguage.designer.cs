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
/// Represents Language Core Application DataEntities Interfaces (DOTNET090000).
/// </summary>
/// <remarks>
/// The Language Core Application DataEntities Interfaces (DOTNET090000).
/// </remarks>
public partial interface ILanguage : COREAPPDENTINTERFACES.IEntity<COREDOMAINDATAMODELSDOMAIN.Language>
{
    #region Properties

    #endregion

    #region Methods

    /// <summary>
    /// Get Languages
    /// </summary>
	/// <param name="request">request</param>
    Task<IQueryable<COREDOMAINDATAMODELSDOMAIN.Language>> GetLanguagesAsync(COREAPPDATAREPOMODELSDOMAIN.GetLanguageRequest request);

    /// <summary>
    /// Merge Languages
    /// </summary>
	/// <param name="request">request</param>
    Task<COREDOMAINDATAMODELS.MergeResult> MergeLanguagesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeLanguageRequest request);

    #endregion

}
