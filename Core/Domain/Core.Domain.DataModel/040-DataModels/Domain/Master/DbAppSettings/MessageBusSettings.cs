#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Core.Domain.DataModels.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead 

#endregion

/// <summary>
/// Message Bus Settings from DB
/// </summary>
public class MessageBusSettings
{
	#region Field

	#endregion

	#region Properties

	/// <summary>
	/// Provider Type
	/// </summary>
	public String ProviderType { get; set; } = default!;

	/// <summary>
	/// Queue Name
	/// </summary>
	public String QueueName { get; set; } = default!;

	/// <summary>
	/// Gets or sets the topicname.
	/// </summary>
	/// <value>
	/// The topicname.
	/// </value>
	public String TopicName { get; set; } = default!;

	/// <summary>
	/// ServiceBus Processor Auto Lock Renewal
	/// </summary>
	public Int32 MaxAutoLockRenewalDurationInHours { get; set; }

	#endregion

	#region Constructors

	#endregion

	#region Methods

	#endregion

}

