using System;
using System.ComponentModel.DataAnnotations;
using WeChatService.Library.Models;
using WeChatService.Library.Models.Enum;
using WeChatService.Library.Models.Interfaces;

namespace WeChatService.Library.Models
{
    public class Account : IDtStamped
    {
        [Key]
        public Guid Id { get; set; }
        public string RongCloudUserToken { get; set; }
        public string Email { get; set; }
        public Guid Token { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public virtual Company Company { get; set; }
        public string Phone { get; set; }
        public virtual City City { get; set; }
        public bool Valid { get; set; }
        public string VerificationCode { get; set; }
        public virtual QQInfo QQInfo { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
