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
/// Represents ICustomerRepository Core Application DataRepositories Interfaces (DOTNET090000).
/// </summary>
/// <remarks>
/// The ICustomerRepository Core Application DataRepositories Interfaces (DOTNET090000).
/// </remarks>
public partial interface ICustomerRepository : COREAPPDREPOINTERFACES.IRepository
{
	#region Properties

	#endregion

	#region Methods

	/// <summary>
	/// Get Customers
	/// </summary>
	/// <param name="request">request</param>
	/// <returns></returns>
	Task<COREAPPDATAREPOMODELSDOMAIN.GetCustomerResponse> GetCustomersAsync(COREAPPDATAREPOMODELSDOMAIN.GetCustomerRequest request);

	/// <summary>
	/// Merge Customers
	/// </summary>
	/// <param name="request">request</param>
	/// <returns></returns>
	Task<COREAPPDATAREPOMODELSDOMAIN.MergeCustomerResponse> MergeCustomersAsync(COREAPPDATAREPOMODELSDOMAIN.MergeCustomerRequest request);

	/// <summary>
	/// Save Customers
	/// </summary>
	/// <returns></returns>
	Task SaveCustomerAsync();

	#endregion

}
