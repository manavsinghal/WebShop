#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.SharedKernal.Libraries;

#region Namespace Reference

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead 

#endregion

/// <summary>
/// Represents ILoggerExtension class   
/// </summary>
public static class LoggerExtensions
{
	#region Fields

	private static readonly String _empty = "\"\"";

	private static readonly Lazy<String> _logTemplate = new(() =>
	{
		return SHAREDKERNALRESX.WebShop.LogTemplate;
	});

	#endregion

	#region Properties

	#endregion

	#region Constructor

	#endregion

	#region Public Methods

	#region LogProcessInfo

	/// <summary>
	/// Represents LogInfo method.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="message"></param>
	/// <param name="title"></param>
	public static void LogInfo(this ILogger logger, String message, String? title = null)
	{
		LogInternal(logger, LogLevel.Information, message, title);
	}

	/// <summary>
	/// Represents LogInfo method.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="message"></param>
	/// <param name="title"></param>
	/// <param name="type"></param>
	public static void LogInfo(this ILogger logger, String message, String? title = null, Type? type = null)
	{
		LogInternal(logger, LogLevel.Information, message, title, null, type);
	}

	/// <summary>
	/// Represents LogDebug method.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="message"></param>
	/// <param name="title"></param>
	public static void LogDebug(this ILogger logger, String message, String? title = null)
	{
		LogInternal(logger, LogLevel.Debug, message, title);
	}

	/// <summary>
	/// Represents LogInfo method.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="message"></param>
	/// <param name="title"></param>
	/// <param name="type"></param>
	/// <param name="correlationUId"></param>
	public static void LogInfo(this ILogger logger, String message, String? title = null, Type? type = null, Guid? correlationUId = null)
	{
		LogInternal(logger, LogLevel.Information, message, title, null, type, correlationUId);
	}

	/// <summary>
	/// Represents LogInfo method.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="logLevel"></param>
	/// <param name="message"></param>
	/// <param name="title"></param>
	/// <param name="type"></param>
	/// <param name="correlationUId"></param>
	/// <param name="component"></param>
	public static void LogInfo(this ILogger logger, LogLevel logLevel, String message, String? title = null, Type? type = null, Guid? correlationUId = null, String? component = null)
	{
		LogInternal(logger, logLevel, message, title, null, type, correlationUId, component);
	}

	/// <summary>
	/// Logs the information.
	/// </summary>
	/// <param name="logger">The logger.</param>
	/// <param name="message">The message.</param>
	/// <param name="title">The title.</param>
	/// <param name="type">The type.</param>
	/// <param name="component">The component.</param>
	public static void LogInfo(this ILogger logger, String message, String? title = null, Type? type = null, ApplicationTier? component = null)
	{
		LogInternal(logger, LogLevel.Information, message, title, null, type, null, Convert.ToString(component));
	}

	#endregion

	#region LogProcessWarning

	/// <summary>
	/// Represents LogWarning method.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="message"></param>
	/// <param name="title"></param>
	public static void LogWarning(this ILogger logger, String message, String? title = null)
	{
		LogInternal(logger, LogLevel.Warning, message, title);
	}

	/// <summary>
	/// Represents LogWarning method.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="message"></param>
	/// <param name="title"></param>
	/// <param name="type"></param>
	public static void LogWarning(this ILogger logger, String message, String? title = null, Type? type = null)
	{
		LogInternal(logger, LogLevel.Warning, message, title, null, type);
	}

	/// <summary>
	/// Represents LogWarning method.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="message"></param>
	/// <param name="title"></param>
	/// <param name="type"></param>
	/// <param name="correlationUId"></param>
	public static void LogWarning(this ILogger logger, String message, String? title = null, Type? type = null, Guid? correlationUId = null)
	{
		LogInternal(logger, LogLevel.Warning, message, title, null, type, correlationUId);
	}

