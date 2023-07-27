using System;
using System.Security.Cryptography;
using System.Text;

namespace SageTools.Utils
{
    /// <summary>
    /// RSA非对称算法加/解密工具类
    /// </summary>
    public static class RsaDecryptUtils
    {
        /// <summary>
        /// 生成一组公钥和私钥
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="privateKey">私钥</param>
        public static void GenerateKeys(out string publicKey, out string privateKey)
        {
            using var rsa = new RSACryptoServiceProvider();
            publicKey = Convert.ToBase64String(rsa.ExportCspBlob(false));
            privateKey = Convert.ToBase64String(rsa.ExportCspBlob(true));
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="originalText">原文</param>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        public static string Encrypt(string originalText, string publicKey)
        {
            var dataBytes = Encoding.UTF8.GetBytes(originalText);
            using var rsa = new RSACryptoServiceProvider();
            rsa.ImportCspBlob(Convert.FromBase64String(publicKey));
            var encryptedData = rsa.Encrypt(dataBytes, false);
            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptedData">密文</param>
        /// <param name="privateKey">私钥</param>
        /// <returns></returns>
        public static string Decrypt(string encryptedData, string privateKey)
        {
            var encryptedDataBytes = Convert.FromBase64String(encryptedData);
            using var rsa = new RSACryptoServiceProvider();
            rsa.ImportCspBlob(Convert.FromBase64String(privateKey));
            var decryptedDataBytes = rsa.Decrypt(encryptedDataBytes, false);
            return Encoding.UTF8.GetString(decryptedDataBytes);
        }
    }
}