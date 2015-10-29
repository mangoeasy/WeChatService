using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using WeChatService.Library.Services;
using WeChatService.Web.Models;
using WeChatService.Web.Models.APIModel;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace WeChatService.Web.Infrastructure.Filters
{
    public class CallApiAuthorityAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            //约定的Authorization格式：appid={0}&signature={1}&timestamp={2} 
            bool valid;
            IEnumerable<string> arr;
            filterContext.Request.Headers.TryGetValues("Authorization", out arr);
            if (arr == null)
            {
                valid = false;
            }
            else
            {
                var token = arr.FirstOrDefault();
                CheckToken(token, out valid);
            }
            if (!valid)
            {
                var response = new ResponseModel { Error = true, ErrorCode = 2000 };
                var errorResponse = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized, response);//InternalServerError
                filterContext.Response = errorResponse;
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        private static void CheckToken(string token, out bool valid)
        {
            valid = false;
            try
            {
                var bpath = Convert.FromBase64String(token);
                token = System.Text.Encoding.Default.GetString(bpath);
                var jsonStr = "{'" + token.Replace("&", "','").Replace("=", "':'") + "'}";
                var serializer = new JavaScriptSerializer();
                var objs = serializer.Deserialize<TokenModel>(jsonStr);
                using (var accountService = DependencyResolver.Current.GetService<IAccountService>())
                {
                    var user = accountService.GetAccounts().FirstOrDefault(n => n.AppId == objs.appid && n.IsDeleted == false);
                    if (user == null) return;
                    var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                    var timestamp = (int)(DateTime.Now - startTime).TotalSeconds;
                    for (var i = 0; i < 600; i++)
                    {
                        var signature = SHA1_Hash(string.Format("appsecret={0}&random={1}&timestamp={2}", user.AppSecret, objs.random, timestamp - i));
                        if (objs.signature.ToUpper() == signature)
                        {
                            valid = true;
                            break;
                        }
                    }
                    if (!valid) return;
                    var identity = new CustomIdentity(user);
                    var principal = new CustomPrincipal(identity);
                    HttpContext.Current.User = principal;
                    //得到token 如果超时则重新获取  
                    if (user.GetAccessTokenDateTime == null || DateTime.Now.Subtract(user.GetAccessTokenDateTime.Value).Duration().TotalSeconds > 7000)
                    {
                        var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", user.AppId, user.AppSecret);
                        var accessToken = JsonConvert.DeserializeObject<AccessToken>(HttpGet(url)).access_token;
                        user.GetAccessTokenDateTime = DateTime.Now;
                        user.AccessToken = accessToken;
                        accountService.Update();
                    }
                }
            }
            catch (Exception)
            {
                valid = false;
            }

        }
        private static string SHA1_Hash(string strSha1In)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var bytesSha1In = System.Text.Encoding.Default.GetBytes(strSha1In);
            var bytesSha1Out = sha1.ComputeHash(bytesSha1In);
            var strSha1Out = BitConverter.ToString(bytesSha1Out);
            strSha1Out = strSha1Out.Replace("-", "").ToUpper();
            return strSha1Out;
        }
        private static string HttpGet(string url)
        {
            try
            {
                var myWebClient = new WebClient { Credentials = CredentialCache.DefaultCredentials };
                var pageData = myWebClient.DownloadData(url); //从指定网站下载数据  
                var pageHtml = System.Text.Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句
                return pageHtml;
            }
            catch (WebException webEx)
            {
                return webEx.Message;
            }
        }
    }
}