	/// <summary>
	/// Represents LogWarning method.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="logLevel"></param>
	/// <param name="message"></param>
	/// <param name="title"></param>
	/// <param name="type"></param>
	/// <param name="correlationUId"></param>
	/// <param name="component"></param>
	public static void LogWarning(this ILogger logger, LogLevel logLevel, String message, String? title = null, Type? type = null, Guid? correlationUId = null, String? component = null)
	{
		LogInternal(logger, logLevel, message, title, null, type, correlationUId, component);
	}

	/// <summary>
	/// Logs the warning.
	/// </summary>
	/// <param name="logger">The logger.</param>
	/// <param name="message">The message.</param>
	/// <param name="title">The title.</param>
	/// <param name="type">The type.</param>
	/// <param name="component">The component.</param>
	public static void LogWarning(this ILogger logger, String message, String? title = null, Type? type = null, ApplicationTier? component = null)
	{
		LogInternal(logger, LogLevel.Warning, message, title, null, type, null, Convert.ToString(component));
	}

	#endregion

	#region LogDiagnosticsInfo

	/// <summary>
	/// Represents LogDiagnosticsInfo method.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="message"></param>
	/// <param name="title"></param>
	/// <param name="parameters"></param>
	public static void LogDiagnosticsInfo(this ILogger logger, String message, String? title = null, params Object[] parameters)
	{
		LogInternal(logger, LogLevel.Debug, message, title, null, null, null, _empty, null, parameters);
	}

	/// <summary>
	/// Represents LogDiagnosticsInfo method.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="message"></param>
	/// <param name="title"></param>
	/// <param name="correlationUId"></param>
	/// <param name="parameters"></param>
	public static void LogDiagnosticsInfo(this ILogger logger, String message, String? title = null, Guid? correlationUId = null, params Object[] parameters)
	{
		LogInternal(logger, LogLevel.Debug, message, title, null, null, correlationUId, _empty, null, parameters);
	}

	/// <summary>
	/// Represents LogDiagnosticsInfo method.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="message"></param>
	/// <param name="logLevel"></param>
	/// <param name="title"></param>
	/// <param name="correlationUId"></param>
	/// <param name="type"></param>
	/// <param name="component"></param>
	/// <param name="parameters"></param>
	public static void LogDiagnosticsInfo(this ILogger logger, String message, LogLevel logLevel, String? title = null, Guid? correlationUId = null, Type? type = null, String? component = null, params Object[] parameters)
	{
		LogInternal(logger, logLevel, message, title, null, type, correlationUId, component, null, parameters);
	}

	#endregion

	#region LogFaults

	/// <summary>
	/// Represents LogFault method.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="message"></param>
	/// <param name="title"></param>
	/// <param name="faults"></param>
	public static void LogFault(this ILogger logger, String message, String? title = null, FaultCollection? faults = null)
	{
		LogInternal(logger, LogLevel.Error, message, title, null, null, null, _empty, faults);
	}

	/// <summary>
	/// Represents LogFault method.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="message"></param>
	/// <param name="title"></param>
	/// <param name="faults"></param>
	/// <param name="correlationUId"></param>
	public static void LogFault(this ILogger logger, String message, String? title = null, FaultCollection? faults = null, Guid? correlationUId = null)
	{
		LogInternal(logger, LogLevel.Error, message, title, null, null, correlationUId, _empty, faults);
	}

	/// <summary>
	/// Represents LogFault method.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="message"></param>
	/// <param name="title"></param>
	/// <param name="faults"></param>
	/// <param name="correlationUId"></param>
	/// <param name="type"></param>
	/// <param name="component"></param>
	public static void LogFault(this ILogger logger, String message, String? title = null, FaultCollection? faults = null, Guid? correlationUId = null, Type? type = null, String? component = null)
	{
		LogInternal(logger, LogLevel.Error, message, title, null, type, correlationUId, component, faults);
	}

