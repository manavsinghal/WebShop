#region Copyright (c) 2024 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2024 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

#endregion

namespace Accenture.WebShop.Obfuscator;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// Obfuscator
/// </summary>
public class Obfuscator
{
	#region Fields

	private readonly IEnumerable<RegExRule> _defaultRegExRules;

	#endregion

	#region Properties      

	/// <summary>
	/// Configuration
	/// </summary>
	private readonly ObfuscatorConfig _obfuscatorConfig;

	#endregion

	#region Constructors

	/// <summary>
	/// ctor
	/// </summary>
	/// <param name="configFilePath"></param>
	/// <exception cref="FileNotFoundException"></exception>
	public Obfuscator(String configFilePath, AppSettingsSecretProvider appSettingsSecretProvider)
	{
		if (File.Exists(configFilePath))
		{
			var jsonSerializerOptions = new JsonSerializerOptions
			{
				ReadCommentHandling = JsonCommentHandling.Skip,
				AllowTrailingCommas = true
			};
			jsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());

			this._obfuscatorConfig = JsonSerializer.Deserialize<ObfuscatorConfig>(File.ReadAllText(configFilePath), jsonSerializerOptions)!;
			_defaultRegExRules = JsonSerializer.Deserialize<IEnumerable<RegExRule>>(File.ReadAllText(this._obfuscatorConfig.RegExRuleFilePath!), jsonSerializerOptions)!;

			if (this._obfuscatorConfig.Entities != null && this._obfuscatorConfig.Entities.Count != 0)
			{
				SecretClient? kvClient = null;

				if (!String.IsNullOrEmpty(appSettingsSecretProvider.Url) && !String.IsNullOrEmpty(appSettingsSecretProvider.UserAssignedManagedIdentity))
				{
					var options = new SecretClientOptions()
					{
						Retry =
						{
							Delay= TimeSpan.FromSeconds(2),
							MaxDelay = TimeSpan.FromSeconds(16),
							MaxRetries = 5,
							Mode = 0
						}
					};

					var managedIdentity = appSettingsSecretProvider.UserAssignedManagedIdentity;
					var credentials = new DefaultAzureCredential();

					if (!String.IsNullOrWhiteSpace(managedIdentity))
					{
						var defaultAzureCredentialOptions = new DefaultAzureCredentialOptions()
						{
							ManagedIdentityClientId = managedIdentity
						};

						credentials = new DefaultAzureCredential(defaultAzureCredentialOptions);
					}

					kvClient = new SecretClient(new Uri(appSettingsSecretProvider.Url), credentials, options);
				}

				var kvsecrets = new Dictionary<String, String>();

				var propertyRuleSet = this._obfuscatorConfig.Entities!.SelectMany(e => e.Properties!);
				foreach (var propertyRule in propertyRuleSet)
				{
					var maskingRule = propertyRule.MaskingRule;
					propertyRule.MaskingRule = this.LoadMaskingRules(jsonSerializerOptions, maskingRule);

					var encryptionRule = propertyRule.EncryptionRule;
					if (encryptionRule != null)
					{
						if (String.IsNullOrEmpty(appSettingsSecretProvider.Url) && String.IsNullOrEmpty(appSettingsSecretProvider.UserAssignedManagedIdentity))
						{
							if (!String.IsNullOrEmpty(encryptionRule.SecretManagerName) && encryptionRule.SecretManagerName.Equals(appSettingsSecretProvider.Name))
							{
								if (appSettingsSecretProvider.ObfuscatorEncryptionKeys!.Count() != 0)
								{
									var appSettingSecrets = appSettingsSecretProvider.ObfuscatorEncryptionKeys!;

									if (appSettingSecrets != null)
									{
										this.LoadEncryptionRulesAndSecretsFromAppSetting(appSettingSecrets, jsonSerializerOptions, encryptionRule);
									}
								}
							}
						}
						else if (!String.IsNullOrEmpty(appSettingsSecretProvider.Url) && !String.IsNullOrEmpty(appSettingsSecretProvider.UserAssignedManagedIdentity))
						{
							if (!String.IsNullOrEmpty(encryptionRule.SecretManagerName) && encryptionRule.SecretManagerName.Equals(appSettingsSecretProvider.Name))
							{
								this.LoadEncryptionRulesAndSecretsFromKeyVault(kvsecrets, jsonSerializerOptions, encryptionRule, kvClient!);
							}
						}
						else
						{
							throw new Exception($"Secret manager name '{encryptionRule.SecretManagerName}' not found.");
						}
					}
				}

				var ruleSet = this._obfuscatorConfig.Entities!.SelectMany(e => e.Properties!.Where(q => q.VisibilityRules != null).SelectMany(p => p.VisibilityRules!));
				foreach (var rule in ruleSet)
				{
					var maskingRule = rule.MaskingRule;
					rule.MaskingRule = this.LoadMaskingRules(jsonSerializerOptions, maskingRule);

					var encryptionRule = rule.EncryptionRule;
					if (encryptionRule != null)
					{
						if (!String.IsNullOrEmpty(encryptionRule.SecretManagerName) && encryptionRule.SecretManagerName.Equals(appSettingsSecretProvider.Name))
						{
							if (appSettingsSecretProvider.ObfuscatorEncryptionKeys!.Count() != 0)
							{
								var appSettingSecrets = appSettingsSecretProvider.ObfuscatorEncryptionKeys!;

								if (appSettingSecrets != null)
								{
									this.LoadEncryptionRulesAndSecretsFromAppSetting(appSettingSecrets, jsonSerializerOptions, encryptionRule);
								}
							}
						}
						else if (!String.IsNullOrEmpty(encryptionRule.SecretManagerName) && encryptionRule.SecretManagerName.Equals(appSettingsSecretProvider.Name))
						{
							this.LoadEncryptionRulesAndSecretsFromKeyVault(kvsecrets, jsonSerializerOptions, encryptionRule, kvClient!);
						}
						else
						{
							throw new Exception($"Secret manager name '{encryptionRule.SecretManagerName}' not found.");
						}
					}
				}
			}
		}
		else
		{
			throw new FileNotFoundException($"{configFilePath} not found.");
		}
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Obfuscate Collection of Objects
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="collection"></param>
	/// <returns></returns>
	public ICollection<T> Obfuscate<T>(ICollection<T> collection)
	{
		var result = new ConcurrentBag<T>();

		_ = Parallel.ForEach(collection, entity =>
		{
			result.Add(this.Obfuscate(entity));
		});

		return result.ToList();
	}

