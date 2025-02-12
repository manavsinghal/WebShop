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
/// Represents CountryLanguage class.
/// </summary>
/// <remarks>
/// The CountryLanguage class.
/// </remarks>
public partial interface IMaster 
{
	#region Methods

	/// <summary>
	/// Get CountryLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetCountryLanguageResponse> GetCountryLanguagesAsync(COREAPPDATAMODELSDOMAIN.GetCountryLanguageRequest request);

	/// <summary>
	/// Merge CountryLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeCountryLanguageResponse> MergeCountryLanguagesAsync(COREAPPDATAMODELSDOMAIN.MergeCountryLanguageRequest request);

	/// <summary>
	/// Decrypts CountryLanguages data
	/// </summary>
	/// <param name="countryLanguageUId">countryLanguage id</param>
	/// <returns></returns>
	Task DecryptCountryLanguagesAsync(Guid countryLanguageUId);

	#endregion
}
