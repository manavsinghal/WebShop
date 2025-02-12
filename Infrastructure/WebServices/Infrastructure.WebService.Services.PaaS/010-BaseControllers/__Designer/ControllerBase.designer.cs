namespace Accenture.WebShop.Infrastructure.WebService.Services.PaaS;

#region Namespace References    

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents Base Controller
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[Authorize]
[EnableQuery(MaxExpansionDepth = 25)]
public partial class ControllerBase<T> : Microsoft.AspNetCore.Mvc.ControllerBase
{
	#region Fields

	/// <summary>
	/// Logger instance
	/// </summary>
	protected ILogger<T> Logger { get; init; }

	/// <summary>
	/// _messageHub
	/// </summary>
	protected readonly MESSAGEHUBINTERFACES.IMessageHub _messageHub;

	#endregion

	#region Constructors

	/// <summary>
	/// Represents Controller Base
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="messageHub"></param>
	public ControllerBase(ILogger<T> logger, MESSAGEHUBINTERFACES.IMessageHub messageHub)
	{
		this.Logger = logger;
		this._messageHub = messageHub;
	}

	#endregion

	#region Protected Methods

	/// <summary>
	/// Gets the success response.
	/// </summary>
	/// <typeparam name="R"></typeparam>
	/// <param name="value">The value.</param>
	/// <returns>Http response message</returns>
	protected virtual IActionResult GetSuccessResponse<R>(R value)
	{
		return Ok(value);
	}

	/// <summary>
	/// Gets the error response.
	/// </summary>
	/// <param name="faults">The faults.</param>
	/// <returns></returns>
	protected virtual IActionResult GetErrorResponse(FaultCollection faults)
	{
		faults.ToList().ForEach(f => f.Message = CustomMessage(!String.IsNullOrEmpty(f.StackTrace) ? f.StackTrace : f.Message));
		
		var httpStatusCode = new HttpStatusCode();

		var httpStatusCodes = faults.Select(x => x.FaultType).Distinct();

		if (httpStatusCodes.Count() == 1)
		{
			httpStatusCode = GetStatusCode(httpStatusCodes.FirstOrDefault());
		}
		else
		{
			httpStatusCode = HttpStatusCode.BadRequest;
		}

		var newFaults = faults.Select(m => (FaultBase)m);

		return StatusCode((int)httpStatusCode, newFaults.ToJson());
	}

	/// <summary>
	/// Gets the error response.
	/// </summary>
	/// <param name="fault">The fault.</param>
	/// <returns></returns>
	protected virtual IActionResult GetErrorResponse(Fault fault)
	{
		FaultBase baseFault = fault;
		HttpStatusCode httpStatusCode = GetStatusCode(fault.FaultType);
		return StatusCode((int)httpStatusCode, fault.ToJson());
	}

	/// <summary>
	/// Gets the error response.
	/// </summary>
	/// <param name="faultType">The fault type.</param>
	/// <returns></returns>
	protected virtual HttpStatusCode GetStatusCode(FaultType faultType)
	{
		HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

		if (faultType == FaultType.Exception)
		{
			httpStatusCode = HttpStatusCode.InternalServerError;
		}
		if (faultType == FaultType.NotFound)
		{
			httpStatusCode = HttpStatusCode.NotFound;
		}
		if (faultType == FaultType.ValidationError)
		{
			httpStatusCode = HttpStatusCode.BadRequest;
		}
		if (faultType == FaultType.Conflict)
		{
			httpStatusCode = HttpStatusCode.InternalServerError;
		}
		return httpStatusCode;
	}

	/// <summary>
	/// Gets the error response.
	/// </summary>
	/// <param name="ex">The ex.</param>
	/// <returns>Http response message</returns>
	protected virtual IActionResult GetErrorResponse(Exception ex)
	{
		var faults = new FaultCollection();

		faults.Add(CreateExceptionFault(ex));

		var response = GetErrorResponse(faults);

		return response;
	}

