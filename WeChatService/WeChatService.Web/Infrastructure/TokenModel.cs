using System;

namespace WeChatService.Web.Infrastructure
{
    public class TokenModel
    {
        public Guid appid { get; set; }
        public string random { get; set; }
        public string signature { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get; set; }
    }
    
}