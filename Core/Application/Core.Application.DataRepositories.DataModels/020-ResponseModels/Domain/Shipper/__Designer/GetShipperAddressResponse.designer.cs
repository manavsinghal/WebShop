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
/// Represents ShipperAddress Core Application DataRepositories DataModels (DOTNET090000).
/// </summary>
/// <remarks>
/// The ShipperAddress Core Application DataRepositories DataModels (DOTNET090000).
/// </remarks>
public partial class GetShipperAddressResponse : COREAPPDATAREPOMODELS.Response<COREDOMAINDATAMODELSDOMAIN.ShipperAddress>
{
	#region Fields

	#endregion

	#region Properties

    /// <summary>
    /// Gets or Sets ShipperAddress
    /// </summary>
	/// <value>
    /// The The ShipperAddress
    /// </value>
	public IQueryable<COREDOMAINDATAMODELSDOMAIN.ShipperAddress> ShipperAddresses  { get; set; }

	#endregion

	#region Constructors

    /// <summary>
    /// Initializes a new instance of the &lt;see cref=ShipperAddress/&gt; class.
    /// </summary>
    public GetShipperAddressResponse()
	{		
	}

	#endregion

	#region Methods

	#endregion

}
