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
        /// 是否为日期型字符串
        /// </summary>
        /// <param name="StrSource">日期字符串(2008-05-08)</param>
        /// <returns></returns>
        public static bool newVerifesToDateTime(string StrSource)
        {
            var time= Regex.IsMatch(StrSource, @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");

            if (time == false)
            {
                return IsDateTime(StrSource);
            }
            return time;
        }
        /// <summary>
        /// 获取字符数
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static int GetByteLen(string content)
        {
            return System.Text.Encoding.Default.GetBytes(content).Length;
        }
        /// <summary>
        /// 是否为日期+时间型字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsDateTime(string StrSource)
        {
            return Regex.IsMatch(StrSource, @"^(((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$ ");
        }
        /// <summary>
        /// 验证是否是 日期类型
        /// </summary>
        /// <returns></returns>
        public static bool VerifesToDateTime(string dateTime)
        {
            //if (dateTime.Contains("/"))
            //{

            //}
            //if (dateTime.Contains("-"))
            //{

            //}
            DateTime result;
            var bo= DateTime.TryParse(dateTime, out result);
            if (bo)
            {
                if (result.Year < 2012)
                {
                    return false;
                }
            }
            return bo;
        }

        /// <summary>
        /// 验证是否是 数字类型
        /// </summary>
        /// <returns></returns>
        public static bool VerifesToNum(string num)
        {
            //if (num.Contains(",") || num.Contains("，"))
            //{
            //    return false;
            //}
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