#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Infrastructure.WebService.SharedLibrary.Libraries;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion

/// <summary>
/// EntityViewRewriteRule
/// </summary>
public static class EntityViewRewriteRule
{
	#region Fields

	static readonly List<COREDOMAINDATAMODELSDOMAIN.EntityDynamicView> _entityViews = [];

	#endregion

	#region Properties

	#endregion

	#region Constructor

	/// <summary>
	/// Constructor EntityViewRewriteRule
	/// </summary>
	static EntityViewRewriteRule()
	{		
		var filesPath = SHAREDKERNALLIB.AppSettings.UrlRewriteRulePath.Split(';');

		var jsonSerializerOptions = new JsonSerializerOptions
		{
			ReadCommentHandling = JsonCommentHandling.Skip,
			AllowTrailingCommas = true,
		};
		jsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());

		foreach (var filePath in filesPath)
		{
			if (File.Exists(filePath))
			{
				_entityViews.AddRange(JsonSerializer.Deserialize<List<COREDOMAINDATAMODELSDOMAIN.EntityDynamicView >>(File.ReadAllText(filePath), jsonSerializerOptions)!);
			}
		}
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// RewriteRequest
	/// </summary>
	/// <param name="context"></param>
	public static void RewriteRequest(RewriteContext context)
	{
		context.HttpContext.Request.EnableBuffering();

		var request = context.HttpContext.Request;

		if (request.Path.Value != null)
		{
			if (_entityViews != null)
			{
				COREDOMAINDATAMODELSDOMAIN.View? dynamicView = null;

				if (request.QueryString.Value != null)
				{
					if (request.QueryString.Value.Contains("View=", StringComparison.OrdinalIgnoreCase))
					{
						var viewName = HttpUtility.ParseQueryString(request.QueryString.Value).Get("View");

						dynamicView = _entityViews.SelectMany(ev => ev.Views!.Where(dv => dv.Name!.Equals(viewName, StringComparison.OrdinalIgnoreCase) && request.Path.Value.EndsWith(ev.Url!, StringComparison.OrdinalIgnoreCase))).FirstOrDefault();
					}

					else
					{
						var content = new StreamReader(context.HttpContext.Request.Body).ReadToEndAsync().Result;

						// Rewind, so the core is not lost when it looks at the body for the request

						request.Body.Position = 0;

						if (!String.IsNullOrEmpty(content))
						{
							var jsonSerializerOptions = new JsonSerializerOptions
							{
								ReadCommentHandling = JsonCommentHandling.Skip,
								AllowTrailingCommas = true,
							};

							jsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());

							if (content.Contains("FilterView", StringComparison.OrdinalIgnoreCase) || content.Contains("FilterQuery", StringComparison.OrdinalIgnoreCase))
							{
								var rawBodyDataDictionary = JsonSerializer.Deserialize<Dictionary<String, dynamic>>(content, jsonSerializerOptions);

								if (content.Contains("FilterView", StringComparison.OrdinalIgnoreCase))
								{
									if (rawBodyDataDictionary != null)
									{
										var viewdata = rawBodyDataDictionary["FilterView"];

										dynamicView = JsonSerializer.Deserialize<COREDOMAINDATAMODELSDOMAIN.View>(viewdata, jsonSerializerOptions);
									}
								}

								if (content.Contains("FilterQuery", StringComparison.OrdinalIgnoreCase))
								{
									if (rawBodyDataDictionary != null)
									{
										var filterQuery = rawBodyDataDictionary["FilterQuery"];
										request.QueryString = new QueryString(request.QueryString.Value + filterQuery.ToString().TrimEnd(';'));
									}
								}
							}
							else
							{
								dynamicView = _entityViews.SelectMany(ev => ev.Views!.Where(dv => dv.IsDefault != null && dv.IsDefault == true && request.Path.Value.EndsWith(ev.Url!, StringComparison.OrdinalIgnoreCase))).FirstOrDefault();
							}
						}
					}
				}

				if (dynamicView != null)
				{
					if (dynamicView.Properties != null)
					{
						if (dynamicView.Properties.Equals("ALL", StringComparison.OrdinalIgnoreCase))
						{
							context.Result = RuleResult.SkipRemainingRules;
						}
						else
						{
							var dynamicQuery = new StringBuilder();

							_ = dynamicQuery.Append($"$select={dynamicView.Properties}");

							if (dynamicView.ExtendedProperties != null)
							{
								_ = dynamicQuery.Append('&');
								var parentViewName = dynamicView.Name;

								_ = dynamicQuery.Append($"$expand=");

								var counter = 0;

								foreach (var extendedProperty in dynamicView.ExtendedProperties)
								{
									if (counter > 0)
									{
										_ = dynamicQuery.Append(',');
									}

									BuildExtendedPropertyQuery(dynamicQuery, extendedProperty, parentViewName!);
									counter++;
								}
							}

							if (request.QueryString.Value != null && request.QueryString.Value.Contains("View=", StringComparison.OrdinalIgnoreCase))
							{
								var queryString = request.QueryString.Value[..(request.QueryString.Value.LastIndexOf("View=", StringComparison.OrdinalIgnoreCase))];

								if (queryString != null)
								{
									context.Result = RuleResult.SkipRemainingRules;
									request.QueryString = new QueryString(queryString + dynamicQuery.ToString().TrimEnd(';'));
								}
							}
							else if (!String.IsNullOrEmpty(request.QueryString.Value))
							{
								var queryString = request.QueryString.Value;
								context.Result = RuleResult.SkipRemainingRules;
								request.QueryString = new QueryString(queryString + "&" + dynamicQuery.ToString().TrimEnd(';'));
							}
							else
							{
								context.Result = RuleResult.SkipRemainingRules;
								request.QueryString = new QueryString("?" + dynamicQuery.ToString().TrimEnd(';'));
							}
						}
					}
				}
			}
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// BuildExtendedPropertyQuery
	/// </summary>
	/// <param name="dynamicQuery"></param>
	/// <param name="extendedProperty"></param>
	private static void BuildExtendedPropertyQuery(StringBuilder dynamicQuery, COREDOMAINDATAMODELSDOMAIN.Extendedproperty extendedProperty, String parentViewName)
	{
		var currentViewName = extendedProperty.View ?? parentViewName;

		_ = dynamicQuery.Append(extendedProperty.Name);

		if (extendedProperty.Properties != null)
		{
			var showAllProperties = extendedProperty.Properties.Equals("ALL", StringComparison.OrdinalIgnoreCase);

			if (!showAllProperties)
			{
				_ = dynamicQuery.Append($"($select={extendedProperty.Properties};");
			}

			if (extendedProperty.ExtendedProperties != null)
			{
				_ = showAllProperties ? dynamicQuery.Append($"($expand=") : dynamicQuery.Append($"$expand=");

				var counter = 0;

				foreach (var extendedChildProperty in extendedProperty.ExtendedProperties)
				{
					if(counter > 0)
					{
						_ = dynamicQuery.Append(',');
					}

					BuildExtendedPropertyQuery(dynamicQuery, extendedChildProperty, currentViewName);
					counter++;
				}

				if (showAllProperties)
				{
					_ = dynamicQuery.Append(')');
				}
			}

			if (!showAllProperties)
			{
				_ = dynamicQuery.Append(')');
			}
		}
		else
		{
			if (_entityViews != null)
			{
				var dynamicView = _entityViews.SelectMany(ev => ev.Views!.Where(dv => dv.Name!.Equals(currentViewName, StringComparison.OrdinalIgnoreCase) && ev.ControllerName != null && ev.ControllerName!.Equals(extendedProperty.Name, StringComparison.OrdinalIgnoreCase))).FirstOrDefault();

				if (dynamicView != null && dynamicView.Properties != null)
				{
					var showAllProperties = dynamicView.Properties.Equals("ALL", StringComparison.OrdinalIgnoreCase);

					if (!showAllProperties)
					{
						_ = dynamicQuery.Append($"($select={dynamicView.Properties};");
					}

					if (extendedProperty.ExtendedProperties != null)
					{
						_ = showAllProperties ? dynamicQuery.Append($"($expand=") : dynamicQuery.Append($"$expand=");

						var counter = 0;

						foreach (var extendedChildProperty in extendedProperty.ExtendedProperties)
						{
							if (counter > 0)
							{
								_ = dynamicQuery.Append(',');
							}

							BuildExtendedPropertyQuery(dynamicQuery, extendedChildProperty, currentViewName);
							counter++;
						}

						if (showAllProperties)
						{
							_ = dynamicQuery.Append(')');
						}
					}

					if (!showAllProperties)
					{
						_ = dynamicQuery.Append(')');
					}
				}
			}
		}
	}

	#endregion
}

