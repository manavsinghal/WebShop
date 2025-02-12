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
/// Represents Request Model
/// </summary>
/// <remarks>
/// Represents Request Model
/// </remarks>
public class Request<T> where T : COREDOMAINDATAMODELS.DataModel<T>
{
	#region Properties

	/// <summary>
	/// Gets or sets the MatchExpression
	/// </summary>
	/// <value>
	/// The MatchExpression
	/// </value>
	public Expression<Func<T,Boolean>> MatchExpression { get; set; }

	/// <summary>
	/// Gets or sets the CorrelationUId
	/// </summary>
	/// <value>
	/// The CorrelationUId
	/// </value>
	public Guid CorrelationUId { get; set; }

	#endregion
}
