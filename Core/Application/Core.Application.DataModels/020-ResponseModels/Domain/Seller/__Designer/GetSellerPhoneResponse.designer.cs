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
/// Represents SellerPhone Data Model class.
/// </summary>
/// <remarks>
/// The SellerPhone Data Model class.
/// </remarks>
public partial class GetSellerPhoneResponse : COREAPPDATAMODELS.Response<COREDOMAINDATAMODELSDOMAIN.SellerPhone>
{
	#region Fields

	#endregion

	#region Properties
	
	/// <summary>
	/// Represents SellerPhones property.
	/// </summary>
	/// <value>
	/// The SellerPhones.
	/// </value>
	public IEnumerable<COREDOMAINDATAMODELSDOMAIN.SellerPhone> SellerPhones  { get; set; }

	#endregion

	#region Constructors

	#endregion

	#region Methods

	#endregion
}
