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
/// Represents Json extensions
/// </summary>
public static partial class JsonExtensions
{

	/// <summary>
	/// Converts to json.
	/// </summary>
	/// <typeparam name="M"></typeparam>
	/// <param name="obj">The object.</param>
	/// <returns></returns>
	public static String ToJson<M>(this M obj) where M : class
	{
		return JsonSerializer.Serialize(obj, JsonSerializerOptions);
	}

	/// <summary>
	/// Converts to json.
	/// </summary>
	/// <typeparam name="M"></typeparam>
	/// <param name="obj"></param>
	/// <param name="options"></param>
	/// <returns></returns>
	public static String ToJson<M>(this M obj, JsonSerializerOptions options) where M : class
	{
		return JsonSerializer.Serialize(obj, options);
	}

	/// <summary>
	/// Converts to json.
	/// </summary>
	/// <typeparam name="M"></typeparam>
	/// <param name="obj">The object.</param>
	/// <returns></returns>
	public static String ToJsonRemoveNullProperties<M>(this M obj) where M : class
	{
		var options = new JsonSerializerOptions
		{
			ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
		};
		return JsonSerializer.Serialize(obj, options);
	}

	/// <summary>
	/// Converts to object.
	/// </summary>
	/// <typeparam name="M"></typeparam>
	/// <param name="json">The json.</param>
	/// <returns></returns>
	public static M? ToObject<M>(this String json) where M : class
	{
		return !String.IsNullOrEmpty(json) ? JsonSerializer.Deserialize<M>(json, JsonSerializerOptions) : null;
	}

	/// <summary>
	/// Converts to object.
	/// </summary>
	/// <typeparam name="M"></typeparam>
	/// <param name="json"></param>
	/// <param name="options"></param>
	/// <returns></returns>
	public static M? ToObject<M>(this String json, JsonSerializerOptions options) where M : class
	{
		return JsonSerializer.Deserialize<M>(json, options);
	}

	private static JsonSerializerOptions JsonSerializerOptions
	{
		get
		{
			var jsonSerializerOptions = new JsonSerializerOptions
			{
				ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
				Converters =
				{
					new DataSetJsonConverter()
				}
			};

			return jsonSerializerOptions;
		}
	}

	/// <summary>
	/// To object from json element.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="element"></param>
	/// <param name="options"></param>
	/// <returns></returns>
	public static T ToObject<T>(this JsonElement element, JsonSerializerOptions? options = null)
	{
		var bufferWriter = new ArrayBufferWriter<Byte>();

		using (var writer = new Utf8JsonWriter(bufferWriter))
		{
			element.WriteTo(writer);
		}

		return JsonSerializer.Deserialize<T>(bufferWriter.WrittenSpan, options)!;
	}

	/// <summary>
	/// Find if an element has collection propery for the given propertyName
	/// </summary>
	/// <param name="document"></param>
	/// <param name="propertyName"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException"></exception>
	public static Boolean HasProperty(this JsonElement element, String propertyName)
	{
		var hasCollectionArray =
				element.TryGetProperty(propertyName, out var jsonElement)
				&& jsonElement.ValueKind == JsonValueKind.Object;

		return hasCollectionArray;
	}

	/// <summary>
	/// Find if an element has collection propery for the given propertyName
	/// </summary>
	/// <param name="element"></param>
	/// <param name="propertyName"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException"></exception>
	public static Boolean HasCollectionProperty(this JsonElement element, String propertyName)
	{
		var hasCollectionArray =
				element.TryGetProperty(propertyName, out var jsonElement)
				&& jsonElement.ValueKind == JsonValueKind.Array;

		return hasCollectionArray;
	}

	/// <summary>
	/// Convert list to datatable
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="items"></param>
	/// <returns></returns>
	public static DataTable ToDataTable<T>(this List<T> items)
	{
		var dataTable = new DataTable(typeof(T).Name);

		//Get all the properties
		var Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

		foreach (var prop in Props)
		{
			//Defining type of data column gives proper data table 
			var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);

			//Setting column names as Property names
			_ = dataTable.Columns.Add(prop.Name, type!);
		}

		foreach (var item in items)
		{
			var values = new Object[Props.Length];

			for (var i = 0; i < Props.Length; i++)
			{
				//inserting property values to datatable rows
				values[i] = Props[i].GetValue(item, null)!;
			}

			_ = dataTable.Rows.Add(values);
		}

		//put a breakpoint here and check datatable
		return dataTable;
	}
}
