#region Copyright (c) 2024 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2024 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion

namespace Accenture.WebShop.MessageHub;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Notifier
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.SignalR.Hub" />
public class Notifier : Hub
{
	/// <summary>
	/// Represents DeveloperAndUser.
	/// </summary>
	/// <param name="message"></param>
	/// <returns></returns>
	public async Task DeveloperAndUser(DMMODELS.Message message)
	{
			await Clients.All.SendAsync(ENUMS.Topic.DeveloperAndUser.ToString(), message);
	}

	/// <summary>
	/// Represents Developer.
	/// </summary>
	/// <param name="message"></param>
	/// <returns></returns>
	public async Task Developer(DMMODELS.Message message)
	{
		await Clients.All.SendAsync(ENUMS.Topic.Developer.ToString(), message);
	}

	/// <summary>
	/// Represents User.
	/// </summary>
	/// <param name="message"></param>
	/// <returns></returns>
	public async Task User(DMMODELS.Message message)
	{
		await Clients.All.SendAsync(ENUMS.Topic.User.ToString(), message);
	}
}
