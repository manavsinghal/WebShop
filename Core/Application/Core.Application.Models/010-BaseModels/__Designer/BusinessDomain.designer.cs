#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion

namespace Accenture.WebShop.Core.Application.Models;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents BusinessDomain class.
/// </summary>
/// <remarks>
/// Represents BusinessDomain class.
/// </remarks>
public partial class BusinessDomain<T> where T : COREDOMAINDATAMODELS.DataModelWithAudit<T>
{
	#region Fields

	#endregion

	#region Properties

	/// <summary>
	/// Represents Faults property
	/// </summary>
	protected SHAREDKERNALLIB.FaultCollection? faults = new();

	#endregion

	#region Constructors

	#endregion

	#region Methods

	#endregion
}