	/// <summary>
	/// Obfuscate Enumerable of Objects
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="collection"></param>
	/// <returns></returns>
	public IEnumerable<T> Obfuscate<T>(IEnumerable<T> collection)
	{
		var result = new ConcurrentBag<T>();

		_ = Parallel.ForEach(collection, entity =>
		{
			result.Add(this.Obfuscate(entity));
		});

		return result.AsEnumerable();
	}

	/// <summary>
	/// Obfuscate object
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="entity"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException"></exception>
	public T Obfuscate<T>(T entity)
	{
		if (entity is null)
		{
			throw new ArgumentNullException(nameof(entity));
		}

		if (_obfuscatorConfig.Entities != null && _obfuscatorConfig.Entities.Count != 0)
		{
			_ = Parallel.ForEach(_obfuscatorConfig.Entities!, entityMaskRule =>
			{
				if (entity.GetType().Name == entityMaskRule.Name)
				{
					_ = Parallel.ForEach(entity.GetType().GetProperties(), propertyInfo =>
					{
						if (propertyInfo.PropertyType.IsPrimitive || propertyInfo.PropertyType.IsValueType || propertyInfo.PropertyType == typeof(String) || propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(Guid))
						{
							foreach (var entityMaskRuleProperty in entityMaskRule.Properties!)
							{
								if (propertyInfo.Name == entityMaskRuleProperty.Name)
								{
									var maskedValue = this.EvaluateMask(propertyInfo.GetValue(entity), entityMaskRuleProperty);
									propertyInfo.SetValue(entity, maskedValue);
								}
							}
						}
						else if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
						{
							var subEntity = propertyInfo.GetValue(entity);

							if (subEntity != null)
							{
								var subEntityEnum = (subEntity as IEnumerable);

								if (subEntityEnum != null)
								{
									foreach (var item in subEntityEnum)
									{
										_ = this.Obfuscate(item);
									}
								}
							}
						}
						else
						{
							var subEntity = propertyInfo.GetValue(entity);

							if (subEntity != null)
							{
								_ = this.Obfuscate(propertyInfo.GetValue(entity));
							}
						}
					});
				}
			});
		}

		return entity;
	}

