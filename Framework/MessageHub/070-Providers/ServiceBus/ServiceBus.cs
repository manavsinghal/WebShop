#region Copyright (c) 2024 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2024 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion

namespace Accenture.WebShop.MessageHub.Providers.ServiceBus;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// ServiceBus Provider
/// </summary>
public class ServiceBus : INTERFACES.IServiceBus
{
	/// <summary>
	/// Sends the message asynchronous.
	/// </summary>
	/// <param name="queueName">Name of the queue.</param>
	/// <param name="message">The message.</param>
	public async Task SendMessageToQueueAsync(String queueName, Message message)
	{
		if (!String.IsNullOrEmpty(queueName))
		{
			try
			{
				var administrationClient = new ServiceBusAdministrationClient(SHARED.AppSettings.ServiceBusConnectionString);
				if (!await administrationClient.QueueExistsAsync(queueName))
				{
					_ = await administrationClient.CreateQueueAsync(queueName);
					await PushMessageAsync(queueName, message);
				}
				else
				{
					await PushMessageAsync(queueName, message);
				}
			}
			catch (ServiceBusException ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}
	}

	/// <summary>
	/// Sends the message asynchronous.
	/// </summary>
	/// <param name="queueName">Name of the queue.</param>
	/// <param name="message">The message.</param>
	public async Task SendMessageToQueueAsync(String queueName, IEnumerable<Message> message)
	{
		if (!String.IsNullOrEmpty(queueName))
		{
			try
			{
				var administrationClient = new ServiceBusAdministrationClient(SHARED.AppSettings.ServiceBusConnectionString);
				if (!await administrationClient.QueueExistsAsync(queueName))
				{
					_ = await administrationClient.CreateQueueAsync(queueName);

					await PushMessagesAsync(queueName, message);
				}
				else
				{
					await PushMessagesAsync(queueName, message);
				}
			}
			catch (ServiceBusException ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}
		else
		{
			Debug.WriteLine($"Message is Empty");
		}
	}

	/// <summary>
	/// Sends the message As Batch asynchronous.
	/// </summary>
	/// <param name="queueName">Name of the queue.</param>
	/// <param name="message">The message.</param>
	public async Task SendMessageToQueueAsBatchAsync(String queueName, IEnumerable<Message> message)
	{
		if (!String.IsNullOrEmpty(queueName))
		{
			try
			{
				var administrationClient = new ServiceBusAdministrationClient(SHARED.AppSettings.ServiceBusConnectionString);
				if (!await administrationClient.QueueExistsAsync(queueName))
				{
					_ = await administrationClient.CreateQueueAsync(queueName);
					await PushMessagesAsBatchesAsync(queueName, message);
				}
				else
				{
					await PushMessagesAsBatchesAsync(queueName, message);
				}
			}
			catch (ServiceBusException ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}
		else
		{
			Debug.WriteLine($"Message is Empty");
		}
	}

	/// <summary>
	/// Sends the message to topic asynchronous.
	/// </summary>
	/// <param name="topic">The topic.</param>
	/// <param name="message">The message.</param>
	public async Task SendMessageToTopicAsync(String topic, Message message)
	{
		if (!String.IsNullOrEmpty(topic))
		{
			try
			{
				var administrationClient = new ServiceBusAdministrationClient(SHARED.AppSettings.ServiceBusConnectionString);
				if (!await administrationClient.TopicExistsAsync(topic))
				{
					_ = await administrationClient.CreateTopicAsync(topic);
					await PushMessageAsync(topic, message);
				}
				else
				{
					await PushMessageAsync(topic, message);
				}
			}
			catch (ServiceBusException ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}
	}

	/// <summary>
	/// Sends the message to topic asynchronous.
	/// </summary>
	/// <param name="topic">The topic.</param>
	/// <param name="message">The message.</param>
	public async Task SendMessageToTopicAsync(String topic, IEnumerable<Message> message)
	{
		if (!String.IsNullOrEmpty(topic))
		{
			try
			{
				var administrationClient = new ServiceBusAdministrationClient(SHARED.AppSettings.ServiceBusConnectionString);
				if (!await administrationClient.TopicExistsAsync(topic))
				{
					_ = await administrationClient.CreateTopicAsync(topic);

					await PushMessagesAsync(topic, message);
				}
				else
				{
					await PushMessagesAsync(topic, message);
				}
			}
			catch (ServiceBusException ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}
		else
		{
			Debug.WriteLine($"topic is Empty");
		}
	}

	/// <summary>
	/// Sends the message to topic as batch asynchronous.
	/// </summary>
	/// <param name="topic">The topic.</param>
	/// <param name="message">The message.</param>
	public async Task SendMessageToTopicAsBatchAsync(String topic, IEnumerable<Message> message)
	{
		if (!String.IsNullOrEmpty(topic))
		{
			try
			{
				var administrationClient = new ServiceBusAdministrationClient(SHARED.AppSettings.ServiceBusConnectionString);
				if (!await administrationClient.TopicExistsAsync(topic))
				{
					_ = await administrationClient.CreateTopicAsync(topic);
					await PushMessagesAsBatchesAsync(topic, message);
				}
				else
				{
					await PushMessagesAsBatchesAsync(topic, message);
				}
			}
			catch (ServiceBusException ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}
		else
		{
			Debug.WriteLine($"topic is Empty");
		}
	}

	/// <summary>
	/// Creates the queue If not available asynchronous.
	/// </summary>
	/// <param name="queueName">Name of the queue.</param>
	public async Task CreateQueueAsync(String queueName)
	{
		if (!String.IsNullOrEmpty(queueName))
		{
			try
			{
				var administrationClient = new ServiceBusAdministrationClient(SHARED.AppSettings.ServiceBusConnectionString);
				_ = await administrationClient.CreateQueueAsync(queueName);
			}
			catch (ServiceBusException ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}
		else
		{
			Debug.WriteLine($"queueName is null");
		}
	}

	/// <summary>
	/// Creates the topic asynchronous.
	/// </summary>
	/// <param name="topic">The topic.</param>
	/// <exception cref="System.Exception">The topic already exists.</exception>
	public async Task CreateTopicAsync(String topic)
	{
		if (!String.IsNullOrEmpty(topic))
		{
			try
			{
				var administrationClient = new ServiceBusAdministrationClient(SHARED.AppSettings.ServiceBusConnectionString);
				try
				{
					if (!await administrationClient.TopicExistsAsync(topic))
					{
						_ = await administrationClient.CreateTopicAsync(topic);
					}
				}
				catch (ServiceBusException ex)
				{
					throw new Exception($"The topic already exists.", ex);
				}
			}
			catch (ServiceBusException ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}
		else
		{
			Debug.WriteLine($"topic is null");
		}
	}

	/// <summary>
	/// Creates the subscription asynchronous.
	/// </summary>
	/// <param name="topic">The topic.</param>
	/// <param name="subscription">The subscription.</param>
	/// <exception cref="System.Exception">The subscription already exists.</exception>
	public async Task CreateSubscriptionAsync(String topic, String subscription)
	{
		if (!String.IsNullOrEmpty(subscription))
		{
			try
			{
				var administrationClient = new ServiceBusAdministrationClient(SHARED.AppSettings.ServiceBusConnectionString);
				try
				{
					if (!await administrationClient.SubscriptionExistsAsync(topic, subscription))
					{
						_ = await administrationClient.CreateSubscriptionAsync(topic, subscription);
					}
				}
				catch (ServiceBusException ex)
				{
					throw new Exception($"The subscription already exists.", ex);
				}
			}
			catch (ServiceBusException ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}
		else
		{
			Debug.WriteLine($"subscription is null");
		}
	}

	/// <summary>
	/// Pushes the message asynchronous.
	/// </summary>
	/// <param name="queueName">Name of the queue.</param>
	/// <param name="message">The message.</param>
	private static async Task PushMessageAsync(String queueName, Message message)
	{
		await using var client = new ServiceBusClient(SHARED.AppSettings.ServiceBusConnectionString);
		await using var sender = client.CreateSender(queueName);

		await sender.SendMessageAsync(new ServiceBusMessage(message.Description));
	}

	/// <summary>
	/// Pushes the messages asynchronous.
	/// </summary>
	/// <param name="queueName">Name of the queue.</param>
	/// <param name="message">The message.</param>
	private static async Task PushMessagesAsync(String queueName, IEnumerable<Message> message)
	{
		await using var client = new ServiceBusClient(SHARED.AppSettings.ServiceBusConnectionString);
		await using var sender = client.CreateSender(queueName);

		foreach (var item in message)
		{
			var serializedContents = new ServiceBusMessage(JsonSerializer.Serialize(item));
			await sender.SendMessageAsync(serializedContents);
		}
	}

	/// <summary>
	/// Pushes the messages as batches asynchronous.
	/// </summary>
	/// <param name="queueName">Name of the queue.</param>
	/// <param name="message">The message.</param>
	/// <exception cref="System.Exception">The message is too large to fit in the batch.</exception>
	private static async Task PushMessagesAsBatchesAsync(String queueName, IEnumerable<Message> message)
	{
		var serviceBusMessage = new Queue<ServiceBusMessage>();
		await using var client = new ServiceBusClient(SHARED.AppSettings.ServiceBusConnectionString);
		await using var sender = client.CreateSender(queueName);
		using var messageBatch = await sender.CreateMessageBatchAsync();

		foreach (var item in message)
		{
			serviceBusMessage.Enqueue(new ServiceBusMessage(JsonSerializer.Serialize(item)));
		}

		while (serviceBusMessage.Count > 0)
		{

			if (!messageBatch.TryAddMessage(serviceBusMessage.Dequeue()))
			{
				throw new Exception($"The message is too large to fit in the batch.");
			}
		}

		try
		{
			await sender.SendMessagesAsync(messageBatch);
			Debug.WriteLine($"A batch of {serviceBusMessage.Count} messages has been published to the queue.");
		}
		finally
		{
			await sender.DisposeAsync();
			await client.DisposeAsync();
		}
	}
}

