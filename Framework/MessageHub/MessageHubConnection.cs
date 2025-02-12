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
/// Represent the MessageHubConnection
/// </summary>
public class MessageHubConnection : INTERFACES.IMessageHub
{
	#region Fields

	private readonly HubConnection _connection;

	#endregion

	#region Properties
	#endregion

	#region Constructor

	/// <summary>
	/// MessageHubConnection constructor
	/// </summary>
	/// <param name="connection"></param>
	public MessageHubConnection(HubConnection connection)
	{
		this._connection = connection;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Send information message from client to host
	/// </summary>
	/// <param name="topics"></param>
	/// <param name="message"></param>
	/// <returns></returns>
	public async Task SendMessageAsync(IEnumerable<ENUMS.Topic> topics, DMMODELS.Message message)
	{
		foreach (var topic in topics)
		{
			if (this._connection != null && this._connection.State == HubConnectionState.Connected)
			{
				await this._connection.SendAsync(topic.ToString(), message);
			}
		}
	}

	#endregion

}

