namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS;

#region Namespace References    

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents Base Controller
/// </summary>
[Authorize(Scopes = new[] { Scopes.FunctionsAccess },
		   UserRoles = new[] { UserRoles.User, UserRoles.Admin },
		   AppRoles = new[] { AppRoles.AccessAllFunctions })]
/// <summary>
/// Represents Base Controller
/// </summary>
public partial class FunctionBase<T>
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

	/// <summary>
	/// _httpContextAccessor
	/// </summary>
	private readonly IFunctionContextAccessor _functionContextAccessor;

	/// <summary>
	/// _httpRequestData
	/// </summary>
	private readonly HttpRequestData _httpRequestData;

	#endregion

	#region Constructors

	/// <summary>
	/// Represents Controller Base
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="messageHub"></param>
	public FunctionBase(ILogger<T> logger
													   , MESSAGEHUBINTERFACES.IMessageHub messageHub
													, IFunctionContextAccessor functionContextAccessor)
	{
		this.Logger = logger;
		this._messageHub = messageHub;
		this._functionContextAccessor = functionContextAccessor;
		this._httpRequestData = GetHttpRequestData();
	}

	#endregion

	#region Protected Methods

	/// <summary>
    /// Get the request header value
    /// </summary>
    /// <returns>Request header value</returns>
    protected String GetHeaderValue(String headerName)
    {
        var headerValue = String.Empty;

        if (_httpRequestData.Headers.TryGetValues(headerName, out var headerValues))
		{
			headerValue = headerValues.FirstOrDefault();
		}

		return headerValue;
    }

	/// <summary>
    /// Gets QueryString Value
    /// </summary>
    /// <returns>Request query name</returns>
	protected String GetQueryStringValue(String name)
    {
        var queryValue = String.Empty;

		var query = System.Web.HttpUtility.ParseQueryString(_httpRequestData.Url.Query);

		var dictionaryEntries = ConvertToDictionary(query);

		if (dictionaryEntries.ContainsKey(name))
		{
			foreach (DictionaryEntry value in dictionaryEntries)
			{
				if (String.Equals(value.Key.ToString(), name, StringComparison.OrdinalIgnoreCase))
				{
					queryValue = value.Value.ToString();
					break;
				}
			}
		}

		return queryValue;       
    }

	/// <summary>
    /// Gets Request Body
    /// </summary>
    /// <returns>Request body value</returns>
    protected async Task<String> GetRequestBody()
    {
        using (var reader = new StreamReader(_httpRequestData.Body))
		{
			var request = await reader.ReadToEndAsync();
			return request;
		}
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
			var queryCorrelationUId = GetQueryStringValue("CorrelationUId");

			if (!String.IsNullOrEmpty(queryCorrelationUId) && Guid.Parse(queryCorrelationUId) != Guid.Empty)
			{
				correlationUId = Guid.Parse(queryCorrelationUId);
			}
		}

		return correlationUId;
    }

	/// <summary>
	/// Gets the error response.
	/// </summary>
	/// <param name="faults">The faults.</param>
	/// <returns></returns>
	protected virtual HttpResponseData GetErrorResponse(FaultCollection faults)
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

		var httpResponseData = _httpRequestData.CreateResponse(httpStatusCode);

		httpResponseData.WriteAsJsonAsync(newFaults);

		return httpResponseData;
	}

	/// <summary>
	/// Gets the error response.
	/// </summary>
	/// <param name="fault">The fault.</param>
	/// <returns></returns>
	protected virtual HttpResponseData GetErrorResponse(Fault fault)
	{
		FaultBase baseFault = fault;
		HttpStatusCode httpStatusCode = GetStatusCode(fault.FaultType);

		var httpResponseData = _httpRequestData.CreateResponse(httpStatusCode);

		httpResponseData.WriteAsJsonAsync(fault);

		return httpResponseData;
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
	/// Returns the user's name from context
	/// </summary>
	/// <returns></returns>
	protected virtual String GetUserContext()
    {
        var isADIntegrationEnabled = SHAREDKERNALLIB.AppSettings.IsADIntegrationEnabled;

		if (isADIntegrationEnabled)
		{
			JwtPrincipalFeature jwtPrincipalFeature = _functionContextAccessor.FunctionContext.Features.Get<JwtPrincipalFeature>();

			var claimPrincipal = jwtPrincipalFeature.Principal;

			var claim = claimPrincipal.FindFirst("appid");

			return claim != null ? claim.Value : "SYSTEM";
		}
		else
		{
			return GetHeaderValue("Email");
		}
    }

	/// <summary>
	/// Gets the success response.
	/// </summary>
	/// <typeparam name="R"></typeparam>
	/// <param name="value">The value.</param>
	/// <returns>Http response message</returns>
	protected virtual HttpResponseData GetSuccessResponse<R>(R value)
    {	
		var httpResponseData = _httpRequestData.CreateResponse(HttpStatusCode.OK);

		httpResponseData.WriteAsJsonAsync(value);

		return httpResponseData;
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
	/// Gets the error response.
	/// </summary>
	/// <param name="ex">The ex.</param>
	/// <returns>Http response message</returns>
	protected virtual HttpResponseData GetErrorResponse(Exception ex)
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

	#endregion

	#region Private Methods

	private static StringDictionary ConvertToDictionary(NameValueCollection valueCollection)
	{
		var dictionary = new StringDictionary();
		foreach (var key in valueCollection.AllKeys)
		{
			if (key != null)
			{
				dictionary.Add(key.ToLowerInvariant(), valueCollection[key]);
			}
		}
		return dictionary;
	}

	private HttpRequestData GetHttpRequestData()
	{
		var keyValuePair = _functionContextAccessor.FunctionContext.Features.SingleOrDefault(f => f.Key.Name == "IFunctionBindingsFeature");

		var functionBindingsFeature = keyValuePair.Value;

		var type = functionBindingsFeature.GetType();

		var inputData = type.GetProperties().Single(p => p.Name == "InputData").GetValue(functionBindingsFeature) as IReadOnlyDictionary<string, object>;

		var httpRequestData = inputData?.Values.SingleOrDefault(o => o is HttpRequestData) as HttpRequestData;

		return httpRequestData;

	}

	#endregion
}
