#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion  

namespace Accenture.WebShop.Core.Application.DataRepositories.DataModels;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents Response Model
/// </summary>
/// <remarks>
/// Represents Request Model
/// </remarks>
public class Response<T> where T : COREDOMAINDATAMODELS.DataModel<T>
{
	#region Properties
	
	/// <summary>
	/// Gets or sets the MergeResult
	/// </summary>
	/// <value>
	/// The MergeResult
	/// </value>
	public COREDOMAINDATAMODELS.MergeResult MergeResult { get; set; }

	/// <summary>
	/// Gets or sets the Faults collection
	/// </summary>
	/// <value>
	/// The Faults collection
	/// </value>
	public SHAREDKERNALLIB.FaultCollection Faults { get; set; } = new();

	#endregion
}
