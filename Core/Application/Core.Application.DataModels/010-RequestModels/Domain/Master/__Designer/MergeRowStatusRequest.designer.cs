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
/// Represents RowStatus Data Model class.
/// </summary>
/// <remarks>
/// The RowStatus Data Model class.
/// </remarks>
public partial class MergeRowStatusRequest : COREAPPDATAMODELS.Request<COREDOMAINDATAMODELSDOMAIN.RowStatus>
{
	#region Fields

	#endregion

	#region Properties
	
	/// <summary>
	/// Represents RowStatuses property.
	/// </summary>
	/// <value>
	/// The RowStatuses.
	/// </value>
	public ICollection<COREDOMAINDATAMODELSDOMAIN.RowStatus> RowStatuses  { get; set; }

	#endregion

	#region Constructors

	#endregion

	#region Methods
	
	#endregion
}
