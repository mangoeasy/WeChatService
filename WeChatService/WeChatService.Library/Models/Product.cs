using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeChatService.Library.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Sort { get; set; }
        public virtual ICollection<ProductStandard> ProductStandards { get; set; } 
    }
}
