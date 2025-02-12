#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion

namespace Accenture.WebShop.Core.Application.Interfaces.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents Language class.
/// </summary>
/// <remarks>
/// The Language class.
/// </remarks>
public partial interface IMaster 
{
	#region Methods

	/// <summary>
	/// Get Languages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetLanguageResponse> GetLanguagesAsync(COREAPPDATAMODELSDOMAIN.GetLanguageRequest request);

	/// <summary>
	/// Merge Languages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeLanguageResponse> MergeLanguagesAsync(COREAPPDATAMODELSDOMAIN.MergeLanguageRequest request);

	/// <summary>
	/// Decrypts Languages data
	/// </summary>
	/// <param name="languageUId">language id</param>
	/// <returns></returns>
	Task DecryptLanguagesAsync(Guid languageUId);

	#endregion
}
