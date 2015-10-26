using System;

namespace WeChatService.Web.Models
{
    public class CompanyModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CityModel CityModel { get; set; }
       
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}