using System;
using System.ComponentModel.DataAnnotations;

namespace WeChatService.Library.Models
{
    public class ProductStandard
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual Product Product { get; set; }
    }
}
