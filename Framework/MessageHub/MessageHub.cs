#region Copyright (c) 2024 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2024 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion

namespace Accenture.WebShop.MessageHub;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represent the MessageHub
/// </summary>
public class MessageHub : INTERFACES.IMessageHub
{
	#region Fields

	private readonly IHubContext<Notifier> _hubContext;

	#endregion

	#region Properties
	#endregion

	#region Constructor

	/// <summary>
	/// MessageHub constructor
	/// </summary>
	/// <param name="hubContext"></param>
	public MessageHub(IHubContext<Notifier> hubContext)
	{
		this._hubContext = hubContext;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Send information message to eventhub
	/// </summary>
	/// <param name="topics"></param>
	/// <param name="message"></param>
	/// <returns></returns>
	public async Task SendMessageAsync(IEnumerable<ENUMS.Topic> topics, DMMODELS.Message message)
	{
		foreach (var topic in topics)
		{
			if (topic == ENUMS.Topic.EventAndNotificationService)
			{
				// 1: Transform message to ENS event format.
				// 2: Call an ENS service and raise an event.
			}

			if (this._hubContext != null)
			{
				await this._hubContext.Clients.All.SendAsync(topic.ToString(), message);
			}
		}
	}

	#endregion

}

