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
/// DataTableJsonConverter - Refer https://github.com/dotnet/docs/issues/21366
/// </summary>
public class DataSetJsonConverter : JsonConverter<DataSet>
{
	#region Fields
	#endregion

	#region Properties        
	#endregion

	#region Constructors
	#endregion

	#region Public Methods

	/// <summary>
	/// JsonConverter - Read
	/// </summary>
	/// <param name="reader"></param>
	/// <param name="typeToConvert"></param>
	/// <param name="options"></param>
	/// <returns></returns>
	public override DataSet? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// JsonConverter - Write
	/// </summary>
	/// <param name="writer"></param>
	/// <param name="value"></param>
	/// <param name="options"></param>
	public override void Write(Utf8JsonWriter writer, DataSet value, JsonSerializerOptions options)
	{
		writer.WriteStartArray();
		foreach (DataTable source in value.Tables)
		{
			foreach (DataRow dr in source.Rows)
			{
				writer.WriteStartObject();
				foreach (DataColumn col in source.Columns)
				{
					var key = col.ColumnName.Trim();
					var valueString = dr[col].ToString();
					switch (col.DataType.FullName)
					{
						case "System.Guid":
							writer.WriteString(key, valueString);
							break;
						case "System.Char":
						case "System.String":
							writer.WriteString(key, valueString);
							break;
						case "System.Boolean":
							_ = Boolean.TryParse(valueString, out var boolValue);
							writer.WriteBoolean(key, boolValue);
							break;
						case "System.DateTime":
							if (DateTime.TryParse(valueString, out var dateValue))
							{
								writer.WriteString(key, dateValue);
							}
							else
							{
								writer.WriteString(key, "");
							}

							break;
						case "System.TimeSpan":
							if (DateTime.TryParse(valueString, out var timeSpanValue))
							{
								writer.WriteString(key, timeSpanValue.ToString());
							}
							else
							{
								writer.WriteString(key, "");
							}

							break;
						case "System.Byte":
						case "System.SByte":
						case "System.Decimal":
						case "System.Double":
						case "System.Single":
						case "System.Int16":
						case "System.Int32":
						case "System.Int64":
						case "System.UInt16":
						case "System.UInt32":
						case "System.UInt64":
							if (Int64.TryParse(valueString, out var intValue))
							{
								writer.WriteNumber(key, intValue);
							}
							else
							{
								_ = Double.TryParse(valueString, out var doubleValue);
								writer.WriteNumber(key, doubleValue);
							}

							break;
						default:
							writer.WriteString(key, valueString);
							break;
					}
				}

				writer.WriteEndObject();
			}

		}

		writer.WriteEndArray();
	}

	/// <summary>
	/// Jsons the element to data table.
	/// </summary>
	/// <param name="dataRoot">The data root.</param>
	/// <returns></returns>
	public static DataTable JsonElementToDataTable(JsonElement dataRoot)
	{
		var dataTable = new DataTable();
		var firstPass = true;
		foreach (var element in dataRoot.EnumerateArray())
		{
			if (firstPass)
			{
				foreach (var col in element.EnumerateObject())
				{
					var colValue = col.Value;
					dataTable.Columns.Add(new DataColumn(col.Name, ValueKindToType(colValue.ValueKind, colValue.ToString())));
				}

				firstPass = false;
			}

			var row = dataTable.NewRow();
			foreach (var col in element.EnumerateObject())
			{
				row[col.Name] = JsonElementToTypedValue(col.Value);
			}

			dataTable.Rows.Add(row);
		}

		return dataTable;
	}

	#endregion

	#region Private Methods

	private static Type ValueKindToType(JsonValueKind valueKind, String value)
	{
		switch (valueKind)
		{
			case JsonValueKind.String:
				return typeof(System.String);
			case JsonValueKind.Number:
				return Int64.TryParse(value, out var intValue) ? typeof(System.Int64) : typeof(System.Double);
			case JsonValueKind.True:
			case JsonValueKind.False:
				return typeof(System.Boolean);
			case JsonValueKind.Undefined:
				return null!;
			case JsonValueKind.Object:
				return typeof(System.Object);
			case JsonValueKind.Array:
				return typeof(System.Array);
			case JsonValueKind.Null:
				return null!;
			default:
				return typeof(System.Object);
		}
	}

	private static Object JsonElementToTypedValue(JsonElement jsonElement)
	{
		switch (jsonElement.ValueKind)
		{
			case JsonValueKind.Object:
			case JsonValueKind.Array:
			case JsonValueKind.String:
				if (jsonElement.TryGetGuid(out var guidValue))
				{
					return guidValue;
				}
				else
				{
					if (jsonElement.TryGetDateTime(out var datetime))
					{
						if (datetime.Kind == DateTimeKind.Local)
						{
							if (jsonElement.TryGetDateTimeOffset(out var datetimeOffset))
							{
								return datetimeOffset;
							}
						}

						return datetime;
					}

					return jsonElement.ToString();
				}
			case JsonValueKind.Number:
				return jsonElement.TryGetInt64(out var longValue) ? longValue : (Object)jsonElement.GetDouble();
			case JsonValueKind.True:
			case JsonValueKind.False:
				return jsonElement.GetBoolean();
			case JsonValueKind.Undefined:
			case JsonValueKind.Null:
				return null!;
			default:
				return jsonElement.ToString();
		}
	}

	#endregion
}