	/// <summary>
	/// Logs the fault.
	/// </summary>
	/// <param name="logger">The logger.</param>
	/// <param name="message">The message.</param>
	/// <param name="title">The title.</param>
	/// <param name="faults">The faults.</param>
	/// <param name="component">The component.</param>
	public static void LogFault(this ILogger logger, String message, String? title = null, FaultCollection? faults = null, ApplicationTier? component = null)
	{
		LogInternal(logger, LogLevel.Error, message, title, null, null, null, Convert.ToString(component), faults);
	}

	/// <summary>
	/// Represents LogFault method.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="message"></param>
	/// <param name="title"></param>
	/// <param name="exception"></param>
	public static void LogFault(this ILogger logger, String message, String? title = null, Exception? exception = null)
	{
		LogInternal(logger, LogLevel.Information, message, title, exception);
	}

	/// <summary>
	/// Logs the fault.
	/// </summary>
	/// <param name="logger">The logger.</param>
	/// <param name="message">The message.</param>
	/// <param name="title">The title.</param>
	/// <param name="exception">The exception.</param>
	/// <param name="component">The component.</param>
	public static void LogFault(this ILogger logger, String message, String? title = null, Exception? exception = null, ApplicationTier? component = null)
	{
		LogInternal(logger, LogLevel.Information, message, title, exception, null, null, Convert.ToString(component));
	}

	/// <summary>
	/// Represents LogFault method.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="message"></param>
	/// <param name="title"></param>
	/// <param name="exception"></param>
	/// <param name="type"></param>
	/// <param name="correlationUId"></param>
	/// <param name="component"></param>
	public static void LogFault(this ILogger logger, String message, String? title = null, Exception? exception = null, Type? type = null, Guid? correlationUId = null, String? component = null)
	{
		LogInternal(logger, LogLevel.Information, message, title, exception, type, correlationUId, component);
	}

	#endregion

