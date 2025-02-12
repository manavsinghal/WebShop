#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion  

namespace Accenture.WebShop.Infrastructure.DataRepositories;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents DataRepositoryBase Repository Class.
/// </summary>
/// <remarks>
/// Represents DataRepositoryBase Repository Class.
/// </remarks>
public abstract class DataRepositoryBase
{
	#region Fields

	/// <summary>
	/// Gets the logger.
	/// </summary>
	/// <value>
	/// The logger.
	/// </value>
	protected MSLOGGING.ILogger Logger { get; init; }

	/// <summary>
	/// The message hub
	/// </summary>
	protected readonly MESSAGEHUBINTERFACES.IMessageHub _messageHub;

	#endregion

	#region Properties

	#endregion
	
	#region Constructors

	/// <summary>
    /// Initializes a new instance of the <see cref=" DataRepositoryBase"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
	/// <param name="messageHub">The messageHub.</param>
	 public DataRepositoryBase(MSLOGGING.ILogger logger, MESSAGEHUBINTERFACES.IMessageHub messageHub)
	{
		this.Logger = logger;
		this._messageHub = messageHub;
	}

	#endregion

	#region Public Methods

	#region MessageHub

	/// <summary>
	/// Send private information message to eventhub
	/// </summary>
	/// <param name="topic"></param>
	/// <param name="component"></param>
	/// <param name="title"></param>
	/// <param name="description"></param>
	/// <param name="parameters"></param>
	/// <returns></returns>
	public async Task SendPrivateInfoAsync(MESSAGEHUBENUMS.Topic topic
										  , String component
										  , String title
										  , String description
										  , params Tuple<String, String>[] parameters)
	{
		if (AppSettings.IsDataAccessTierNotificationEnabled &&
			AppSettings.IsPrivateMethodNotificationEnabled)
		{
			var message = new MESSAGEHUBMODELS.Message
			{
				Component = component,
				Title = title,
				Description = description,
				LogLevel = MESSAGEHUBENUMS.LogLevel.Information,
				IsPrivate = true,
				Sublevel = 2,
				Parameters = parameters
			};

			await this.SendMessageAsync(new List<MESSAGEHUBENUMS.Topic>() { topic }, message).ConfigureAwait(false);
		}
	}

	/// <summary>
	/// Send public information message to eventhub
	/// </summary>
	/// <param name="topic"></param>
	/// <param name="component"></param>
	/// <param name="title"></param>
	/// <param name="description"></param>
	/// <param name="parameters"></param>
	/// <returns></returns>
	public async Task SendInfoAsync(MESSAGEHUBENUMS.Topic topic
								   , String component
								   , String title
								   , String description
								   , params Tuple<String, String>[] parameters)
	{
		var message = new MESSAGEHUBMODELS.Message
		{
			Component = component,
			Title = title,
			Description = description,
			LogLevel = MESSAGEHUBENUMS.LogLevel.Information,
			Sublevel = 0,
			Parameters = parameters
		};

		await this.SendMessageAsync(new List<MESSAGEHUBENUMS.Topic>() { topic }, message).ConfigureAwait(false);
	}

	/// <summary>
	/// Send private/public error message to eventhub
	/// </summary>
	/// <param name="topic"></param>
	/// <param name="component"></param>
	/// <param name="title"></param>
	/// <param name="description"></param>
	/// <param name="parameters"></param>
	/// <returns></returns>
	public async Task SendErrorAsync(MESSAGEHUBENUMS.Topic topic
								   , String component
								   , String title
								   , String description
								   , params Tuple<String, String>[] parameters)
	{
		var message = new MESSAGEHUBMODELS.Message
		{
			Component = component,
			Title = title,
			Description = description,
			LogLevel = MESSAGEHUBENUMS.LogLevel.Error,
			Sublevel = 1,
			Parameters = parameters
		};

		await this.SendMessageAsync(new List<MESSAGEHUBENUMS.Topic>() { topic }, message).ConfigureAwait(false);
	}

	/// <summary>
	/// Send public information message to eventhub on multiple topics
	/// </summary>
	/// <param name="topics"></param>
	/// <param name="message"></param>
	/// <returns></returns>
	public async Task SendMessageAsync(IEnumerable<MESSAGEHUBENUMS.Topic> topics
									  , MESSAGEHUBMODELS.Message message)
	{
		if (AppSettings.IsDataAccessTierNotificationEnabled)
		{
			message = message ?? new MESSAGEHUBMODELS.Message();

			message.Service = AppSettings.AppName;
			message.Tier = MESSAGEHUBENUMS.Tier.DALREPO;
			message.RaisedOn = DateTime.UtcNow;
			message.RaisedBy = AppSettings.CurrentUserEmail.ToString();
			message.Level = 4;
			message.Operation = MESSAGEHUBENUMS.Operation.Begin;

			await this._messageHub.SendMessageAsync(topics, message).ConfigureAwait(false);
		}
	}

	#endregion

	#endregion

	#region Protected Methods - Handle DB Exception

	/// <summary>
    /// Handles the database exception.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="exception">The exception.</param>
    /// <returns></returns>
     protected async Task<SHAREDKERNALLIB.WebShopException> HandleDBException(String message, Exception exception = null)
	 {
		SHAREDKERNALLIB.WebShopException webShopException = null;

		if (exception != null)
		{
			String innerExceptionMessage = null;

			if (exception.InnerException != null)
			{
				innerExceptionMessage = exception.InnerException.Message;
			}
			webShopException = new SHAREDKERNALLIB.WebShopException(message, exception);
		}

		return await Task.FromResult(webShopException).ConfigureAwait(false);
	 }
	
	#endregion

}
