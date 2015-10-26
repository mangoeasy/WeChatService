using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using WeChatService.Library.Models;
using WeChatService.Library.Services;
using WeChatService.Web.Help;
using WeChatService.Web.Infrastructure;
using WeChatService.Web.Models.APIModel;

namespace WeChatService.Web.Controllers.API
{
    public class JsApiConfigController : BaseApiController
    {
         private readonly IAccountService _accountService;

        public JsApiConfigController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public object Get()
        {
            var user = HttpContext.Current.User.Identity.GetUser();
            var jsApiList = HttpContext.Current.Request["jsApiList"];
            var account = _accountService.GetAccount(user.Id);
            if (account != null)
            {
                var ticket = GetTicket(account);
                var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                var timestamp = (int)(DateTime.Now - startTime).TotalSeconds;
                var nonceStr = Tools.CreateNonceStr();
                var url = HttpContext.Current.Request["url"];
             
                var signature = Tools.SHA1_Hash(string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", ticket, nonceStr, timestamp, url));
                var model = new JsApiConfigModel
                {
                    AppId = account.AppId,
                    Signature = signature,
                    Debug = true,
                    NonceStr = nonceStr,
                    Timestamp = timestamp.ToString(),
                    JsApiList = jsApiList.Split(',')
                };
                return model;
            }
            return null;
        }
        private string GetTicket(Account account)
        {
           
            var ticketUrl = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", account.AccessToken);
            return JsonConvert.DeserializeObject<Ticket>(Tools.HttpGet(ticketUrl)).ticket;
        }
        
    }
}