	/// <summary>
	/// DeObfuscate Collection of Objects
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="collection"></param>
	/// <param name="userContext"></param>
	/// <returns></returns>
	public ICollection<T> DeObfuscate<T>(ICollection<T> collection, Object userContext)
	{
		var result = new ConcurrentBag<T>();

		_ = Parallel.ForEach(collection, entity =>
		{
			result.Add(this.DeObfuscate(entity, userContext));
		});

		return result.ToList();
	}

	/// <summary>
	/// DeObfuscate Enumerable of Objects
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="collection"></param>
	/// <param name="userContext"></param>
	/// <returns></returns>
	public IEnumerable<T> DeObfuscate<T>(IEnumerable<T> collection, Object userContext)
	{
		var result = new ConcurrentBag<T>();

		_ = Parallel.ForEach(collection, entity =>
		{
			result.Add(this.DeObfuscate(entity, userContext));
		});

		return result.AsEnumerable();
	}

	/// <summary>
	/// DeObfuscate entity
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="entity"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException"></exception>
	public T DeObfuscate<T>(T entity, Object userContext)
	{
		if (entity is null)
		{
			throw new ArgumentNullException(nameof(entity));
		}

		var regexPattern = $"{_obfuscatorConfig.TokenPrefix!.Replace("[", "\\[")}(.*?){_obfuscatorConfig.TokenSuffix!.Replace("]", "\\]")}";
		var regex = new Regex(regexPattern);

		if (_obfuscatorConfig.Entities != null && _obfuscatorConfig.Entities.Count != 0)
		{
			_ = Parallel.ForEach(_obfuscatorConfig.Entities!, entityMaskRule =>
			{
				if (entity.GetType().Name == entityMaskRule.Name)
				{
					_ = Parallel.ForEach(entity.GetType().GetProperties(), propertyInfo =>
					{
						if (propertyInfo.PropertyType.IsPrimitive || propertyInfo.PropertyType.IsValueType || propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(Guid))
						{
							return;
						}
						else if (propertyInfo.PropertyType == typeof(String))
						{
							foreach (var entityMaskRuleProperty in entityMaskRule.Properties!)
							{
								if (propertyInfo.Name == entityMaskRuleProperty.Name)
								{
									if (entityMaskRuleProperty.VisibilityRules == null)
									{
										//Set Invisible in case of no rules match
										propertyInfo.SetValue(entity, default);
										continue;
									}

									var entityRulePropertyRule = EvaluatePropertyRule(userContext, entityMaskRuleProperty.VisibilityRules!);
									if (entityRulePropertyRule != null)
									{
										if (entityMaskRuleProperty.EncryptionRule != null)
										{
											var entityRulePropertyObfuscateRule = entityMaskRuleProperty.EncryptionRule!;
											var encryptionKey = String.IsNullOrEmpty(entityRulePropertyObfuscateRule.SecretKey) ? String.Empty : entityRulePropertyObfuscateRule.SecretKey;

											var maskedValue = propertyInfo.GetValue(entity);

											if (maskedValue != null)
											{
												var result = maskedValue.ToString();

												foreach (var match in regex.Matches(result!).Cast<Match>())
												{
													result = result!.Replace(match.Value, DecryptText(encryptionKey, match.Groups[1].Value));
												}

												var maskedValueAfterDeobfuscate = this.EvaluateMaskForVisbility(result, entityRulePropertyRule);
												propertyInfo.SetValue(entity, maskedValueAfterDeobfuscate);
											}
										}
										else
										{
											var propertyValue = propertyInfo.GetValue(entity);

											if (propertyValue != null)
											{
												var result = propertyValue.ToString();

												var maskedValueAfterDeobfuscate = this.EvaluateMaskForVisbility(result, entityRulePropertyRule);
												propertyInfo.SetValue(entity, maskedValueAfterDeobfuscate);
											}
										}
									}
									else
									{
										//Set Invisible in case of no rules match
										propertyInfo.SetValue(entity, default);
									}
								}
							}
						}
						else if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
						{
							var subEntity = propertyInfo.GetValue(entity);

							if (subEntity != null)
							{
								var subEntityEnum = (subEntity as IEnumerable);

								if (subEntityEnum != null)
								{
									foreach (var item in subEntityEnum)
									{
										_ = this.DeObfuscate(item, userContext);
									}
								}
							}
						}
						else
						{
							var subEntity = propertyInfo.GetValue(entity);

							if (subEntity != null)
							{
								_ = this.DeObfuscate(propertyInfo.GetValue(entity), userContext);
							}
						}
					});
				}
			});
		}

		return entity;
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Load Masking Rules
	/// </summary>
	/// <param name="jsonSerializerOptions"></param>
	/// <param name="maskingRule"></param>
	/// <returns></returns>
	private MaskingRule LoadMaskingRules(JsonSerializerOptions jsonSerializerOptions, MaskingRule? maskingRule)
	{
		if (maskingRule != null)
		{
			if (String.IsNullOrEmpty(maskingRule.RuleFilePath) && maskingRule.Rules == null)
			{
				maskingRule.Rules = _defaultRegExRules;
			}

			if (!String.IsNullOrEmpty(maskingRule.RuleFilePath))
			{
				maskingRule.Rules = JsonSerializer.Deserialize<IEnumerable<RegExRule>>(File.ReadAllText(maskingRule.RuleFilePath), jsonSerializerOptions);
			}
		}
		else
		{
			maskingRule = new MaskingRule
			{
				Rules = _defaultRegExRules
			};
		}

		return maskingRule;
	}

	/// <summary>
	/// Load EncryptionRules And Secrets From AppSetting
	/// </summary>
	/// <param name="appSettingSecrets"></param>
	/// <param name="jsonSerializerOptions"></param>
	/// <param name="encryptionRule"></param>
	private void LoadEncryptionRulesAndSecretsFromAppSetting(Dictionary<String, String> appSettingSecrets, JsonSerializerOptions jsonSerializerOptions, EncryptionRule encryptionRule)
	{
		if (encryptionRule != null)
		{
			if (String.IsNullOrEmpty(encryptionRule.RuleFilePath) && encryptionRule.Rules == null)
			{
				encryptionRule.Rules = _defaultRegExRules;
			}

			if (!String.IsNullOrEmpty(encryptionRule.RuleFilePath))
			{
				encryptionRule.Rules = JsonSerializer.Deserialize<IEnumerable<RegExRule>>(File.ReadAllText(encryptionRule.RuleFilePath), jsonSerializerOptions);
			}

			encryptionRule.SecretKey = !String.IsNullOrEmpty(encryptionRule.SecretKeyName)
				? appSettingSecrets.ContainsKey(encryptionRule!.SecretKeyName)
					? appSettingSecrets[encryptionRule!.SecretKeyName!]
					: throw new Exception($"Secret name '{encryptionRule.SecretKeyName}' does not found in secret manager '{encryptionRule.SecretManagerName}'.")
				: throw new Exception($"Secret key name cannot be null or empty.");
		}
	}

	/// <summary>
	/// Load Encryption Rules And Secrets From KeyVault
	/// </summary>
	/// <param name="kvsecrets"></param>
	/// <param name="jsonSerializerOptions"></param>
	/// <param name="encryptionRule"></param>
	/// <param name="kvClient"></param>
	/// <exception cref="Exception"></exception>
	private void LoadEncryptionRulesAndSecretsFromKeyVault(Dictionary<String, String> kvsecrets, JsonSerializerOptions jsonSerializerOptions, EncryptionRule encryptionRule, SecretClient kvClient)
	{
		if (encryptionRule != null)
		{
			if (String.IsNullOrEmpty(encryptionRule.RuleFilePath) && encryptionRule.Rules == null)
			{
				encryptionRule.Rules = _defaultRegExRules;
			}

			if (!String.IsNullOrEmpty(encryptionRule.RuleFilePath))
			{
				encryptionRule.Rules = JsonSerializer.Deserialize<IEnumerable<RegExRule>>(File.ReadAllText(encryptionRule.RuleFilePath), jsonSerializerOptions);
			}

			if (!String.IsNullOrEmpty(encryptionRule.SecretKeyName))
			{
				if (kvsecrets.ContainsKey(encryptionRule.SecretKeyName))
				{
					encryptionRule.SecretKey = kvsecrets[encryptionRule.SecretKeyName];
				}
				else
				{
					encryptionRule.SecretKey = kvClient.GetSecret(encryptionRule.SecretKeyName).Value.Value;

					if (String.IsNullOrEmpty(encryptionRule.SecretKey))
					{
						throw new Exception($"Encryption key for '{encryptionRule.SecretKeyName}' cannot be null or empty.");
					}
					else
					{
						kvsecrets.Add(encryptionRule.SecretKeyName, encryptionRule.SecretKey);
					}
				}
			}
			else
			{
				throw new Exception($"Secret key name cannot be null or empty.");
			}
		}
	}

	/// <summary>
	/// Return the Visibility Rule to be applied
	/// </summary>
	/// <param name="userContext"></param>
	/// <param name="visibilityRules"></param>
	/// <returns></returns>
	private static VisibilityRule? EvaluatePropertyRule(Object userContext, IEnumerable<VisibilityRule> visibilityRules)
	{
		foreach (var entityMaskRulePropertyRule in visibilityRules)
		{
			if (entityMaskRulePropertyRule.Value == null)
			{
				continue;
			}
			else if (entityMaskRulePropertyRule.PermissionLevel == null && entityMaskRulePropertyRule.Value.Contains("ALL", StringComparer.OrdinalIgnoreCase))
			{
				return entityMaskRulePropertyRule;
			}
			else if (entityMaskRulePropertyRule!.PermissionLevel == null)
			{
				continue;
			}
			else
			{
				var permissionLevel = Convert.ToString(entityMaskRulePropertyRule.PermissionLevel);

				var permissionLevelProperty = userContext.GetType().GetProperty(permissionLevel!);

				if (permissionLevelProperty != null)
				{
					var permissionLevelValues = (IEnumerable<String>)permissionLevelProperty.GetValue(userContext, null)!;

					if (entityMaskRulePropertyRule.Value!.Contains("ALL", StringComparer.OrdinalIgnoreCase) || entityMaskRulePropertyRule.Value!.Any(r => permissionLevelValues.Any(ar => ar == r)))
					{
						return entityMaskRulePropertyRule;
					}
				}
			}
		}

		return null;
	}

	/// <summary>
	/// Evaluate Mask
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="input"></param>
	/// <param name="entityProperty"></param>
	/// <returns></returns>
	private T? EvaluateMask<T>(T input, EntityProperty? entityProperty)
	{
		return entityProperty == null
			? default
			: entityProperty.ObfuscatorRuleType switch
			{
				ObfuscatorRuleType.Visible => input,
				ObfuscatorRuleType.PropertyEncryption => this.DoPropertyEncryption(input, String.IsNullOrEmpty(entityProperty.EncryptionRule!.SecretKey) ? String.Empty : entityProperty.EncryptionRule!.SecretKey),
				ObfuscatorRuleType.PropertyMasking => this.DoPropertyMasking(input),
				ObfuscatorRuleType.DataEncryption => this.DoDataEncryption(input, entityProperty.EncryptionRule!),
				ObfuscatorRuleType.DataMasking => DoDataMasking(input, entityProperty.MaskingRule!),
				ObfuscatorRuleType.Invisible => default,
				_ => default,
			};
	}

	/// <summary>
	/// Evaluate Mask
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="input"></param>
	/// <param name="visibilityRule"></param>
	/// <returns></returns>
	private T? EvaluateMaskForVisbility<T>(T input, VisibilityRule? visibilityRule)
	{
		return visibilityRule == null
			? default
			: visibilityRule.ObfuscatorRuleType switch
			{
				ObfuscatorRuleType.Visible => input,
				ObfuscatorRuleType.PropertyEncryption => this.DoPropertyEncryption(input, String.IsNullOrEmpty(visibilityRule.EncryptionRule!.SecretKey) ? String.Empty : visibilityRule.EncryptionRule!.SecretKey),
				ObfuscatorRuleType.PropertyMasking => this.DoPropertyMasking(input),
				ObfuscatorRuleType.DataEncryption => this.DoDataEncryption(input, visibilityRule.EncryptionRule!),
				ObfuscatorRuleType.DataMasking => DoDataMasking(input, visibilityRule.MaskingRule!),
				ObfuscatorRuleType.Invisible => default,
				_ => default,
			};
	}

	/// <summary>
	/// Property Encryption
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="input"></param>
	/// <param name="encryptionKey"></param>
	/// <returns></returns>
	private T? DoPropertyEncryption<T>(T input, String encryptionKey)
	{
		if (String.IsNullOrEmpty(encryptionKey))
		{
			throw new Exception($"Encryption key cannot be null or empty.");
		}

		var maskedValue = String.Empty;

		var emailRegex1 = "(\\.|[a-z]|[A-Z]|[0-9])*@(\\.|[a-z]|[A-Z]|[0-9])*";
		var emailRegex2 = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";

		if (input != null && input.ToString() != String.Empty && input.GetType() == typeof(String))
		{
			if (Regex.IsMatch(input.ToString()!, emailRegex1, RegexOptions.IgnoreCase) ||
				Regex.IsMatch(input.ToString()!, emailRegex2, RegexOptions.IgnoreCase))
			{
				if (!input.ToString()!.Contains(_obfuscatorConfig.TokenPrefix!))
				{
					maskedValue = $"{_obfuscatorConfig.TokenPrefix} {EncryptText(encryptionKey, (String)(Object)input!)} {_obfuscatorConfig.TokenSuffix}";
				}
				else
				{
					maskedValue = (String)(Object)input!;
				}
			}
			else
			{
				maskedValue = (String)(Object)input!;
			}

			return (T)Convert.ChangeType(maskedValue, typeof(T));
		}
		else
		{
			return (T)Convert.ChangeType(maskedValue, typeof(T));
		}
	}

	/// <summary>
	/// Property Masking
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="input"></param>
	/// <returns></returns>
	private T? DoPropertyMasking<T>(T input)
	{
		if (input != null && input.GetType() == typeof(String))
		{
			var result = (String)Convert.ChangeType(input, typeof(String));

			foreach (var rule in _defaultRegExRules!)
			{
				var regex = new Regex(rule.Pattern!);

				foreach (var match in regex.Matches(result).Cast<Match>())
				{
					result = Regex.Replace(result, rule.Pattern!, MaskText(rule.ReplaceWith!, match.Value));
				}
			}

			return (T)Convert.ChangeType(result, typeof(T));
		}
		else
		{
			return default;
		}
	}

	/// <summary>
	/// Data Encryption
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="input"></param>
	/// <param name="dataEncryptionRule"></param>
	/// <param name="encryptionKey"></param>
	/// <returns></returns>
	private T? DoDataEncryption<T>(T input, EncryptionRule dataEncryptionRule)
	{
		if (String.IsNullOrEmpty(dataEncryptionRule.SecretKey))
		{
			throw new Exception($"Encryption key cannot be null or empty.");
		}

		if (input != null && input.GetType() == typeof(String))
		{
			var result = (String)Convert.ChangeType(input, typeof(String));

			if (dataEncryptionRule.Rules != null)
			{
				var resultMatches = new Dictionary<String, String>();

				foreach (var rule in dataEncryptionRule.Rules!)
				{
					var regex = new Regex(rule.Pattern!);

					foreach (var match in regex.Matches(result).Cast<Match>())
					{
						if (!resultMatches.ContainsKey(match.Value))
						{
							if (!String.IsNullOrEmpty(match.Value))
							{
								if (!(match.Value).ToString()!.Contains(_obfuscatorConfig.TokenPrefix!))
								{
									resultMatches.Add(match.Value, $"{_obfuscatorConfig.TokenPrefix}{EncryptText(dataEncryptionRule.SecretKey, match.Value)}{_obfuscatorConfig.TokenSuffix}");
								}
								else
								{
									resultMatches.Add(match.Value, match.Value);
								}
							}
							else
							{
								resultMatches.Add(match.Value, match.Value);
							}
						}
					}
				}

				foreach (var resultMatch in resultMatches)
				{
					result = result.Replace(resultMatch.Key, resultMatch.Value);
				}
			}

			return (T)Convert.ChangeType(result, typeof(T));
		}
		else
		{
			return default;
		}
	}

	/// <summary>
	/// Data Masking
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="input"></param>
	/// <param name="dataMaskingRule"></param>
	/// <returns></returns>
	private static T? DoDataMasking<T>(T? input, MaskingRule dataMaskingRule)
	{
		if (input != null && input.GetType() == typeof(String))
		{
			var result = (String)Convert.ChangeType(input, typeof(String));

			if (dataMaskingRule.Rules != null)
			{
				foreach (var rule in dataMaskingRule.Rules!)
				{
					var regex = new Regex(rule.Pattern!);

					foreach (var match in regex.Matches(result).Cast<Match>())
					{
						result = Regex.Replace(result, rule.Pattern!, $"{MaskText(rule.ReplaceWith!, match.Value)}");
					}
				}
			}

			return (T)Convert.ChangeType(result, typeof(T));
		}
		else
		{
			return default;
		}
	}

	/// <summary>
	/// Mask text with patterns like XXX##-##XXX
	/// </summary>
	/// <param name="key"></param>
	/// <param name="text"></param>
	/// <returns></returns>
	private static String MaskText(String key, String text)
	{
		var sb = new StringBuilder(key.Length);

		for (var i = 0; i < key.Length; i++)
		{
			if (i < text.Length)
			{
				_ = key[i] == '#' ? sb.Append('#') : sb.Append(text[i]);
			}
			else
			{
				break;
			}
		}

		return sb.ToString();
	}

	/// <summary>
	/// AES CCM Encryption
	/// </summary>
	/// <param name="key"></param>
	/// <param name="text"></param>
	/// <returns></returns>
	private static String EncryptText(String key, String text)
	{
		var textBytes = Encoding.UTF8.GetBytes(text);
		var cipherTextBytes = new Byte[textBytes.Length];
		var nonce = new Byte[AesCcm.NonceByteSizes.MaxSize];
		var tag = new Byte[AesCcm.TagByteSizes.MaxSize];

		//RandomNumberGenerator.Fill(nonce);
		using var aes = new AesCcm(Encoding.UTF8.GetBytes(key));
		aes.Encrypt(nonce, textBytes, cipherTextBytes, tag);

		return Convert.ToBase64String(nonce.Concat(tag).Concat(cipherTextBytes).ToArray());
	}

	/// <summary>
	/// AES CCM Decryption
	/// </summary>
	/// <param name="key"></param>
	/// <param name="text"></param>
	/// <returns></returns>
	private static String DecryptText(String key, String text)
	{
		var inputTextBytes = Convert.FromBase64String(text);
		var nonce = inputTextBytes.Take(AesCcm.NonceByteSizes.MaxSize).ToArray();
		var tag = inputTextBytes.Skip(AesCcm.NonceByteSizes.MaxSize).Take(AesCcm.TagByteSizes.MaxSize).ToArray();
		var cipherTextBytes = inputTextBytes.Skip(AesCcm.NonceByteSizes.MaxSize + AesCcm.TagByteSizes.MaxSize).ToArray();
		var textBytes = new Byte[cipherTextBytes.Length];

		using var aes = new AesCcm(Encoding.UTF8.GetBytes(key));
		aes.Decrypt(nonce, cipherTextBytes, tag, textBytes);

		return Encoding.UTF8.GetString(textBytes);
	}

	#endregion
}