	/// <summary>
	/// Represents Log Trace Method
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="logLevel"></param>
	/// <param name="message"></param>
	/// <param name="title"></param>
	/// <param name="type"></param>
	/// <param name="correlationUId"></param>
	/// <param name="component"></param>
	public static void LogTrace(this ILogger logger, LogLevel logLevel, String message, String? title = null, Type? type = null, Guid? correlationUId = null, String? component = null)
	{
		LogInternal(logger, logLevel, message, title, null, type, correlationUId, component);
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Represents JsonSerializerObject method.
	/// </summary>
	/// <param name="obj"></param>
	/// <returns></returns>
	private static String JsonSerializerObject(Object obj)
	{
		return JsonSerializer.Serialize(obj);
	}

	/// <summary>
	/// Represents GetFaultString method.
	/// </summary>
	/// <param name="faults"></param>
	/// <returns></returns>
	private static String GetFaultString(Collection<Fault> faults)
	{
		var sb = new StringBuilder();

		_ = sb.Append('[');
		foreach (var fault in faults)
		{
			var faultFormat = (FormattableString)$"{fault.ToJson()},";
			_ = sb.Append(faultFormat.ToString(CultureInfo.InvariantCulture));
		}

		_ = sb.Append(']');

		return sb.ToString();
	}

	/// <summary>
	/// Represents FillTemplate method.
	/// </summary>
	/// <param name="message"></param>
	/// <param name="logType"></param>
	/// <param name="title"></param>
	/// <param name="correlationUId"></param>
	/// <param name="exceptionMessage"></param>
	/// <param name="jsonStacktrace"></param>
	/// <param name="assemblyNamespace"></param>
	/// <param name="component"></param>
	/// <returns></returns>
	[ExcludeFromCodeCoverage]
	private static String FillTemplate(String message, String? logType = null, String? title = null, Guid? correlationUId = null, String? exceptionMessage = null
							, String? jsonStacktrace = null, String? assemblyNamespace = null, String? component = null)

	{
		return _logTemplate.Value
							  .Replace("{DateTime}", DateTime.UtcNow.ToString("s"))
							  .Replace("{MessageText}", message)
							  .Replace("{logtype}", !String.IsNullOrEmpty(logType) ? logType : String.Empty)
							  .Replace("{title}", !String.IsNullOrEmpty(title) ? title : String.Empty)
							  .Replace("{correlationUId}", correlationUId.HasValue ? correlationUId.ToString() : String.Empty)
							  .Replace("{exceptionMessage}", !String.IsNullOrEmpty(exceptionMessage) ? exceptionMessage : String.Empty)
							  .Replace("{jsonStacktrace}", !String.IsNullOrEmpty(jsonStacktrace) ? jsonStacktrace : String.Empty)
							  .Replace("{assemblyNamespace}", !String.IsNullOrEmpty(assemblyNamespace) ? assemblyNamespace : String.Empty)
							  .Replace("{component}", !String.IsNullOrEmpty(component) ? component : String.Empty);

	}

	/// <summary>
	/// Represents LogInternal method.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="logLevel"></param>
	/// <param name="message"></param>
	/// <param name="title"></param>
	/// <param name="exception"></param>
	/// <param name="type"></param>
	/// <param name="correlationUId"></param>
	/// <param name="component"></param>
	/// <param name="faults"></param>
	/// <param name="parameters"></param>
	private static void LogInternal(ILogger logger, LogLevel logLevel, String? message = "", String? title = null, Exception? exception = null, Type? type = null, Guid? correlationUId = null
						, String? component = null, FaultCollection? faults = null, params Object[] parameters)
	{

		var jsonMessage = String.Empty;
		var typeText = type != null ? type.FullName : String.Empty;
		var exceptionMessage = _empty;
		var jsonStacktrace = _empty;

		if ((parameters != null && parameters.Length != 0) || exception != null)
		{
			var titleFormatString = (FormattableString)$@"Fault: {title}";

			if (exception != null)
			{
				logLevel = LogLevel.Error;
				exceptionMessage = JsonSerializerObject(exception.Message);
				jsonStacktrace = JsonSerializerObject(exception.StackTrace!);
				jsonMessage = JsonSerializerObject(message!);
				if (exception.InnerException != null)
				{
					exceptionMessage = $"{exceptionMessage},\"InnerExceptionMessage\" : \"{JsonSerializerObject(exception.InnerException.Message)}\"";
					jsonStacktrace = $"{jsonStacktrace},\"InnerStackTrace\" : \"{JsonSerializerObject(exception.InnerException.StackTrace!)}\"";
				}
			}

			if (parameters != null && parameters.Length != 0)
			{
				if (correlationUId != null)
				{
					title = titleFormatString.ToString();
				}

				jsonMessage = "{";
				for (var param = 0; param < parameters.Length; param++)
				{
					jsonMessage = String.Concat(jsonMessage, $"\"Parameter{param + 1}\": \"{parameters[param]}\",");
				}

				jsonMessage = String.Concat(jsonMessage.Remove(jsonMessage.Length - 1), "}");
			}
		}
		else if (faults != null)
		{
			jsonMessage = JsonSerializerObject(message!);
			var faultMessage = GetFaultString(faults!);
			exceptionMessage = JsonSerializerObject(faultMessage);
			var titleFormat = (FormattableString)$@"Fault: {title}";
			title = titleFormat.ToString();
		}
		else
		{
			var messageFormat = type != null ? ((FormattableString)$@"{(typeText)}: {message}") : ((FormattableString)$@"{(message)}");
			jsonMessage = JsonSerializerObject(messageFormat.ToString(CultureInfo.InvariantCulture));
		}

		var formattedMessage = FillTemplate(jsonMessage, logLevel.ToString(), title, correlationUId, exceptionMessage, jsonStacktrace, typeText, component);
		logger.Log(logLevel, message: formattedMessage);

	}
	#endregion
}

/// <summary>
/// Represents Log Stage Names.
/// </summary>
/// <remarks>
/// Represents Log Stage Names.
/// </remarks>
public enum Stage
{
	/// <summary>
	/// Start
	/// </summary>
	Start = 0,

	/// <summary>
	/// InProgress
	/// </summary>
	InProgress = 1,

