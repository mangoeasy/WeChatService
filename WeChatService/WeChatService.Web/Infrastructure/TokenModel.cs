using System;

namespace WeChatService.Web.Infrastructure
{
    public class TokenModel
    {
        public Guid appid { get; set; }
        public string random { get; set; }
        public string signature { get; set; }
    }

    public class RongCloudUerModel
    {
        public string code { get; set; }
        public string token { get; set; }
        public string userId { get; set; }
    }
}