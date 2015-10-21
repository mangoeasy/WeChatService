using System;

namespace WeChatService.Web.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Sort { get; set; }
    }
    
}