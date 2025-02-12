#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Core.Domain.DataModels;

/// <summary>
/// Represents CacheObjectMappingConfig DataModel.
/// </summary>
/// <remarks>
/// The CacheObjectMappingConfig DataModel.
/// </remarks>
public class CacheObjectMappingConfig
{
	/// <summary>
	/// Gets or Sets the EntityName.
	/// </summary>
	public String? EntityName { get; set; }

	/// <summary>
	/// Gets or Sets the OperationName.
	/// </summary>
	public String? OperationName { get; set; }

	/// <summary>
	/// Gets or Sets the IsCachingEnabled.
	/// </summary>
	public Boolean IsCachingEnabled { get; set; }
}


