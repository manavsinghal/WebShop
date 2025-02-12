#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion  

namespace Accenture.WebShop.Core.Domain.DataModels;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents TransactionDataModel Data Model.
/// </summary>
/// <remarks>
/// Represents TransactionDataModel Data Model.
/// </remarks>
[Table("TransactionDataModel")]
public partial class TransactionDataModel<T> : DataModel<T> where T : class
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Constructors

    /// <summary>
	/// Initializes a new instance of the <see cref="TransactionDataModel{T}"/> class.
	/// </summary>
	public TransactionDataModel() : base()
	{
	}

	#endregion

	#region Methods

	#endregion
}
