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
/// XmlExtensions
/// </summary>
public static class XmlExtensions
{
	#region Field

	#endregion

	#region Properties

	#endregion

	#region Methods

	/// <summary>
	/// Appends the specified element to append.
	/// </summary>
	/// <param name="parent">The parent.</param>
	/// <param name="elementToAppend">The element to append.</param>
	/// <returns></returns>
	public static XElement Append(this XElement parent, XElement elementToAppend)
	{
		if (elementToAppend != null)
		{			
			parent.Add(new XElement(elementToAppend));
		}
		
		return parent;
	}

	/// <summary>
	/// Adds the attribute.
	/// </summary>
	/// <param name="element">The element.</param>
	/// <param name="attribute">The attribute.</param>
	/// <returns></returns>
	public static XElement AddAttribute(this XElement element, XAttribute attribute)
	{
		if (attribute != null)
		{			
			element.Add(attribute);
		}
		
		return element;
	}

	/// <summary>
	/// Adds the attribute.
	/// </summary>
	/// <param name="element">The element.</param>
	/// <param name="name">The name.</param>
	/// <param name="value">The value.</param>
	/// <returns></returns>
	public static XElement AddAttribute(this XElement element, String name, String value)
	{		
		return AddAttribute(element, new XAttribute(name, value));
	}

	/// <summary>
	/// Gets the elements.
	/// </summary>
	/// <param name="doc">The document.</param>
	/// <param name="elementName">Name of the element.</param>
	/// <returns></returns>
	public static List<XElement> GetElements(this XDocument doc, String elementName)
	{
		var elements = new List<XElement>();

		foreach (var node in doc.DescendantNodes())
		{			
			if (node is XElement element)
			{				
				if (element.Name.LocalName.Equals(elementName))
				{					
					elements.Add(element);
				}
			}
		}

		return elements;
	}

	#endregion
}

