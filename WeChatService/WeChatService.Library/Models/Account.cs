using System;
using System.ComponentModel.DataAnnotations;
using WeChatService.Library.Models.Interfaces;

namespace WeChatService.Library.Models
{
    public class Account : IDtStamped
    {
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// 微信APPID
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 微信AppSecret
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// 本账户的Token用于验证
        /// </summary>
        public string AccountToken { get; set; }
        /// <summary>
        /// 微信返回的Token
        /// </summary>
        public string AccessToken { get; set; }
        public DateTime? GetAccessTokenDateTime { get; set; }
        public virtual Company Company { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
