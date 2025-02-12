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
/// Represents Country class.
/// </summary>
/// <remarks>
/// The Country class.
/// </remarks>
public partial interface IMaster 
{
	#region Methods

	/// <summary>
	/// Get Countries
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.GetCountryResponse> GetCountriesAsync(COREAPPDATAMODELSDOMAIN.GetCountryRequest request);

	/// <summary>
	/// Merge Countries
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	Task<COREAPPDATAMODELSDOMAIN.MergeCountryResponse> MergeCountriesAsync(COREAPPDATAMODELSDOMAIN.MergeCountryRequest request);

	/// <summary>
	/// Decrypts Countries data
	/// </summary>
	/// <param name="countryUId">country id</param>
	/// <returns></returns>
	Task DecryptCountriesAsync(Guid countryUId);

	#endregion
}
