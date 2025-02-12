#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
  
#endregion       

namespace Accenture.WebShop.Core.Application.Models.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents CustomerPhone class.
/// </summary>
/// <remarks>
/// The CustomerPhone class.
/// </remarks>
public partial class Customer : COREAPPMODELS.DomainModel<COREDOMAINDATAMODELSDOMAIN.Customer>, COREAPPINTERFACESDOMAIN.ICustomer
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	#endregion
	
	#region Public Methods

	/// <summary>
	/// Get CustomerPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected async virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetCustomerPhonePreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetCustomerPhoneRequest request)
	{
		var response = new COREAPPDATAMODELS.PreProcessorResponse();

		//// Add custom code here if needed

		return await Task.FromResult(response).ConfigureAwait(false);
	}


	/// <summary>
	/// Get CustomerPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected async virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetCustomerPhonePostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetCustomerPhoneRequest request)
	{
		var response = new COREAPPDATAMODELS.PostProcessorResponse();

		//// Add custom code here if needed

		return await Task.FromResult(response).ConfigureAwait(false);
	}


	/// <summary>
	/// Merge CustomerPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected async virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeCustomerPhonePostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeCustomerPhoneRequest request)
	{
		var response = new COREAPPDATAMODELS.PostProcessorResponse();

		//// Add custom code here if needed

		return await Task.FromResult(response).ConfigureAwait(false);
	}


	/// <summary>
	/// Merge CustomerPhones
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected async virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeCustomerPhonePreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeCustomerPhoneRequest request)
	{
		var response = new COREAPPDATAMODELS.PreProcessorResponse();

		//// Add custom code here if needed

		return await Task.FromResult(response).ConfigureAwait(false);
	}

	#endregion

	#region Private Methods

	#endregion
}
