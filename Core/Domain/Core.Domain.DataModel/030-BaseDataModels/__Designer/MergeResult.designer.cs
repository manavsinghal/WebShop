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
/// Represents MergeResult.
/// </summary>
/// <remarks>
/// Represents MergeResult.
/// </remarks>
public class MergeResult
{
	#region Fields

	#endregion

	#region Properties

	/// <summary>
	/// Gets or sets the RequestCount.
	/// </summary>
	/// <value>
	/// The RequestCount.
	/// </value>
	public Int64 RequestCount { get; set; }

	/// <summary>
	/// Gets or sets the InsertCount.
	/// </summary>
	/// <value>
	/// The InsertCount.
	/// </value>
	public Int64 InsertCount { get; set; }

	/// <summary>
	/// Gets or sets the UpdateCount.
	/// </summary>
	/// <value>
	/// The UpdateCount.
	/// </value>
	public Int64 UpdateCount { get; set; }

	/// <summary>
	/// Gets or sets the DeleteCount.
	/// </summary>
	/// <value>
	/// The DeleteCount.
	/// </value>
	public Int64 DeleteCount { get; set; }

	/// <summary>
	/// Gets or sets the UnchangedCount.
	/// </summary>
	/// <value>
	/// The UnchangedCount.
	/// </value>
	public Int64 UnchangedCount { get; set; }

	#endregion

	#region Constructors

	#endregion

	#region Methods

	/// <summary>
	/// 
	/// </summary>
	/// <param name="mergeResult"></param>
	/// <param name="otherMergeResult"></param>
	/// <returns></returns>
	public static MergeResult operator +(MergeResult mergeResult, MergeResult otherMergeResult)
	{
		mergeResult.DeleteCount += otherMergeResult.DeleteCount;
		mergeResult.InsertCount += otherMergeResult.InsertCount;
		mergeResult.RequestCount += otherMergeResult.RequestCount;
		mergeResult.UnchangedCount += otherMergeResult.UnchangedCount;
		mergeResult.UpdateCount += otherMergeResult.UpdateCount;

		return mergeResult;
	}

	#endregion
}