	/// <summary>
	/// End
	/// </summary>
	End = 2,
}

/// <summary>
/// Represents Severity Names.
/// </summary>
/// <remarks>
/// Represents Severity Names.
/// </remarks>
public enum Severity
{
	/// <summary>
	/// The none
	/// </summary>
	None = 0,

	/// <summary>
	/// The critical
	/// </summary>
	Critical = 1,

	/// <summary>
	/// The non critical
	/// </summary>
	NonCritical = 2,
}

/// <summary>
/// Represents LogType Names.
/// </summary>
/// <remarks>
/// Represents LogType Names.
/// </remarks>
public enum LogType
{
	/// <summary>
	/// The info
	/// </summary>
	Info = 0,

	/// <summary>
	/// The debug
	/// </summary>
	Debug = 1,

	/// <summary>
	/// The warning
	/// </summary>
	Warning = 2,

	/// <summary>
	/// The error
	/// </summary>
	Error = 3
}

/// <summary>
/// Represents LogLevels Names.
/// </summary>
/// <remarks>
/// Represents LogLevels Names.
/// </remarks>
public enum LogLevels
{
	/// <summary>
	/// The Product Level
	/// </summary>
	Product = 1,

	/// <summary>
	/// The AppService Level
	/// </summary>
	AppService = 2,

	/// <summary>
	/// The Component Level
	/// </summary>
	Component = 3,

	/// <summary>
	/// The SubComponent Level
	/// </summary>
	SubComponent = 4
}

/// <summary>
/// Represents FaultType Names.
/// </summary>
/// <remarks>
/// Represents FaultType Names.
/// </remarks>
public enum FaultType
{
	/// <summary>
	/// The none
	/// </summary>
	None = 0,

	/// <summary>
	/// The exception
	/// </summary>
	Exception = 1,

	/// <summary>
	/// The validation error
	/// </summary>
	ValidationError = 2,

	/// <summary>
	/// The information
	/// </summary>
	Information = 3,

	/// <summary>
	/// The notFound
	/// </summary>
	NotFound = 4,

	/// <summary>
	/// The conflict
	/// </summary>
	Conflict = 5,
}

/// <summary>
/// Represents ApplicationTier Names.
/// </summary>
/// <remarks>
/// Represents ApplicationTier Names.
/// </remarks>
public enum ApplicationTier
{
	/// <summary>
	/// The unknown
	/// </summary>
	Unknown = 0,

	/// <summary>
	/// The database
	/// </summary>
	Database = 1,

	/// <summary>
	/// The orm
	/// </summary>
	ORM = 2,

	/// <summary>
	/// The data access
	/// </summary>
	DataAccess = 3,

	/// <summary>
	/// The business domain
	/// </summary>
	BusinessDomain = 4,

	/// <summary>
	/// The service
	/// </summary>
	Service = 5,

	/// <summary>
	/// The view model
	/// </summary>
	ViewModel = 6,

	/// <summary>
	/// The client
	/// </summary>
	Client = 7,

	/// <summary>
	/// The web client
	/// </summary>
	WebClient = 8,

	/// <summary>
	/// The WPF client
	/// </summary>
	WPFClient = 9,

	/// <summary>
	/// The mobile client
	/// </summary>
	MobileClient = 10,

	/// <summary>
	/// The data repository
	/// </summary>
	DataRepository = 11

}

/// <summary>
/// Represents Fault Contract.
/// </summary>
/// <remarks>
/// Represents Fault Contract.
/// </remarks>
[ExcludeFromCodeCoverage]
public class FaultBase
{
	#region Fields

	#endregion

	#region Properties

	/// <summary>
	/// Gets or sets the fault identifier.
	/// </summary>
	/// <value>
	/// The fault identifier.
	/// </value>
	[DataMember(Name = "FaultId", IsRequired = false, Order = 10)]
	public Int32 FaultId
	{
		get;
		set;
	}