	/// <summary>
	/// Creates the exception fault.
	/// </summary>
	/// <param name="ex">The ex.</param>
	/// <returns>Fault</returns>
	protected static Fault CreateExceptionFault(Exception ex)
	{
		Fault fault = null;

		if (ex != null)
		{
			fault = new Fault
			{
				Title = "Exception has occurred",
				Message = ex.Message,
				FaultType = FaultType.Exception,
				Severity = Severity.Critical,
				ApplicationTier = ApplicationTier.Service,
			};

			if (ex.InnerException != null)
			{
				//fault.StackTrace = ex.InnerException.ToString();
			}
		}

		return fault;
	}

	/// <summary>
	/// Handles the error.
	/// </summary>
	/// <param name="message">The message.</param>
	/// <param name="title">The title.</param>
	/// <param name="exception">The exception.</param>
	/// <returns></returns>
	protected virtual async Task<SHAREDKERNALLIB.WebShopException> HandleError(String message, String? title, Exception? exception = null)
	{
		SHAREDKERNALLIB.WebShopException handleException = null;

		if (exception != null)
		{
			String innerExceptionMessage = null;

			if (exception.InnerException != null)
			{
				innerExceptionMessage = exception.InnerException.Message;
			}
			
			handleException = new SHAREDKERNALLIB.WebShopException(message, exception);
		}

		Logger.LogError(exception.Message, $"{SHAREDKERNALRESX.WebShop.Exception}", nameof(title), this.GetType());

		return await Task.FromResult(handleException).ConfigureAwait(false);
	}

	/// <summary>
	/// Returns the user's name from context
	/// </summary>
	/// <returns></returns>
	protected virtual String GetUserContext()
	{
		var isADIntegrationEnabled = SHAREDKERNALLIB.AppSettings.IsADIntegrationEnabled;

		if (isADIntegrationEnabled)
		{
			if (String.IsNullOrEmpty(User.Identity.Name))
			{				
				var claimPrincipal = (User as ClaimsPrincipal);

				if (claimPrincipal != null)
				{
					var claim = claimPrincipal.FindFirst("appid");

					return claim != null ? claim.Value : "SYSTEM";
				}
			}

			return User.Identity.Name;
		}
		else
		{
			return Request.Headers["Email"];
		}
	}

	/// <summary>
	/// Get the request header value
	/// </summary>
	/// <returns>Request header value</returns>
	protected String GetHeaderValue(String headerName)
	{
		var headerValue = String.Empty;

		StringValues headerValues;

		if (Request.Headers.TryGetValue(headerName, out headerValues))
		{
			headerValue = headerValues.FirstOrDefault();
		}

		return headerValue;
	}

	/// <summary>
	/// Returns the correlationUId from context
	/// </summary>
	/// <returns></returns>
	protected virtual Guid GetCorrelationUId()
	{
		var correlationUId = Guid.NewGuid();

		var headerCorrelationUId = this.GetHeaderValue("CorrelationUId");

		if (!String.IsNullOrEmpty(headerCorrelationUId) && Guid.Parse(headerCorrelationUId) != Guid.Empty)
		{
			correlationUId = Guid.Parse(headerCorrelationUId);
		}
		else
		{
			var queryCorrelationUId = HttpContext.Request.Query["CorrelationUId"].ToString();

			if (!String.IsNullOrEmpty(queryCorrelationUId) && Guid.Parse(queryCorrelationUId) != Guid.Empty)
			{
				correlationUId = Guid.Parse(queryCorrelationUId);
			}
		}

		return correlationUId;
	}

	/// <summary>
	/// Returns the request content 
	/// </summary>
	/// <returns></returns>
	protected virtual String GetReplacedContent(String content, Guid correlationUId)
	{
		var regex = "\"CorrelationUId\"\\s*:\\s*\"[0-9a-fA-F]{8}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{12}\""; 
		
		if (Regex.IsMatch(content, regex, RegexOptions.IgnoreCase | RegexOptions.Compiled))
		{
			content = Regex.Replace(content, regex, $"\"CorrelationUId\":\"{correlationUId.ToString()}\"");
		}

		return content;
	}

