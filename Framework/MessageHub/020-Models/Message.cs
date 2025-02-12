#region Copyright (c) 2024 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2024 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion

namespace Accenture.WebShop.MessageHub.Models;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents message model.
/// </summary>
/// <remarks>
/// The Messge model.
/// </remarks>
public class Message
{
	/// <summary>
	/// Gets or sets the service.
	/// </summary>
	/// <value>
	/// The service.
	/// </value>
	public String? Service { get; set; }

	/// <summary>
	/// Gets or sets the tier.
	/// </summary>
	/// <value>
	/// The tier.
	/// </value>
	public ENUMS.Tier Tier { get; set; }

	/// <summary>
	/// Gets or sets the component.
	/// </summary>
	/// <value>
	/// The component.
	/// </value>
	public String? Component { get; set; }

	/// <summary>
	/// Gets or sets the sub component.
	/// </summary>
	/// <value>
	/// The sub component.
	/// </value>
	public String? SubComponent { get; set; }

	/// <summary>
	/// Gets or sets the level.
	/// </summary>
	/// <value>
	/// The level.
	/// </value>
	public Int16 Level { get; set; }

	/// <summary>
	/// Gets or sets the sublevel.
	/// </summary>
	/// <value>
	/// The sublevel.
	/// </value>
	public Int16 Sublevel { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether this instance is private.
	/// </summary>
	/// <value>
	///   <c>true</c> if this instance is private; otherwise, <c>false</c>.
	/// </value>
	public Boolean IsPrivate { get; set; } = false;

	/// <summary>
	/// Gets or sets the log level.
	/// </summary>
	/// <value>
	/// The log level.
	/// </value>
	public ENUMS.LogLevel LogLevel { get; set; }

	/// <summary>
	/// Gets or sets the operation.
	/// </summary>
	/// <value>
	/// The operation.
	/// </value>
	public ENUMS.Operation Operation { get; set; }

	/// <summary>
	/// Gets or sets the title.
	/// </summary>
	/// <value>
	/// The title.
	/// </value>
	public String? Title { get; set; }

	/// <summary>
	/// Gets or sets the description.
	/// </summary>
	/// <value>
	/// The description.
	/// </value>
	public String? Description { get; set; }

	/// <summary>
	/// Gets or sets the trace.
	/// </summary>
	/// <value>
	/// The trace.
	/// </value>
	public String? Trace { get; set; }

	/// <summary>
	/// Gets or sets the raised on.
	/// </summary>
	/// <value>
	/// The raised on.
	/// </value>
	public DateTime RaisedOn { get; set; }

	/// <summary>
	/// Gets or sets the raised by.
	/// </summary>
	/// <value>
	/// The raised by.
	/// </value>
	public String RaisedBy { get; set; }

	/// <summary>
	/// Gets or sets the associated item identifier.
	/// </summary>
	/// <value>
	/// The associated item identifier.
	/// </value>
	public String? AssociatedItemId { get; set; }

	/// <summary>
	/// Gets or sets the callback URL.
	/// </summary>
	/// <value>
	/// The callback URL.
	/// </value>
	public String? CallbackUrl { get; set; }

	/// <summary>
	/// Gets or sets the error callback URL.
	/// </summary>
	/// <value>
	/// The error callback URL.
	/// </value>
	public String? ErrorCallbackUrl { get; set; }

	/// <summary>
	/// Gets or sets the client u identifier.
	/// </summary>
	/// <value>
	/// The client u identifier.
	/// </value>
	public Guid? ClientUId { get; set; }

	/// <summary>
	/// Gets or sets the delivery construct u identifier.
	/// </summary>
	/// <value>
	/// The delivery construct u identifier.
	/// </value>
	public Guid? DeliveryConstructUId { get; set; }

	/// <summary>
	/// Gets or sets the parameters.
	/// </summary>
	/// <value>
	/// The parameters.
	/// </value>
	public Tuple<String, String>[]? Parameters { get; set; }

	/// <summary>
	/// Gets or sets the messages.
	/// </summary>
	/// <value>
	/// The messages.
	/// </value>
	public ICollection<Message>? Messages { get; set; }

	/// <summary>
	/// Default Constrcutor 
	/// </summary>
	public Message()
	{
		Level = 1;
		Sublevel = 0;
		IsPrivate = false;
		LogLevel = ENUMS.LogLevel.Information;
		Operation = ENUMS.Operation.Unknown;
		RaisedOn = DateTime.UtcNow.ToLocalTime();
		RaisedBy = System.Environment.UserName;
	}
}

