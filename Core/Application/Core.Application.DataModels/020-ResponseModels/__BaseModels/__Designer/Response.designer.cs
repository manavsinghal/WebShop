#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion

namespace Accenture.WebShop.Core.Application.DataModels;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents Response class.
/// </summary>
/// <remarks>
/// Represents Response class.
/// </remarks>
public class Response<T> where T : COREDOMAINDATAMODELS.DataModel<T>
{
	#region Properties

	/// <summary>
	/// Represents Faults property
	/// </summary>
	public SHAREDKERNALLIB.FaultCollection? Faults { get; set; } = new();

	/// <summary>
	/// Represents Status property
	/// </summary>
	public Status? Status { get; set; }

	/// <summary>
	/// Represents MergeResult property
	/// </summary>
	public COREDOMAINDATAMODELS.MergeResult? MergeResult { get; set; }

	#endregion
}

/// <summary>
/// Represents Status Class
/// </summary>
public class Status
{
	#region Properties

	/// <summary>
	/// Represents OperationStatus property
	/// </summary>
	public COREDOMAINDATAMODELSENUM.OperationStatus? OperationStatus { get; set; }

	/// <summary>
	/// Represents Code property
	/// </summary>
	public String? Code { get; set; }

	#endregion
}

