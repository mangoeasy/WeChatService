using System;
using System.ComponentModel.DataAnnotations;
using WeChatService.Library.Models.Interfaces;

namespace WeChatService.Library.Models
{
    public class Account : IDtStamped
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AppId { get; set; }
        public string AppSecret { get; set; }
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
