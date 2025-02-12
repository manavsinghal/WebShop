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
/// CryptographyHelper
/// </summary>
public class CryptographyHelper
{
	#region Fields

	#endregion

	public CryptographyHelper()
	{
	}

	#region MydiagnosticEncryption

	private static readonly String _nonce = AppSettings.IVKeyId;
	private static readonly String _encryptionKey = AppSettings.AESKeyId;

	#endregion

	/// <summary>
	/// Encrypts the URLs.
	/// </summary>
	/// <param name="URL"></param>
	/// <returns></returns>
	public static String? UrlEncryption(String? URL)
	{
		/* Use parameters from AssessmentTypeParameter table
		 * Perform encryption of QueryString parameters in given URL (from GetAssessmentUrl method above).
		 * It should look something like
		 * https://mydiagnostic-uaf-assessment-uat.accenture.com/?{EncryptedValueHere}
		 */
		var url = URL;

		var encurl = url!.Split("?");

		if(encurl.Length > 1 )
		{
			url = encurl[0] + "?" + EncryptGCM(encurl[1], _encryptionKey);

			URL = url.ToString();

		}		

		return URL;
	}

	/// <summary>
	/// Encrypt
	/// </summary>
	/// <param name="decryptedText"></param>
	/// <returns></returns>
	public static String Encrypt(String decryptedText)
	{
		if (!String.IsNullOrEmpty(decryptedText))
		{
			try
			{
				return EncryptText(decryptedText);
			}
			catch
			{
				return decryptedText;
			}
		}

		return decryptedText;
	}

	/// <summary>
	/// Decrypt
	/// </summary>
	/// <param name="encryptedText"></param>
	/// <returns></returns>
	public static String Decrypt(String encryptedText)
	{

		if (!String.IsNullOrEmpty(encryptedText))
		{
			try
			{
				return DecryptText(encryptedText);
			}
			catch
			{
				return encryptedText;
			}
		}

		return encryptedText;
	}

	/// <summary>
	/// EncryptText
	/// </summary>
	/// <param name="inputText"></param>
	/// <returns></returns>
	public static String EncryptText(String inputText)
	{
		Accenture.OAAA.Cryptography.Utility.CryptographyUtility cryptography = new();
		return cryptography.Encrypt(inputText);
	}

	/// <summary>
	/// DecryptText
	/// </summary>
	/// <param name="EncryptedText"></param>
	/// <returns></returns>
	public static String DecryptText(String EncryptedText)
	{
		Accenture.OAAA.Cryptography.Utility.CryptographyUtility cryptography = new();
		return cryptography.Decrypt(EncryptedText);
	}

	private static String? EncryptGCM(String textData, String encryptionKey)
	{
		try
		{
			if (!String.IsNullOrEmpty(textData) && !String.IsNullOrEmpty(encryptionKey))
			{
				// Get bytes of plaintext string
				var plainBytes = Encoding.UTF8.GetBytes(textData);
				//if you are using AES-256-GCM, you’ll need a key 256-bits (32-bytes) in   length               
				var key = Encoding.UTF8.GetBytes(encryptionKey);
				//For AES-GCM, the nonce must be 96 - bits(12 - bytes) in length
				var nonce = Encoding.UTF8.GetBytes(_nonce);

				// Get parameter sizes
				var tagSize = AesGcm.TagByteSizes.MaxSize;
				var cipherSize = plainBytes.Length;

				// We write everything into one big array for easier encoding
				var encryptedDataLength = tagSize + cipherSize;
				Span<Byte> encryptedData = stackalloc Byte[encryptedDataLength];

				//ciphertext is always the same length as the plaintext bytes
				var cipherBytes = encryptedData.Slice(0, cipherSize);

				//128-bits (16-bytes)
				var tag = encryptedData.Slice(cipherSize, tagSize);

				// Encrypt
				using var aes = new AesGcm(key, tagSizeInBytes: 16);

				aes.Encrypt(nonce, plainBytes.AsSpan(), cipherBytes, tag);

				// Encode for transmission
				return Convert.ToBase64String(encryptedData);
			}
			else
			{
				return null;
			}
		}
		catch (Exception)
		{
			throw;
		}

		throw new NotImplementedException();
	}
}
