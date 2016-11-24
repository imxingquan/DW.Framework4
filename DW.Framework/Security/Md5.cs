/**
 * 大连西数网络技术有限公司 (C) 2007
 * 文件名: MD5.cs
 * 创建日期: 2007.5.20
 * 描述: 
 *       返回 MD5 加密字符串
 * 
 */

using System;
using System.Security.Cryptography;
using System.Text;

namespace DW.Framework.Security
{
    /// <summary>
    /// Md5加密类
    /// </summary>
    public class Md5
    {
        // Hash an input string and return the hash as
        // a 32 character hexadecimal string.
        /// <summary>
        /// 返回给定字符串的MD5加密字符串
        /// </summary>
        /// <param name="input">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        /// <summary>
        /// 根据流对象来返回一个Md5字符中
        /// </summary>
        /// <param name="input">Stream对象</param>
        /// <returns>返回加密后的字符串</returns>
        public static string getMd5Hash(System.IO.Stream input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(input);
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        /// <summary>
        /// 验证一个hash是否是给定字符串的MD5加密结果。
        /// </summary>
        /// <param name="input">要比较的字符串</param>
        /// <param name="hash">要比较的hash</param>
        /// <returns>如果相等返回true,否则返回 false</returns>
        public static bool verifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = getMd5Hash(input);

            // Create a StringComparer an comare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}