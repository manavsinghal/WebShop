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
/// Represents ShipperBankAccount Data Model class.
/// </summary>
/// <remarks>
/// The ShipperBankAccount Data Model class.
/// </remarks>
public partial class GetShipperBankAccountRequest : COREAPPDATAMODELS.Request<COREDOMAINDATAMODELSDOMAIN.ShipperBankAccount>
{
	#region Fields

	#endregion

	#region Properties

    /// <summary>
    /// Represents ShipperBankAccountUId property.
    /// </summary>
	/// <value>
    /// The ShipperBankAccountUId.
    /// </value>
	public Guid ShipperBankAccountUId  { get; set; }
    /// <summary>
    /// Represents ShipperUId property.
    /// </summary>
	/// <value>
    /// The ShipperUId.
    /// </value>
	public Guid ShipperUId  { get; set; }

	#endregion

	#region Constructors

	#endregion

	#region Methods

	#endregion	
}
