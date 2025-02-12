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
/// Represents Processor Response class.
/// </summary>
public class ProcessorResponse
{
	#region Field

	#endregion

	#region Properties

	/// <summary>
	/// Represents Faults.
	/// </summary>
	public SHAREDKERNALLIB.FaultCollection Faults { get; } = [];

	#endregion

	#region Constructors	
	
	/// <summary>
	/// Implements the operator +.
	/// </summary>
	/// <param name="left">The left.</param>
	/// <param name="right">The right.</param>
	/// <returns>
	/// The result of the operator.
	/// </returns>
	public static ProcessorResponse operator +(ProcessorResponse left, ProcessorResponse right)
	{
		left.Faults.AddRange(right.Faults);
		return left;
	}

	#endregion

	#region Methods

	#endregion
}

