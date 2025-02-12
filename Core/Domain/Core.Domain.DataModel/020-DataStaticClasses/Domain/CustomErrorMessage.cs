#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Core.Domain.DataModels.Domain.Enumerators;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents theme static class/enumerator.
/// </summary>
/// <remarks>
/// This class is generated from the data in the MasterListItem database table/view.
/// </remarks>
public static class CustomErrorMessage
{
	/// <summary>
	/// Gets the check constraint.
	/// </summary>
	/// <value>
	/// The check constraint.
	/// </value>
	public static String CheckConstraint { get; private set; } = "CHECK CONSTRAINT";

	/// <summary>
	/// Gets the foriegn key violation.
	/// </summary>
	/// <value>
	/// The foriegn key violation.
	/// </value>
	public static String ForiegnKeyViolation { get; private set; } = "FOREIGN KEY";

	/// <summary>
	/// Gets the not null constraint.
	/// </summary>
	/// <value>
	/// The not null constraint.
	/// </value>
	public static String NotNullConstraint { get; private set; } = "VALUE NULL INTO COLUMN";

	/// <summary>
	/// Gets the primary key violation.
	/// </summary>
	/// <value>
	/// The primary key violation.
	/// </value>
	public static String PrimaryKeyViolation { get; private set; } = "PRIMARY KEY";

	/// <summary>
	/// Gets the unique key violation.
	/// </summary>
	/// <value>
	/// The unique key violation.
	/// </value>
	public static String UniqueKeyViolation { get; private set; } = "UNIQUE KEY";

	/// <summary>
	/// Gets the update concurrency exception.
	/// </summary>
	/// <value>
	/// The update concurrency exception.
	/// </value>
	public static String UpdateConcurrencyException { get; private set; } = "CONCURRENCY EXCEPTIONS";

	/// <summary>
	/// Gets the delete reference exception.
	/// </summary>
	/// <value>
	/// The delete reference exception.
	/// </value>
	public static String DeleteException { get; private set; } = "DELETE STATEMENT CONFLICTED WITH THE REFERENCE";

	/// <summary>
	/// Gets the field length exception.
	/// </summary>
	/// <value>
	/// The field length exception.
	/// </value>
	public static String FieldLengthException { get; private set; } = "DATA WOULD BE TRUNCATED IN TABLE";

}