	/// <summary>
	/// Gets or sets the fault unique identifier.
	/// </summary>
	/// <value>
	/// The fault unique identifier.
	/// </value>
	[DataMember(Name = "FaultUId", IsRequired = false, Order = 15)]
	public Guid FaultUId
	{
		get;
		set;
	}

	/// <summary>
	/// Gets or sets the title.
	/// </summary>
	/// <value>
	/// The title.
	/// </value>
	[DataMember(Name = "Title", IsRequired = false, Order = 20)]
	public String Title
	{
		get;
		set;
	}

	/// <summary>
	/// Gets or sets the message.
	/// </summary>
	/// <value>
	/// The message.
	/// </value>
	[DataMember(Name = "Message", IsRequired = false, Order = 25)]
	public String Message
	{
		get;
		set;
	}

	/// <summary>
	/// Gets or sets the custom message.
	/// </summary>
	/// <value>
	/// The custom message.
	/// </value>
	[DataMember(Name = "CustomMessage", IsRequired = false, Order = 30)]
	public String CustomMessage
	{
		get;
		set;
	}

	/// <summary>
	/// Gets or sets the severity.
	/// </summary>
	/// <value>
	/// The severity.
	/// </value>
	[DataMember(Name = "Severity", IsRequired = false, Order = 35)]
	public Severity Severity
	{
		get;
		set;
	}

	/// <summary>
	/// Gets or sets the application tier.
	/// </summary>
	/// <value>
	/// The application tier.
	/// </value>
	[DataMember(Name = "ApplicationTier", IsRequired = false, Order = 40)]
	public ApplicationTier ApplicationTier
	{
		get;
		set;
	}

	/// <summary>
	/// Gets or sets the name of the entity.
	/// </summary>
	/// <value>
	/// The name of the entity.
	/// </value>
	[DataMember(Name = "EntityName", IsRequired = false, Order = 45)]
	public String EntityName
	{
		get;
		set;
	}

	/// <summary>
	/// Gets or sets the operation.
	/// </summary>
	/// <value>
	/// The operation.
	/// </value>
	[DataMember(Name = "Operation", IsRequired = false, Order = 50)]
	public String Operation
	{
		get;
		set;
	}

	/// <summary>
	/// Gets or sets the class type.
	/// </summary>
	/// <value>
	/// The class type.
	/// </value>
	[DataMember(Name = "ClassType", IsRequired = false, Order = 55)]
	public String ClassType
	{
		get;
		set;
	}

	/// <summary>
	/// Gets or sets the inner fault.
	/// </summary>
	/// <value>
	/// The inner fault.
	/// </value>
	[DataMember(Name = "InnerFault", IsRequired = false, Order = 65)]
	public Fault InnerFault
	{
		get;
		set;
	}

	/// <summary>
	/// Gets or sets the type of the fault.
	/// </summary>
	/// <value>
	/// The type of the fault.
	/// </value>
	[DataMember(Name = "FaultType", IsRequired = false, Order = 70)]
	public FaultType FaultType
	{
		get;
		set;
	}

	#endregion

	#region Constructors

	/// <summary>
	/// Default Constructor
	/// </summary>
	/// <remarks>
	/// Default Constructor
	/// </remarks>
	public FaultBase()
	{
		FaultId = -1;
		FaultUId = Guid.NewGuid();
		Title = "Error occurred";
		Message = "Error occurred";
		CustomMessage = "Error occurred";
		Severity = Severity.NonCritical;
		ApplicationTier = ApplicationTier.Unknown;
		EntityName = String.Empty;
		Operation = String.Empty;
		ClassType = String.Empty;
		FaultType = FaultType.Exception;
		InnerFault = null!;
	}

	#endregion

	#region Methods

