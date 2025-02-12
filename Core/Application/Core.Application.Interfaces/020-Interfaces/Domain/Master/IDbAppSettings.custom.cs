#region Copyright (c) 2024 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2024 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion

namespace Accenture.WebShop.Core.Application.Interfaces.DbAppSettings;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead 

#endregion

public partial interface IDbAppSettings
{
	#region Fields
	#endregion

	#region Properties
	COREDOMAINDATAMODELSDOMAIN.CommonSettings Common { get; }
	COREDOMAINDATAMODELSDOMAIN.ProvisionServiceSettings ProvisionService { get; }
	COREDOMAINDATAMODELSDOMAIN.WebAPIServiceSettings WebAPIService { get; }
	COREDOMAINDATAMODELSDOMAIN.WebUIServiceSettings WebUIService { get; }
	COREDOMAINDATAMODELSDOMAIN.MessageBusSettings MessageBus { get; }
	COREDOMAINDATAMODELSDOMAIN.EmailSettings Email { get; }
	COREDOMAINDATAMODELSDOMAIN.CachingSettings Caching{ get; }

	#endregion

	#region Constructors
	#endregion

	#region Public Methods

	/// <summary>
	/// GetAsync
	/// </summary>
	/// <param name="componentName"></param>
	/// <param name="key"></param>
	/// <returns></returns>
	Task<COREDOMAINDATAMODELSDOMAIN.AppSetting> GetAsync(String componentName, String key);

	/// <summary>
	/// SetAsync
	/// </summary>
	/// <param name="appSetting"></param>
	/// <returns></returns>
	Task<Boolean> SetAsync(COREDOMAINDATAMODELSDOMAIN.AppSetting appSetting);

	/// <summary>
	/// GetValueAsync
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="componentName"></param>
	/// <param name="key"></param>
	/// <returns></returns>
	Task<T?> GetValueAsync<T>(String componentName, String key);

	/// <summary>
	/// SetValueAsync
	/// </summary>
	/// <param name="componentName"></param>
	/// <param name="key"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	Task<Boolean> SetValueAsync(String componentName, String key, String value);

	/// <summary>
	/// RefreshAsync
	/// </summary>
	/// <returns></returns>
	Task RefreshAsync();

	/// <summary>
	/// RefreshAppSettingsAsync
	/// </summary>
	/// <returns></returns>
	Task RefreshAppSettingsAsync();

	#endregion

	#region Private Methods
	#endregion
}
