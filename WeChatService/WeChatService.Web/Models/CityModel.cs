using System;

namespace WeChatService.Web.Models
{
    public class CityModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProvinceId { get; set; }
    }

    public class ProvinceModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CityModel[] CityModels { get; set; }
    }
}