	/// <summary>
	/// To the XML.
	/// </summary>
	/// <remarks>
	/// To the XML.
	/// </remarks>
	/// <returns>XElement</returns>
	/// <exception cref="System.Exception"></exception>
	public virtual XElement ToXml()
	{
		var xml = new XElement("Fault"
							, new XAttribute("FaultId", Convert.ToString(FaultId, CultureInfo.InvariantCulture))
							, new XAttribute("FaultUId", Convert.ToString(FaultUId, CultureInfo.InvariantCulture)!)
							, new XAttribute("FaultType", Convert.ToString(FaultType, CultureInfo.InvariantCulture)!)
							, new XAttribute("Title", Title ?? String.Empty)
							, new XAttribute("Message", Message ?? String.Empty)
							, new XAttribute("CustomMessage", CustomMessage ?? String.Empty)
							, new XAttribute("Severity", Convert.ToString(Severity, CultureInfo.InvariantCulture)!)
							, new XAttribute("ApplicationTier", Convert.ToString(ApplicationTier, CultureInfo.InvariantCulture)!)
							, new XAttribute("EntityName", EntityName ?? String.Empty)
							, new XAttribute("Operation", Operation ?? String.Empty)
							, new XAttribute("Type", ClassType ?? String.Empty)
							, InnerFault?.ToXml()
			);

		return xml;
	}

	#endregion
}

/// <summary>
/// Represents Fault Contract.
/// </summary>
/// <remarks>
/// Represents Fault Contract.
/// </remarks>
[ExcludeFromCodeCoverage]
public class Fault : FaultBase
{
	#region Fields

	#endregion

	#region Properties

	/// <summary>
	/// Gets or sets the stack trace.
	/// </summary>
	/// <value>
	/// The stack trace.
	/// </value>
	[DataMember(Name = "StackTrace", IsRequired = false, Order = 60)]
	public String StackTrace
	{
		get;
		set;
	}

	#endregion

	#region Constructors

	/// <summary>
	/// Default Constructor
	/// </summary>
	/// <remarks>
	/// Default Constructor
	/// </remarks>
	public Fault()
	{
		StackTrace = String.Empty;
	}

	#endregion

	#region Methods

	/// <summary>
	/// To the XML.
	/// </summary>
	/// <remarks>
	/// To the XML.
	/// </remarks>
	/// <returns>XElement</returns>
	/// <exception cref="System.Exception"></exception>
	public override XElement ToXml()
	{
		var element = base.ToXml();
		_ = element.AddAttribute(new XAttribute("StackTrace", StackTrace ?? String.Empty));

		return element;
	}

	#endregion
}

/// <summary>
/// Represents Default Fault Contract.
/// </summary>
/// <remarks>
/// Represents Default Fault Contract.
/// </remarks>
[Serializable]
[KnownType(typeof(Fault))]
[ExcludeFromCodeCoverage]
public class FaultCollection : Collection<Fault>
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

	/// <summary>
	/// Default Constructor
	/// </summary>
	/// <remarks>
	/// Default Constructor
	/// </remarks>
	public FaultCollection()
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="FaultCollection"/> class.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="FaultCollection"/> class.
	/// </remarks>
	/// <param name="fault">The fault.</param>
	public FaultCollection(Fault fault)
	{
		this.Add(fault);
	}

	#endregion

	#region Methods

	/// <summary>
	/// To the XML.
	/// </summary>
	/// <remarks>
	/// To the XML.
	/// </remarks>
	/// <returns>XElement</returns>
	public XElement ToXml()
	{
		var currentDateTime = (FormattableString)$"{DateTime.UtcNow:MM/dd/yyyy HH:mm}";

		var faultsElement = new XElement("Faults"
									 , new XAttribute("ProcessedOn", currentDateTime.ToString(CultureInfo.InvariantCulture)));

		if (this.Count != 0)
		{
			foreach (var fault in this)
			{
				var faultXml = fault.ToXml();
				faultsElement.Add(faultXml);
			}
		}

		return faultsElement;
	}

	/// <summary>
	/// Adds a collection in this collection.
	/// </summary>
	/// <param name="faults"></param>
	public void AddRange(IEnumerable<Fault> faults)
	{
		foreach (var fault in faults)
		{
			this.Add(fault);
		}
	}

	#endregion
}
