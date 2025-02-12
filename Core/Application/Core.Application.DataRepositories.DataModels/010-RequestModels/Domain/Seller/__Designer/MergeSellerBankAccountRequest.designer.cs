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
/// Represents SellerBankAccount Data Model Core Application DataRepositories DataModels (DOTNET090000).
/// </summary>
/// <remarks>
/// The SellerBankAccount Data Model Core Application DataRepositories DataModels (DOTNET090000).
/// </remarks>
public partial class MergeSellerBankAccountRequest : COREAPPDATAREPOMODELS.Request<COREDOMAINDATAMODELSDOMAIN.SellerBankAccount>
{
	#region Fields

	#endregion

	#region Properties
    /// <summary>
    /// Gets or Sets SellerBankAccount
    /// </summary>
	/// <value>
    /// The SellerBankAccount
    /// </value>
	public ICollection<COREDOMAINDATAMODELSDOMAIN.SellerBankAccount>  SellerBankAccounts  { get; set; }
	#endregion

	#region Constructors

    /// <summary>
    /// Initializes a new instance of the &lt;see cref=SellerBankAccount/&gt; class.
    /// </summary>
    public MergeSellerBankAccountRequest()
	{		
	}

	#endregion

	#region Methods

	#endregion
}
