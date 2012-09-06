#region CopyrightHeader
//
//  Copyright by Contributors
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0.txt
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//
#endregion

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for SSTCryptographer
/// </summary>
public class SSTCryptographer
{
    private static string _key;
        
    public static string Key
    {
        set { _key = value; }
    }

    /// <summary>
    /// Encrypt the given string using the default key.
    /// </summary>
    /// <param name="strToEncrypt">The string to be encrypted.</param>
    /// <returns>The encrypted string.</returns>
    public static string Encrypt(string strToEncrypt)
    {
        return Encrypt(strToEncrypt, _key);
    }

    /// <summary>
    /// Decrypt the given string using the default key.
    /// </summary>
    /// <param name="strEncrypted">The string to be decrypted.</param>
    /// <returns>The decrypted string.</returns>
    public static string Decrypt(string strEncrypted)
    {
        return Decrypt(strEncrypted, _key);
    }

    /// <summary>
    /// Encrypt the given string using the specified key.
    /// </summary>
    /// <param name="strToEncrypt">The string to be encrypted.</param>
    /// <param name="strKey">The encryption key.</param>
    /// <returns>The encrypted string.</returns>
    public static string Encrypt(string strToEncrypt, string strKey)
    {
        TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
        MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();

        byte[] byteHash, byteBuff;
        string strTempKey = strKey;

        byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
        objHashMD5 = null;
        objDESCrypto.Key = byteHash;
        objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB

        byteBuff = ASCIIEncoding.ASCII.GetBytes(strToEncrypt);
        return Convert.ToBase64String(objDESCrypto.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
    }

    /// <summary>
    /// Decrypt the given string using the specified key.
    /// </summary>
    /// <param name="strEncrypted">The string to be decrypted.</param>
    /// <param name="strKey">The decryption key.</param>
    /// <returns>The decrypted string.</returns>
    public static string Decrypt(string strEncrypted, string strKey)
    {
        TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
        MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();

        byte[] byteHash, byteBuff;
        string strTempKey = strKey;

        byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
        objHashMD5 = null;
        objDESCrypto.Key = byteHash;
        objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB

        byteBuff = Convert.FromBase64String(strEncrypted);
        string strDecrypted = ASCIIEncoding.ASCII.GetString(objDESCrypto.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
        objDESCrypto = null;

        return strDecrypted;
    }
}
