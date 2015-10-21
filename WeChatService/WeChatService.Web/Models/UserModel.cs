using System;

namespace WeChatService.Web.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public Guid Token { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public CompanyModel CompanyModel { get; set; }
        public QQInfoModel QQInfoModel { get; set; }
        public string RongCloudUserId
        {
            get
            {
                return Id.ToString().Replace("-", string.Empty);
            }
        }
        /// <summary>
        /// 融云用户token
        /// </summary>
        public string RongCloudUserToken { get; set; }
    }
}