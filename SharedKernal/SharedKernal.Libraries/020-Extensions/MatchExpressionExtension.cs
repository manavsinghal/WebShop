#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.SharedKernal.Libraries;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Extension method to concatenate MatchExpression(s)
/// </summary>
public static class MatchExpressionExtension
{
	public static Expression<Func<T, Boolean>> AndAlso<T>(this Expression<Func<T, Boolean>> expr1, Expression<Func<T, Boolean>> expr2)
	{
		var param = expr1.Parameters[0];

		return ReferenceEquals(param, expr2.Parameters[0])
			? Expression.Lambda<Func<T, Boolean>>(Expression.AndAlso(expr1.Body, expr2.Body), param)
			: Expression.Lambda<Func<T, Boolean>>(Expression.AndAlso(expr1.Body, Expression.Invoke(expr2, param)), param);
	}
}
