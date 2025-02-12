#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Core.Domain.DataModels.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead 

#endregion

/// <summary>
/// Group record
/// </summary>
public class Extendedproperty
{
	#region Fields
	#endregion

	#region Properties
	public String? Name { get; set; }
	public Int64 Order { get; set; }
	public String? Properties { get; set; }
	public String? View { get; set; }
	public List<Extendedproperty>? ExtendedProperties { get; set; }

	#endregion

	#region Constructors
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion
}
