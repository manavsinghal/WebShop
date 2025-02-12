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
/// Represents MasterListItem Data Model class.
/// </summary>
/// <remarks>
/// The MasterListItem Data Model class.
/// </remarks>
public partial class GetMasterListItemRequest : COREAPPDATAMODELS.Request<COREDOMAINDATAMODELSDOMAIN.MasterListItem>
{
	#region Fields

	#endregion

	#region Properties

    /// <summary>
    /// Represents MasterListItemUId property.
    /// </summary>
	/// <value>
    /// The MasterListItemUId.
    /// </value>
	public Guid MasterListItemUId  { get; set; }
    /// <summary>
    /// Represents MasterListUId property.
    /// </summary>
	/// <value>
    /// The MasterListUId.
    /// </value>
	public Guid MasterListUId  { get; set; }

	#endregion

	#region Constructors

	#endregion

	#region Methods

	#endregion	
}
