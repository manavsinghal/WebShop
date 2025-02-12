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
/// FunctionContextExtensions
/// </summary>
public static class FunctionContextExtensions
{
	#region Static methods

	/// <summary>
	/// SetHttpResponseStatusCode
	/// </summary>
	/// <param name="context"></param>
	/// <param name="statusCode"></param>
	/// <exception cref="InvalidOperationException"></exception>
	public static void SetHttpResponseStatusCode(this FunctionContext context, HttpStatusCode statusCode)
	{
		var coreAssembly = Assembly.Load("Microsoft.Azure.Functions.Worker.Core");
		var featureInterfaceName = "Microsoft.Azure.Functions.Worker.Context.Features.IFunctionBindingsFeature";

		var featureInterfaceType = coreAssembly.GetType(featureInterfaceName) ?? throw new InvalidOperationException($"Type '{featureInterfaceName}' not found in assembly 'Microsoft.Azure.Functions.Worker.Core'.");
		var bindingsFeature = context.Features.Single(f => f.Key.FullName == featureInterfaceType.FullName).Value;

		var invocationResultProp = featureInterfaceType.GetProperty("InvocationResult") ?? throw new InvalidOperationException($"Property 'InvocationResult' not found on type '{featureInterfaceName}'.");
		var grpcAssembly = Assembly.Load("Microsoft.Azure.Functions.Worker.Grpc");
		var responseDataType = grpcAssembly.GetType("Microsoft.Azure.Functions.Worker.GrpcHttpResponseData") ?? throw new InvalidOperationException("Type 'Microsoft.Azure.Functions.Worker.GrpcHttpResponseData' not found in assembly 'Microsoft.Azure.Functions.Worker.Grpc'.");

		var responseData = Activator.CreateInstance(responseDataType, context, statusCode) ?? throw new InvalidOperationException("Failed to create an instance of 'Microsoft.Azure.Functions.Worker.GrpcHttpResponseData'.");

		invocationResultProp.SetMethod?.Invoke(bindingsFeature, [responseData]);
	}

	/// <summary>
	/// GetTargetFunctionMethod
	/// </summary>
	/// <param name="context"></param>
	/// <returns></returns>
	public static MethodInfo GetTargetFunctionMethod(this FunctionContext context)
	{
		var entryPoint = context.FunctionDefinition.EntryPoint;

		var assemblyPath = context.FunctionDefinition.PathToAssembly;
		var assembly = Assembly.LoadFrom(assemblyPath);
		var typeName = entryPoint.Substring(0, entryPoint.LastIndexOf('.'));
		var type = assembly.GetType(typeName) ?? throw new InvalidOperationException($"Type '{typeName}' not found in assembly '{assemblyPath}'.");
		var methodName = entryPoint.Substring(entryPoint.LastIndexOf('.') + 1);

		var method = type.GetMethod(methodName) ?? throw new InvalidOperationException($"Method '{methodName}' not found in type '{typeName}'.");
		return method;
	}

	#endregion
}

