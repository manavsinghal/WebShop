#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion  

namespace Accenture.WebShop.Core.Application.DataRepositories.DataModels.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents ShipperBankAccount Data Model Core Application DataRepositories DataModels (DOTNET090000).
/// </summary>
/// <remarks>
/// The ShipperBankAccount Data Model Core Application DataRepositories DataModels (DOTNET090000).
/// </remarks>
public partial class MergeShipperBankAccountRequest : COREAPPDATAREPOMODELS.Request<COREDOMAINDATAMODELSDOMAIN.ShipperBankAccount>
{
	#region Fields

	#endregion

	#region Properties
    /// <summary>
    /// Gets or Sets ShipperBankAccount
    /// </summary>
	/// <value>
    /// The ShipperBankAccount
    /// </value>
	public ICollection<COREDOMAINDATAMODELSDOMAIN.ShipperBankAccount>  ShipperBankAccounts  { get; set; }
	#endregion

	#region Constructors

    /// <summary>
    /// Initializes a new instance of the &lt;see cref=ShipperBankAccount/&gt; class.
    /// </summary>
    public MergeShipperBankAccountRequest()
	{		
	}

	#endregion

	#region Methods

	#endregion
}
