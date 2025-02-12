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
/// Represents Language Data Model class.
/// </summary>
/// <remarks>
/// The Language Data Model class.
/// </remarks>
public partial class GetLanguageResponse : COREAPPDATAMODELS.Response<COREDOMAINDATAMODELSDOMAIN.Language>
{
	#region Fields

	#endregion

	#region Properties
	
	/// <summary>
	/// Represents Languages property.
	/// </summary>
	/// <value>
	/// The Languages.
	/// </value>
	public IEnumerable<COREDOMAINDATAMODELSDOMAIN.Language> Languages  { get; set; }

	#endregion

	#region Constructors

	#endregion

	#region Methods

	#endregion
}
