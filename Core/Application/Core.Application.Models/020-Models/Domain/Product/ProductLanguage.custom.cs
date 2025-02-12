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
/// Represents ProductLanguage class.
/// </summary>
/// <remarks>
/// The ProductLanguage class.
/// </remarks>
public partial class Product : COREAPPMODELS.DomainModel<COREDOMAINDATAMODELSDOMAIN.Product>, COREAPPINTERFACESDOMAIN.IProduct
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	#endregion
	
	#region Public Methods

	/// <summary>
	/// Get ProductLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected async virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> GetProductLanguagePostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetProductLanguageRequest request)
	{
		var response = new COREAPPDATAMODELS.PostProcessorResponse();

		//// Add custom code here if needed

		return await Task.FromResult(response).ConfigureAwait(false);
	}


	/// <summary>
	/// Merge ProductLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected async virtual partial Task<COREAPPDATAMODELS.PostProcessorResponse> MergeProductLanguagePostProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeProductLanguageRequest request)
	{
		var response = new COREAPPDATAMODELS.PostProcessorResponse();

		//// Add custom code here if needed

		return await Task.FromResult(response).ConfigureAwait(false);
	}


	/// <summary>
	/// Get ProductLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected async virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> GetProductLanguagePreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.GetProductLanguageRequest request)
	{
		var response = new COREAPPDATAMODELS.PreProcessorResponse();

		//// Add custom code here if needed

		return await Task.FromResult(response).ConfigureAwait(false);
	}


	/// <summary>
	/// Merge ProductLanguages
	/// </summary>
	/// <param name="request"> The request.</param>
	/// <returns></returns>
	protected async virtual partial Task<COREAPPDATAMODELS.PreProcessorResponse> MergeProductLanguagePreProcessorResponseAsync(COREAPPDATAMODELSDOMAIN.MergeProductLanguageRequest request)
	{
		var response = new COREAPPDATAMODELS.PreProcessorResponse();

		//// Add custom code here if needed

		return await Task.FromResult(response).ConfigureAwait(false);
	}

	#endregion

	#region Private Methods

	#endregion
}
