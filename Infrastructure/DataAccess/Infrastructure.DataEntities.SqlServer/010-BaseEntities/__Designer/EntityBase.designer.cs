#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion  

namespace Accenture.WebShop.Infrastructure.DataEntities.SqlServer;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents EntityBase Class.
/// </summary>
/// <remarks>
/// Represents EntityBase Class.
/// </remarks>
public partial class EntityBase<T> : COREAPPDENTINTERFACES.IEntity<T> where T : class
{
	#region Fields

	/// <summary>
	/// The message hub
	/// </summary>
	protected readonly MESSAGEHUBINTERFACES.IMessageHub _messageHub;

	#endregion

	#region Properties

	/// <summary>
    /// Gets or sets the logger.
    /// </summary>
    /// <value>
    /// The logger.
    /// </value>
	protected MSLOGGING.ILogger Logger { get; init; }	

	/// <summary>
    /// Gets or sets the database context.
    /// </summary>
    /// <value>
    /// The database context.
    /// </value>
	protected DbContext DbContext { get; init; }	

	/// <summary>
    /// Gets or sets the database context.
    /// </summary>
    /// <value>
    /// The database context.
    /// </value>
	protected DbContext QueryDbContext { get; init; }

	#endregion

	#region Constructor

	/// <summary>
    /// Initializes EntityBase class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <param name="logger">The logger.</param>
	/// <param name="messageHub">The notifier Sender</param>
    public EntityBase(DbContext dbContext, DbContext queryDbContext,  MSLOGGING.ILogger logger, MESSAGEHUBINTERFACES.IMessageHub messageHub)
	{
		this.DbContext = dbContext;
		this.QueryDbContext = queryDbContext;
		this.Logger = logger;
		this._messageHub = messageHub;
	}	

	#endregion

	#region Public Methods

	/// <summary>
    /// Adds the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
	public virtual async Task AddAsync(T entity)
	{
		await DbContext.AddAsync<T>(entity).ConfigureAwait(false);
	}	

	/// <summary>
    /// Merges the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
	public virtual async Task<COREDOMAINDATAMODELS.MergeResult> MergeAsync(T entity)
	{
		var mergeResult = new COREDOMAINDATAMODELS.MergeResult() { RequestCount = 1 };

		return await Task.FromResult(mergeResult).ConfigureAwait(false);
	}	

	/// <summary>
    /// Saves the asynchronous.
    /// </summary>
	public virtual async Task SaveAsync()
	{
		await DbContext.SaveChangesAsync().ConfigureAwait(false);
	}	

	/// <summary>
    /// Updates the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
	public Task UpdateAsync(T entity)
	{
		return Task.FromResult(DbContext.Update(entity));
	}	

	/// <summary>
    /// Gets Entity.
    /// </summary>
    /// <returns></returns>
	public virtual IQueryable<T> AsIQuerable()
	{
		return QueryDbContext.Set<T>().AsNoTracking();
	}

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
			message.Tier = MESSAGEHUBENUMS.Tier.DAL;
			message.RaisedOn = DateTime.UtcNow;
			message.RaisedBy = AppSettings.CurrentUserEmail.ToString();
			message.Level = 5;
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

		Logger.LogFault(message, "DBException", exception, SHAREDKERNALLIB.ApplicationTier.DataAccess);

		return await Task.FromResult(webShopException).ConfigureAwait(false);
	}

	#endregion

	#region AuditField Command Param

	/// <summary>
	/// Audits the field command parameter.
	/// </summary>
	/// <param name="correlationUId">The correlation u identifier.</param>
	/// <param name="command">The command.</param>
	/// <param name="currentUserEmail">The current user email.</param>
	public void AuditFieldCommandParam(Guid? correlationUId, SqlCommand command, String currentUserEmail, String parentProcessName)
	{
		Guid identifier;

		if( !Guid.TryParse(currentUserEmail, out identifier))
		{
			identifier = AppSettings.CurrentUserEmail;
		}

		command.Parameters.Add("@ExecutedByUser", SqlDbType.UniqueIdentifier).Value = identifier;
		command.Parameters.Add("@ExecutedByApp", SqlDbType.NVarChar).Value = AppSettings.AppName;
		command.Parameters.Add("@ExecutedOn", SqlDbType.DateTime).Value = DateTime.UtcNow;
		command.Parameters.Add("@CorrelationUId", SqlDbType.UniqueIdentifier).Value = correlationUId == null ? Guid.NewGuid() : correlationUId;
		command.Parameters.Add("@ParentProcessName", SqlDbType.NVarChar).Value = parentProcessName;
		command.Parameters.Add("@ProcessLogs", SqlDbType.NVarChar, -1);
		command.Parameters["@ProcessLogs"].Direction = ParameterDirection.Output;
		command.Parameters.Add("@ReturnValue", SqlDbType.Int);
		command.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
	}
		#endregion
}
