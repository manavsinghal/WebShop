#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Core.Domain.DataModels;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead 

#endregion

/// <summary>
/// EntityEventConfig
/// </summary>
public class EntityEventConfig
{
	/// <summary>
	///  Gets or Sets the EntityName.
	/// </summary>
	public String? EntityName { get; set; }

	/// <summary>
	/// Gets or Sets the OperationName.
	/// </summary>
	public String? OperationName { get; set; }

	/// <summary>
	///  Gets or Sets the IsRaiseGlobalENSEvent.
	/// </summary>
	public Boolean? IsRaiseGlobalENSEvent { get; set; }

	/// <summary>
	///  Gets or Sets the IsRaiseInternalENSEvent.
	/// </summary>
	public Boolean? IsRaiseInternalENSEvent { get; set; }

	/// <summary>
	///  Gets or Sets the IsRaiseLocalEvent.
	/// </summary>
	public Boolean? IsRaiseLocalEvent { get; set; }

	/// <summary>
	///  Gets or Sets the IsRaiseSingleEventForAllObjectInMerge.
	/// </summary>
	public Boolean? IsRaiseSingleEventForAllObjectInMerge { get; set; }

	/// <summary>
	///  Gets or Sets the Topics.
	/// </summary>
	public String? Topics { get; set; }

	/// <summary>
	///  Gets or Sets the CallbackUrl.
	/// </summary>
	public String? CallbackUrl { get; set; }

	/// <summary>
	///  Gets or Sets the Events.
	/// </summary>
	public Event[]? Events { get; set; }
}

/// <summary>
/// Event
/// </summary>
public class Event
{
	/// <summary>
	///  Gets or Sets the Topic.
	/// </summary>
	public String? Topic { get; set; }

	/// <summary>
	///  Gets or Sets the IsRaiseGlobalENSEvent.
	/// </summary>
	public Boolean? IsRaiseGlobalENSEvent { get; set; }

	/// <summary>
	///  Gets or Sets the IsRaiseInternalENSEvent.
	/// </summary>
	public Boolean? IsRaiseInternalENSEvent { get; set; }

	/// <summary>
	///  Gets or Sets the IsRaiseLocalEvent.
	/// </summary>
	public Boolean? IsRaiseLocalEvent { get; set; }

	/// <summary>
	///  Gets or Sets the CallbackUrl.
	/// </summary>
	public String? CallbackUrl { get; set; }

	/// <summary>
	///  Gets or Sets the ErrorCallbackUrl.
	/// </summary>
	public String? ErrorCallbackUrl { get; set; }

	/// <summary>
	///  Gets or Sets the Parameters.
	/// </summary>
	public Parameter[]? Parameters { get; set; }
}

/// <summary>
/// Parameter
/// </summary>
public class Parameter
{
	/// <summary>
	///  Gets or Sets the Name.
	/// </summary>
	public String? Name { get; set; }

	/// <summary>
	///  Gets or Sets the Value.
	/// </summary>
	public String? Value { get; set; }
}


