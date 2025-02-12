#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS;

#region Namespace References    

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// FunctionContextMiddleware
/// </summary>
public class FunctionContextMiddleware : IFunctionsWorkerMiddleware
{
	#region Public Method

	/// <summary>
	/// Invoke
	/// </summary>
	/// <param name="context"></param>
	/// <param name="next"></param>
	/// <returns></returns>
	public Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
	{
		var _functionContextAccessor = context.InstanceServices.GetService<IFunctionContextAccessor>();
		
		if (_functionContextAccessor != null)
		{
			_functionContextAccessor.FunctionContext = context;
		}
		
		return next(context);
	}

	#endregion
}

/// <summary>
/// IFunctionContextAccessor
/// </summary>
public interface IFunctionContextAccessor
{
	public FunctionContext FunctionContext { get; set; }
}

/// <summary>
/// DefaultFunctionContextAccessor
/// </summary>
internal sealed class DefaultFunctionContextAccessor : IFunctionContextAccessor
{
	private static readonly AsyncLocal<FunctionContextHolder> _functionContextCurrent = new();

	/// <summary>
	/// FunctionContext
	/// </summary>
	public static FunctionContext? FunctionContext
	{
		get => _functionContextCurrent.Value?.Context;
		set
		{
			var holder = _functionContextCurrent.Value;
			if (holder != null)
			{
				holder.Context = null;
			}

			if (value != null)
			{
				_functionContextCurrent.Value = new FunctionContextHolder { Context = value };
			}
		}
	}

	FunctionContext IFunctionContextAccessor.FunctionContext
	{
		get => FunctionContext ?? throw new InvalidOperationException("FunctionContext is not set.");
		set => FunctionContext = value;
	}

	/// <summary>
	/// FunctionContextHolder
	/// </summary>
	private class FunctionContextHolder
	{
		public FunctionContext? Context;
	}
}

