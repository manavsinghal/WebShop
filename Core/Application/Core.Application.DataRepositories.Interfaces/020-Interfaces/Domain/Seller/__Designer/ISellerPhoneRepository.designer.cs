#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion  

namespace Accenture.WebShop.Core.Application.DataRepositories.Interfaces.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents ISellerRepository Core Application DataRepositories Interfaces (DOTNET090000).
/// </summary>
/// <remarks>
/// The ISellerRepository Core Application DataRepositories Interfaces (DOTNET090000).
/// </remarks>
public partial interface ISellerRepository : COREAPPDREPOINTERFACES.IRepository
{
	#region Properties

	#endregion

	#region Methods

	/// <summary>
	/// Get SellerPhones
	/// </summary>
	/// <param name="request">request</param>
	/// <returns></returns>
	Task<COREAPPDATAREPOMODELSDOMAIN.GetSellerPhoneResponse> GetSellerPhonesAsync(COREAPPDATAREPOMODELSDOMAIN.GetSellerPhoneRequest request);

	/// <summary>
	/// Merge SellerPhones
	/// </summary>
	/// <param name="request">request</param>
	/// <returns></returns>
	Task<COREAPPDATAREPOMODELSDOMAIN.MergeSellerPhoneResponse> MergeSellerPhonesAsync(COREAPPDATAREPOMODELSDOMAIN.MergeSellerPhoneRequest request);

	/// <summary>
	/// Save SellerPhones
	/// </summary>
	/// <returns></returns>
	Task SaveSellerPhoneAsync();

	#endregion

}
