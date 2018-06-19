using System;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace VAN_OA
{
    /// <summary>
    /// 公共方法类
    /// </summary>
    public static class CommHelp
    {
        /// <summary>
        /// 验证是否是 日期类型
        /// </summary>
        /// <returns></returns>
        public static bool VerifesToDateTime(string dateTime)
        {
            DateTime result;
            return DateTime.TryParse(dateTime, out result);
        }

        /// <summary>
        /// 验证是否是 数字类型
        /// </summary>
        /// <returns></returns>
        public static bool VerifesToNum(string num)
        {
            decimal result;
            return decimal.TryParse(num, out result);
        }

        #region desc加密
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="strString"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string DES3Encrypt(string strString, string strKey)
        {
            try
            {
                var DES = new TripleDESCryptoServiceProvider();
                var hashMD5 = new MD5CryptoServiceProvider();
                DES.Key = hashMD5.ComputeHash(Encoding.UTF8.GetBytes(strKey));
                DES.Mode = CipherMode.ECB;
                ICryptoTransform DESEncrypt = DES.CreateEncryptor();
                byte[] Buffer = Encoding.UTF8.GetBytes(strString);
                return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch (Exception)
            {

                return "";
            }
        } 
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="strString"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string DES3Decrypt(string strString, string strKey)
        {
            try
            {
                var DES = new TripleDESCryptoServiceProvider();
                var hashMD5 = new MD5CryptoServiceProvider();
                DES.Key = hashMD5.ComputeHash(Encoding.UTF8.GetBytes(strKey));
                DES.Mode = CipherMode.ECB;
                ICryptoTransform DESDecrypt = DES.CreateDecryptor();
                string result = "";

                byte[] Buffer = Convert.FromBase64String(strString);
                result = Encoding.UTF8.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
                return result;
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion
    }
}