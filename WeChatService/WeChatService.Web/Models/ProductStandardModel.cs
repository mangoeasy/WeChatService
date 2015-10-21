using System;

namespace WeChatService.Web.Models
{
    public class ProductStandardModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProductId { get; set; }
    }
    
}