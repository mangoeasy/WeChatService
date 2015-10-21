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
using WeChatService.Library.Services;
using WeChatService.Web.Models;
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
                    var user = accountService.GetAccounts().FirstOrDefault(n => n.Id == objs.appid && n.IsDeleted == false);
                    if (user == null) return;
                    var signature = SHA1_Hash(string.Format("appsecret={0}&random={1}", user.Token, objs.random));
                    if (objs.signature.ToUpper() != signature) return;
                    valid = true;
                    var identity = new CustomIdentity(user);
                    var principal = new CustomPrincipal(identity);
                    HttpContext.Current.User = principal;
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
    }
    //public class CallApiAuthorityAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(HttpActionContext filterContext)
    //    {
    //        string accessToken;
    //        var valid = filterContext.Request.Headers.Authorization != null;
    //        if (valid && GetCredentials(filterContext.Request.Headers.Authorization.ToString(), out accessToken, out valid))
    //        {
    //            try
    //            {
    //                //using (var weiXinPropertyService = DependencyResolver.Current.GetService<IWeiXinPropertyService>())
    //                //{
    //                //    var siteDomain = filterContext.Request.RequestUri.Host;
    //                //    var weiXinProperty = weiXinPropertyService.GetWeiXinProperty(accessToken, siteDomain);
    //                //    if (weiXinProperty == null)
    //                //    {
    //                //        valid = false;
    //                //    }
    //                //    //if (valid)
    //                //    //{
    //                //    //    CustomIdentity identity = new CustomIdentity(weiXinProperty.Account);
    //                //    //    CustomPrincipal principal = new CustomPrincipal(identity);
    //                //    //    HttpContext.Current.User = principal;
    //                //    //}
    //                //}
    //            }
    //            catch (Exception)
    //            {
    //                valid = false;

    //            }
    //        }

    //        if (!valid)
    //        {
    //            var result = new JsonResult();
    //            var response = new ResponseModel {Error = true, ErrorCode = 2000};

    //            var errorResponse = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized, response);//InternalServerError
    //            filterContext.Response = errorResponse;

    //            return;
    //        }

    //        base.OnActionExecuting(filterContext);
    //    }

    //    private static bool GetCredentials(string credentials, out string token, out bool valid)
    //    {
    //        token = null;

    //        if (!string.IsNullOrEmpty(credentials))
    //        {
    //            try
    //            {
    //                if (!string.IsNullOrEmpty(credentials))
    //                {
    //                    //需要加密
    //                    //token = Encoding.ASCII.GetString(Convert.FromBase64String(credentials));
    //                    token = credentials;
    //                    valid = true;
    //                    return true;
    //                }
    //                //var credentialParts = credentials.Split(new[] { ' ' });
    //                //if (credentialParts.Length == 2 &&
    //                //    credentialParts[0].Equals("basic",
    //                //                              StringComparison.OrdinalIgnoreCase))
    //                //{
    //                //    credentials = Encoding.ASCII.GetString(
    //                //        Convert.FromBase64String(credentialParts[1]));
    //                //    credentialParts = credentials.Split(new[] { ':' }, 2);
    //                //    if (credentialParts.Length == 2)
    //                //    {
    //                //        userName = credentialParts[0];
    //                //        password = credentialParts[1];
    //                //        valid = true;
    //                //        return true;
    //                //    }
    //                //}
    //            }
    //            catch (Exception)
    //            {
    //                //this.GetLogger().Debug("Extraction of auth detail failed with exception.", ex);
    //            }
    //        }
    //        valid = false;
    //        return false;
    //    }
    //}
}