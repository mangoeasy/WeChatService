using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;
using WeChatService.Web.Infrastructure;

namespace WeChatService.Web.Help
{
    public static class RongCloudHelp
    {
        public static RongCloudUerModel HttpPost(string postStr, string url = "https://api.cn.ronghub.com/user/getToken.json")
        {
            const string appKey = "x18ywvqf8hoqc";
            const string appSecret = "gOXkZx553MLuD";
            var nonce = CreateNonceStr();
            var timestamp = ConvertDateTimeInt(DateTime.Now);
            var signature = SHA1_Hash(string.Format("{0}{1}{2}", appSecret, nonce, timestamp));
            var myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.Headers.Add("App-Key", appKey);
            myRequest.Headers.Add("Nonce", nonce);
            myRequest.Headers.Add("Timestamp", timestamp.ToString());
            myRequest.Headers.Add("Signature", signature);
            myRequest.ReadWriteTimeout = 30 * 1000;
            byte[] data = Encoding.UTF8.GetBytes(postStr);
            myRequest.ContentLength = data.Length;
            var newStream = myRequest.GetRequestStream();
            // Send the data.
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            HttpWebResponse myResponse;
            try
            {
                myResponse = (HttpWebResponse)myRequest.GetResponse();
                var reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                var content = reader.ReadToEnd();
                return Json.Decode<RongCloudUerModel>(content);
            }
            //异常请求
            catch (WebException e)
            {
                myResponse = (HttpWebResponse)e.Response;
                using (var errData = myResponse.GetResponseStream())
                {
                    if (errData != null)
                        using (var reader = new StreamReader(errData))
                        {
                            var text = reader.ReadToEnd();

                            return null;
                        }
                }

            }
            return null;
        }
        public static int ConvertDateTimeInt(DateTime time)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        //创建随机字符串  
        public  static string CreateNonceStr()
        {
            const int length = 16;
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var str = string.Empty;
            var rad = new Random();
            for (var i = 0; i < length; i++)
            {
                str += chars.Substring(rad.Next(0, chars.Length - 1), 1);
            }
            return str;
        }
        public static string SHA1_Hash(string strSha1In)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var bytesSha1In = System.Text.Encoding.UTF8.GetBytes(strSha1In);
            var bytesSha1Out = sha1.ComputeHash(bytesSha1In);
            var strSha1Out = BitConverter.ToString(bytesSha1Out);
            strSha1Out = strSha1Out.Replace("-", "").ToLower();
            return strSha1Out;
        }
    }
}