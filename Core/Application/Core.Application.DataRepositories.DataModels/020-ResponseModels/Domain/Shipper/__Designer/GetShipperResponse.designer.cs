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
/// Represents Shipper Core Application DataRepositories DataModels (DOTNET090000).
/// </summary>
/// <remarks>
/// The Shipper Core Application DataRepositories DataModels (DOTNET090000).
/// </remarks>
public partial class GetShipperResponse : COREAPPDATAREPOMODELS.Response<COREDOMAINDATAMODELSDOMAIN.Shipper>
{
	#region Fields

	#endregion

	#region Properties

    /// <summary>
    /// Gets or Sets Shipper
    /// </summary>
	/// <value>
    /// The The Shipper
    /// </value>
	public IQueryable<COREDOMAINDATAMODELSDOMAIN.Shipper> Shippers  { get; set; }

	#endregion

	#region Constructors

    /// <summary>
    /// Initializes a new instance of the &lt;see cref=Shipper/&gt; class.
    /// </summary>
    public GetShipperResponse()
	{		
	}

	#endregion

	#region Methods

	#endregion

}
