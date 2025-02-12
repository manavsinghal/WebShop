#region Copyright (c) 2024 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2024 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion

namespace Accenture.WebShop.MessageHub.Interfaces;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Represents message hub inteface.
/// </summary>
/// <remarks>
/// The MessageHub Interfaces.
/// </remarks>
public interface IMessageHub
{
	#region Properties

	#endregion

	#region Methods

	/// <summary>
	/// Sends the message asynchronous.
	/// </summary>
	/// <param name="topics">The topics.</param>
	/// <param name="message">The message.</param>
	/// <returns></returns>
	Task SendMessageAsync(IEnumerable<ENUMS.Topic> topics, DMMODELS.Message message);

	#endregion

}

