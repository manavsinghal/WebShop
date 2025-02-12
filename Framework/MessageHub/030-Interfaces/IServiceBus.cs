#region Copyright (c) 2024 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2024 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion

namespace Accenture.WebShop.MessageHub.Interfaces;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// IServiceBus
/// </summary>
public interface IServiceBus
{
	/// <summary>
	/// Sends the message to queue asynchronous.
	/// </summary>
	/// <param name="queueName">Name of the queue.</param>
	/// <param name="message">The message.</param>
	/// <returns></returns>
	Task SendMessageToQueueAsync(String queueName, Message message);

	/// <summary>
	/// Sends the message to queue asynchronous.
	/// </summary>
	/// <param name="queueName">Name of the queue.</param>
	/// <param name="message">The message.</param>
	/// <returns></returns>
	Task SendMessageToQueueAsync(String queueName, IEnumerable<Message> message);

	/// <summary>
	/// Sends the message to queue as batch asynchronous.
	/// </summary>
	/// <param name="queueName">Name of the queue.</param>
	/// <param name="message">The message.</param>
	/// <returns></returns>
	Task SendMessageToQueueAsBatchAsync(String queueName, IEnumerable<Message> message);

	/// <summary>
	/// Sends the message to topic asynchronous.
	/// </summary>
	/// <param name="topic">The topic.</param>
	/// <param name="message">The message.</param>
	/// <returns></returns>
	Task SendMessageToTopicAsync(String topic, Message message);

	/// <summary>
	/// Sends the message to topic asynchronous.
	/// </summary>
	/// <param name="topic">The topic.</param>
	/// <param name="message">The message.</param>
	/// <returns></returns>
	Task SendMessageToTopicAsync(String topic, IEnumerable<Message> message);

	/// <summary>
	/// Sends the message to topic as batch asynchronous.
	/// </summary>
	/// <param name="topic">The topic.</param>
	/// <param name="message">The message.</param>
	/// <returns></returns>
	Task SendMessageToTopicAsBatchAsync(String topic, IEnumerable<Message> message);

	/// <summary>
	/// Creates the queue asynchronous.
	/// </summary>
	/// <param name="queueName">Name of the queue.</param>
	/// <returns></returns>
	Task CreateQueueAsync(String queueName);

	/// <summary>
	/// Creates the topic asynchronous.
	/// </summary>
	/// <param name="topic">The topic.</param>
	/// <returns></returns>
	Task CreateTopicAsync(String topic);

	/// <summary>
	/// Creates the subscription asynchronous.
	/// </summary>
	/// <param name="topic">The topic.</param>
	/// <param name="subscription">The subscription.</param>
	/// <returns></returns>
	Task CreateSubscriptionAsync(String topic, String subscription);

}


