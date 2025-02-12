#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion

namespace Accenture.WebShop.Core.Application.DataModels.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents ProductCategoryLanguage Data Model class.
/// </summary>
/// <remarks>
/// The ProductCategoryLanguage Data Model class.
/// </remarks>
public partial class GetProductCategoryLanguageResponse : COREAPPDATAMODELS.Response<COREDOMAINDATAMODELSDOMAIN.ProductCategoryLanguage>
{
	#region Fields

	#endregion

	#region Properties
	
	/// <summary>
	/// Represents ProductCategoryLanguages property.
	/// </summary>
	/// <value>
	/// The ProductCategoryLanguages.
	/// </value>
	public IEnumerable<COREDOMAINDATAMODELSDOMAIN.ProductCategoryLanguage> ProductCategoryLanguages  { get; set; }

	#endregion

	#region Constructors

	#endregion

	#region Methods

	#endregion
}
