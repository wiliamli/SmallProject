using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Ruanmou04.Core.Utility.Security
{
    public class Encrypt
    {

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }

        /// <summary>
        /// 此代码示例通过创建哈希字符串适用于任何 MD5 哈希函数 （在任何平台） 上创建 32 个字符的十六进制格式哈希字符串
        /// 官网案例改编
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private static string GetMD5(string encryptKey,string source)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(encryptKey+source));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                string hash = sBuilder.ToString();
                return hash.ToUpper();
            }
        }

        /// <summary>
        /// 得到加密后的密码字符串
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncryptionPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return "";
            }
            return Encrypt.MD5Hash(Encrypt.MD5Hash(password));
        }
        /// <summary>
        /// AES加密 
        /// </summary>
        /// <param name="text">加密字符</param>
        /// <param name="password">加密的密码</param>
        /// <param name="iv">密钥</param>
        /// <returns></returns>
        private static string AESEncrypt(string toEncrypt, string key= "0123456789abcdef", string iv= "0123456789abcdef")
        {
            try
            {
                byte[] keyArray = Encoding.UTF8.GetBytes(key);
                byte[] ivArray = Encoding.UTF8.GetBytes(iv);
                byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

                RijndaelManaged rDel = new RijndaelManaged
                {
                    Key = keyArray,
                    IV = ivArray,
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.Zeros
                };

                ICryptoTransform cTransform = rDel.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="password"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string AESDecrypt(string toDecrypt, string key= "0123456789abcdef", string iv= "0123456789abcdef")
        {
            try
            {
                byte[] keyArray = Encoding.UTF8.GetBytes(key);
                byte[] ivArray = Encoding.UTF8.GetBytes(iv);
                byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

                RijndaelManaged rDel = new RijndaelManaged
                {
                    Key = keyArray,
                    IV = ivArray,
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7
                };

                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