	/// <summary>
	/// Gets the custom message
	/// </summary>
	/// <param name="Message"></param>
	/// <returns></returns>
	protected static String CustomMessage(String Message)
	{
		var message = Message.ToUpper();

		switch (message)
		{
			case var notNull when message.Contains(COREDOMAINDATAMODELSDOMAINENUM.CustomErrorMessage.NotNullConstraint):
				message = SHAREDKERNALRESX.WebShop.NotNullConstraintMessage;				
				break;
			case var uniqueKey when message.Contains(COREDOMAINDATAMODELSDOMAINENUM.CustomErrorMessage.UniqueKeyViolation):
				message = SHAREDKERNALRESX.WebShop.UniqueKeyViolationMessage;				
				break;
			case var checkCon when message.Contains(COREDOMAINDATAMODELSDOMAINENUM.CustomErrorMessage.CheckConstraint):
				message = SHAREDKERNALRESX.WebShop.CheckConstraintMessage;
				break;
			case var primaryKey when message.Contains(COREDOMAINDATAMODELSDOMAINENUM.CustomErrorMessage.PrimaryKeyViolation):
				message = SHAREDKERNALRESX.WebShop.PrimaryKeyViolationMessage;
				break;
			case var foriegnKey when message.Contains(COREDOMAINDATAMODELSDOMAINENUM.CustomErrorMessage.ForiegnKeyViolation):
				message = SHAREDKERNALRESX.WebShop.ForiegnKeyViolationMessage;
				break;
			case var updateCon when message.Contains(COREDOMAINDATAMODELSDOMAINENUM.CustomErrorMessage.UpdateConcurrencyException):
				message = SHAREDKERNALRESX.WebShop.ConcurrencyExceptionMessage;
				break;
			case var deleteExc when message.Contains(COREDOMAINDATAMODELSDOMAINENUM.CustomErrorMessage.DeleteException):
				message = SHAREDKERNALRESX.WebShop.DeleteExceptionMessage;
				break;
			case var fieldLengthExc when message.Contains(COREDOMAINDATAMODELSDOMAINENUM.CustomErrorMessage.FieldLengthException):
				message = SHAREDKERNALRESX.WebShop.FieldLengthExceptionMessage;
				break;
			default:
				message = Message;
				break;
		}

		return message;
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
	protected async Task SendPrivateInfoAsync(MESSAGEHUBENUMS.Topic topic
										  , String component
										  , String title
										  , String description
										  , params Tuple<String, String>[] parameters)
	{
		if (SHAREDKERNALLIB.AppSettings.IsDataAccessTierNotificationEnabled &&
			SHAREDKERNALLIB.AppSettings.IsPrivateMethodNotificationEnabled)
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
	protected async Task SendInfoAsync(MESSAGEHUBENUMS.Topic topic
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
	protected async Task SendErrorAsync(MESSAGEHUBENUMS.Topic topic
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
	protected async Task SendMessageAsync(IEnumerable<MESSAGEHUBENUMS.Topic> topics
									  , MESSAGEHUBMODELS.Message message)
	{
		if (SHAREDKERNALLIB.AppSettings.IsDataAccessTierNotificationEnabled)
		{
			message = message ?? new MESSAGEHUBMODELS.Message();

			message.Service = SHAREDKERNALLIB.AppSettings.AppName;
			message.Tier = MESSAGEHUBENUMS.Tier.SERVICE;
			message.RaisedOn = DateTime.UtcNow;
			message.RaisedBy = SHAREDKERNALLIB.AppSettings.CurrentUserEmail.ToString();
			message.Level = 2;

			await this._messageHub.SendMessageAsync(topics, message).ConfigureAwait(false);
		}
	}

	#endregion

	#endregion

	#region Public Methods

	#endregion
	
}
